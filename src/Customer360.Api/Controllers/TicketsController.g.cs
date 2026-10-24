#nullable enable
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Customer360.Application;
using Customer360.Contracts;
using Customer360.Security;
namespace Customer360.Controllers;
[ApiController]
[Route("api/v1/tickets")]
public sealed class TicketsController(Procedures procedures, ActorAccessor actors) : ControllerBase
{
 [HttpPost("ticket/approve")]
 [Authorize(Policy="MANAGER")]
 public async Task<ActionResult<ApiResponse<Ticket_ApproveResponse>>> Ticket_Approve([FromBody] Ticket_ApproveRequest request, CancellationToken ct) => Ok(ApiResponse<Ticket_ApproveResponse>.Ok(await procedures.Ticket_ApproveAsync(request, actors.Current, ct), HttpContext.TraceIdentifier));
 [HttpPost("ticket/reject")]
 [Authorize(Policy="MANAGER")]
 public async Task<ActionResult<ApiResponse<Ticket_RejectResponse>>> Ticket_Reject([FromBody] Ticket_RejectRequest request, CancellationToken ct) => Ok(ApiResponse<Ticket_RejectResponse>.Ok(await procedures.Ticket_RejectAsync(request, actors.Current, ct), HttpContext.TraceIdentifier));
 [HttpPost("ticket/update-status")]
 [Authorize(Policy="MANAGER")]
 public async Task<ActionResult<ApiResponse<Ticket_UpdateStatusResponse>>> Ticket_UpdateStatus([FromBody] Ticket_UpdateStatusRequest request, CancellationToken ct) => Ok(ApiResponse<Ticket_UpdateStatusResponse>.Ok(await procedures.Ticket_UpdateStatusAsync(request, actors.Current, ct), HttpContext.TraceIdentifier));
 [HttpPost("ticket/get-by-id")]
 [Authorize(Policy="READ")]
 public async Task<ActionResult<ApiResponse<Ticket_GetByIDResponse>>> Ticket_GetByID([FromBody] Ticket_GetByIDRequest request, CancellationToken ct) => Ok(ApiResponse<Ticket_GetByIDResponse>.Ok(await procedures.Ticket_GetByIDAsync(request, actors.Current, ct), HttpContext.TraceIdentifier));
 [HttpPost("ticket/get-by-number")]
 [Authorize(Policy="READ")]
 public async Task<ActionResult<ApiResponse<Ticket_GetByNumberResponse>>> Ticket_GetByNumber([FromBody] Ticket_GetByNumberRequest request, CancellationToken ct) => Ok(ApiResponse<Ticket_GetByNumberResponse>.Ok(await procedures.Ticket_GetByNumberAsync(request, actors.Current, ct), HttpContext.TraceIdentifier));
 [HttpPost("ticket/search")]
 [Authorize(Policy="READ")]
 public async Task<ActionResult<ApiResponse<Ticket_SearchResponse>>> Ticket_Search([FromBody] Ticket_SearchRequest request, CancellationToken ct) => Ok(ApiResponse<Ticket_SearchResponse>.Ok(await procedures.Ticket_SearchAsync(request, actors.Current, ct), HttpContext.TraceIdentifier));
 [HttpPost("ticket/get-recent-by-customer")]
 [Authorize(Policy="READ")]
 public async Task<ActionResult<ApiResponse<Ticket_GetRecentByCustomerResponse>>> Ticket_GetRecentByCustomer([FromBody] Ticket_GetRecentByCustomerRequest request, CancellationToken ct) => Ok(ApiResponse<Ticket_GetRecentByCustomerResponse>.Ok(await procedures.Ticket_GetRecentByCustomerAsync(request, actors.Current, ct), HttpContext.TraceIdentifier));
 [HttpPost("ticket/get-pending-approval")]
 [Authorize(Policy="MANAGER")]
 public async Task<ActionResult<ApiResponse<Ticket_GetPendingApprovalResponse>>> Ticket_GetPendingApproval([FromBody] Ticket_GetPendingApprovalRequest request, CancellationToken ct) => Ok(ApiResponse<Ticket_GetPendingApprovalResponse>.Ok(await procedures.Ticket_GetPendingApprovalAsync(request, actors.Current, ct), HttpContext.TraceIdentifier));
 [HttpPost("ticket/get-priority-queue")]
 [Authorize(Policy="MANAGER")]
 public async Task<ActionResult<ApiResponse<Ticket_GetPriorityQueueResponse>>> Ticket_GetPriorityQueue([FromBody] Ticket_GetPriorityQueueRequest request, CancellationToken ct) => Ok(ApiResponse<Ticket_GetPriorityQueueResponse>.Ok(await procedures.Ticket_GetPriorityQueueAsync(request, actors.Current, ct), HttpContext.TraceIdentifier));
 [HttpPost("ticket/get-status-history")]
 [Authorize(Policy="READ")]
 public async Task<ActionResult<ApiResponse<Ticket_GetStatusHistoryResponse>>> Ticket_GetStatusHistory([FromBody] Ticket_GetStatusHistoryRequest request, CancellationToken ct) => Ok(ApiResponse<Ticket_GetStatusHistoryResponse>.Ok(await procedures.Ticket_GetStatusHistoryAsync(request, actors.Current, ct), HttpContext.TraceIdentifier));
 [HttpPost("ticket/get-activity-history")]
 [Authorize(Policy="READ")]
 public async Task<ActionResult<ApiResponse<Ticket_GetActivityHistoryResponse>>> Ticket_GetActivityHistory([FromBody] Ticket_GetActivityHistoryRequest request, CancellationToken ct) => Ok(ApiResponse<Ticket_GetActivityHistoryResponse>.Ok(await procedures.Ticket_GetActivityHistoryAsync(request, actors.Current, ct), HttpContext.TraceIdentifier));
 [HttpPost("ticket/get-dashboard-summary")]
 [Authorize(Policy="MANAGER")]
 public async Task<ActionResult<ApiResponse<Ticket_GetDashboardSummaryResponse>>> Ticket_GetDashboardSummary([FromBody] Ticket_GetDashboardSummaryRequest request, CancellationToken ct) => Ok(ApiResponse<Ticket_GetDashboardSummaryResponse>.Ok(await procedures.Ticket_GetDashboardSummaryAsync(request, actors.Current, ct), HttpContext.TraceIdentifier));
}
