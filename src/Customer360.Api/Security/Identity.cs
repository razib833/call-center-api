using System.Security.Claims;
using Customer360.Contracts;
namespace Customer360.Security;
public sealed record Actor(string Id,string Name,string Capability,string Source,DateTime VerifiedUntil,Guid CorrelationId,string? InteractionId=null)
{
 public static Actor Public(Guid correlation) => new("PUBLIC","Public tracking","PUBLIC","PUBLIC",DateTime.UtcNow.AddMinutes(1),correlation);
 public string SqlRole => Capability switch {"AGENT"=>"AGENT","MANAGER" or "CONFIG"=>"MANAGER","WORKER"=>"WORKER","AUDIT"=>"AUDITOR","PUBLIC"=>"PUBLIC",_=>throw new ApiFault(403,"CAPABILITY_DENIED","Access denied")};
 public void Demand(string capability)
 {
  if(VerifiedUntil<=DateTime.UtcNow)throw new ApiFault(401,"SESSION_EXPIRED","Authentication required");
  var allowed=capability switch {"READ"=>Capability is "AGENT" or "MANAGER" or "CONFIG","TRUSTED"=>Capability is "AGENT" or "MANAGER" or "CONFIG" or "WORKER",_=>Capability==capability};
  if(!allowed)throw new ApiFault(403,"CAPABILITY_DENIED","Access denied");
 }
}
public sealed class ActorAccessor(IHttpContextAccessor accessor)
{
 public Actor Current
 {
  get
  {
   var h=accessor.HttpContext??throw new ApiFault(401,"NO_SESSION","Authentication required");var p=h.User;
   if(p.Identity?.IsAuthenticated!=true)throw new ApiFault(401,"NO_SESSION","Authentication required");
   var expiry=p.FindFirstValue("verified_until");
   if(!long.TryParse(expiry,out var unix))throw new ApiFault(401,"NO_SESSION","Authentication required");
   return new(p.FindFirstValue(ClaimTypes.NameIdentifier)!,p.FindFirstValue(ClaimTypes.Name)!,p.FindFirstValue("capability")!,p.FindFirstValue("auth_source")!,DateTimeOffset.FromUnixTimeSeconds(unix).UtcDateTime,Guid.Parse(h.TraceIdentifier),p.FindFirstValue("interaction_id"));
  }
 }
}
public static class IdentityClaims
{
 public static ClaimsPrincipal Principal(Actor actor) => new(new ClaimsIdentity(new[]{new Claim(ClaimTypes.NameIdentifier,actor.Id),new Claim(ClaimTypes.Name,actor.Name),new Claim("capability",actor.Capability),new Claim("auth_source",actor.Source),new Claim("verified_until",new DateTimeOffset(actor.VerifiedUntil).ToUnixTimeSeconds().ToString()),new Claim("interaction_id",actor.InteractionId??"")},"Customer360"));
}
