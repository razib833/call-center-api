using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Customer360.Application;
using Customer360.Contracts;
using Customer360.Security;
namespace Customer360.Controllers;
[ApiController,Route("api/v1")]
public sealed class WorkflowController(TicketApplication tickets,ActorAccessor actors,Procedures procedures) : ControllerBase
{
 [HttpPost("tickets"),Authorize(Policy="AGENT")]
 public async Task<ActionResult<ApiResponse<CreatedTicket>>> Create(CreateTicketRequest request,CancellationToken ct)=>Ok(ApiResponse<CreatedTicket>.Ok(await tickets.CreateAsync(request,actors.Current,ct),HttpContext.TraceIdentifier));
 [HttpPost("tracking"),AllowAnonymous]
 public async Task<ActionResult<ApiResponse<PublicTrackingResponse>>> Track(PublicTrackingRequest request,CancellationToken ct)=>Ok(ApiResponse<PublicTrackingResponse>.Ok(await tickets.TrackAsync(request,Guid.Parse(HttpContext.TraceIdentifier),ct),HttpContext.TraceIdentifier));
 [HttpPost("tracking-management/replace"),Authorize(Policy="MANAGER")]
 public async Task<ActionResult<ApiResponse<TrackingToken_ReplaceResponse>>> Replace(TokenReplacementRequest request,CancellationToken ct)=>Ok(ApiResponse<TrackingToken_ReplaceResponse>.Ok(await tickets.ReplaceAsync(request,actors.Current,ct),HttpContext.TraceIdentifier));
 [HttpPost("tracking-management/revoke"),Authorize(Policy="MANAGER")]
 public async Task<ActionResult<ApiResponse<TrackingToken_RevokeResponse>>> Revoke(TrackingToken_RevokeRequest request,CancellationToken ct)=>Ok(ApiResponse<TrackingToken_RevokeResponse>.Ok(await procedures.TrackingToken_RevokeAsync(request,actors.Current,ct),HttpContext.TraceIdentifier));
}
