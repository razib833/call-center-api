using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Customer360.Contracts;
using Customer360.Integrations;
using Customer360.Security;
namespace Customer360.Controllers;
[ApiController,Route("api/v1/auth")]
public sealed class AuthenticationController(IAvayaIntegration avaya,IManagerAuthentication managers,IHostEnvironment environment,IConfiguration configuration,ActorAccessor actors,Customer360.Application.Procedures procedures,ILogger<AuthenticationController> logger) : ControllerBase
{
 [HttpGet("csrf"),AllowAnonymous]
 public ActionResult<ApiResponse<CsrfToken>> Csrf([FromServices] IAntiforgery antiforgery)=>Ok(ApiResponse<CsrfToken>.Ok(new(antiforgery.GetAndStoreTokens(HttpContext).RequestToken??throw new ApiFault(503,"CSRF_UNAVAILABLE","Request verification unavailable")),HttpContext.TraceIdentifier));
 [HttpPost("avaya"),AllowAnonymous]
 public async Task<ActionResult<ApiResponse<SessionView>>> Avaya(AvayaAssertion request,CancellationToken ct)
 {var actor=await avaya.ValidateAsync(request,ct);if(actor.Capability!="AGENT"||actor.Source!="AVAYA"||string.IsNullOrWhiteSpace(actor.InteractionId))throw new ApiFault(401,"INVALID_AVAYA_CONTEXT","Verified Avaya context required");return await SignIn(actor);}
 [HttpPost("manager"),AllowAnonymous]
 public async Task<ActionResult<ApiResponse<SessionView>>> Manager(ManagerCredentials request,CancellationToken ct)
 {var actor=await managers.AuthenticateAsync(request,ct);if(actor.Capability is not("MANAGER" or "CONFIG")||actor.Source!="EXTERNAL_MANAGER")throw new ApiFault(401,"INVALID_MANAGER_CONTEXT","Verified Manager context required");return await SignIn(actor);}
 [HttpPost("development/session"),AllowAnonymous]
 public async Task<ActionResult<ApiResponse<SessionView>>> Development(DevelopmentSession request)
 {
  if(!FeatureSafety.AllowDevelopment(environment.EnvironmentName,configuration.GetValue<bool>("Development:EnableMockAuth")))return NotFound();
  if(HttpContext.Connection.RemoteIpAddress is not { } ip || !System.Net.IPAddress.IsLoopback(ip))return Forbid();
  var actor=request.Persona switch {
   "Agent"=>new Actor("AGENT001","Development Agent","AGENT","AVAYA",DateTime.UtcNow.AddMinutes(15),Guid.NewGuid(),"DEV-"+Guid.NewGuid()),
   "Manager"=>new Actor("MGR001","Development Manager","MANAGER","EXTERNAL_MANAGER",DateTime.UtcNow.AddMinutes(15),Guid.NewGuid()),
   "Configuration"=>new Actor("MGR001","Development Configuration","CONFIG","EXTERNAL_MANAGER",DateTime.UtcNow.AddMinutes(15),Guid.NewGuid()),
   "Auditor"=>new Actor("AUDIT001","Development Auditor","AUDIT","SERVICE_IDENTITY",DateTime.UtcNow.AddMinutes(15),Guid.NewGuid()),
   _=>throw new ApiFault(400,"PERSONA_INVALID","Unknown development persona")};
  return await SignIn(actor);
 }
 [HttpGet("session"),Authorize]
 public ActionResult<ApiResponse<SessionView>> Session(){var a=actors.Current;a.Demand(a.Capability);return Ok(ApiResponse<SessionView>.Ok(new(a.Id,a.Name,a.Capability,a.VerifiedUntil),HttpContext.TraceIdentifier));}
 [HttpPost("logout"),Authorize]
 public async Task<ActionResult<ApiResponse<SessionClosed>>> Logout(){await HttpContext.SignOutAsync();return Ok(ApiResponse<SessionClosed>.Ok(new(true),HttpContext.TraceIdentifier));}
 async Task<ActionResult<ApiResponse<SessionView>>> SignIn(Actor actor)
 {
  actor.Demand(actor.Capability);
  if(string.IsNullOrWhiteSpace(actor.Id)||actor.Id.Length>100||string.IsNullOrWhiteSpace(actor.Name)||actor.Name.Length>200)throw new ApiFault(401,"INVALID_IDENTITY","Verified identity is invalid");
  actor=actor with{CorrelationId=Guid.Parse(HttpContext.TraceIdentifier),VerifiedUntil=actor.VerifiedUntil<DateTime.UtcNow.AddMinutes(15)?actor.VerifiedUntil:DateTime.UtcNow.AddMinutes(15)};
  if(actor.Capability!="AUDIT")await procedures.AuthenticationLog_CreateAsync(new(){AuthenticationMethod=actor.Source,AuthenticationResult="SUCCESS",EnvironmentCode=environment.EnvironmentName},actor,HttpContext.RequestAborted);
  else logger.LogInformation("Audit authentication succeeded; correlation {Correlation}. SQL authentication logging requires an approved separate writer contract",actor.CorrelationId);
  await HttpContext.SignInAsync(IdentityClaims.Principal(actor),new AuthenticationProperties{IsPersistent=false,ExpiresUtc=new DateTimeOffset(actor.VerifiedUntil),AllowRefresh=false});
  return Ok(ApiResponse<SessionView>.Ok(new(actor.Id,actor.Name,actor.Capability,actor.VerifiedUntil),HttpContext.TraceIdentifier));
 }
}
public sealed record DevelopmentSession([Required]string Persona);
public sealed record SessionView(string Id,string Name,string Capability,DateTime VerifiedUntil);
public sealed record SessionClosed(bool Closed);
public sealed record CsrfToken(string Token);
public static class FeatureSafety
{
 public static bool AllowDevelopment(string environment,bool enabled)=>enabled && environment is "Development" or "Testing";
 public static void Validate(string environment,bool mocks,bool auth,bool integrations,bool workers)
 {if((mocks||auth||integrations)&&environment is not("Development" or "Testing"))throw new InvalidOperationException("Development doubles cannot run outside Development/Testing");if(workers)throw new InvalidOperationException("Worker activation requires completed live SQL/provider/reconciliation validation; no worker activation approved in this release");}
}
