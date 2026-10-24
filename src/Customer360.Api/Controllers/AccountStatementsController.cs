using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Customer360.Application;
using Customer360.Contracts;
using Customer360.Integrations;
using Customer360.Security;
namespace Customer360.Controllers;
[ApiController,Route("api/v1/account-statements"),Authorize(Policy="AGENT")]
public sealed class AccountStatementsController(IAccountStatementDelivery delivery,IntegrationAudit audit,ActorAccessor actors):ControllerBase
{
 [HttpPost("info")]
 public async Task<ActionResult<ApiResponse<AccountStatementInfo>>> Info(CustomerReference request,CancellationToken ct)
 {
  var info=await audit.ExecuteAsync("CBS","StatementInfo",actors.Current,()=>delivery.InfoAsync(request.Reference,ct),ct);
  if(info.AccountReference!=request.Reference)throw new ApiFault(503,"ACCOUNT_MISMATCH","Statement account verification failed");
  return Ok(ApiResponse<AccountStatementInfo>.Ok(info,HttpContext.TraceIdentifier));
 }
 [HttpPost("send")]
 public async Task<ActionResult<ApiResponse<AccountStatementReceipt>>> Send(StatementSendRequest request,CancellationToken ct)
 {
  if(request.IdempotencyKey==Guid.Empty||request.FromDate==default||request.ToDate==default||request.FromDate>request.ToDate||request.ToDate.DayNumber-request.FromDate.DayNumber>366)throw new ApiFault(400,"STATEMENT_REQUEST_INVALID","Choose a valid statement period of at most 366 days");
  var receipt=await audit.ExecuteAsync("CBS","StatementEmail",actors.Current,async()=>{
   var info=await delivery.InfoAsync(request.Reference,ct);
   if(info.AccountReference!=request.Reference)throw new ApiFault(503,"ACCOUNT_MISMATCH","Statement account verification failed");
   if(!info.CanSend)throw new ApiFault(503,"STATEMENT_DELIVERY_UNCONFIGURED","Statement PDF and attachment email delivery are not configured. No email was sent.");
   var result=await delivery.SendAsync(request.Reference,request.FromDate,request.ToDate,request.IdempotencyKey,ct);
   if(result.AccountReference!=request.Reference||result.Status is not("ACCEPTED" or "DELIVERED" or "SIMULATED")||(result.Status=="SIMULATED"&&!info.IsSimulation))throw new ApiFault(503,"STATEMENT_OUTCOME_UNKNOWN","Statement outcome is unconfirmed");
   return result;
  },ct);
  if(receipt.AccountReference!=request.Reference||receipt.Status is not("ACCEPTED" or "DELIVERED" or "SIMULATED"))throw new ApiFault(503,"STATEMENT_OUTCOME_UNKNOWN","Statement delivery outcome is unconfirmed. Do not submit another request until checked.");
  return Ok(ApiResponse<AccountStatementReceipt>.Ok(receipt,HttpContext.TraceIdentifier));
 }
}
public sealed record StatementSendRequest([Required,MaxLength(200)]string Reference,DateOnly FromDate,DateOnly ToDate,Guid IdempotencyKey);
