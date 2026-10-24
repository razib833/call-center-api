using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using System.Reflection;
namespace Customer360.Contracts;

public sealed record ApiResponse<T>(bool Success, string Message, T? Data, IReadOnlyList<string> Errors, string CorrelationId)
{
 public static ApiResponse<T> Ok(T data, string correlationId) => new(true,"Request completed",data,[],correlationId);
 public static ApiResponse<T> Error(string message, string correlationId) => new(false,message,default,[message],correlationId);
}
public sealed class ApiFault(int status, string code, string message) : Exception(message)
{ public int Status {get;}=status; public string Code {get;}=code; }
public static class Validation
{
 public static void Validate(object request)
 {
  var errors=new List<ValidationResult>();
  if(!Validator.TryValidateObject(request,new(request),errors,true)) throw new ApiFault(400,"INVALID_REQUEST","Request validation failed");
  foreach(var parameter in request.GetType().GetConstructors().SelectMany(c=>c.GetParameters()))
  {
   var property=request.GetType().GetProperty(parameter.Name??"");
   if(property!=null&&!Validator.TryValidateValue(property.GetValue(request),new ValidationContext(request){MemberName=property.Name},errors,parameter.GetCustomAttributes<ValidationAttribute>()))throw new ApiFault(400,"INVALID_REQUEST","Request validation failed");
  }
  var props=request.GetType().GetProperties();
  foreach(var p in props)
  {
   var value=p.GetValue(request);
   if(p.Name.EndsWith("ID",StringComparison.Ordinal) && value is long number && number<=0)throw new ApiFault(400,"IDENTIFIER_INVALID","A positive identifier is required");
   if(p.Name.EndsWith("ID",StringComparison.Ordinal) && value is int integer && integer<=0)throw new ApiFault(400,"IDENTIFIER_INVALID","A positive identifier is required");
   if(p.Name.Contains("RowVersion",StringComparison.Ordinal) && (value is not byte[] bytes || bytes.Length!=8)) throw new ApiFault(400,"ROWVERSION_REQUIRED","An eight-byte row version is required");
   if(p.Name=="Remarks" && request.GetType().Name.Contains("Reject") && string.IsNullOrWhiteSpace(value as string)) throw new ApiFault(400,"REASON_REQUIRED","A rejection reason is required");
   if(p.Name=="Note" && string.IsNullOrWhiteSpace(value as string)) throw new ApiFault(400,"NOTE_REQUIRED","A follow-up note is required");
   if(p.Name=="Reason" && string.IsNullOrWhiteSpace(value as string)) throw new ApiFault(400,"REASON_REQUIRED","A reason is required");
  }
  var from=props.FirstOrDefault(p=>p.Name=="FromDate")?.GetValue(request) as DateTime?;
  var to=props.FirstOrDefault(p=>p.Name=="ToDate")?.GetValue(request) as DateTime?;
  if(from>to)throw new ApiFault(400,"DATE_RANGE","Date range is invalid");
  if(request is Forwarding_CreateRequest forwarding)
  {
   if(forwarding.ForwardingType is not("BRANCH" or "DIVISION")||forwarding.Subject.IndexOfAny(['\r','\n'])>=0||forwarding.Body.Contains('<')||forwarding.Body.Contains('>'))throw new ApiFault(400,"FORWARDING_CONTENT","Forwarding requires a valid destination and plain-text content");
   ValidateAddresses(forwarding.ToAddresses,true);ValidateAddresses(forwarding.CcAddresses,false);
  }
  if(request is FollowUpSchedule_CreateRequest reminder && (reminder.ScheduledAt.Kind!=DateTimeKind.Utc||reminder.ScheduledAt<=DateTime.UtcNow))throw new ApiFault(400,"REMINDER_TIME","Reminder must use a future UTC timestamp");
 }
 static void ValidateAddresses(string? value,bool required)
 {
  if(string.IsNullOrWhiteSpace(value)){if(required)throw new ApiFault(400,"EMAIL_ADDRESS","Recipient address required");return;}
  var entries=value.Split([',',';'],StringSplitOptions.TrimEntries|StringSplitOptions.RemoveEmptyEntries);
  if(value.Length>2000||value.IndexOfAny(['\r','\n'])>=0||entries.Length is <1 or >20)throw new ApiFault(400,"EMAIL_ADDRESS","Recipient addresses are invalid");
  foreach(var entry in entries)if(!System.Net.Mail.MailAddress.TryCreate(entry,out var parsed)||parsed.Address!=entry||!parsed.Host.Contains('.'))throw new ApiFault(400,"EMAIL_ADDRESS","Recipient addresses are invalid");
 }
}
