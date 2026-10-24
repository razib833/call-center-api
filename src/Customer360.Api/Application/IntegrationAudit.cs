using Customer360.Contracts;
using Customer360.Security;
namespace Customer360.Application;
public sealed class IntegrationAudit(Procedures db)
{
 public async Task<T> ExecuteAsync<T>(string integration,string operation,Actor actor,Func<Task<T>> invoke,CancellationToken ct)
 {
  actor.Demand("TRUSTED");
  // EndpointCode is a logical internal label, not a claimed external URI or HTTP contract.
  var started=await db.IntegrationLog_StartAsync(new(){IntegrationCode=integration,OperationCode=operation,IntegrationType="MIDDLEWARE",EndpointCode=integration+"."+operation,EnvironmentCode=Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")??"UNSPECIFIED"},actor,ct);
  var id=started.IntegrationLogID??throw new ApiFault(503,"AUDIT_CONTRACT","Integration audit result is incomplete");
  T value;
  try{value=await invoke();}
  catch
  {
   await db.IntegrationLog_CompleteAsync(new(){IntegrationLogID=id,Result="FAILURE",ErrorCode="MIDDLEWARE_OPERATION_FAILED",SanitizedError="Trusted middleware operation failed"},actor,ct);
   throw;
  }
  await db.IntegrationLog_CompleteAsync(new(){IntegrationLogID=id,Result="SUCCESS"},actor,ct);
  return value;
 }
}
