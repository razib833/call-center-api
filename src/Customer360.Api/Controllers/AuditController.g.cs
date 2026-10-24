#nullable enable
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Customer360.Application;
using Customer360.Contracts;
using Customer360.Security;
namespace Customer360.Controllers;
[ApiController]
[Route("api/v1/audit")]
public sealed class AuditController(Procedures procedures, ActorAccessor actors) : ControllerBase
{
 [HttpPost("authentication-log/search")]
 [Authorize(Policy="AUDIT")]
 public async Task<ActionResult<ApiResponse<AuthenticationLog_SearchResponse>>> AuthenticationLog_Search([FromBody] AuthenticationLog_SearchRequest request, CancellationToken ct) => Ok(ApiResponse<AuthenticationLog_SearchResponse>.Ok(await procedures.AuthenticationLog_SearchAsync(request, actors.Current, ct), HttpContext.TraceIdentifier));
 [HttpPost("audit-log/search")]
 [Authorize(Policy="AUDIT")]
 public async Task<ActionResult<ApiResponse<AuditLog_SearchResponse>>> AuditLog_Search([FromBody] AuditLog_SearchRequest request, CancellationToken ct) => Ok(ApiResponse<AuditLog_SearchResponse>.Ok(await procedures.AuditLog_SearchAsync(request, actors.Current, ct), HttpContext.TraceIdentifier));
 [HttpPost("audit-log/get-by-ticket")]
 [Authorize(Policy="AUDIT")]
 public async Task<ActionResult<ApiResponse<AuditLog_GetByTicketResponse>>> AuditLog_GetByTicket([FromBody] AuditLog_GetByTicketRequest request, CancellationToken ct) => Ok(ApiResponse<AuditLog_GetByTicketResponse>.Ok(await procedures.AuditLog_GetByTicketAsync(request, actors.Current, ct), HttpContext.TraceIdentifier));
 [HttpPost("audit-log/get-by-actor")]
 [Authorize(Policy="AUDIT")]
 public async Task<ActionResult<ApiResponse<AuditLog_GetByActorResponse>>> AuditLog_GetByActor([FromBody] AuditLog_GetByActorRequest request, CancellationToken ct) => Ok(ApiResponse<AuditLog_GetByActorResponse>.Ok(await procedures.AuditLog_GetByActorAsync(request, actors.Current, ct), HttpContext.TraceIdentifier));
 [HttpPost("integration-log/search")]
 [Authorize(Policy="AUDIT")]
 public async Task<ActionResult<ApiResponse<IntegrationLog_SearchResponse>>> IntegrationLog_Search([FromBody] IntegrationLog_SearchRequest request, CancellationToken ct) => Ok(ApiResponse<IntegrationLog_SearchResponse>.Ok(await procedures.IntegrationLog_SearchAsync(request, actors.Current, ct), HttpContext.TraceIdentifier));
 [HttpPost("integration-log/get-failures")]
 [Authorize(Policy="AUDIT")]
 public async Task<ActionResult<ApiResponse<IntegrationLog_GetFailuresResponse>>> IntegrationLog_GetFailures([FromBody] IntegrationLog_GetFailuresRequest request, CancellationToken ct) => Ok(ApiResponse<IntegrationLog_GetFailuresResponse>.Ok(await procedures.IntegrationLog_GetFailuresAsync(request, actors.Current, ct), HttpContext.TraceIdentifier));
}
