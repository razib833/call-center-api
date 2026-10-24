using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Text.Json;
using Microsoft.AspNetCore.WebUtilities;
using Customer360.Contracts;
using Customer360.Integrations;
using Customer360.Security;
namespace Customer360.Application;
public sealed record DynamicValue(long FieldID,string? Value);
public sealed record CreateTicketRequest([Range(1,long.MaxValue)]long ServiceID,[Range(1,long.MaxValue)]long VersionID,Guid IdempotencyKey,[Required]string CustomerReference,[Required]IReadOnlyList<DynamicValue> Fields,string? Description);
public sealed record CreatedTicket(long RequestID,string TicketNumber);
public sealed record TokenReplacementRequest(long RequestID,[MinLength(8),MaxLength(8)]byte[] ExpectedRowVersion,[Required]string Reason);
public sealed record PublicTrackingRequest([Required,MinLength(43),MaxLength(43)]string Token);
public sealed record PublicTrackingResponse(IReadOnlyList<Tracking_GetPublicStatusRow1> Status,IReadOnlyList<Tracking_GetPublicTimelineRow1> Timeline);
public static class TrackingTokens
{
 public static (string Token,byte[] Hash) Create(){var bytes=RandomNumberGenerator.GetBytes(32);return(WebEncoders.Base64UrlEncode(bytes),SHA256.HashData(bytes));}
 public static byte[] Hash(string value)
 {try{var bytes=WebEncoders.Base64UrlDecode(value);if(bytes.Length!=32||WebEncoders.Base64UrlEncode(bytes)!=value)throw new FormatException();return SHA256.HashData(bytes);}catch(FormatException){throw new ApiFault(404,"TRACKING_UNAVAILABLE","Tracking is unavailable");}}
}
public sealed class TicketApplication(Procedures db,ICbsMiddleware customers,IDeliveryProtector protector,IntegrationAudit audit)
{
 public async Task<CreatedTicket> CreateAsync(CreateTicketRequest request,Actor actor,CancellationToken ct)
 {
  actor.Demand("AGENT");Validation.Validate(request);
  if(request.IdempotencyKey==Guid.Empty||string.IsNullOrWhiteSpace(actor.InteractionId)||request.Fields.Count>100||request.Fields.Select(v=>v.FieldID).Distinct().Count()!=request.Fields.Count||request.Fields.Any(v=>v.FieldID<=0))throw new ApiFault(400,"TICKET_INVALID","Invalid ticket request or verified interaction");
  var customer=await audit.ExecuteAsync("CBS","CustomerProfile",actor,()=>customers.ProfileAsync(request.CustomerReference,ct),ct);
  var token=TrackingTokens.Create();var envelope=await protector.ProtectAsync(new(token.Token,customer.Reference),DateTime.UtcNow.AddDays(29),ct);
  var result=await db.Ticket_CreateAsync(new(){ServiceID=request.ServiceID,VersionID=request.VersionID,IdempotencyKey=request.IdempotencyKey,AvayaInteractionID=actor.InteractionId,FieldValuesJson=JsonSerializer.Serialize(request.Fields),TrackingTokenHash=token.Hash,EncryptedDeliveryPayload=envelope.Ciphertext,PayloadKeyReference=envelope.KeyReference,PayloadExpiresAt=envelope.ExpiresAt,RequestDescription=request.Description,CustomerName=customer.Name,CustomerMobile=customer.Mobile,CBS_CIF=customer.Cif},actor,ct);
  return new(result.RequestID??throw new ApiFault(503,"CONTRACT_MISMATCH","Ticket creation result is incomplete"),result.TicketNumber??throw new ApiFault(503,"CONTRACT_MISMATCH","Ticket creation result is incomplete"));
 }
 public async Task<TrackingToken_ReplaceResponse> ReplaceAsync(TokenReplacementRequest request,Actor actor,CancellationToken ct)
 {
  actor.Demand("MANAGER");Validation.Validate(request);
  // The read DTO deliberately omits customer routing identifiers. No existing procedure can safely
  // retrieve the delivery customer reference and active token rowversion. Do not invent either.
  await Task.CompletedTask;
  throw new ApiFault(503,"TOKEN_REPLACEMENT_CONTRACT_GAP","Token replacement requires an approved secure customer-routing and active-token-version contract");
 }
 public async Task<PublicTrackingResponse> TrackAsync(PublicTrackingRequest request,Guid correlation,CancellationToken ct)
 {
  var hash=TrackingTokens.Hash(request.Token);var actor=Actor.Public(correlation);
  var valid=await db.TrackingToken_ValidateAsync(new(){TokenHash=hash},actor,ct);
  if(valid.Rows1.SingleOrDefault()?.IsValid!=true)throw new ApiFault(404,"TRACKING_UNAVAILABLE","Tracking is unavailable");
  var status=await db.Tracking_GetPublicStatusAsync(new(){TokenHash=hash},actor,ct);
  var timeline=await db.Tracking_GetPublicTimelineAsync(new(){TokenHash=hash},actor,ct);
  await db.TrackingToken_RecordAccessAsync(new(){TokenHash=hash},actor,ct);
  if(status.Rows1.Count==0)throw new ApiFault(404,"TRACKING_UNAVAILABLE","Tracking is unavailable");
  return new(status.Rows1,timeline.Rows1);
 }
}
