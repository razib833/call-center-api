using System.Data;
using System.Security.Cryptography;
using System.Text.Json;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.Configuration;
using Customer360.Application;
using Customer360.Contracts;
using Customer360.Controllers;
using Customer360.Data;
using Customer360.Integrations;
using Customer360.Security;
using Customer360.Workers;

var results=new List<object>();var failed=0;
async Task Test(string name,Func<Task> body){try{await body();results.Add(new{name,status="PASS"});Console.WriteLine("PASS "+name);}catch(Exception e){failed++;results.Add(new{name,status="FAIL",error=e.Message});Console.WriteLine("FAIL "+name+": "+e.Message);}}
void Assert(bool ok,string message="Assertion failed"){if(!ok)throw new Exception(message);}
async Task Fault(int status,Func<Task> action){try{await action();throw new Exception("Expected failure did not occur");}catch(ApiFault f){Assert(f.Status==status,$"Expected {status}, got {f.Status}");}}
Actor ActorFor(string cap)=>new("TEST-"+cap,"Synthetic",cap,cap switch{"AGENT"=>"AVAYA","MANAGER" or "CONFIG"=>"EXTERNAL_MANAGER",_=>"SERVICE_IDENTITY"},DateTime.UtcNow.AddMinutes(5),Guid.NewGuid(),cap=="AGENT"?"VERIFIED-INTERACTION":null);
Task Action(Action action){action();return Task.CompletedTask;}
var agent=ActorFor("AGENT");var manager=ActorFor("MANAGER");
foreach(var role in new[]{"AGENT","MANAGER","CONFIG","WORKER","AUDIT","PUBLIC"})
foreach(var cap in new[]{"AGENT","MANAGER","CONFIG","WORKER","AUDIT","PUBLIC","READ","TRUSTED"})
{
 var allowed=cap==role||cap=="READ"&&role is "AGENT" or "MANAGER" or "CONFIG"||cap=="TRUSTED"&&role is "AGENT" or "MANAGER" or "CONFIG" or "WORKER";
 await Test($"Capability {role} -> {cap}",()=>allowed?Action(()=>ActorFor(role).Demand(cap)):Fault(403,()=>Action(()=>ActorFor(role).Demand(cap))));
}
await Test("Expired identity rejected",()=>Fault(401,()=>Action(()=>(agent with{VerifiedUntil=DateTime.UtcNow.AddSeconds(-1)}).Demand("AGENT"))));
await Test("Mock production startup rejected",()=>Action(()=>{try{FeatureSafety.Validate("Production",true,false,false,false);throw new Exception("Expected startup rejection");}catch(InvalidOperationException){}}));
await Test("Mock authentication outside dev rejected",()=>Action(()=>Assert(!FeatureSafety.AllowDevelopment("Production",true))));
await Test("Worker activation rejected",()=>Action(()=>{try{FeatureSafety.Validate("Development",false,false,false,true);throw new Exception("Expected rejection");}catch(InvalidOperationException){}}));
await Test("Avaya unconfigured fails closed",()=>Fault(503,async()=>await new UnconfiguredIntegrations().ValidateAsync(new("untrusted-header"),default)));
await Test("Manager unconfigured fails closed",()=>Fault(503,async()=>await new UnconfiguredIntegrations().AuthenticateAsync(new("MGR001","not-a-real-password"),default)));
await Test("Rejection requires reason",()=>Fault(400,()=>Action(()=>Validation.Validate(new Ticket_RejectRequest{RequestID=1,ExpectedRowVersion=new byte[8],Remarks="  "}))));
await Test("Rowversion size enforced",()=>Fault(400,()=>Action(()=>Validation.Validate(new Ticket_ApproveRequest{RequestID=1,ExpectedRowVersion=new byte[7]}))));
await Test("Paging is bounded",()=>Fault(400,()=>Action(()=>Validation.Validate(new Ticket_SearchRequest{PageSize=201}))));
await Test("Positive identifiers required",()=>Fault(400,()=>Action(()=>Validation.Validate(new Ticket_GetByIDRequest{RequestID=-1}))));
await Test("Reversed dates rejected",()=>Fault(400,()=>Action(()=>Validation.Validate(new Ticket_GetDashboardSummaryRequest{FromDate=DateTime.UtcNow,ToDate=DateTime.UtcNow.AddDays(-1)}))));
await Test("Follow-up note required",()=>Fault(400,()=>Action(()=>Validation.Validate(new FollowUp_AddRequest{RequestID=1,FollowUpType="NOTE",Note=""}))));
await Test("Manual retry reason required",()=>Fault(400,()=>Action(()=>Validation.Validate(new CommunicationOutbox_ManualRetryRequest{CommunicationID=1,ExpectedRowVersion=new byte[8],Reason=""}))));
Forwarding_CreateRequest Forwarding()=>new(){RequestID=1,ExpectedRowVersion=new byte[8],ForwardingType="BRANCH",DestinationName="Synthetic Branch",ToAddresses="qa@example.invalid",Subject="Synthetic forwarding",Body="Plain text only"};
await Test("Forwarding plain-text request accepted",()=>Action(()=>Validation.Validate(Forwarding())));
await Test("Forwarding header injection rejected",()=>Fault(400,()=>Action(()=>Validation.Validate(Forwarding() with{ToAddresses="qa@example.invalid\r\nBcc: other@example.invalid"}))));
await Test("Forwarding HTML rejected",()=>Fault(400,()=>Action(()=>Validation.Validate(Forwarding() with{Body="<script>bad</script>"}))));
await Test("Forwarding unknown destination rejected",()=>Fault(400,()=>Action(()=>Validation.Validate(Forwarding() with{ForwardingType="UNKNOWN"}))));
await Test("Past reminder rejected",()=>Fault(400,()=>Action(()=>Validation.Validate(new FollowUpSchedule_CreateRequest{RequestID=1,Subject="Synthetic",ScheduledAt=DateTime.UtcNow.AddMinutes(-1)}))));
await Test("Future UTC reminder accepted",()=>Action(()=>Validation.Validate(new FollowUpSchedule_CreateRequest{RequestID=1,Subject="Synthetic",ScheduledAt=DateTime.UtcNow.AddMinutes(10)})));
await Test("Published mutation conflict simulated",()=>Fault(422,async()=>await new Procedures(new MockProcedureExecutor()).ServiceVersion_PublishDraftAsync(new(){VersionID=1,ExpectedRowVersion=new byte[8]},ActorFor("CONFIG"))));
var mock=new MockProcedureExecutor();var procedures=new Procedures(mock);
var doubles=new DevelopmentIntegrations(new EphemeralDataProtectionProvider());var app=new TicketApplication(procedures,doubles,doubles,new IntegrationAudit(procedures));
await Test("Synthetic middleware demographics and mobile isolation",async()=>{
 var profiles=await doubles.SearchAsync("01712345678",default);Assert(profiles.Count==1);
 var profile=profiles[0];Assert(profile.Reference=="SYNTHETIC-CUSTOMER"&&profile.IsSynthetic);
 Assert(profile.FatherName is not null&&profile.MotherName is not null&&profile.DateOfBirth=="1991-06-30"&&profile.Gender=="Male");
 Assert(profile.MaskedEmail=="a***@example.invalid"&&profile.NidLastFour=="0000");
 Assert(profile.PresentAddress is not null&&profile.PermanentAddress is not null);
 Assert(await doubles.ProfileAsync(profile.Reference,default)==profile);
 Assert((await doubles.SearchAsync("01999999999",default)).Count==0);
 var accounts=await doubles.AccountsAsync(profile.Reference,default);Assert(accounts.Count==5);
 Assert(accounts.All(a=>profile.Cifs!.Contains(a.Cif!)&&a.MaskedAccount.Contains('*')&&a.Branch is not null&&a.OpeningDate is not null));
 Assert(accounts.Count(a=>a.FreezeStatus==true)==1);Assert(profile.Cifs!.Count==2);Assert(accounts.Count(a=>a.Cif=="TEST-CIF")==4);Assert(accounts.Count(a=>a.Cif=="TEST-CIF-2")==1);
});
await Test("Account detail selection and synthetic financial information",async()=>{
 var accounts=await doubles.AccountsAsync("SYNTHETIC-CUSTOMER",default);
 foreach(var account in accounts){
  var d=await doubles.AccountAsync(account.Reference,default);
  Assert(d.Reference==account.Reference&&d.MaskedAccount==account.MaskedAccount&&d.Cif==account.Cif&&d.FreezeStatus==account.FreezeStatus);
  Assert(d.IsSynthetic&&d.Signatories!.Count==3&&d.LedgerBalance==d.AvailableBalance+d.HoldAmount);
  Assert(d.MaskedTin=="******0000"&&d.Signatories!.All(s=>!string.IsNullOrWhiteSpace(s.Name)&&!string.IsNullOrWhiteSpace(s.Mobile)&&!string.IsNullOrWhiteSpace(s.DateOfBirth)));
  foreach(var property in typeof(AccountDetails).GetProperties())Assert(property.GetValue(d) is not null,"Missing demo field: "+property.Name);
 }
 await Fault(404,async()=>await doubles.AccountAsync("UNKNOWN",default));
});
await Test("Account transactions are selected, filtered and financially consistent",async()=>{
 var from=new DateOnly(2026,10,1);var to=new DateOnly(2026,10,9);
 var history=await doubles.AccountTransactionsAsync("SYNTHETIC-ACCOUNT-5",from,to,default);
 Assert(history.AccountReference=="SYNTHETIC-ACCOUNT-5"&&history.Transactions.Count==8&&history.IsSynthetic);
 Assert(history.Transactions.Sum(t=>t.Type=="DEBIT"?t.Amount:0)==22500m);
 Assert(history.Transactions.Sum(t=>t.Type=="CREDIT"?t.Amount:0)==100000m);
 Assert(history.Transactions[0].Balance==history.CurrentBalance);
 var chronological=history.Transactions.Reverse().ToArray();
 for(var i=1;i<chronological.Length;i++)Assert(chronological[i].Balance==chronological[i-1].Balance+(chronological[i].Type=="CREDIT"?chronological[i].Amount:-chronological[i].Amount));
 var filtered=await doubles.AccountTransactionsAsync("SYNTHETIC-ACCOUNT",new(2026,10,9),new(2026,10,9),default);Assert(filtered.Transactions.Count==1);
 Assert((await doubles.AccountTransactionsAsync("SYNTHETIC-ACCOUNT",new(2026,11,1),new(2026,11,2),default)).Transactions.Count==0);
 await Fault(400,async()=>await doubles.AccountTransactionsAsync("SYNTHETIC-ACCOUNT",to,from,default));
 await Fault(400,async()=>await doubles.AccountTransactionsAsync("SYNTHETIC-ACCOUNT",from,from.AddDays(367),default));
 await Fault(404,async()=>await doubles.AccountTransactionsAsync("UNKNOWN",from,to,default));
});
await Test("Statement mock simulates action without email and does not accept browser recipients",async()=>{
 var info=await doubles.InfoAsync("SYNTHETIC-ACCOUNT",default);
 Assert(info.AccountReference=="SYNTHETIC-ACCOUNT"&&info.CanSend&&info.IsSimulation&&info.MaskedEmail.Contains('*'));
  var key=Guid.NewGuid();var sent=await doubles.SendAsync("SYNTHETIC-ACCOUNT",new(2026,10,1),new(2026,10,9),key,default);
 Assert(sent.Status=="SIMULATED"&&sent.ProviderReference=="MOCK-"+key.ToString("N"));
 Assert((await doubles.SendAsync("SYNTHETIC-ACCOUNT",new(2026,10,1),new(2026,10,9),key,default)).ProviderReference==sent.ProviderReference);
 await Fault(400,async()=>await doubles.SendAsync("SYNTHETIC-ACCOUNT",new(2026,10,9),new(2026,10,1),key,default));
 Assert(!typeof(StatementSendRequest).GetProperties().Any(p=>p.Name.Contains("Email")||p.Name.Contains("Recipient")||p.Name.Contains("Attachment")));
 await Fault(404,async()=>await doubles.InfoAsync("UNKNOWN",default));
});
await Test("Card middleware supplies two clients and multiple masked cards",async()=>{
 var cards=await doubles.CardsAsync("SYNTHETIC-CUSTOMER",default);
 Assert(cards.Count==6&&cards.Select(c=>c.ClientId).Distinct().Count()==2);
 Assert(cards.Count(c=>c.ClientId=="DEMO-CARD-CLIENT-1")==4&&cards.Count(c=>c.ClientId=="DEMO-CARD-CLIENT-2")==2);
 Assert(cards.All(c=>c.LastFour.Length==4&&c.CardType is "DEBIT" or "CREDIT"&&c.Product is not null));
 Assert(cards.Where(c=>c.CardType=="CREDIT").All(c=>c.CreditLimit>0&&c.Outstanding>=0));
 Assert(cards.Any(c=>c.Status=="BLOCKED"));await Fault(404,async()=>await doubles.CardsAsync("UNKNOWN",default));
});
await Test("Single card details match selected client, balances and masked identities",async()=>{
 foreach(var card in await doubles.CardsAsync("SYNTHETIC-CUSTOMER",default)){
  var details=await doubles.CardAsync(card.Reference,default);Assert(details.Card==card&&details.Cardholder.Reference=="SYNTHETIC-CUSTOMER"&&details.IsSynthetic);
  Assert(details.ExpiryDate=="12/28"&&details.MaskedPassport.Contains('*')&&details.MaskedLinkedAccount.Contains('*'));
  Assert(details.RecentTransactions.Count==8&&details.RecentTransactions.All(t=>t.Reference.StartsWith(card.Reference+"-TX-")&&t.Status=="SUCCESSFUL"));
  if(card.CardType=="CREDIT"){Assert(details.AvailableLimit==card.CreditLimit-card.Outstanding);Assert(details.RecentTransactions[0].Balance==card.Outstanding);}
  else Assert(details.AvailableLimit is null&&details.MinimumPaymentDue is null&&details.BillingCycle is null);
 }
 await Fault(404,async()=>await doubles.CardAsync("UNKNOWN",default));
});
await Test("Internet banking details and filtered paginated user log",async()=>{
 var d=await doubles.DetailsAsync("SYNTHETIC-CUSTOMER",default);Assert(d.Cifs.Count==2&&d.CardClients.Count==1&&d.Transactions.Count==10&&d.IsSynthetic);
 var first=await doubles.LogsAsync(d.CustomerReference,new(2026,10,1),new(2026,10,20),"ALL",1,10,default);
 var second=await doubles.LogsAsync(d.CustomerReference,new(2026,10,1),new(2026,10,20),"ALL",2,10,default);
 Assert(first.TotalCount==35&&first.Rows.Count==10&&second.Rows.Count==10&&!first.Rows.Select(r=>r.Reference).Intersect(second.Rows.Select(r=>r.Reference)).Any());
 var filtered=await doubles.LogsAsync(d.CustomerReference,new(2026,10,1),new(2026,10,20),"LOGIN",1,10,default);Assert(filtered.Rows.All(r=>r.ActivityType=="LOGIN"));
 await Fault(400,async()=>await doubles.LogsAsync(d.CustomerReference,new(2026,10,20),new(2026,10,1),"ALL",1,10,default));
 await Fault(404,async()=>await doubles.DetailsAsync("UNKNOWN",default));
});
await Test("Internet banking actions simulate without status changes or approval claims",async()=>{
 var key=Guid.NewGuid();var r=await doubles.ActionAsync("SYNTHETIC-CUSTOMER","BLOCK","Other","Demo reason",key,default);Assert(r.Status=="SIMULATED"&&r.Reference.StartsWith("MOCK-IB-"));
 Assert(((await ((IInternetBankingMiddleware)doubles).ProfileAsync("SYNTHETIC-CUSTOMER",default)).Status)=="ACTIVE");
 Assert((await doubles.ActionAsync("SYNTHETIC-CUSTOMER","ACTIVATE",null,null,key,default)).Status=="SIMULATED");
 await Fault(400,async()=>await doubles.ActionAsync("SYNTHETIC-CUSTOMER","BLOCK",null,null,key,default));
 await Fault(400,async()=>await doubles.ActionAsync("SYNTHETIC-CUSTOMER","ACTIVATE",null,new string('x',501),key,default));
 await Fault(404,async()=>await doubles.ActionAsync("UNKNOWN","ACTIVATE",null,null,key,default));
});
CreateTicketRequest Request(long version=1)=>new(1,version,Guid.NewGuid(),"SYNTHETIC-CUSTOMER",[new(1,"Synthetic value")],"Synthetic request");
await Test("Ticket creation derives trusted Avaya interaction",async()=>{var t=await app.CreateAsync(Request(),agent,default);Assert(t.RequestID>0);var call=mock.Calls.Last();Assert((string)call.Parameters["AvayaInteractionID"]! == agent.InteractionId);Assert(call.Actor.Id==agent.Id);Assert(call.Parameters["CustomerName"] as string==(await doubles.ProfileAsync("SYNTHETIC-CUSTOMER",default)).Name);});
await Test("Manager cannot create Agent ticket",()=>Fault(403,async()=>await app.CreateAsync(Request(),manager,default)));
await Test("Unverified interaction rejected",()=>Fault(400,async()=>await app.CreateAsync(Request(),agent with{InteractionId=null},default)));
await Test("Unpublished service version rejected by mock contract",()=>Fault(422,async()=>await app.CreateAsync(Request(999),agent,default)));
await Test("Duplicate dynamic fields rejected",()=>Fault(400,async()=>await app.CreateAsync(Request() with{Fields=[new(1,"a"),new(1,"b")]},agent,default)));
await Test("Idempotent duplicate conflicts",async()=>{var request=Request();await app.CreateAsync(request,agent,default);await Fault(409,async()=>await app.CreateAsync(request,agent,default));});
await Test("Mandatory approval initial state",async()=>{var created=await app.CreateAsync(Request(),agent,default);var read=await procedures.Ticket_GetByIDAsync(new(){RequestID=created.RequestID},agent);Assert(read.Rows1.Single().ApprovalStatus=="PENDING");});
await Test("Approval and stale-rowversion conflict",async()=>{var t=await app.CreateAsync(Request(),agent,default);await procedures.Ticket_ApproveAsync(new(){RequestID=t.RequestID,ExpectedRowVersion=BitConverter.GetBytes(1L)},manager);await Fault(409,async()=>await procedures.Ticket_RejectAsync(new(){RequestID=t.RequestID,ExpectedRowVersion=BitConverter.GetBytes(1L),Remarks="Reason"},manager));});
await Test("Agent Manager-only call blocked",()=>Fault(403,async()=>await procedures.Ticket_ApproveAsync(new(){RequestID=1,ExpectedRowVersion=new byte[8]},agent)));
await Test("Unapproved status transition rejected",async()=>{var t=await app.CreateAsync(Request(),agent,default);await Fault(422,async()=>await procedures.Ticket_UpdateStatusAsync(new(){RequestID=t.RequestID,ToStatusID=5,ExpectedRowVersion=BitConverter.GetBytes(1L)},manager));});
await Test("Token randomness and canonical hashing",()=>Action(()=>{var a=TrackingTokens.Create();var b=TrackingTokens.Create();Assert(a.Token.Length==43&&a.Token!=b.Token&&a.Hash.Length==32);Assert(a.Hash.SequenceEqual(TrackingTokens.Hash(a.Token)));}));
await Test("Invalid token has generic response",()=>Fault(404,()=>Action(()=>TrackingTokens.Hash("invalid"))));
await Test("Expired or unknown tracking returns generic unavailable",()=>Fault(404,async()=>await app.TrackAsync(new(TrackingTokens.Create().Token),Guid.NewGuid(),default)));
await Test("Created token can track without PII",async()=>{var token=TrackingTokens.Create();var t=await procedures.Ticket_CreateAsync(new(){ServiceID=1,VersionID=1,IdempotencyKey=Guid.NewGuid(),AvayaInteractionID="TEST",FieldValuesJson="[]",TrackingTokenHash=token.Hash,EncryptedDeliveryPayload=RandomNumberGenerator.GetBytes(64),PayloadKeyReference="DEVELOPMENT",PayloadExpiresAt=DateTime.UtcNow.AddDays(1)},agent);var r=await app.TrackAsync(new(token.Token),Guid.NewGuid(),default);Assert(r.Status.Single().TicketNumber==t.TicketNumber);Assert(!JsonSerializer.Serialize(r).Contains("CustomerMobile"));});
await Test("Replacement contract gap fails closed",()=>Fault(503,async()=>await app.ReplaceAsync(new(1,new byte[8],"Replace"),manager,default)));
await Test("Protected delivery round trip",async()=>{var e=await doubles.ProtectAsync(new("secret-token","SYNTHETIC-CUSTOMER"),DateTime.UtcNow.AddMinutes(5),default);Assert(!System.Text.Encoding.UTF8.GetString(e.Ciphertext).Contains("secret-token"));Assert((await doubles.UnprotectAsync(e,default)).Token=="secret-token");});
await Test("Expired delivery cannot decrypt",()=>Fault(410,async()=>await doubles.UnprotectAsync(new([],"DEVELOPMENT",DateTime.UtcNow.AddSeconds(-1)),default)));
await Test("Typed projection refuses missing result columns",()=>Fault(503,()=>Action(()=>{var r=new ProcedureResult();r.Sets.Add([new(){{"RequestID",1L}}]);r.Rows<Ticket_GetByIDRow1>(0);})));
foreach(var (code,status) in new[]{(52000,403),(229,403),(52001,400),(52004,409),(52005,422),(52006,422),(52009,409),(52011,410),(1205,409),(-2,503)})await Test($"SQL error {code} safe HTTP {status}",()=>Action(()=>Assert(DatabaseErrors.Map(code).Status==status&&!DatabaseErrors.Map(code).Message.Contains("dbo."))));
await Test("Exact SQL output metadata and no ad-hoc command",()=>Action(()=>{using var c=SqlProcedureExecutor.BuildCommand(ProcedureCatalog.All["usp_Ticket_Create"],ParameterValues.From(new Ticket_CreateRequest{IdempotencyKey=Guid.NewGuid(),ServiceID=1,VersionID=1,AvayaInteractionID="A",FieldValuesJson="[]",TrackingTokenHash=new byte[32],EncryptedDeliveryPayload=new byte[64],PayloadKeyReference="K",PayloadExpiresAt=DateTime.UtcNow}));Assert(c.CommandType==CommandType.StoredProcedure&&c.CommandText=="dbo.usp_Ticket_Create");Assert(c.Parameters["@RequestID"].Direction==ParameterDirection.Output);Assert(c.Parameters["@TicketNumber"].Size==30);Assert(c.Parameters["@TrackingTokenHash"].SqlDbType==SqlDbType.VarBinary);}));
await Test("Only verified snapshots populate SQL session",()=>Action(()=>{var context=SqlProcedureExecutor.Context(agent);Assert((string)context["Customer360.ActorRole"]! == "AGENT");Assert((string)context["Customer360.AuthenticationSource"]! == "AVAYA");}));
// Explicit provider unknown-outcome safety test: must not write an invented UNKNOWN @Result.
await Test("Unknown provider outcome leaves started attempt for reconciliation",async()=>{
 var executor=new MockProcedureExecutor();var db=new Procedures(executor);var provider=new UnknownProvider();var processor=new CommunicationProcessor(db,provider,provider,doubles,new ConfigurationBuilder().Build());
 await processor.ProcessAsync(new(){CommunicationID=123,LeaseID="LEASE",ChannelCode="SMS",RecipientAddress="SYNTHETIC",MessageBody="Synthetic"},ActorFor("WORKER"),default);
 Assert(executor.Calls.Any(c=>c.Procedure=="usp_CommunicationAttempt_Start"));Assert(!executor.Calls.Any(c=>c.Procedure=="usp_CommunicationAttempt_Complete"));
});
await Test("Reconciliation missing database contract is explicit",()=>Fault(503,async()=>await new UnconfiguredReconciliationBacklog().ReadAsync(default)));
await Test("Middleware success audited without customer bodies",async()=>{
 var m=new MockProcedureExecutor();var audit=new IntegrationAudit(new Procedures(m));var result=await audit.ExecuteAsync("CBS","Profile",agent,()=>Task.FromResult("SYNTHETIC_CUSTOMER_BODY"),default);
 Assert(result=="SYNTHETIC_CUSTOMER_BODY");Assert(m.Calls.Count==2);Assert(m.Calls[0].Procedure=="usp_IntegrationLog_Start"&&m.Calls[1].Procedure=="usp_IntegrationLog_Complete");Assert(!JsonSerializer.Serialize(m.Calls.Select(c=>c.Parameters)).Contains("SYNTHETIC_CUSTOMER_BODY"));
});
await Test("Middleware failure logs sanitized code only",async()=>{
 var m=new MockProcedureExecutor();var audit=new IntegrationAudit(new Procedures(m));
 try{await audit.ExecuteAsync<string>("CBS","Profile",agent,()=>throw new InvalidOperationException("SYNTHETIC_SECRET"),default);throw new Exception("Expected failure");}catch(InvalidOperationException){}
 Assert(m.Calls.Last().Parameters["Result"] as string=="FAILURE");Assert(!JsonSerializer.Serialize(m.Calls.Select(c=>c.Parameters)).Contains("SYNTHETIC_SECRET"));
});
await Test("Development lookup and creation profile share the default mobile",async()=>{
 var found=await doubles.SearchAsync("01712345678",default);
 Assert(found.Count==1&&found[0].Mobile=="01712345678");
 var profile=await doubles.ProfileAsync(found[0].Reference,default);
 Assert(profile.Mobile==found[0].Mobile&&profile.Reference==found[0].Reference);
});
var output=args.FirstOrDefault()??"unit-results.json";Directory.CreateDirectory(Path.GetDirectoryName(Path.GetFullPath(output))!);await File.WriteAllTextAsync(output,JsonSerializer.Serialize(new{suite="database-independent .NET unit tests",passed=results.Count-failed,failed,results},new JsonSerializerOptions{WriteIndented=true}));Console.WriteLine($"Unit tests: {results.Count-failed} passed, {failed} failed");return failed==0?0:1;
sealed class UnknownProvider:ISmsProvider,IEmailProvider
{
 public Task<ProviderReceipt> SendAsync(ProviderMessage m,CancellationToken ct)=>Task.FromResult(new ProviderReceipt(ProviderOutcome.Unknown,null,"UNKNOWN"));
 public Task<ProviderReceipt> ReconcileAsync(string r,CancellationToken ct)=>Task.FromResult(new ProviderReceipt(ProviderOutcome.Unknown,null,"UNKNOWN"));
}

