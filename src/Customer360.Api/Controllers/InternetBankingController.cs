using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Customer360.Application;
using Customer360.Contracts;
using Customer360.Integrations;
using Customer360.Security;
namespace Customer360.Controllers;
[ApiController,Route("api/v1/internet-banking"),Authorize(Policy="READ")]
public sealed class InternetBankingController(IInternetBankingMiddleware banking,IntegrationAudit audit,ActorAccessor actors):ControllerBase
{
 [HttpPost("details")]
 public async Task<ActionResult<ApiResponse<IbDetails>>> Details(CustomerReference request,CancellationToken ct)=>Ok(ApiResponse<IbDetails>.Ok(await audit.ExecuteAsync("IB","Details",actors.Current,()=>banking.DetailsAsync(request.Reference,ct),ct),HttpContext.TraceIdentifier));
 [HttpPost("logs")]
 public async Task<ActionResult<ApiResponse<IbLogPage>>> Logs(IbLogRequest request,CancellationToken ct)=>Ok(ApiResponse<IbLogPage>.Ok(await audit.ExecuteAsync("IB","UserLog",actors.Current,()=>banking.LogsAsync(request.Reference,request.FromDate,request.ToDate,request.ActivityType,request.Page,request.PageSize,ct),ct),HttpContext.TraceIdentifier));
 [HttpPost("action"),Authorize(Policy="AGENT")]
 public async Task<ActionResult<ApiResponse<IbActionReceipt>>> Action(IbActionRequest request,CancellationToken ct)=>Ok(ApiResponse<IbActionReceipt>.Ok(await audit.ExecuteAsync("IB","UserAction",actors.Current,()=>banking.ActionAsync(request.Reference,request.Action,request.Reason,request.Remarks,request.IdempotencyKey,ct),ct),HttpContext.TraceIdentifier));
}
public sealed record IbLogRequest([Required,MaxLength(200)]string Reference,DateOnly FromDate,DateOnly ToDate,[Required] string ActivityType,int Page=1,int PageSize=10);
public sealed record IbActionRequest([Required,MaxLength(200)]string Reference,[Required]string Action,[MaxLength(100)]string? Reason,[MaxLength(500)]string? Remarks,Guid IdempotencyKey);
