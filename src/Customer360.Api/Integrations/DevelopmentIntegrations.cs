using System.Text.Json;
using Microsoft.AspNetCore.DataProtection;
using Customer360.Contracts;
using Customer360.Security;
namespace Customer360.Integrations;
public sealed class DevelopmentIntegrations(IDataProtectionProvider protection) : IAccountStatementDelivery,ICbsMiddleware,ICardMiddleware,IInternetBankingMiddleware,IDeliveryProtector,ISmsProvider,IEmailProvider
{
 readonly IDataProtector protector=protection.CreateProtector("Customer360.DevelopmentDelivery.v1");
 static readonly CustomerProfile DemoCustomer=new("SYNTHETIC-CUSTOMER","Arif Hasan (Demo)","01712345678","TEST-CIF")
 {
  FatherName="Mahmud Hasan (Demo)",MotherName="Nasima Hasan (Demo)",DateOfBirth="1991-06-30",Gender="Male",
  MaskedEmail="a***@example.invalid",NidLastFour="0000",IsSynthetic=true,Cifs=["TEST-CIF","TEST-CIF-2"],
  PresentAddress="House DEMO-7, Fictional Avenue, Dhaka — demonstration only",
  PermanentAddress="Village Demo, Fictional District — demonstration only"
 };
 public Task<IReadOnlyList<CustomerProfile>> SearchAsync(string m,CancellationToken ct)=>Task.FromResult<IReadOnlyList<CustomerProfile>>(m==DemoCustomer.Mobile?[DemoCustomer]:[]);
 public Task<CustomerProfile> ProfileAsync(string r,CancellationToken ct)=>r==DemoCustomer.Reference?Task.FromResult(DemoCustomer):throw new ApiFault(404,"NOT_FOUND","Synthetic customer not found");
 public Task<IReadOnlyList<AccountSummary>> AccountsAsync(string r,CancellationToken ct)=>r!=DemoCustomer.Reference?throw new ApiFault(404,"NOT_FOUND","Synthetic customer not found"):Task.FromResult<IReadOnlyList<AccountSummary>>([
  new("SYNTHETIC-ACCOUNT","******0001","Savings Plus (Demo)","BDT"){Cif="TEST-CIF",AccountName=DemoCustomer.Name,Branch="Demo Gulshan",OpeningDate="2018-01-15",AccountType="Savings",Status="ACTIVE",FreezeStatus=false},
  new("SYNTHETIC-ACCOUNT-2","******0002","Current Plus (Demo)","BDT"){Cif="TEST-CIF",AccountName=DemoCustomer.Name,Branch="Demo Motijheel",OpeningDate="2019-03-10",AccountType="Current",Status="ACTIVE",FreezeStatus=true},
  new("SYNTHETIC-ACCOUNT-3","******0003","DPS Regular (Demo)","BDT"){Cif="TEST-CIF",AccountName=DemoCustomer.Name,Branch="Demo Uttara",OpeningDate="2020-07-05",AccountType="DPS",Status="ACTIVE",FreezeStatus=false},
  new("SYNTHETIC-ACCOUNT-4","******0004","FDR Special (Demo)","BDT"){Cif="TEST-CIF",AccountName=DemoCustomer.Name,Branch="Demo Banani",OpeningDate="2021-12-12",AccountType="FDR",Status="ACTIVE",FreezeStatus=false},
  new("SYNTHETIC-ACCOUNT-5","******0005","Personal Loan (Demo)","BDT"){Cif="TEST-CIF-2",AccountName=DemoCustomer.Name,Branch="Demo Mirpur",OpeningDate="2023-02-20",AccountType="Loan",Status="ACTIVE",FreezeStatus=false}
 ]);
 public async Task<AccountDetails> AccountAsync(string r,CancellationToken ct)
 {
  var accounts=await AccountsAsync(DemoCustomer.Reference,ct);
  var account=accounts.SingleOrDefault(a=>a.Reference==r)??throw new ApiFault(404,"NOT_FOUND","Synthetic account not found");
  return new(r,account.MaskedAccount,account.Status!,125432.50m,account.Currency)
  {
   Cif=account.Cif,Product=account.Product,Branch=account.Branch,AccountType=account.AccountType,OpeningDate=account.OpeningDate,FreezeStatus=account.FreezeStatus,
   HoldAmount=5000m,LedgerBalance=130432.50m,InterestRate=3.5m,ChequeBookStatus="ACTIVE",ChequeBooksIssued=3,LastChequeBookIssueDate="2025-01-12",
   ProductCode="DEMO-"+account.AccountType,MinimumBalance=500m,AverageBalance=75432.50m,LastTransactionDate="2026-10-07",HoldReason="Demo clearing hold",JointAccount=false,NomineeAvailable=true,
   LinkedAccount="Demo linked account ******0099",StandingInstruction="Demo monthly savings transfer",Remarks="Fictional development account; no real banking balance",LastUpdated="2026-10-18T00:00:00Z",MaskedTin="******0000",IsSynthetic=true,
   Signatories=[new("DEMO-SIGNATORY-1",DemoCustomer.Name,"1991-06-30",DemoCustomer.Mobile),new("DEMO-SIGNATORY-2","Farzana Hasan (Demo)","1988-02-14","01812345678"),new("DEMO-SIGNATORY-3","Tahmid Hasan (Demo)","1992-11-22","01912345678")]
  };
 }
 public async Task<AccountStatementInfo> InfoAsync(string r,CancellationToken ct)
 {
  await AccountAsync(r,ct);
  return new(r,DemoCustomer.MaskedEmail!,true,null,true);
 }
 public async Task<AccountStatementReceipt> SendAsync(string r,DateOnly fromDate,DateOnly toDate,Guid idempotencyKey,CancellationToken ct)
 {
  if(idempotencyKey==Guid.Empty||fromDate==default||toDate==default||fromDate>toDate||toDate.DayNumber-fromDate.DayNumber>366)throw new ApiFault(400,"STATEMENT_REQUEST_INVALID","Choose a valid statement period of at most 366 days");
  await AccountAsync(r,ct);ct.ThrowIfCancellationRequested();
  return new(r,"SIMULATED","MOCK-"+idempotencyKey.ToString("N"));
 }
 public Task<IReadOnlyList<CardSummary>> CardsAsync(string r,CancellationToken ct)=>r!=DemoCustomer.Reference?throw new ApiFault(404,"NOT_FOUND","Synthetic customer not found"):Task.FromResult<IReadOnlyList<CardSummary>>([
  new("SYNTHETIC-CARD","4582","ACTIVE"){ClientId="DEMO-CARD-CLIENT-1",CardType="DEBIT",Product="Visa Classic Debit (Demo)",Currency="BDT"},
  new("SYNTHETIC-CARD-2","9021","ACTIVE"){ClientId="DEMO-CARD-CLIENT-1",CardType="CREDIT",Product="Visa Gold Credit (Demo)",Currency="BDT",Outstanding=25430.50m,CreditLimit=200000m},
  new("SYNTHETIC-CARD-3","6745","BLOCKED"){ClientId="DEMO-CARD-CLIENT-1",CardType="DEBIT",Product="Mastercard Platinum (Demo)",Currency="BDT"},
  new("SYNTHETIC-CARD-4","9987","ACTIVE"){ClientId="DEMO-CARD-CLIENT-1",CardType="CREDIT",Product="Mastercard Standard (Demo)",Currency="BDT",Outstanding=5200m,CreditLimit=100000m},
  new("SYNTHETIC-CARD-5","1122","ACTIVE"){ClientId="DEMO-CARD-CLIENT-2",CardType="DEBIT",Product="Visa Classic Debit (Demo)",Currency="BDT"},
  new("SYNTHETIC-CARD-6","3344","ACTIVE"){ClientId="DEMO-CARD-CLIENT-2",CardType="CREDIT",Product="Visa Signature (Demo)",Currency="BDT",Outstanding=12890m,CreditLimit=300000m}
 ]);
 public async Task<AccountTransactionHistory> AccountTransactionsAsync(string r,DateOnly fromDate,DateOnly toDate,CancellationToken ct)
 {
  if(fromDate>toDate||toDate.DayNumber-fromDate.DayNumber>366)throw new ApiFault(400,"DATE_RANGE","Choose a valid date range of at most 366 days");
  var account=await AccountAsync(r,ct);
  string[] descriptions=["Demo interest credit","Demo mobile banking transfer","Demo utility bill payment","Demo POS purchase","Demo inward transfer","Demo ATM withdrawal","Demo salary credit","Demo bank transfer"];
  decimal[] amounts=[5000m,4000m,1000m,2500m,20000m,10000m,75000m,5000m];
  var balance=47932.50m;var rows=new List<AccountTransaction>();
  for(var i=0;i<amounts.Length;i++){
   ct.ThrowIfCancellationRequested();var credit=i is 0 or 4 or 6;balance+=credit?amounts[i]:-amounts[i];
   var occurred=new DateTime(2026,10,i+2,9+i,12,5,DateTimeKind.Utc);
   if(DateOnly.FromDateTime(occurred)>=fromDate&&DateOnly.FromDateTime(occurred)<=toDate)rows.Add(new(r+"-TX-"+(i+1),occurred,descriptions[i],credit?"CREDIT":"DEBIT",amounts[i],balance));
  }
  return new(r,account.Currency,account.AvailableBalance,rows.OrderByDescending(t=>t.OccurredAt).ToArray(),true);
 }
 public Task<IReadOnlyList<CardTransaction>> TransactionsAsync(string r,CancellationToken ct)=>Task.FromResult<IReadOnlyList<CardTransaction>>([new(DateTime.UtcNow.Date,10m,"BDT","Synthetic transaction")]);
 public async Task<CardDetails> CardAsync(string r,CancellationToken ct)
 {
  var cards=await CardsAsync(DemoCustomer.Reference,ct);var card=cards.SingleOrDefault(c=>c.Reference==r)??throw new ApiFault(404,"NOT_FOUND","Synthetic card not found");
  var credit=card.CardType=="CREDIT";decimal[] amounts=[2250,1800,5000,3150,10000,8000,4200,2500];
  string[] descriptions=["Demo POS purchase","Demo online subscription","Demo fuel purchase","Demo restaurant purchase","Demo payment received","Demo cash withdrawal","Demo e-commerce purchase","Demo POS purchase"];
  var current=credit?card.Outstanding!.Value:125432.50m;var direction=credit?1m:-1m;var balance=current-16900m*direction;var rows=new List<AccountTransaction>();
  for(var i=0;i<amounts.Length;i++){ct.ThrowIfCancellationRequested();var type=i==4?"CREDIT":"DEBIT";balance+=(type=="DEBIT"?amounts[i]:-amounts[i])*direction;rows.Add(new(r+"-TX-"+(i+1),new DateTime(2026,10,i+1,10,42,0,DateTimeKind.Utc),descriptions[i],type,amounts[i],balance){Status="SUCCESSFUL"});}
  return new(card,DemoCustomer,"12/28",credit?card.CreditLimit-card.Outstanding:null,"2023-01-15","Demo Gulshan Branch","DEMO-"+card.CardType,credit?"1st of every month":null,credit?"2026-10-02":null,credit?5000m:null,credit?"2026-10-20":null,credit?"2026-09-28":null,"DEMO-****5678","******0001",rows.AsEnumerable().Reverse().ToArray(),true);
 }
 Task<InternetBankingProfile> IInternetBankingMiddleware.ProfileAsync(string r,CancellationToken ct)=>r!=DemoCustomer.Reference?throw new ApiFault(404,"NOT_FOUND","Synthetic customer not found"):Task.FromResult(new InternetBankingProfile(r,"demo.ibuser12345","ACTIVE"));
 static IReadOnlyList<IbActivity> IbRows(string r)=>Enumerable.Range(0,35).Select(i=>{
  string[] types=["LOGIN","VIEW_ACCOUNT","BILL_PAYMENT","FUND_TRANSFER","SESSION","CARD_PAYMENT"];
  var type=types[i%types.Length];var financial=type is "BILL_PAYMENT" or "FUND_TRANSFER" or "CARD_PAYMENT";
  return new IbActivity(r+"-IB-"+(i+1),new DateTime(2026,10,18,16,0,0,DateTimeKind.Utc).AddHours(-i*10),type,type=="SESSION"?"Demo failed login":"Demo "+type.Replace('_',' ')+(type=="FUND_TRANSFER"?" ••••0001 → ••••0002":""),financial?500m+i*100:null,type=="SESSION"?"FAILED":"SUCCESS","192.0.2."+(10+i%3),i%2==0?"Mobile App (Demo)":"Web Browser (Demo)");
 }).ToArray();
 public async Task<IbDetails> DetailsAsync(string r,CancellationToken ct){await ProfileAsync(r,ct);return new(r,"demo.ibuser12345","ACTIVE",DemoCustomer,"2023-01-15","2026-10-18T16:00:00Z","Mobile App",false,0,3,"User ID + Password + OTP","DEMO-****5678",[new("TEST-CIF",DemoCustomer.Name,"Primary"),new("TEST-CIF-2",DemoCustomer.Name,"Joint")],[new("DEMO-CARD-CLIENT-1","Debit / Credit","Linked")],IbRows(r).Take(10).ToArray(),true);}
 public async Task<IbLogPage> LogsAsync(string r,DateOnly from,DateOnly to,string activity,int page,int pageSize,CancellationToken ct){
  await ProfileAsync(r,ct);
  if(from==default||to==default||from>to||to.DayNumber-from.DayNumber>366||page<1||pageSize is <1 or >50||!new[]{"ALL","LOGIN","VIEW_ACCOUNT","BILL_PAYMENT","FUND_TRANSFER","SESSION","CARD_PAYMENT"}.Contains(activity))throw new ApiFault(400,"IB_FILTER_INVALID","Invalid activity log filter");
  var rows=IbRows(r).Where(a=>DateOnly.FromDateTime(a.OccurredAt)>=from&&DateOnly.FromDateTime(a.OccurredAt)<=to&&(activity=="ALL"||a.ActivityType==activity)).ToArray();
  return new(r,page,pageSize,rows.Length,rows.Skip((int)Math.Min((long)(page-1)*pageSize,int.MaxValue)).Take(pageSize).ToArray());
 }
 public async Task<IbActionReceipt> ActionAsync(string r,string action,string? reason,string? remarks,Guid idempotencyKey,CancellationToken ct){
  await ProfileAsync(r,ct);ct.ThrowIfCancellationRequested();
  string[] reasons=["Lost/Stolen Device","Customer Request","Suspected Fraudulent Activity","Multiple Failed Login Attempts","Security Concern","Other"];
  if(idempotencyKey==Guid.Empty||action is not("BLOCK" or "ACTIVATE" or "PASSWORD_RESET")||(remarks?.Length??0)>500||(action=="BLOCK"&&(!reasons.Contains(reason)||string.IsNullOrWhiteSpace(remarks))))throw new ApiFault(400,"IB_ACTION_INVALID","Check the action, reason and remarks");
  return new(r,action,"SIMULATED","MOCK-IB-"+idempotencyKey.ToString("N"));
 }
 public Task<DeliveryEnvelope> ProtectAsync(DeliverySecret s,DateTime expiry,CancellationToken ct)=>Task.FromResult(new DeliveryEnvelope(protector.Protect(JsonSerializer.SerializeToUtf8Bytes(s)),"DEVELOPMENT-EPHEMERAL",expiry));
 public Task<DeliverySecret> UnprotectAsync(DeliveryEnvelope e,CancellationToken ct)=>e.ExpiresAt<=DateTime.UtcNow?throw new ApiFault(410,"ENVELOPE_EXPIRED","Delivery envelope expired"):Task.FromResult(JsonSerializer.Deserialize<DeliverySecret>(protector.Unprotect(e.Ciphertext))!);
 public Task<ProviderReceipt> SendAsync(ProviderMessage m,CancellationToken ct)=>Task.FromResult(new ProviderReceipt(ProviderOutcome.Accepted,"SYNTHETIC-"+m.IdempotencyKey,null));
 public Task<ProviderReceipt> ReconcileAsync(string r,CancellationToken ct)=>Task.FromResult(new ProviderReceipt(ProviderOutcome.Unknown,null,"SYNTHETIC_UNKNOWN"));
}
