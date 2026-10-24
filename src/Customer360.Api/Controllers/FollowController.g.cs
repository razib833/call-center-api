#nullable enable
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Customer360.Application;
using Customer360.Contracts;
using Customer360.Security;
namespace Customer360.Controllers;
[ApiController]
[Route("api/v1/follow")]
public sealed class FollowController(Procedures procedures, ActorAccessor actors) : ControllerBase
{
 [HttpPost("follow-up/add")]
 [Authorize(Policy="MANAGER")]
 public async Task<ActionResult<ApiResponse<FollowUp_AddResponse>>> FollowUp_Add([FromBody] FollowUp_AddRequest request, CancellationToken ct) => Ok(ApiResponse<FollowUp_AddResponse>.Ok(await procedures.FollowUp_AddAsync(request, actors.Current, ct), HttpContext.TraceIdentifier));
 [HttpPost("follow-up/get-by-ticket")]
 [Authorize(Policy="MANAGER")]
 public async Task<ActionResult<ApiResponse<FollowUp_GetByTicketResponse>>> FollowUp_GetByTicket([FromBody] FollowUp_GetByTicketRequest request, CancellationToken ct) => Ok(ApiResponse<FollowUp_GetByTicketResponse>.Ok(await procedures.FollowUp_GetByTicketAsync(request, actors.Current, ct), HttpContext.TraceIdentifier));
 [HttpPost("follow-up-schedule/create")]
 [Authorize(Policy="MANAGER")]
 public async Task<ActionResult<ApiResponse<FollowUpSchedule_CreateResponse>>> FollowUpSchedule_Create([FromBody] FollowUpSchedule_CreateRequest request, CancellationToken ct) => Ok(ApiResponse<FollowUpSchedule_CreateResponse>.Ok(await procedures.FollowUpSchedule_CreateAsync(request, actors.Current, ct), HttpContext.TraceIdentifier));
 [HttpPost("follow-up-schedule/get-by-ticket")]
 [Authorize(Policy="MANAGER")]
 public async Task<ActionResult<ApiResponse<FollowUpSchedule_GetByTicketResponse>>> FollowUpSchedule_GetByTicket([FromBody] FollowUpSchedule_GetByTicketRequest request, CancellationToken ct) => Ok(ApiResponse<FollowUpSchedule_GetByTicketResponse>.Ok(await procedures.FollowUpSchedule_GetByTicketAsync(request, actors.Current, ct), HttpContext.TraceIdentifier));
 [HttpPost("follow-up-schedule/get-due")]
 [Authorize(Policy="MANAGER")]
 public async Task<ActionResult<ApiResponse<FollowUpSchedule_GetDueResponse>>> FollowUpSchedule_GetDue([FromBody] FollowUpSchedule_GetDueRequest request, CancellationToken ct) => Ok(ApiResponse<FollowUpSchedule_GetDueResponse>.Ok(await procedures.FollowUpSchedule_GetDueAsync(request, actors.Current, ct), HttpContext.TraceIdentifier));
 [HttpPost("follow-up-schedule/complete")]
 [Authorize(Policy="MANAGER")]
 public async Task<ActionResult<ApiResponse<FollowUpSchedule_CompleteResponse>>> FollowUpSchedule_Complete([FromBody] FollowUpSchedule_CompleteRequest request, CancellationToken ct) => Ok(ApiResponse<FollowUpSchedule_CompleteResponse>.Ok(await procedures.FollowUpSchedule_CompleteAsync(request, actors.Current, ct), HttpContext.TraceIdentifier));
 [HttpPost("follow-up-schedule/cancel")]
 [Authorize(Policy="MANAGER")]
 public async Task<ActionResult<ApiResponse<FollowUpSchedule_CancelResponse>>> FollowUpSchedule_Cancel([FromBody] FollowUpSchedule_CancelRequest request, CancellationToken ct) => Ok(ApiResponse<FollowUpSchedule_CancelResponse>.Ok(await procedures.FollowUpSchedule_CancelAsync(request, actors.Current, ct), HttpContext.TraceIdentifier));
 [HttpPost("forwarding/create")]
 [Authorize(Policy="MANAGER")]
 public async Task<ActionResult<ApiResponse<Forwarding_CreateResponse>>> Forwarding_Create([FromBody] Forwarding_CreateRequest request, CancellationToken ct) => Ok(ApiResponse<Forwarding_CreateResponse>.Ok(await procedures.Forwarding_CreateAsync(request, actors.Current, ct), HttpContext.TraceIdentifier));
 [HttpPost("forwarding/get-by-ticket")]
 [Authorize(Policy="MANAGER")]
 public async Task<ActionResult<ApiResponse<Forwarding_GetByTicketResponse>>> Forwarding_GetByTicket([FromBody] Forwarding_GetByTicketRequest request, CancellationToken ct) => Ok(ApiResponse<Forwarding_GetByTicketResponse>.Ok(await procedures.Forwarding_GetByTicketAsync(request, actors.Current, ct), HttpContext.TraceIdentifier));
}
