#nullable enable
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Customer360.Application;
using Customer360.Contracts;
using Customer360.Security;
namespace Customer360.Controllers;
[ApiController]
[Route("api/v1/track")]
public sealed class TrackController(Procedures procedures, ActorAccessor actors) : ControllerBase
{
 [HttpPost("customer-status-mapping/get-all")]
 [Authorize(Policy="CONFIG")]
 public async Task<ActionResult<ApiResponse<CustomerStatusMapping_GetAllResponse>>> CustomerStatusMapping_GetAll([FromBody] CustomerStatusMapping_GetAllRequest request, CancellationToken ct) => Ok(ApiResponse<CustomerStatusMapping_GetAllResponse>.Ok(await procedures.CustomerStatusMapping_GetAllAsync(request, actors.Current, ct), HttpContext.TraceIdentifier));
 [HttpPost("customer-status-mapping/update")]
 [Authorize(Policy="CONFIG")]
 public async Task<ActionResult<ApiResponse<CustomerStatusMapping_UpdateResponse>>> CustomerStatusMapping_Update([FromBody] CustomerStatusMapping_UpdateRequest request, CancellationToken ct) => Ok(ApiResponse<CustomerStatusMapping_UpdateResponse>.Ok(await procedures.CustomerStatusMapping_UpdateAsync(request, actors.Current, ct), HttpContext.TraceIdentifier));
}
