using System.Collections.Concurrent;
using Customer360.Contracts;
using Customer360.Security;
namespace Customer360.Data;
// Synthetic test double only. Never a substitute for SQL Server workflow/security tests.
public sealed class MockProcedureExecutor : IProcedureExecutor
{
 readonly object gate=new();long next=1000;
 readonly Dictionary<long,TicketState> tickets=[];
 readonly HashSet<long> published=[1];
 readonly HashSet<Guid> keys=[];
 readonly Dictionary<string,(long Ticket,DateTime Expiry,bool Revoked)> tokens=[];
 public sealed record TicketState(long Id,string Number,string Approval,long Version,byte[] RowVersion,DateTime Created);
 public List<(string Procedure,Actor Actor,IReadOnlyDictionary<string,object?> Parameters)> Calls {get;}=[];
 public Func<ProcedureSpec,IReadOnlyDictionary<string,object?>,Actor,ProcedureResult?>? Override {get;set;}
 public Task<ProcedureResult> ExecuteAsync(ProcedureSpec p,IReadOnlyDictionary<string,object?> v,Actor actor,CancellationToken ct)
 {
  ct.ThrowIfCancellationRequested();actor.Demand(p.Capability);
  lock(gate)
  {
   Calls.Add((p.Name,actor,v));
   var custom=Override?.Invoke(p,v,actor);if(custom!=null)return Task.FromResult(custom);
   var r=new ProcedureResult();for(var i=0;i<p.ResultSets;i++)r.Sets.Add([]);
   foreach(var output in p.Parameters.Where(x=>x.Output))r.Outputs[output.Name]=output.SqlType=="varchar"?"SYNTHETIC-"+(++next):++next;
   long Id(string key)=>v.TryGetValue(key,out var value)&&value!=null?Convert.ToInt64(value):0;
   if(p.Capability=="CONFIG" && v.TryGetValue("VersionID",out var version) && version!=null && published.Contains(Convert.ToInt64(version)) && !p.Name.Contains("Retire"))throw DatabaseErrors.Map(52006);
   if(p.Name=="usp_ServiceVersion_PublishDraft")published.Add(Id("VersionID"));
   if(p.Name=="usp_Ticket_Create")
   {
    if(!published.Contains(Id("VersionID")))throw DatabaseErrors.Map(52005);
    var key=(Guid)v["IdempotencyKey"]!;if(!keys.Add(key))throw DatabaseErrors.Map(52009);
    var id=++next;var t=new TicketState(id,"SYNTHETIC-"+id,"PENDING",Id("VersionID"),BitConverter.GetBytes(1L),DateTime.UtcNow);tickets[id]=t;
    r.Outputs["RequestID"]=id;r.Outputs["TicketNumber"]=t.Number;
    tokens[Convert.ToHexString((byte[])v["TrackingTokenHash"]!)]=(id,t.Created.AddDays(30),false);
   }
   if(p.Name is "usp_Ticket_Approve" or "usp_Ticket_Reject" or "usp_Ticket_UpdateStatus")
   {
    if(!tickets.TryGetValue(Id("RequestID"),out var t)||v["ExpectedRowVersion"] is not byte[] rv||!rv.SequenceEqual(t.RowVersion))throw DatabaseErrors.Map(52004);
    if(p.Name!="usp_Ticket_UpdateStatus"&&t.Approval!="PENDING")throw DatabaseErrors.Map(52006);
    if(p.Name=="usp_Ticket_UpdateStatus"&&t.Approval!="APPROVED")throw DatabaseErrors.Map(52006);
    if(p.Name=="usp_Ticket_Reject"&&string.IsNullOrWhiteSpace(v["Remarks"] as string))throw DatabaseErrors.Map(52001);
    tickets[t.Id]=t with{Approval=p.Name=="usp_Ticket_Approve"?"APPROVED":p.Name=="usp_Ticket_Reject"?"REJECTED":t.Approval,RowVersion=BitConverter.GetBytes(BitConverter.ToInt64(t.RowVersion)+1)};
   }
   if(p.Name=="usp_Ticket_GetByID"&&tickets.TryGetValue(Id("RequestID"),out var found))
   {
    var row=EmptyTicketRow();row["RequestID"]=found.Id;row["TicketNumber"]=found.Number;row["ApprovalStatus"]=found.Approval;row["RowVersion"]=found.RowVersion;row["CreatedAt"]=found.Created;r.Sets[0].Add(row);
   }
   if(p.Name=="usp_TrackingToken_Validate")r.Sets[0].Add(new(){{"IsValid",tokens.TryGetValue(Convert.ToHexString((byte[])v["TokenHash"]!),out var token)&&!token.Revoked&&token.Expiry>DateTime.UtcNow}});
   if(p.Name=="usp_TrackingToken_Revoke")
   {
    if(!tickets.ContainsKey(Id("RequestID")))throw DatabaseErrors.Map(52004);
    foreach(var key in tokens.Where(t=>t.Value.Ticket==Id("RequestID")).Select(t=>t.Key).ToArray()){var token=tokens[key];tokens[key]=(token.Ticket,token.Expiry,true);}
   }
   if(p.Name=="usp_Tracking_GetPublicStatus"&&tokens.TryGetValue(Convert.ToHexString((byte[])v["TokenHash"]!),out var tk)&&!tk.Revoked&&tk.Expiry>DateTime.UtcNow)
   {var t=tickets[tk.Ticket];r.Sets[0].Add(new(){{"TicketNumber",t.Number},{"ServiceDisplayName","Synthetic Service"},{"CreatedAt",t.Created},{"PublicStatusCode","RECEIVED"},{"PublicStatusName","Received"},{"PublicDescription","Synthetic tracking status"}});}
   return Task.FromResult(r);
  }
 }
 static Dictionary<string,object?> EmptyTicketRow()=>typeof(Ticket_GetByIDRow1).GetProperties().ToDictionary(p=>p.Name,_=>(object?)null);
}
