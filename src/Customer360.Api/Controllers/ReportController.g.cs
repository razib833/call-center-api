#nullable enable
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Customer360.Application;
using Customer360.Contracts;
using Customer360.Security;
namespace Customer360.Controllers;
[ApiController]
[Route("api/v1/report")]
public sealed class ReportController(Procedures procedures, ActorAccessor actors) : ControllerBase
{
 [HttpPost("dashboard/summary-by-status")]
 [Authorize(Policy="MANAGER")]
 public async Task<ActionResult<ApiResponse<Dashboard_SummaryByStatusResponse>>> Dashboard_SummaryByStatus([FromBody] Dashboard_SummaryByStatusRequest request, CancellationToken ct) => Ok(ApiResponse<Dashboard_SummaryByStatusResponse>.Ok(await procedures.Dashboard_SummaryByStatusAsync(request, actors.Current, ct), HttpContext.TraceIdentifier));
 [HttpPost("dashboard/pending-approval-count")]
 [Authorize(Policy="MANAGER")]
 public async Task<ActionResult<ApiResponse<Dashboard_PendingApprovalCountResponse>>> Dashboard_PendingApprovalCount([FromBody] Dashboard_PendingApprovalCountRequest request, CancellationToken ct) => Ok(ApiResponse<Dashboard_PendingApprovalCountResponse>.Ok(await procedures.Dashboard_PendingApprovalCountAsync(request, actors.Current, ct), HttpContext.TraceIdentifier));
 [HttpPost("dashboard/critical-high")]
 [Authorize(Policy="MANAGER")]
 public async Task<ActionResult<ApiResponse<Dashboard_CriticalHighResponse>>> Dashboard_CriticalHigh([FromBody] Dashboard_CriticalHighRequest request, CancellationToken ct) => Ok(ApiResponse<Dashboard_CriticalHighResponse>.Ok(await procedures.Dashboard_CriticalHighAsync(request, actors.Current, ct), HttpContext.TraceIdentifier));
 [HttpPost("dashboard/overdue-approval")]
 [Authorize(Policy="MANAGER")]
 public async Task<ActionResult<ApiResponse<Dashboard_OverdueApprovalResponse>>> Dashboard_OverdueApproval([FromBody] Dashboard_OverdueApprovalRequest request, CancellationToken ct) => Ok(ApiResponse<Dashboard_OverdueApprovalResponse>.Ok(await procedures.Dashboard_OverdueApprovalAsync(request, actors.Current, ct), HttpContext.TraceIdentifier));
 [HttpPost("dashboard/overdue-resolution")]
 [Authorize(Policy="MANAGER")]
 public async Task<ActionResult<ApiResponse<Dashboard_OverdueResolutionResponse>>> Dashboard_OverdueResolution([FromBody] Dashboard_OverdueResolutionRequest request, CancellationToken ct) => Ok(ApiResponse<Dashboard_OverdueResolutionResponse>.Ok(await procedures.Dashboard_OverdueResolutionAsync(request, actors.Current, ct), HttpContext.TraceIdentifier));
 [HttpPost("dashboard/sent-to-branch")]
 [Authorize(Policy="MANAGER")]
 public async Task<ActionResult<ApiResponse<Dashboard_SentToBranchResponse>>> Dashboard_SentToBranch([FromBody] Dashboard_SentToBranchRequest request, CancellationToken ct) => Ok(ApiResponse<Dashboard_SentToBranchResponse>.Ok(await procedures.Dashboard_SentToBranchAsync(request, actors.Current, ct), HttpContext.TraceIdentifier));
 [HttpPost("dashboard/sent-to-division")]
 [Authorize(Policy="MANAGER")]
 public async Task<ActionResult<ApiResponse<Dashboard_SentToDivisionResponse>>> Dashboard_SentToDivision([FromBody] Dashboard_SentToDivisionRequest request, CancellationToken ct) => Ok(ApiResponse<Dashboard_SentToDivisionResponse>.Ok(await procedures.Dashboard_SentToDivisionAsync(request, actors.Current, ct), HttpContext.TraceIdentifier));
 [HttpPost("dashboard/completed")]
 [Authorize(Policy="MANAGER")]
 public async Task<ActionResult<ApiResponse<Dashboard_CompletedResponse>>> Dashboard_Completed([FromBody] Dashboard_CompletedRequest request, CancellationToken ct) => Ok(ApiResponse<Dashboard_CompletedResponse>.Ok(await procedures.Dashboard_CompletedAsync(request, actors.Current, ct), HttpContext.TraceIdentifier));
 [HttpPost("dashboard/failed")]
 [Authorize(Policy="MANAGER")]
 public async Task<ActionResult<ApiResponse<Dashboard_FailedResponse>>> Dashboard_Failed([FromBody] Dashboard_FailedRequest request, CancellationToken ct) => Ok(ApiResponse<Dashboard_FailedResponse>.Ok(await procedures.Dashboard_FailedAsync(request, actors.Current, ct), HttpContext.TraceIdentifier));
 [HttpPost("dashboard/pending-follow-ups")]
 [Authorize(Policy="MANAGER")]
 public async Task<ActionResult<ApiResponse<Dashboard_PendingFollowUpsResponse>>> Dashboard_PendingFollowUps([FromBody] Dashboard_PendingFollowUpsRequest request, CancellationToken ct) => Ok(ApiResponse<Dashboard_PendingFollowUpsResponse>.Ok(await procedures.Dashboard_PendingFollowUpsAsync(request, actors.Current, ct), HttpContext.TraceIdentifier));
 [HttpPost("dashboard/overdue-follow-ups")]
 [Authorize(Policy="MANAGER")]
 public async Task<ActionResult<ApiResponse<Dashboard_OverdueFollowUpsResponse>>> Dashboard_OverdueFollowUps([FromBody] Dashboard_OverdueFollowUpsRequest request, CancellationToken ct) => Ok(ApiResponse<Dashboard_OverdueFollowUpsResponse>.Ok(await procedures.Dashboard_OverdueFollowUpsAsync(request, actors.Current, ct), HttpContext.TraceIdentifier));
 [HttpPost("dashboard/ticket-volume-by-service")]
 [Authorize(Policy="MANAGER")]
 public async Task<ActionResult<ApiResponse<Dashboard_TicketVolumeByServiceResponse>>> Dashboard_TicketVolumeByService([FromBody] Dashboard_TicketVolumeByServiceRequest request, CancellationToken ct) => Ok(ApiResponse<Dashboard_TicketVolumeByServiceResponse>.Ok(await procedures.Dashboard_TicketVolumeByServiceAsync(request, actors.Current, ct), HttpContext.TraceIdentifier));
 [HttpPost("dashboard/ticket-volume-by-date")]
 [Authorize(Policy="MANAGER")]
 public async Task<ActionResult<ApiResponse<Dashboard_TicketVolumeByDateResponse>>> Dashboard_TicketVolumeByDate([FromBody] Dashboard_TicketVolumeByDateRequest request, CancellationToken ct) => Ok(ApiResponse<Dashboard_TicketVolumeByDateResponse>.Ok(await procedures.Dashboard_TicketVolumeByDateAsync(request, actors.Current, ct), HttpContext.TraceIdentifier));
 [HttpPost("dashboard/agent-created-ticket-counts")]
 [Authorize(Policy="MANAGER")]
 public async Task<ActionResult<ApiResponse<Dashboard_AgentCreatedTicketCountsResponse>>> Dashboard_AgentCreatedTicketCounts([FromBody] Dashboard_AgentCreatedTicketCountsRequest request, CancellationToken ct) => Ok(ApiResponse<Dashboard_AgentCreatedTicketCountsResponse>.Ok(await procedures.Dashboard_AgentCreatedTicketCountsAsync(request, actors.Current, ct), HttpContext.TraceIdentifier));
 [HttpPost("dashboard/manager-activity-summary")]
 [Authorize(Policy="MANAGER")]
 public async Task<ActionResult<ApiResponse<Dashboard_ManagerActivitySummaryResponse>>> Dashboard_ManagerActivitySummary([FromBody] Dashboard_ManagerActivitySummaryRequest request, CancellationToken ct) => Ok(ApiResponse<Dashboard_ManagerActivitySummaryResponse>.Ok(await procedures.Dashboard_ManagerActivitySummaryAsync(request, actors.Current, ct), HttpContext.TraceIdentifier));
}
