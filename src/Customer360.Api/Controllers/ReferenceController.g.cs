#nullable enable
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Customer360.Application;
using Customer360.Contracts;
using Customer360.Security;
namespace Customer360.Controllers;
[ApiController]
[Route("api/v1/reference")]
public sealed class ReferenceController(Procedures procedures, ActorAccessor actors) : ControllerBase
{
 [HttpPost("priority/get-all")]
 [Authorize(Policy="READ")]
 public async Task<ActionResult<ApiResponse<Priority_GetAllResponse>>> Priority_GetAll([FromBody] Priority_GetAllRequest request, CancellationToken ct) => Ok(ApiResponse<Priority_GetAllResponse>.Ok(await procedures.Priority_GetAllAsync(request, actors.Current, ct), HttpContext.TraceIdentifier));
 [HttpPost("priority/get-active")]
 [Authorize(Policy="READ")]
 public async Task<ActionResult<ApiResponse<Priority_GetActiveResponse>>> Priority_GetActive([FromBody] Priority_GetActiveRequest request, CancellationToken ct) => Ok(ApiResponse<Priority_GetActiveResponse>.Ok(await procedures.Priority_GetActiveAsync(request, actors.Current, ct), HttpContext.TraceIdentifier));
 [HttpPost("priority/create")]
 [Authorize(Policy="CONFIG")]
 public async Task<ActionResult<ApiResponse<Priority_CreateResponse>>> Priority_Create([FromBody] Priority_CreateRequest request, CancellationToken ct) => Ok(ApiResponse<Priority_CreateResponse>.Ok(await procedures.Priority_CreateAsync(request, actors.Current, ct), HttpContext.TraceIdentifier));
 [HttpPost("priority/update")]
 [Authorize(Policy="CONFIG")]
 public async Task<ActionResult<ApiResponse<Priority_UpdateResponse>>> Priority_Update([FromBody] Priority_UpdateRequest request, CancellationToken ct) => Ok(ApiResponse<Priority_UpdateResponse>.Ok(await procedures.Priority_UpdateAsync(request, actors.Current, ct), HttpContext.TraceIdentifier));
 [HttpPost("priority/set-active")]
 [Authorize(Policy="CONFIG")]
 public async Task<ActionResult<ApiResponse<Priority_SetActiveResponse>>> Priority_SetActive([FromBody] Priority_SetActiveRequest request, CancellationToken ct) => Ok(ApiResponse<Priority_SetActiveResponse>.Ok(await procedures.Priority_SetActiveAsync(request, actors.Current, ct), HttpContext.TraceIdentifier));
 [HttpPost("request-source/get-all")]
 [Authorize(Policy="READ")]
 public async Task<ActionResult<ApiResponse<RequestSource_GetAllResponse>>> RequestSource_GetAll([FromBody] RequestSource_GetAllRequest request, CancellationToken ct) => Ok(ApiResponse<RequestSource_GetAllResponse>.Ok(await procedures.RequestSource_GetAllAsync(request, actors.Current, ct), HttpContext.TraceIdentifier));
 [HttpPost("request-source/get-active")]
 [Authorize(Policy="READ")]
 public async Task<ActionResult<ApiResponse<RequestSource_GetActiveResponse>>> RequestSource_GetActive([FromBody] RequestSource_GetActiveRequest request, CancellationToken ct) => Ok(ApiResponse<RequestSource_GetActiveResponse>.Ok(await procedures.RequestSource_GetActiveAsync(request, actors.Current, ct), HttpContext.TraceIdentifier));
 [HttpPost("request-source/create")]
 [Authorize(Policy="CONFIG")]
 public async Task<ActionResult<ApiResponse<RequestSource_CreateResponse>>> RequestSource_Create([FromBody] RequestSource_CreateRequest request, CancellationToken ct) => Ok(ApiResponse<RequestSource_CreateResponse>.Ok(await procedures.RequestSource_CreateAsync(request, actors.Current, ct), HttpContext.TraceIdentifier));
 [HttpPost("request-source/update")]
 [Authorize(Policy="CONFIG")]
 public async Task<ActionResult<ApiResponse<RequestSource_UpdateResponse>>> RequestSource_Update([FromBody] RequestSource_UpdateRequest request, CancellationToken ct) => Ok(ApiResponse<RequestSource_UpdateResponse>.Ok(await procedures.RequestSource_UpdateAsync(request, actors.Current, ct), HttpContext.TraceIdentifier));
 [HttpPost("request-source/set-active")]
 [Authorize(Policy="CONFIG")]
 public async Task<ActionResult<ApiResponse<RequestSource_SetActiveResponse>>> RequestSource_SetActive([FromBody] RequestSource_SetActiveRequest request, CancellationToken ct) => Ok(ApiResponse<RequestSource_SetActiveResponse>.Ok(await procedures.RequestSource_SetActiveAsync(request, actors.Current, ct), HttpContext.TraceIdentifier));
 [HttpPost("ticket-status/get-all")]
 [Authorize(Policy="READ")]
 public async Task<ActionResult<ApiResponse<TicketStatus_GetAllResponse>>> TicketStatus_GetAll([FromBody] TicketStatus_GetAllRequest request, CancellationToken ct) => Ok(ApiResponse<TicketStatus_GetAllResponse>.Ok(await procedures.TicketStatus_GetAllAsync(request, actors.Current, ct), HttpContext.TraceIdentifier));
 [HttpPost("ticket-status/get-active")]
 [Authorize(Policy="READ")]
 public async Task<ActionResult<ApiResponse<TicketStatus_GetActiveResponse>>> TicketStatus_GetActive([FromBody] TicketStatus_GetActiveRequest request, CancellationToken ct) => Ok(ApiResponse<TicketStatus_GetActiveResponse>.Ok(await procedures.TicketStatus_GetActiveAsync(request, actors.Current, ct), HttpContext.TraceIdentifier));
 [HttpPost("ticket-status-transition/get-allowed")]
 [Authorize(Policy="READ")]
 public async Task<ActionResult<ApiResponse<TicketStatusTransition_GetAllowedResponse>>> TicketStatusTransition_GetAllowed([FromBody] TicketStatusTransition_GetAllowedRequest request, CancellationToken ct) => Ok(ApiResponse<TicketStatusTransition_GetAllowedResponse>.Ok(await procedures.TicketStatusTransition_GetAllowedAsync(request, actors.Current, ct), HttpContext.TraceIdentifier));
}
