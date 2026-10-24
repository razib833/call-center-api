using System.Text.Json;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using Customer360.Contracts;
using Customer360.Application;
using Customer360.Data;
using Customer360.Security;
// Standalone integration runner. Default is an honest BLOCKED inventory, never a passing stub.
var names=new[]{"SQL stored procedure execution","Database restricted-principal permissions","Ticket transaction consistency","Approval independent-session concurrency","Outbox independent-session concurrency","Tracking lifecycle and fixed original expiry"};
var results=names.Select(name=>new Result(name,"BLOCKED","Live target/identity/fixture/isolation approval and prepared case implementation required")).ToArray();
if(args.Contains("--execute-reference-smoke"))
{
 var path=Environment.GetEnvironmentVariable("C360_INTEGRATION_CONFIG")??throw new InvalidOperationException("C360_INTEGRATION_CONFIG required");
 if(Environment.GetEnvironmentVariable("C360_INTEGRATION_APPROVED")!="true")throw new InvalidOperationException("Explicit live integration approval required");
 var configuration=JsonSerializer.Deserialize<DatabaseOptions>(await File.ReadAllTextAsync(path))??throw new InvalidOperationException("Invalid configuration");
 if(configuration.ExpectedDatabase!="JamunaCustomer360_Test"||configuration.ExpectedServer!="DESKTOP-BUDV4V7"||!configuration.LiveValidationApproved)throw new InvalidOperationException("Approved isolated QA target/identity verification required");
 var actor=new Actor("QA_INTEGRATION","QA integration","AGENT","AVAYA",DateTime.UtcNow.AddMinutes(2),Guid.NewGuid());
 try{var db=new Procedures(new SqlProcedureExecutor(Options.Create(configuration),NullLogger<SqlProcedureExecutor>.Instance));var rows=await db.Priority_GetActiveAsync(new(),actor);if(rows.Rows1.Count==0)throw new Exception("Expected priority seed rows missing");results[0]=new(names[0],"PASS","Actual Priority_GetActive returned expected typed rows; scope is reference smoke only");}
 catch(Exception ex){results[0]=new(names[0],"FAIL",ex is ApiFault f?f.Code:"Integration execution failed; retain restricted diagnostics");}
}
var destination=args.FirstOrDefault(x=>x.EndsWith(".json"))??"integration-results.json";
await File.WriteAllTextAsync(destination,JsonSerializer.Serialize(new{results},new JsonSerializerOptions{WriteIndented=true}));
foreach(var r in results)Console.WriteLine($"{r.Status}: {r.Name} — {r.Evidence}");
return results.Any(r=>r.Status=="FAIL")?1:results.Any(r=>r.Status=="BLOCKED")?2:0;
record Result(string Name,string Status,string Evidence);
