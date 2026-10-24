#nullable enable
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Customer360.Application;
using Customer360.Contracts;
using Customer360.Security;
namespace Customer360.Controllers;
[ApiController]
[Route("api/v1/communications")]
public sealed class CommunicationsController(Procedures procedures, ActorAccessor actors) : ControllerBase
{
 [HttpPost("notification-template/create-draft")]
 [Authorize(Policy="CONFIG")]
 public async Task<ActionResult<ApiResponse<NotificationTemplate_CreateDraftResponse>>> NotificationTemplate_CreateDraft([FromBody] NotificationTemplate_CreateDraftRequest request, CancellationToken ct) => Ok(ApiResponse<NotificationTemplate_CreateDraftResponse>.Ok(await procedures.NotificationTemplate_CreateDraftAsync(request, actors.Current, ct), HttpContext.TraceIdentifier));
 [HttpPost("notification-template/update-draft")]
 [Authorize(Policy="CONFIG")]
 public async Task<ActionResult<ApiResponse<NotificationTemplate_UpdateDraftResponse>>> NotificationTemplate_UpdateDraft([FromBody] NotificationTemplate_UpdateDraftRequest request, CancellationToken ct) => Ok(ApiResponse<NotificationTemplate_UpdateDraftResponse>.Ok(await procedures.NotificationTemplate_UpdateDraftAsync(request, actors.Current, ct), HttpContext.TraceIdentifier));
 [HttpPost("notification-template/publish")]
 [Authorize(Policy="CONFIG")]
 public async Task<ActionResult<ApiResponse<NotificationTemplate_PublishResponse>>> NotificationTemplate_Publish([FromBody] NotificationTemplate_PublishRequest request, CancellationToken ct) => Ok(ApiResponse<NotificationTemplate_PublishResponse>.Ok(await procedures.NotificationTemplate_PublishAsync(request, actors.Current, ct), HttpContext.TraceIdentifier));
 [HttpPost("notification-template/get-published")]
 [Authorize(Policy="READ")]
 public async Task<ActionResult<ApiResponse<NotificationTemplate_GetPublishedResponse>>> NotificationTemplate_GetPublished([FromBody] NotificationTemplate_GetPublishedRequest request, CancellationToken ct) => Ok(ApiResponse<NotificationTemplate_GetPublishedResponse>.Ok(await procedures.NotificationTemplate_GetPublishedAsync(request, actors.Current, ct), HttpContext.TraceIdentifier));
 [HttpPost("communication/get-by-ticket")]
 [Authorize(Policy="MANAGER")]
 public async Task<ActionResult<ApiResponse<Communication_GetByTicketResponse>>> Communication_GetByTicket([FromBody] Communication_GetByTicketRequest request, CancellationToken ct) => Ok(ApiResponse<Communication_GetByTicketResponse>.Ok(await procedures.Communication_GetByTicketAsync(request, actors.Current, ct), HttpContext.TraceIdentifier));
 [HttpPost("communication/get-by-id")]
 [Authorize(Policy="MANAGER")]
 public async Task<ActionResult<ApiResponse<Communication_GetByIDResponse>>> Communication_GetByID([FromBody] Communication_GetByIDRequest request, CancellationToken ct) => Ok(ApiResponse<Communication_GetByIDResponse>.Ok(await procedures.Communication_GetByIDAsync(request, actors.Current, ct), HttpContext.TraceIdentifier));
 [HttpPost("communication-outbox/manual-retry")]
 [Authorize(Policy="MANAGER")]
 public async Task<ActionResult<ApiResponse<CommunicationOutbox_ManualRetryResponse>>> CommunicationOutbox_ManualRetry([FromBody] CommunicationOutbox_ManualRetryRequest request, CancellationToken ct) => Ok(ApiResponse<CommunicationOutbox_ManualRetryResponse>.Ok(await procedures.CommunicationOutbox_ManualRetryAsync(request, actors.Current, ct), HttpContext.TraceIdentifier));
 [HttpPost("communication-outbox/set-max-attempts")]
 [Authorize(Policy="CONFIG")]
 public async Task<ActionResult<ApiResponse<CommunicationOutbox_SetMaxAttemptsResponse>>> CommunicationOutbox_SetMaxAttempts([FromBody] CommunicationOutbox_SetMaxAttemptsRequest request, CancellationToken ct) => Ok(ApiResponse<CommunicationOutbox_SetMaxAttemptsResponse>.Ok(await procedures.CommunicationOutbox_SetMaxAttemptsAsync(request, actors.Current, ct), HttpContext.TraceIdentifier));
}
