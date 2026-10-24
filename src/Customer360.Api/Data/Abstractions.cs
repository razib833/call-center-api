using System.Reflection;
using System.Text.Json;
using Customer360.Contracts;
using Customer360.Security;
namespace Customer360.Data;
public sealed record ParameterSpec(string Name,string SqlType,int Size,bool Output,bool HasDefault);
public sealed record ProcedureSpec(string Name,string Capability,IReadOnlyList<ParameterSpec> Parameters,int ResultSets,IReadOnlyList<IReadOnlyList<string>> Columns);
public static class ParameterValues
{
 public static IReadOnlyDictionary<string,object?> From(object request) => request.GetType().GetProperties().ToDictionary(p=>p.Name,p=>p.GetValue(request));
}
public sealed class ProcedureResult
{
 public Dictionary<string,object?> Outputs {get;}=new(StringComparer.OrdinalIgnoreCase);
 public List<List<Dictionary<string,object?>>> Sets {get;}=[];
 public T? Output<T>(string name) => Outputs.TryGetValue(name,out var v)?ConvertValue<T>(v):default;
 public IReadOnlyList<T> Rows<T>(int index) where T:new()
 {
  if(index>=Sets.Count)throw new ApiFault(503,"CONTRACT_MISMATCH","Database result contract differs");
  return Sets[index].Select(row=>{var obj=new T();foreach(var p in typeof(T).GetProperties()){if(!row.TryGetValue(p.Name,out var v))throw new ApiFault(503,"CONTRACT_MISMATCH","Database result contract differs");p.SetValue(obj,ConvertValue(v,p.PropertyType));}return obj;}).ToArray();
 }
 public static T? ConvertValue<T>(object? value) => (T?)ConvertValue(value,typeof(T));
 private static object? ConvertValue(object? value,Type target)
 {
  if(value is null or DBNull)return null;
  target=Nullable.GetUnderlyingType(target)??target;
  if(target==typeof(DateTime)&&value is DateTime dt)return DateTime.SpecifyKind(dt,DateTimeKind.Utc);
  if(target.IsInstanceOfType(value))return value;
  if(value is JsonElement e)return JsonSerializer.Deserialize(e.GetRawText(),target);
  return Convert.ChangeType(value,target,System.Globalization.CultureInfo.InvariantCulture);
 }
}
public interface IProcedureExecutor
{Task<ProcedureResult> ExecuteAsync(ProcedureSpec procedure,IReadOnlyDictionary<string,object?> parameters,Actor actor,CancellationToken ct);}
public sealed record DatabaseOptions
{
 public string ExpectedServer {get;init;}="DESKTOP-BUDV4V7";
 public string ExpectedDatabase {get;init;}="JamunaCustomer360";
 public Dictionary<string,string> Connections {get;init;}=[];
 public Dictionary<string,string> ExpectedUsers {get;init;}=[];
 public bool LiveValidationApproved {get;init;}
 public bool AllowUnencryptedLocalDevelopment {get;init;}
 public Dictionary<string,string> IdentityVerificationReferences {get;init;}=[];
}
