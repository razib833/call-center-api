#nullable enable
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Customer360.Application;
using Customer360.Contracts;
using Customer360.Security;
namespace Customer360.Controllers;
[ApiController]
[Route("api/v1/configuration")]
public sealed class ConfigurationController(Procedures procedures, ActorAccessor actors) : ControllerBase
{
 [HttpPost("service-category/create")]
 [Authorize(Policy="CONFIG")]
 public async Task<ActionResult<ApiResponse<ServiceCategory_CreateResponse>>> ServiceCategory_Create([FromBody] ServiceCategory_CreateRequest request, CancellationToken ct) => Ok(ApiResponse<ServiceCategory_CreateResponse>.Ok(await procedures.ServiceCategory_CreateAsync(request, actors.Current, ct), HttpContext.TraceIdentifier));
 [HttpPost("service-category/update")]
 [Authorize(Policy="CONFIG")]
 public async Task<ActionResult<ApiResponse<ServiceCategory_UpdateResponse>>> ServiceCategory_Update([FromBody] ServiceCategory_UpdateRequest request, CancellationToken ct) => Ok(ApiResponse<ServiceCategory_UpdateResponse>.Ok(await procedures.ServiceCategory_UpdateAsync(request, actors.Current, ct), HttpContext.TraceIdentifier));
 [HttpPost("service-category/set-active")]
 [Authorize(Policy="CONFIG")]
 public async Task<ActionResult<ApiResponse<ServiceCategory_SetActiveResponse>>> ServiceCategory_SetActive([FromBody] ServiceCategory_SetActiveRequest request, CancellationToken ct) => Ok(ApiResponse<ServiceCategory_SetActiveResponse>.Ok(await procedures.ServiceCategory_SetActiveAsync(request, actors.Current, ct), HttpContext.TraceIdentifier));
 [HttpPost("service-category/get-by-id")]
 [Authorize(Policy="READ")]
 public async Task<ActionResult<ApiResponse<ServiceCategory_GetByIDResponse>>> ServiceCategory_GetByID([FromBody] ServiceCategory_GetByIDRequest request, CancellationToken ct) => Ok(ApiResponse<ServiceCategory_GetByIDResponse>.Ok(await procedures.ServiceCategory_GetByIDAsync(request, actors.Current, ct), HttpContext.TraceIdentifier));
 [HttpPost("service-category/list")]
 [Authorize(Policy="READ")]
 public async Task<ActionResult<ApiResponse<ServiceCategory_ListResponse>>> ServiceCategory_List([FromBody] ServiceCategory_ListRequest request, CancellationToken ct) => Ok(ApiResponse<ServiceCategory_ListResponse>.Ok(await procedures.ServiceCategory_ListAsync(request, actors.Current, ct), HttpContext.TraceIdentifier));
 [HttpPost("service/create")]
 [Authorize(Policy="CONFIG")]
 public async Task<ActionResult<ApiResponse<Service_CreateResponse>>> Service_Create([FromBody] Service_CreateRequest request, CancellationToken ct) => Ok(ApiResponse<Service_CreateResponse>.Ok(await procedures.Service_CreateAsync(request, actors.Current, ct), HttpContext.TraceIdentifier));
 [HttpPost("service/update")]
 [Authorize(Policy="CONFIG")]
 public async Task<ActionResult<ApiResponse<Service_UpdateResponse>>> Service_Update([FromBody] Service_UpdateRequest request, CancellationToken ct) => Ok(ApiResponse<Service_UpdateResponse>.Ok(await procedures.Service_UpdateAsync(request, actors.Current, ct), HttpContext.TraceIdentifier));
 [HttpPost("service/set-active")]
 [Authorize(Policy="CONFIG")]
 public async Task<ActionResult<ApiResponse<Service_SetActiveResponse>>> Service_SetActive([FromBody] Service_SetActiveRequest request, CancellationToken ct) => Ok(ApiResponse<Service_SetActiveResponse>.Ok(await procedures.Service_SetActiveAsync(request, actors.Current, ct), HttpContext.TraceIdentifier));
 [HttpPost("service/get-by-id")]
 [Authorize(Policy="READ")]
 public async Task<ActionResult<ApiResponse<Service_GetByIDResponse>>> Service_GetByID([FromBody] Service_GetByIDRequest request, CancellationToken ct) => Ok(ApiResponse<Service_GetByIDResponse>.Ok(await procedures.Service_GetByIDAsync(request, actors.Current, ct), HttpContext.TraceIdentifier));
 [HttpPost("service/list")]
 [Authorize(Policy="READ")]
 public async Task<ActionResult<ApiResponse<Service_ListResponse>>> Service_List([FromBody] Service_ListRequest request, CancellationToken ct) => Ok(ApiResponse<Service_ListResponse>.Ok(await procedures.Service_ListAsync(request, actors.Current, ct), HttpContext.TraceIdentifier));
 [HttpPost("service-version/create-draft")]
 [Authorize(Policy="CONFIG")]
 public async Task<ActionResult<ApiResponse<ServiceVersion_CreateDraftResponse>>> ServiceVersion_CreateDraft([FromBody] ServiceVersion_CreateDraftRequest request, CancellationToken ct) => Ok(ApiResponse<ServiceVersion_CreateDraftResponse>.Ok(await procedures.ServiceVersion_CreateDraftAsync(request, actors.Current, ct), HttpContext.TraceIdentifier));
 [HttpPost("service-version/update-draft")]
 [Authorize(Policy="CONFIG")]
 public async Task<ActionResult<ApiResponse<ServiceVersion_UpdateDraftResponse>>> ServiceVersion_UpdateDraft([FromBody] ServiceVersion_UpdateDraftRequest request, CancellationToken ct) => Ok(ApiResponse<ServiceVersion_UpdateDraftResponse>.Ok(await procedures.ServiceVersion_UpdateDraftAsync(request, actors.Current, ct), HttpContext.TraceIdentifier));
 [HttpPost("service-version/get-draft")]
 [Authorize(Policy="READ")]
 public async Task<ActionResult<ApiResponse<ServiceVersion_GetDraftResponse>>> ServiceVersion_GetDraft([FromBody] ServiceVersion_GetDraftRequest request, CancellationToken ct) => Ok(ApiResponse<ServiceVersion_GetDraftResponse>.Ok(await procedures.ServiceVersion_GetDraftAsync(request, actors.Current, ct), HttpContext.TraceIdentifier));
 [HttpPost("service-version/get-published")]
 [Authorize(Policy="READ")]
 public async Task<ActionResult<ApiResponse<ServiceVersion_GetPublishedResponse>>> ServiceVersion_GetPublished([FromBody] ServiceVersion_GetPublishedRequest request, CancellationToken ct) => Ok(ApiResponse<ServiceVersion_GetPublishedResponse>.Ok(await procedures.ServiceVersion_GetPublishedAsync(request, actors.Current, ct), HttpContext.TraceIdentifier));
 [HttpPost("service-version/get-history")]
 [Authorize(Policy="READ")]
 public async Task<ActionResult<ApiResponse<ServiceVersion_GetHistoryResponse>>> ServiceVersion_GetHistory([FromBody] ServiceVersion_GetHistoryRequest request, CancellationToken ct) => Ok(ApiResponse<ServiceVersion_GetHistoryResponse>.Ok(await procedures.ServiceVersion_GetHistoryAsync(request, actors.Current, ct), HttpContext.TraceIdentifier));
 [HttpPost("service-version/clone-published")]
 [Authorize(Policy="CONFIG")]
 public async Task<ActionResult<ApiResponse<ServiceVersion_ClonePublishedResponse>>> ServiceVersion_ClonePublished([FromBody] ServiceVersion_ClonePublishedRequest request, CancellationToken ct) => Ok(ApiResponse<ServiceVersion_ClonePublishedResponse>.Ok(await procedures.ServiceVersion_ClonePublishedAsync(request, actors.Current, ct), HttpContext.TraceIdentifier));
 [HttpPost("service-version/publish-draft")]
 [Authorize(Policy="CONFIG")]
 public async Task<ActionResult<ApiResponse<ServiceVersion_PublishDraftResponse>>> ServiceVersion_PublishDraft([FromBody] ServiceVersion_PublishDraftRequest request, CancellationToken ct) => Ok(ApiResponse<ServiceVersion_PublishDraftResponse>.Ok(await procedures.ServiceVersion_PublishDraftAsync(request, actors.Current, ct), HttpContext.TraceIdentifier));
 [HttpPost("service-version/retire-published")]
 [Authorize(Policy="CONFIG")]
 public async Task<ActionResult<ApiResponse<ServiceVersion_RetirePublishedResponse>>> ServiceVersion_RetirePublished([FromBody] ServiceVersion_RetirePublishedRequest request, CancellationToken ct) => Ok(ApiResponse<ServiceVersion_RetirePublishedResponse>.Ok(await procedures.ServiceVersion_RetirePublishedAsync(request, actors.Current, ct), HttpContext.TraceIdentifier));
 [HttpPost("service-field/add")]
 [Authorize(Policy="CONFIG")]
 public async Task<ActionResult<ApiResponse<ServiceField_AddResponse>>> ServiceField_Add([FromBody] ServiceField_AddRequest request, CancellationToken ct) => Ok(ApiResponse<ServiceField_AddResponse>.Ok(await procedures.ServiceField_AddAsync(request, actors.Current, ct), HttpContext.TraceIdentifier));
 [HttpPost("service-field/update")]
 [Authorize(Policy="CONFIG")]
 public async Task<ActionResult<ApiResponse<ServiceField_UpdateResponse>>> ServiceField_Update([FromBody] ServiceField_UpdateRequest request, CancellationToken ct) => Ok(ApiResponse<ServiceField_UpdateResponse>.Ok(await procedures.ServiceField_UpdateAsync(request, actors.Current, ct), HttpContext.TraceIdentifier));
 [HttpPost("service-field/deactivate")]
 [Authorize(Policy="CONFIG")]
 public async Task<ActionResult<ApiResponse<ServiceField_DeactivateResponse>>> ServiceField_Deactivate([FromBody] ServiceField_DeactivateRequest request, CancellationToken ct) => Ok(ApiResponse<ServiceField_DeactivateResponse>.Ok(await procedures.ServiceField_DeactivateAsync(request, actors.Current, ct), HttpContext.TraceIdentifier));
 [HttpPost("service-field/get-by-version")]
 [Authorize(Policy="READ")]
 public async Task<ActionResult<ApiResponse<ServiceField_GetByVersionResponse>>> ServiceField_GetByVersion([FromBody] ServiceField_GetByVersionRequest request, CancellationToken ct) => Ok(ApiResponse<ServiceField_GetByVersionResponse>.Ok(await procedures.ServiceField_GetByVersionAsync(request, actors.Current, ct), HttpContext.TraceIdentifier));
 [HttpPost("service-field-option/add")]
 [Authorize(Policy="CONFIG")]
 public async Task<ActionResult<ApiResponse<ServiceFieldOption_AddResponse>>> ServiceFieldOption_Add([FromBody] ServiceFieldOption_AddRequest request, CancellationToken ct) => Ok(ApiResponse<ServiceFieldOption_AddResponse>.Ok(await procedures.ServiceFieldOption_AddAsync(request, actors.Current, ct), HttpContext.TraceIdentifier));
 [HttpPost("service-field-option/update")]
 [Authorize(Policy="CONFIG")]
 public async Task<ActionResult<ApiResponse<ServiceFieldOption_UpdateResponse>>> ServiceFieldOption_Update([FromBody] ServiceFieldOption_UpdateRequest request, CancellationToken ct) => Ok(ApiResponse<ServiceFieldOption_UpdateResponse>.Ok(await procedures.ServiceFieldOption_UpdateAsync(request, actors.Current, ct), HttpContext.TraceIdentifier));
 [HttpPost("service-field-option/get")]
 [Authorize(Policy="READ")]
 public async Task<ActionResult<ApiResponse<ServiceFieldOption_GetResponse>>> ServiceFieldOption_Get([FromBody] ServiceFieldOption_GetRequest request, CancellationToken ct) => Ok(ApiResponse<ServiceFieldOption_GetResponse>.Ok(await procedures.ServiceFieldOption_GetAsync(request, actors.Current, ct), HttpContext.TraceIdentifier));
 [HttpPost("dropdown-list/create")]
 [Authorize(Policy="CONFIG")]
 public async Task<ActionResult<ApiResponse<DropdownList_CreateResponse>>> DropdownList_Create([FromBody] DropdownList_CreateRequest request, CancellationToken ct) => Ok(ApiResponse<DropdownList_CreateResponse>.Ok(await procedures.DropdownList_CreateAsync(request, actors.Current, ct), HttpContext.TraceIdentifier));
 [HttpPost("dropdown-list-version/create")]
 [Authorize(Policy="CONFIG")]
 public async Task<ActionResult<ApiResponse<DropdownListVersion_CreateResponse>>> DropdownListVersion_Create([FromBody] DropdownListVersion_CreateRequest request, CancellationToken ct) => Ok(ApiResponse<DropdownListVersion_CreateResponse>.Ok(await procedures.DropdownListVersion_CreateAsync(request, actors.Current, ct), HttpContext.TraceIdentifier));
 [HttpPost("dropdown-list-option/add")]
 [Authorize(Policy="CONFIG")]
 public async Task<ActionResult<ApiResponse<DropdownListOption_AddResponse>>> DropdownListOption_Add([FromBody] DropdownListOption_AddRequest request, CancellationToken ct) => Ok(ApiResponse<DropdownListOption_AddResponse>.Ok(await procedures.DropdownListOption_AddAsync(request, actors.Current, ct), HttpContext.TraceIdentifier));
 [HttpPost("dropdown-list-option/update")]
 [Authorize(Policy="CONFIG")]
 public async Task<ActionResult<ApiResponse<DropdownListOption_UpdateResponse>>> DropdownListOption_Update([FromBody] DropdownListOption_UpdateRequest request, CancellationToken ct) => Ok(ApiResponse<DropdownListOption_UpdateResponse>.Ok(await procedures.DropdownListOption_UpdateAsync(request, actors.Current, ct), HttpContext.TraceIdentifier));
 [HttpPost("dropdown-list-version/publish")]
 [Authorize(Policy="CONFIG")]
 public async Task<ActionResult<ApiResponse<DropdownListVersion_PublishResponse>>> DropdownListVersion_Publish([FromBody] DropdownListVersion_PublishRequest request, CancellationToken ct) => Ok(ApiResponse<DropdownListVersion_PublishResponse>.Ok(await procedures.DropdownListVersion_PublishAsync(request, actors.Current, ct), HttpContext.TraceIdentifier));
 [HttpPost("dropdown-list/get-published-options")]
 [Authorize(Policy="READ")]
 public async Task<ActionResult<ApiResponse<DropdownList_GetPublishedOptionsResponse>>> DropdownList_GetPublishedOptions([FromBody] DropdownList_GetPublishedOptionsRequest request, CancellationToken ct) => Ok(ApiResponse<DropdownList_GetPublishedOptionsResponse>.Ok(await procedures.DropdownList_GetPublishedOptionsAsync(request, actors.Current, ct), HttpContext.TraceIdentifier));
 [HttpPost("service-workflow/save")]
 [Authorize(Policy="CONFIG")]
 public async Task<ActionResult<ApiResponse<ServiceWorkflow_SaveResponse>>> ServiceWorkflow_Save([FromBody] ServiceWorkflow_SaveRequest request, CancellationToken ct) => Ok(ApiResponse<ServiceWorkflow_SaveResponse>.Ok(await procedures.ServiceWorkflow_SaveAsync(request, actors.Current, ct), HttpContext.TraceIdentifier));
 [HttpPost("service-workflow/get")]
 [Authorize(Policy="READ")]
 public async Task<ActionResult<ApiResponse<ServiceWorkflow_GetResponse>>> ServiceWorkflow_Get([FromBody] ServiceWorkflow_GetRequest request, CancellationToken ct) => Ok(ApiResponse<ServiceWorkflow_GetResponse>.Ok(await procedures.ServiceWorkflow_GetAsync(request, actors.Current, ct), HttpContext.TraceIdentifier));
 [HttpPost("service-notification-config/save")]
 [Authorize(Policy="CONFIG")]
 public async Task<ActionResult<ApiResponse<ServiceNotificationConfig_SaveResponse>>> ServiceNotificationConfig_Save([FromBody] ServiceNotificationConfig_SaveRequest request, CancellationToken ct) => Ok(ApiResponse<ServiceNotificationConfig_SaveResponse>.Ok(await procedures.ServiceNotificationConfig_SaveAsync(request, actors.Current, ct), HttpContext.TraceIdentifier));
 [HttpPost("service-notification-config/get")]
 [Authorize(Policy="READ")]
 public async Task<ActionResult<ApiResponse<ServiceNotificationConfig_GetResponse>>> ServiceNotificationConfig_Get([FromBody] ServiceNotificationConfig_GetRequest request, CancellationToken ct) => Ok(ApiResponse<ServiceNotificationConfig_GetResponse>.Ok(await procedures.ServiceNotificationConfig_GetAsync(request, actors.Current, ct), HttpContext.TraceIdentifier));
}
