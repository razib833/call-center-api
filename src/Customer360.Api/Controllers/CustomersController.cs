using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Customer360.Contracts;
using Customer360.Integrations;
using Customer360.Application;
using Customer360.Security;
namespace Customer360.Controllers;
[ApiController,Route("api/v1/customers"),Authorize(Policy="READ")]
public sealed class CustomersController(ICbsMiddleware cbs,ICardMiddleware cards,IInternetBankingMiddleware banking,IntegrationAudit audit,ActorAccessor actors) : ControllerBase
{
 ActionResult<ApiResponse<T>> Respond<T>(T value)=>Ok(ApiResponse<T>.Ok(value,HttpContext.TraceIdentifier));
 [HttpPost("search")] public async Task<ActionResult<ApiResponse<IReadOnlyList<CustomerProfile>>>> Search(CustomerSearch request,CancellationToken ct)=>Respond(await audit.ExecuteAsync("CBS","Search",actors.Current,()=>cbs.SearchAsync(request.Mobile,ct),ct));
 [HttpPost("profile")] public async Task<ActionResult<ApiResponse<CustomerProfile>>> Profile(CustomerReference request,CancellationToken ct)=>Respond(await audit.ExecuteAsync("CBS","Profile",actors.Current,()=>cbs.ProfileAsync(request.Reference,ct),ct));
 [HttpPost("accounts")] public async Task<ActionResult<ApiResponse<IReadOnlyList<AccountSummary>>>> Accounts(CustomerReference request,CancellationToken ct)=>Respond(await audit.ExecuteAsync("CBS","Accounts",actors.Current,()=>cbs.AccountsAsync(request.Reference,ct),ct));
 [HttpPost("account-details")] public async Task<ActionResult<ApiResponse<AccountDetails>>> Account(CustomerReference request,CancellationToken ct)=>Respond(await audit.ExecuteAsync("CBS","Account",actors.Current,()=>cbs.AccountAsync(request.Reference,ct),ct));
 [HttpPost("cards")] public async Task<ActionResult<ApiResponse<IReadOnlyList<CardSummary>>>> Cards(CustomerReference request,CancellationToken ct)=>Respond(await audit.ExecuteAsync("CARD","Cards",actors.Current,()=>cards.CardsAsync(request.Reference,ct),ct));
 [HttpPost("account-transactions")] public async Task<ActionResult<ApiResponse<AccountTransactionHistory>>> AccountTransactions(AccountTransactionRequest request,CancellationToken ct)
 {
  if(request.FromDate==default||request.ToDate==default||request.FromDate>request.ToDate||request.ToDate.DayNumber-request.FromDate.DayNumber>366)throw new ApiFault(400,"DATE_RANGE","Choose a valid date range of at most 366 days");
  return Respond(await audit.ExecuteAsync("CBS","AccountTransactions",actors.Current,()=>cbs.AccountTransactionsAsync(request.Reference,request.FromDate,request.ToDate,ct),ct));
 }
 [HttpPost("card-transactions")] public async Task<ActionResult<ApiResponse<IReadOnlyList<CardTransaction>>>> Transactions(CustomerReference request,CancellationToken ct)=>Respond(await audit.ExecuteAsync("CARD","Transactions",actors.Current,()=>cards.TransactionsAsync(request.Reference,ct),ct));
 [HttpPost("card-details")] public async Task<ActionResult<ApiResponse<CardDetails>>> CardDetails(CustomerReference request,CancellationToken ct)=>Respond(await audit.ExecuteAsync("CARD","Details",actors.Current,()=>cards.CardAsync(request.Reference,ct),ct));
 [HttpPost("internet-banking")] public async Task<ActionResult<ApiResponse<InternetBankingProfile>>> Banking(CustomerReference request,CancellationToken ct)=>Respond(await audit.ExecuteAsync("IB","Profile",actors.Current,()=>banking.ProfileAsync(request.Reference,ct),ct));
}
public sealed record CustomerSearch([Required,RegularExpression(@"^\+?[0-9]{8,20}$")]string Mobile);
public sealed record CustomerReference([Required,MaxLength(200)]string Reference);
public sealed record AccountTransactionRequest([Required,MaxLength(200)]string Reference,DateOnly FromDate,DateOnly ToDate);



