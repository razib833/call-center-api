using System.Globalization;
using Customer360.Application;
using Customer360.Contracts;
using Customer360.Integrations;
using Customer360.Security;
namespace Customer360.Workers;
public sealed class CommunicationProcessor(Procedures db,ISmsProvider sms,IEmailProvider email,IDeliveryProtector protector,IConfiguration configuration)
{
 public async Task RunBatchAsync(Actor worker,CancellationToken ct)
 {
  worker.Demand("WORKER");
  var claimed=await db.CommunicationOutbox_ClaimAsync(new(){BatchSize=20,LeaseSeconds=60},worker,ct);
  foreach(var row in claimed.Rows1)await ProcessAsync(row,worker,ct);
 }
 public async Task ProcessAsync(CommunicationOutbox_ClaimRow1 row,Actor worker,CancellationToken ct)
 {
  worker.Demand("WORKER");
  if(row.CommunicationID is not {} id||string.IsNullOrWhiteSpace(row.LeaseID))throw new ApiFault(503,"CLAIM_CONTRACT","Outbox claim is incomplete");
  var requestId="C360-"+id.ToString(CultureInfo.InvariantCulture)+"-"+Guid.NewGuid().ToString("N");
  var started=await db.CommunicationAttempt_StartAsync(new(){CommunicationID=id,LeaseID=row.LeaseID,ProviderCode="CONFIGURED_PROVIDER",ProviderRequestID=requestId},worker,ct);
  var attempt=started.AttemptID??throw new ApiFault(503,"ATTEMPT_CONTRACT","Attempt creation result is incomplete");
  ProviderReceipt receipt;
  try
  {
   var body=row.MessageBody??"";
   if(row.EncryptedDeliveryPayload is not null)
   {
    var delivery=await protector.UnprotectAsync(new(row.EncryptedDeliveryPayload,row.PayloadKeyReference??"",row.PayloadExpiresAt??DateTime.MinValue),ct);
    var baseUri=configuration["Tracking:PublicBaseUri"];
    if(!Uri.TryCreate(baseUri,UriKind.Absolute,out var uri)||uri.Scheme!="https"||!string.IsNullOrEmpty(uri.Query))throw new ApiFault(503,"TRACKING_ORIGIN","Secure public tracking origin is not configured");
    body+=" "+new Uri(uri,"track#"+delivery.Token).AbsoluteUri;
   }
   var message=new ProviderMessage(requestId,row.ChannelCode??"",row.RecipientAddress??"",row.CcAddresses,row.MessageSubject,body);
   receipt=row.ChannelCode switch{"SMS"=>await sms.SendAsync(message,ct),"EMAIL"=>await email.SendAsync(message,ct),_=>new(ProviderOutcome.PermanentFailure,null,"CHANNEL_UNSUPPORTED")};
  }
  catch(OperationCanceledException) when(ct.IsCancellationRequested){throw;}
  catch{receipt=new(ProviderOutcome.Unknown,null,"OUTCOME_UNKNOWN");}
  // UNKNOWN is not an accepted @Result value. Leave STARTED for lease quarantine/reconciliation.
  if(receipt.Outcome==ProviderOutcome.Unknown)return;
  if(receipt.Outcome is ProviderOutcome.Accepted or ProviderOutcome.Delivered && string.IsNullOrWhiteSpace(receipt.MessageId))return;
  if(receipt.Outcome is ProviderOutcome.PermanentFailure or ProviderOutcome.RetryableFailure && string.IsNullOrWhiteSpace(receipt.ErrorCode))return;
  var result=receipt.Outcome switch{ProviderOutcome.Accepted=>"ACCEPTED",ProviderOutcome.Delivered=>"DELIVERED",_=>"FAILED"};
  await db.CommunicationAttempt_CompleteAsync(new(){AttemptID=attempt,LeaseID=row.LeaseID,ProviderRequestID=requestId,Result=result,ProviderMessageID=receipt.MessageId,ErrorCode=receipt.ErrorCode,SanitizedError=receipt.Outcome==ProviderOutcome.Unknown?"Provider outcome requires reconciliation":null,IsRetryable=receipt.Outcome==ProviderOutcome.RetryableFailure},worker,ct);
  // Attempt completion procedure owns durable state/retry handling. Never perform blind HTTP retries.
 }
 public async Task ProcessForwardingAsync(Forwarding_GetPendingRow1 pending,Forwarding_GetByTicketRow1 detail,Actor worker,CancellationToken ct)
 {
  worker.Demand("WORKER");await Task.CompletedTask;
  // Claim omits RelatedForwardingID and pending projection omits recipient/body. Do not resend
  // outside the durable communication attempt or guess the forwarding-to-outbox relationship.
  throw new ApiFault(503,"FORWARDING_WORKER_CONTRACT_GAP","Forwarding acceptance needs an approved durable outbox-to-forwarding resolution contract");
 }
}
public sealed class CommunicationOutboxWorker(IServiceScopeFactory scopes,ILogger<CommunicationOutboxWorker> logger) : BackgroundService
{
 protected override async Task ExecuteAsync(CancellationToken stoppingToken)
 {
  while(!stoppingToken.IsCancellationRequested)
  {
   using var scope=scopes.CreateScope();
   try{await scope.ServiceProvider.GetRequiredService<CommunicationProcessor>().RunBatchAsync(new("OUTBOX","Communication worker","WORKER","SERVICE_IDENTITY",DateTime.UtcNow.AddMinutes(2),Guid.NewGuid()),stoppingToken);}
   catch(OperationCanceledException) when(stoppingToken.IsCancellationRequested){break;}
   catch{logger.LogWarning("Outbox cycle failed; provider outcomes must be reconciled before retry");}
   await Task.Delay(TimeSpan.FromSeconds(15),stoppingToken);
  }
 }
}
public sealed record UnknownAttempt(long AttemptID,string LeaseID,string ProviderRequestID,string Channel);
public interface IReconciliationBacklog {Task<IReadOnlyList<UnknownAttempt>> ReadAsync(CancellationToken ct);}
public sealed class UnconfiguredReconciliationBacklog : IReconciliationBacklog
{public Task<IReadOnlyList<UnknownAttempt>> ReadAsync(CancellationToken ct)=>throw new ApiFault(503,"RECONCILIATION_CONTRACT_GAP","No existing procedure enumerates unknown attempts with complete reconciliation keys");}
public sealed class CommunicationReconciliationWorker(IReconciliationBacklog backlog,ISmsProvider sms,IEmailProvider email,Procedures db) : BackgroundService
{
 public async Task ReconcileOnceAsync(CancellationToken ct)
 {
  foreach(var row in await backlog.ReadAsync(ct))
  {
   var receipt=row.Channel=="SMS"?await sms.ReconcileAsync(row.ProviderRequestID,ct):await email.ReconcileAsync(row.ProviderRequestID,ct);
   if(receipt.Outcome==ProviderOutcome.Unknown)continue;
   await db.Communication_RecordProviderResultAsync(new(){AttemptID=row.AttemptID,LeaseID=row.LeaseID,ProviderRequestID=row.ProviderRequestID,Result=receipt.Outcome switch{ProviderOutcome.Accepted=>"ACCEPTED",ProviderOutcome.Delivered=>"DELIVERED",_=>"FAILED"},ProviderMessageID=receipt.MessageId,ErrorCode=receipt.ErrorCode,IsRetryable=receipt.Outcome==ProviderOutcome.RetryableFailure},new("RECONCILER","Reconciliation worker","WORKER","SERVICE_IDENTITY",DateTime.UtcNow.AddMinutes(2),Guid.NewGuid()),ct);
  }
 }
 protected override async Task ExecuteAsync(CancellationToken ct){while(!ct.IsCancellationRequested){await ReconcileOnceAsync(ct);await Task.Delay(TimeSpan.FromMinutes(1),ct);}}
}
