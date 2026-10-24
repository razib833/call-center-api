using System.Reflection;
using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Customer360.Contracts;
using Customer360.Controllers;
using Customer360.Application;
namespace Customer360.Documentation;
public static class OpenApiDocument
{
 public static string Generate()
 {
  var schemas=new Dictionary<string,object>();var paths=new Dictionary<string,Dictionary<string,object>>();
  object Schema(Type type)
  {
   var actual=Nullable.GetUnderlyingType(type)??type;
   if(actual==typeof(string))return new{type="string"};if(actual==typeof(byte[]))return new{type="string",format="byte"};
   if(actual==typeof(DateOnly))return new{type="string",format="date"};
   if(actual==typeof(bool))return new{type="boolean"};if(actual==typeof(Guid))return new{type="string",format="uuid"};if(actual==typeof(DateTime))return new{type="string",format="date-time"};
   if(actual==typeof(long))return new{type="integer",format="int64"};if(actual==typeof(int)||actual==typeof(short)||actual==typeof(byte))return new{type="integer",format="int32"};if(actual==typeof(decimal))return new{type="number"};
   var enumerable=actual.GetInterfaces().Concat([actual]).FirstOrDefault(t=>t.IsGenericType&&t.GetGenericTypeDefinition()==typeof(IEnumerable<>));
   if(enumerable!=null)return new{type="array",items=Schema(enumerable.GetGenericArguments()[0])};
   var name=actual.Name.Split('`')[0]+(actual.IsGenericType?"Of"+string.Join("And",actual.GetGenericArguments().Select(t=>t.Name)):"");
   if(!schemas.ContainsKey(name))
   {
    schemas[name]=new{};var properties=new Dictionary<string,object>();var required=new List<string>();
    foreach(var p in actual.GetProperties())
    {
     properties[JsonNamingPolicy.CamelCase.ConvertName(p.Name)]=Schema(p.PropertyType);
     if(p.GetCustomAttributes().Any(a=>a is System.ComponentModel.DataAnnotations.RequiredAttribute||a.GetType().Name=="RequiredMemberAttribute"))required.Add(JsonNamingPolicy.CamelCase.ConvertName(p.Name));
    }
    schemas[name]=new{type="object",properties,required,additionalProperties=false};
   }
   return new Dictionary<string,object>{{"$ref","#/components/schemas/"+name}};
  }
  foreach(var controller in typeof(AuthenticationController).Assembly.GetTypes().Where(t=>t.IsSubclassOf(typeof(ControllerBase))))
  {
   var prefix=controller.GetCustomAttribute<RouteAttribute>()?.Template??"";
   foreach(var action in controller.GetMethods(BindingFlags.Public|BindingFlags.Instance|BindingFlags.DeclaredOnly))
   foreach(var http in action.GetCustomAttributes<HttpMethodAttribute>())
   {
    var route="/"+prefix+(!string.IsNullOrEmpty(http.Template)?"/"+http.Template:"");
    var anonymous=action.GetCustomAttribute<AllowAnonymousAttribute>()!=null;
    var policy=action.GetCustomAttribute<AuthorizeAttribute>()?.Policy??controller.GetCustomAttribute<AuthorizeAttribute>()?.Policy;
    var response=action.ReturnType;if(response.IsGenericType&&response.GetGenericTypeDefinition()==typeof(Task<>))response=response.GetGenericArguments()[0];
    if(response.IsGenericType&&response.GetGenericTypeDefinition()==typeof(ActionResult<>))response=response.GetGenericArguments()[0];
    if(typeof(IActionResult).IsAssignableFrom(response))response=typeof(ApiResponse<object>);
    var responses=new Dictionary<string,object>();
    responses["200"]=new{description="Success; data shape follows procedure mapping",content=new Dictionary<string,object>{{"application/json",new{schema=Schema(response)}}}};
    foreach(var code in new[]{"400","401","403","404","409","410","422","429","500","503"})responses[code]=new{description="Safe error envelope",content=new Dictionary<string,object>{{"application/json",new{schema=Schema(typeof(ApiResponse<object>))}}}};
    var op=new Dictionary<string,object>{{"operationId",controller.Name.Replace("Controller","")+"_"+action.Name},{"tags",new[]{controller.Name.Replace("Controller","")}},{"description",anonymous?"Public or trusted-verifier entry point. No caller-supplied role is trusted.":"Required capability: "+(policy??"authenticated session")},{"responses",responses}};
    op["security"]=anonymous?Array.Empty<object>():new object[]{new Dictionary<string,string[]>{{"SessionCookie",[]}}};
    var parameter=action.GetParameters().FirstOrDefault(p=>p.ParameterType!=typeof(CancellationToken)&&p.GetCustomAttribute<FromServicesAttribute>()==null);
    if(parameter!=null&&http.HttpMethods.Contains("POST"))op["requestBody"]=new{required=true,content=new Dictionary<string,object>{{"application/json",new{schema=Schema(parameter.ParameterType)}}}};
    if(http.HttpMethods.Contains("POST")&&route!="/api/v1/tracking")op["parameters"]=new[]{new{name="X-CSRF-TOKEN",@in="header",required=true,schema=new{type="string"},description="Obtain from GET /api/v1/auth/csrf using same cookie jar"}};
    if(!paths.TryGetValue(route,out var methods))paths[route]=methods=[];
    foreach(var verb in http.HttpMethods)methods[verb.ToLowerInvariant()]=op;
   }
  }
  return JsonSerializer.Serialize(new{openapi="3.0.3",info=new{title="Jamuna Bank Customer 360 API",version="v1",description="Phase 4 development build. Live SQL/security/provider integration pending. RPC-style typed POST operations; no worker/internal HTTP endpoints."},paths,components=new{schemas,securitySchemes=new{SessionCookie=new{type="apiKey",@in="cookie",name="__Host-C360"}}}},new JsonSerializerOptions{WriteIndented=true});
 }
}
