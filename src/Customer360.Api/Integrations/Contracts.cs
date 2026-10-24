using Customer360.Contracts;
using Customer360.Security;
using System.ComponentModel.DataAnnotations;
namespace Customer360.Integrations;
// Internal application contracts, not claims about any external provider wire format.
public sealed record ManagerCredentials([Required,MaxLength(256)]string Username,[Required,MaxLength(1024)]string Password);
public sealed record AvayaAssertion([Required,MaxLength(8192)]string Assertion);
public interface IAvayaIntegration {Task<Actor> ValidateAsync(AvayaAssertion assertion,CancellationToken ct);}
public interface IManagerAuthentication {Task<Actor> AuthenticateAsync(ManagerCredentials credentials,CancellationToken ct);}
public sealed record CustomerProfile(string Reference,string Name,string Mobile,string? Cif)
{
 public string? FatherName {get;init;}
 public string? MotherName {get;init;}
 public string? DateOfBirth {get;init;}
 public string? Gender {get;init;}
 public string? MaskedEmail {get;init;}
 public string? PresentAddress {get;init;}
 public string? PermanentAddress {get;init;}
 public string? NidLastFour {get;init;}
 public bool IsSynthetic {get;init;}
 public IReadOnlyList<string>? Cifs {get;init;}
}
public sealed record AccountSummary(string Reference,string MaskedAccount,string Product,string Currency)
{
 public string? Cif {get;init;}
 public string? AccountName {get;init;}
 public string? Branch {get;init;}
 public string? OpeningDate {get;init;}
 public string? AccountType {get;init;}
 public string? Status {get;init;}
 public bool? FreezeStatus {get;init;}
}
public sealed record AccountDetails(string Reference,string MaskedAccount,string Status,decimal? AvailableBalance,string Currency)
{
 public string? Cif {get;init;}
 public string? Product {get;init;}
 public string? Branch {get;init;}
 public string? AccountType {get;init;}
 public string? OpeningDate {get;init;}
 public bool? FreezeStatus {get;init;}
 public decimal? HoldAmount {get;init;}
 public decimal? LedgerBalance {get;init;}
 public decimal? InterestRate {get;init;}
 public string? ChequeBookStatus {get;init;}
 public int? ChequeBooksIssued {get;init;}
 public string? LastChequeBookIssueDate {get;init;}
 public string? ProductCode {get;init;}
 public decimal? MinimumBalance {get;init;}
 public decimal? AverageBalance {get;init;}
 public string? LastTransactionDate {get;init;}
 public string? HoldReason {get;init;}
 public bool? JointAccount {get;init;}
 public bool? NomineeAvailable {get;init;}
 public string? LinkedAccount {get;init;}
 public string? StandingInstruction {get;init;}
 public string? Remarks {get;init;}
 public string? LastUpdated {get;init;}
 public string? MaskedTin {get;init;}
 public IReadOnlyList<AccountSignatory>? Signatories {get;init;}
 public bool IsSynthetic {get;init;}
}
public sealed record AccountSignatory(string Reference,string Name,string DateOfBirth,string Mobile);
public sealed record AccountTransaction(string Reference,DateTime OccurredAt,string Description,string Type,decimal Amount,decimal Balance){public string? Status {get;init;}};
public sealed record AccountTransactionHistory(string AccountReference,string Currency,decimal? CurrentBalance,IReadOnlyList<AccountTransaction> Transactions,bool IsSynthetic);
// Recipient resolution, PDF creation and delivery belong to the trusted server adapter.
// No recipient address or attachment supplied by the browser is accepted.
public sealed record AccountStatementInfo(string AccountReference,string MaskedEmail,bool CanSend,string? UnavailableReason,bool IsSimulation=false);
public sealed record AccountStatementReceipt(string AccountReference,string Status,string? ProviderReference);
public interface IAccountStatementDelivery
{
 Task<AccountStatementInfo> InfoAsync(string accountReference,CancellationToken ct);
 Task<AccountStatementReceipt> SendAsync(string accountReference,DateOnly fromDate,DateOnly toDate,Guid idempotencyKey,CancellationToken ct);
}
public sealed record CardSummary(string Reference,string LastFour,string Status)
{
 public string? ClientId {get;init;}
 public string? CardType {get;init;}
 public string? Product {get;init;}
 public string? Currency {get;init;}
 public decimal? Outstanding {get;init;}
 public decimal? CreditLimit {get;init;}
}
public sealed record CardTransaction(DateTime OccurredAt,decimal Amount,string Currency,string Description);
public sealed record CardDetails(CardSummary Card,CustomerProfile Cardholder,string ExpiryDate,decimal? AvailableLimit,string IssueDate,string IssuingBranch,string ProductCode,string? BillingCycle,string? StatementDate,decimal? MinimumPaymentDue,string? PaymentDueDate,string? LastPaymentDate,string MaskedPassport,string MaskedLinkedAccount,IReadOnlyList<AccountTransaction> RecentTransactions,bool IsSynthetic);
public sealed record InternetBankingProfile(string CustomerReference,string MaskedUserId,string Status);
public sealed record IbLink(string Reference,string Label,string Relationship);
public sealed record IbActivity(string Reference,DateTime OccurredAt,string ActivityType,string Description,decimal? Amount,string Status,string IpAddress,string Device);
public sealed record IbDetails(string CustomerReference,string UserId,string Status,CustomerProfile Customer,string CreatedAt,string LastLoginAt,string LastLoginChannel,bool ForcePasswordChange,int FailedLoginAttempts,int RegisteredDevices,string LoginMethod,string MaskedPassport,IReadOnlyList<IbLink> Cifs,IReadOnlyList<IbLink> CardClients,IReadOnlyList<IbActivity> Transactions,bool IsSynthetic);
public sealed record IbLogPage(string CustomerReference,int Page,int PageSize,int TotalCount,IReadOnlyList<IbActivity> Rows);
public sealed record IbActionReceipt(string CustomerReference,string Action,string Status,string Reference);
public interface ICbsMiddleware
{
 Task<IReadOnlyList<CustomerProfile>> SearchAsync(string mobile,CancellationToken ct);
 Task<CustomerProfile> ProfileAsync(string reference,CancellationToken ct);
 Task<IReadOnlyList<AccountSummary>> AccountsAsync(string reference,CancellationToken ct);
 Task<AccountDetails> AccountAsync(string reference,CancellationToken ct);
 Task<AccountTransactionHistory> AccountTransactionsAsync(string reference,DateOnly fromDate,DateOnly toDate,CancellationToken ct);
}
public interface ICardMiddleware
{
 Task<IReadOnlyList<CardSummary>> CardsAsync(string reference,CancellationToken ct);
 Task<CardDetails> CardAsync(string reference,CancellationToken ct);
 Task<IReadOnlyList<CardTransaction>> TransactionsAsync(string cardReference,CancellationToken ct);
}
public interface IInternetBankingMiddleware {
 Task<InternetBankingProfile> ProfileAsync(string reference,CancellationToken ct);
 Task<IbDetails> DetailsAsync(string reference,CancellationToken ct);
 Task<IbLogPage> LogsAsync(string reference,DateOnly from,DateOnly to,string activity,int page,int pageSize,CancellationToken ct);
 Task<IbActionReceipt> ActionAsync(string reference,string action,string? reason,string? remarks,Guid idempotencyKey,CancellationToken ct);
}
public sealed record DeliveryEnvelope(byte[] Ciphertext,string KeyReference,DateTime ExpiresAt);
public sealed record DeliverySecret(string Token,string CustomerReference);
public interface IDeliveryProtector
{
 Task<DeliveryEnvelope> ProtectAsync(DeliverySecret secret,DateTime expiry,CancellationToken ct);
 Task<DeliverySecret> UnprotectAsync(DeliveryEnvelope envelope,CancellationToken ct);
}
public sealed record ProviderMessage(string IdempotencyKey,string Channel,string Recipient,string? Cc,string? Subject,string Body);
public enum ProviderOutcome {Accepted,Delivered,RetryableFailure,PermanentFailure,Unknown}
public sealed record ProviderReceipt(ProviderOutcome Outcome,string? MessageId,string? ErrorCode);
public interface ISmsProvider {Task<ProviderReceipt> SendAsync(ProviderMessage message,CancellationToken ct);Task<ProviderReceipt> ReconcileAsync(string requestReference,CancellationToken ct);}
public interface IEmailProvider {Task<ProviderReceipt> SendAsync(ProviderMessage message,CancellationToken ct);Task<ProviderReceipt> ReconcileAsync(string requestReference,CancellationToken ct);}
public sealed class UnconfiguredIntegrations : IAccountStatementDelivery,IAvayaIntegration,IManagerAuthentication,ICbsMiddleware,ICardMiddleware,IInternetBankingMiddleware,IDeliveryProtector,ISmsProvider,IEmailProvider
{
 public Task<AccountStatementInfo> InfoAsync(string r,CancellationToken ct)=>throw Missing();
 public Task<AccountStatementReceipt> SendAsync(string r,DateOnly fromDate,DateOnly toDate,Guid idempotencyKey,CancellationToken ct)=>throw Missing(); static ApiFault Missing()=>new(503,"INTEGRATION_UNCONFIGURED","Trusted integration is not configured");
 public Task<Actor> ValidateAsync(AvayaAssertion a,CancellationToken ct)=>throw Missing();
 public Task<Actor> AuthenticateAsync(ManagerCredentials c,CancellationToken ct)=>throw Missing();
 public Task<IReadOnlyList<CustomerProfile>> SearchAsync(string m,CancellationToken ct)=>throw Missing();
 public Task<CustomerProfile> ProfileAsync(string r,CancellationToken ct)=>throw Missing();
 Task<InternetBankingProfile> IInternetBankingMiddleware.ProfileAsync(string r,CancellationToken ct)=>throw Missing();
 public Task<IbDetails> DetailsAsync(string r,CancellationToken ct)=>throw Missing();
 public Task<IbLogPage> LogsAsync(string r,DateOnly from,DateOnly to,string activity,int page,int pageSize,CancellationToken ct)=>throw Missing();
 public Task<IbActionReceipt> ActionAsync(string r,string action,string? reason,string? remarks,Guid key,CancellationToken ct)=>throw Missing();
 public Task<IReadOnlyList<AccountSummary>> AccountsAsync(string r,CancellationToken ct)=>throw Missing();
 public Task<AccountTransactionHistory> AccountTransactionsAsync(string r,DateOnly fromDate,DateOnly toDate,CancellationToken ct)=>throw Missing();
 public Task<AccountDetails> AccountAsync(string r,CancellationToken ct)=>throw Missing();
 public Task<IReadOnlyList<CardSummary>> CardsAsync(string r,CancellationToken ct)=>throw Missing();
 public Task<CardDetails> CardAsync(string r,CancellationToken ct)=>throw Missing();
 public Task<IReadOnlyList<CardTransaction>> TransactionsAsync(string r,CancellationToken ct)=>throw Missing();
 public Task<DeliveryEnvelope> ProtectAsync(DeliverySecret s,DateTime expiry,CancellationToken ct)=>throw Missing();
 public Task<DeliverySecret> UnprotectAsync(DeliveryEnvelope e,CancellationToken ct)=>throw Missing();
 public Task<ProviderReceipt> SendAsync(ProviderMessage m,CancellationToken ct)=>throw Missing();
 public Task<ProviderReceipt> ReconcileAsync(string r,CancellationToken ct)=>throw Missing();
}
