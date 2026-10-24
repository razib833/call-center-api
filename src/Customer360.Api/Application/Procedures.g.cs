#nullable enable
using Customer360.Contracts;
using Customer360.Data;
using Customer360.Security;
namespace Customer360.Application;
public sealed class Procedures(IProcedureExecutor executor)
{
 public async Task<Priority_GetAllResponse> Priority_GetAllAsync(Priority_GetAllRequest request, Actor actor, CancellationToken ct = default)
 {
  Validation.Validate(request);
  var result = await executor.ExecuteAsync(ProcedureCatalog.All["usp_Priority_GetAll"], ParameterValues.From(request), actor, ct);
  return new Priority_GetAllResponse { Rows1=result.Rows<Priority_GetAllRow1>(0) };
 }
 public async Task<Priority_GetActiveResponse> Priority_GetActiveAsync(Priority_GetActiveRequest request, Actor actor, CancellationToken ct = default)
 {
  Validation.Validate(request);
  var result = await executor.ExecuteAsync(ProcedureCatalog.All["usp_Priority_GetActive"], ParameterValues.From(request), actor, ct);
  return new Priority_GetActiveResponse { Rows1=result.Rows<Priority_GetActiveRow1>(0) };
 }
 public async Task<Priority_CreateResponse> Priority_CreateAsync(Priority_CreateRequest request, Actor actor, CancellationToken ct = default)
 {
  Validation.Validate(request);
  var result = await executor.ExecuteAsync(ProcedureCatalog.All["usp_Priority_Create"], ParameterValues.From(request), actor, ct);
  return new Priority_CreateResponse { NewID=result.Output<int?>("NewID") };
 }
 public async Task<Priority_UpdateResponse> Priority_UpdateAsync(Priority_UpdateRequest request, Actor actor, CancellationToken ct = default)
 {
  Validation.Validate(request);
  var result = await executor.ExecuteAsync(ProcedureCatalog.All["usp_Priority_Update"], ParameterValues.From(request), actor, ct);
  return new Priority_UpdateResponse {  };
 }
 public async Task<Priority_SetActiveResponse> Priority_SetActiveAsync(Priority_SetActiveRequest request, Actor actor, CancellationToken ct = default)
 {
  Validation.Validate(request);
  var result = await executor.ExecuteAsync(ProcedureCatalog.All["usp_Priority_SetActive"], ParameterValues.From(request), actor, ct);
  return new Priority_SetActiveResponse {  };
 }
 public async Task<RequestSource_GetAllResponse> RequestSource_GetAllAsync(RequestSource_GetAllRequest request, Actor actor, CancellationToken ct = default)
 {
  Validation.Validate(request);
  var result = await executor.ExecuteAsync(ProcedureCatalog.All["usp_RequestSource_GetAll"], ParameterValues.From(request), actor, ct);
  return new RequestSource_GetAllResponse { Rows1=result.Rows<RequestSource_GetAllRow1>(0) };
 }
 public async Task<RequestSource_GetActiveResponse> RequestSource_GetActiveAsync(RequestSource_GetActiveRequest request, Actor actor, CancellationToken ct = default)
 {
  Validation.Validate(request);
  var result = await executor.ExecuteAsync(ProcedureCatalog.All["usp_RequestSource_GetActive"], ParameterValues.From(request), actor, ct);
  return new RequestSource_GetActiveResponse { Rows1=result.Rows<RequestSource_GetActiveRow1>(0) };
 }
 public async Task<RequestSource_CreateResponse> RequestSource_CreateAsync(RequestSource_CreateRequest request, Actor actor, CancellationToken ct = default)
 {
  Validation.Validate(request);
  var result = await executor.ExecuteAsync(ProcedureCatalog.All["usp_RequestSource_Create"], ParameterValues.From(request), actor, ct);
  return new RequestSource_CreateResponse { NewID=result.Output<string?>("NewID") };
 }
 public async Task<RequestSource_UpdateResponse> RequestSource_UpdateAsync(RequestSource_UpdateRequest request, Actor actor, CancellationToken ct = default)
 {
  Validation.Validate(request);
  var result = await executor.ExecuteAsync(ProcedureCatalog.All["usp_RequestSource_Update"], ParameterValues.From(request), actor, ct);
  return new RequestSource_UpdateResponse {  };
 }
 public async Task<RequestSource_SetActiveResponse> RequestSource_SetActiveAsync(RequestSource_SetActiveRequest request, Actor actor, CancellationToken ct = default)
 {
  Validation.Validate(request);
  var result = await executor.ExecuteAsync(ProcedureCatalog.All["usp_RequestSource_SetActive"], ParameterValues.From(request), actor, ct);
  return new RequestSource_SetActiveResponse {  };
 }
 public async Task<TicketStatus_GetAllResponse> TicketStatus_GetAllAsync(TicketStatus_GetAllRequest request, Actor actor, CancellationToken ct = default)
 {
  Validation.Validate(request);
  var result = await executor.ExecuteAsync(ProcedureCatalog.All["usp_TicketStatus_GetAll"], ParameterValues.From(request), actor, ct);
  return new TicketStatus_GetAllResponse { Rows1=result.Rows<TicketStatus_GetAllRow1>(0) };
 }
 public async Task<TicketStatus_GetActiveResponse> TicketStatus_GetActiveAsync(TicketStatus_GetActiveRequest request, Actor actor, CancellationToken ct = default)
 {
  Validation.Validate(request);
  var result = await executor.ExecuteAsync(ProcedureCatalog.All["usp_TicketStatus_GetActive"], ParameterValues.From(request), actor, ct);
  return new TicketStatus_GetActiveResponse { Rows1=result.Rows<TicketStatus_GetActiveRow1>(0) };
 }
 public async Task<TicketStatusTransition_GetAllowedResponse> TicketStatusTransition_GetAllowedAsync(TicketStatusTransition_GetAllowedRequest request, Actor actor, CancellationToken ct = default)
 {
  Validation.Validate(request);
  var result = await executor.ExecuteAsync(ProcedureCatalog.All["usp_TicketStatusTransition_GetAllowed"], ParameterValues.From(request), actor, ct);
  return new TicketStatusTransition_GetAllowedResponse { Rows1=result.Rows<TicketStatusTransition_GetAllowedRow1>(0) };
 }
 public async Task<ServiceCategory_CreateResponse> ServiceCategory_CreateAsync(ServiceCategory_CreateRequest request, Actor actor, CancellationToken ct = default)
 {
  Validation.Validate(request);
  var result = await executor.ExecuteAsync(ProcedureCatalog.All["usp_ServiceCategory_Create"], ParameterValues.From(request), actor, ct);
  return new ServiceCategory_CreateResponse { NewID=result.Output<int?>("NewID") };
 }
 public async Task<ServiceCategory_UpdateResponse> ServiceCategory_UpdateAsync(ServiceCategory_UpdateRequest request, Actor actor, CancellationToken ct = default)
 {
  Validation.Validate(request);
  var result = await executor.ExecuteAsync(ProcedureCatalog.All["usp_ServiceCategory_Update"], ParameterValues.From(request), actor, ct);
  return new ServiceCategory_UpdateResponse {  };
 }
 public async Task<ServiceCategory_SetActiveResponse> ServiceCategory_SetActiveAsync(ServiceCategory_SetActiveRequest request, Actor actor, CancellationToken ct = default)
 {
  Validation.Validate(request);
  var result = await executor.ExecuteAsync(ProcedureCatalog.All["usp_ServiceCategory_SetActive"], ParameterValues.From(request), actor, ct);
  return new ServiceCategory_SetActiveResponse {  };
 }
 public async Task<ServiceCategory_GetByIDResponse> ServiceCategory_GetByIDAsync(ServiceCategory_GetByIDRequest request, Actor actor, CancellationToken ct = default)
 {
  Validation.Validate(request);
  var result = await executor.ExecuteAsync(ProcedureCatalog.All["usp_ServiceCategory_GetByID"], ParameterValues.From(request), actor, ct);
  return new ServiceCategory_GetByIDResponse { Rows1=result.Rows<ServiceCategory_GetByIDRow1>(0) };
 }
 public async Task<ServiceCategory_ListResponse> ServiceCategory_ListAsync(ServiceCategory_ListRequest request, Actor actor, CancellationToken ct = default)
 {
  Validation.Validate(request);
  var result = await executor.ExecuteAsync(ProcedureCatalog.All["usp_ServiceCategory_List"], ParameterValues.From(request), actor, ct);
  return new ServiceCategory_ListResponse { Rows1=result.Rows<ServiceCategory_ListRow1>(0) };
 }
 public async Task<Service_CreateResponse> Service_CreateAsync(Service_CreateRequest request, Actor actor, CancellationToken ct = default)
 {
  Validation.Validate(request);
  var result = await executor.ExecuteAsync(ProcedureCatalog.All["usp_Service_Create"], ParameterValues.From(request), actor, ct);
  return new Service_CreateResponse { NewID=result.Output<long?>("NewID") };
 }
 public async Task<Service_UpdateResponse> Service_UpdateAsync(Service_UpdateRequest request, Actor actor, CancellationToken ct = default)
 {
  Validation.Validate(request);
  var result = await executor.ExecuteAsync(ProcedureCatalog.All["usp_Service_Update"], ParameterValues.From(request), actor, ct);
  return new Service_UpdateResponse {  };
 }
 public async Task<Service_SetActiveResponse> Service_SetActiveAsync(Service_SetActiveRequest request, Actor actor, CancellationToken ct = default)
 {
  Validation.Validate(request);
  var result = await executor.ExecuteAsync(ProcedureCatalog.All["usp_Service_SetActive"], ParameterValues.From(request), actor, ct);
  return new Service_SetActiveResponse {  };
 }
 public async Task<Service_GetByIDResponse> Service_GetByIDAsync(Service_GetByIDRequest request, Actor actor, CancellationToken ct = default)
 {
  Validation.Validate(request);
  var result = await executor.ExecuteAsync(ProcedureCatalog.All["usp_Service_GetByID"], ParameterValues.From(request), actor, ct);
  return new Service_GetByIDResponse { Rows1=result.Rows<Service_GetByIDRow1>(0) };
 }
 public async Task<Service_ListResponse> Service_ListAsync(Service_ListRequest request, Actor actor, CancellationToken ct = default)
 {
  Validation.Validate(request);
  var result = await executor.ExecuteAsync(ProcedureCatalog.All["usp_Service_List"], ParameterValues.From(request), actor, ct);
  return new Service_ListResponse { Rows1=result.Rows<Service_ListRow1>(0) };
 }
 public async Task<ServiceVersion_CreateDraftResponse> ServiceVersion_CreateDraftAsync(ServiceVersion_CreateDraftRequest request, Actor actor, CancellationToken ct = default)
 {
  Validation.Validate(request);
  var result = await executor.ExecuteAsync(ProcedureCatalog.All["usp_ServiceVersion_CreateDraft"], ParameterValues.From(request), actor, ct);
  return new ServiceVersion_CreateDraftResponse { NewVersionID=result.Output<long?>("NewVersionID") };
 }
 public async Task<ServiceVersion_UpdateDraftResponse> ServiceVersion_UpdateDraftAsync(ServiceVersion_UpdateDraftRequest request, Actor actor, CancellationToken ct = default)
 {
  Validation.Validate(request);
  var result = await executor.ExecuteAsync(ProcedureCatalog.All["usp_ServiceVersion_UpdateDraft"], ParameterValues.From(request), actor, ct);
  return new ServiceVersion_UpdateDraftResponse {  };
 }
 public async Task<ServiceVersion_GetDraftResponse> ServiceVersion_GetDraftAsync(ServiceVersion_GetDraftRequest request, Actor actor, CancellationToken ct = default)
 {
  Validation.Validate(request);
  var result = await executor.ExecuteAsync(ProcedureCatalog.All["usp_ServiceVersion_GetDraft"], ParameterValues.From(request), actor, ct);
  return new ServiceVersion_GetDraftResponse { Rows1=result.Rows<ServiceVersion_GetDraftRow1>(0) };
 }
 public async Task<ServiceVersion_GetPublishedResponse> ServiceVersion_GetPublishedAsync(ServiceVersion_GetPublishedRequest request, Actor actor, CancellationToken ct = default)
 {
  Validation.Validate(request);
  var result = await executor.ExecuteAsync(ProcedureCatalog.All["usp_ServiceVersion_GetPublished"], ParameterValues.From(request), actor, ct);
  return new ServiceVersion_GetPublishedResponse { Rows1=result.Rows<ServiceVersion_GetPublishedRow1>(0) };
 }
 public async Task<ServiceVersion_GetHistoryResponse> ServiceVersion_GetHistoryAsync(ServiceVersion_GetHistoryRequest request, Actor actor, CancellationToken ct = default)
 {
  Validation.Validate(request);
  var result = await executor.ExecuteAsync(ProcedureCatalog.All["usp_ServiceVersion_GetHistory"], ParameterValues.From(request), actor, ct);
  return new ServiceVersion_GetHistoryResponse { Rows1=result.Rows<ServiceVersion_GetHistoryRow1>(0) };
 }
 public async Task<ServiceVersion_ClonePublishedResponse> ServiceVersion_ClonePublishedAsync(ServiceVersion_ClonePublishedRequest request, Actor actor, CancellationToken ct = default)
 {
  Validation.Validate(request);
  var result = await executor.ExecuteAsync(ProcedureCatalog.All["usp_ServiceVersion_ClonePublished"], ParameterValues.From(request), actor, ct);
  return new ServiceVersion_ClonePublishedResponse { NewVersionID=result.Output<long?>("NewVersionID") };
 }
 public async Task<ServiceVersion_PublishDraftResponse> ServiceVersion_PublishDraftAsync(ServiceVersion_PublishDraftRequest request, Actor actor, CancellationToken ct = default)
 {
  Validation.Validate(request);
  var result = await executor.ExecuteAsync(ProcedureCatalog.All["usp_ServiceVersion_PublishDraft"], ParameterValues.From(request), actor, ct);
  return new ServiceVersion_PublishDraftResponse {  };
 }
 public async Task<ServiceVersion_RetirePublishedResponse> ServiceVersion_RetirePublishedAsync(ServiceVersion_RetirePublishedRequest request, Actor actor, CancellationToken ct = default)
 {
  Validation.Validate(request);
  var result = await executor.ExecuteAsync(ProcedureCatalog.All["usp_ServiceVersion_RetirePublished"], ParameterValues.From(request), actor, ct);
  return new ServiceVersion_RetirePublishedResponse {  };
 }
 public async Task<ServiceField_AddResponse> ServiceField_AddAsync(ServiceField_AddRequest request, Actor actor, CancellationToken ct = default)
 {
  Validation.Validate(request);
  var result = await executor.ExecuteAsync(ProcedureCatalog.All["usp_ServiceField_Add"], ParameterValues.From(request), actor, ct);
  return new ServiceField_AddResponse { NewID=result.Output<long?>("NewID") };
 }
 public async Task<ServiceField_UpdateResponse> ServiceField_UpdateAsync(ServiceField_UpdateRequest request, Actor actor, CancellationToken ct = default)
 {
  Validation.Validate(request);
  var result = await executor.ExecuteAsync(ProcedureCatalog.All["usp_ServiceField_Update"], ParameterValues.From(request), actor, ct);
  return new ServiceField_UpdateResponse {  };
 }
 public async Task<ServiceField_DeactivateResponse> ServiceField_DeactivateAsync(ServiceField_DeactivateRequest request, Actor actor, CancellationToken ct = default)
 {
  Validation.Validate(request);
  var result = await executor.ExecuteAsync(ProcedureCatalog.All["usp_ServiceField_Deactivate"], ParameterValues.From(request), actor, ct);
  return new ServiceField_DeactivateResponse {  };
 }
 public async Task<ServiceField_GetByVersionResponse> ServiceField_GetByVersionAsync(ServiceField_GetByVersionRequest request, Actor actor, CancellationToken ct = default)
 {
  Validation.Validate(request);
  var result = await executor.ExecuteAsync(ProcedureCatalog.All["usp_ServiceField_GetByVersion"], ParameterValues.From(request), actor, ct);
  return new ServiceField_GetByVersionResponse { Rows1=result.Rows<ServiceField_GetByVersionRow1>(0) };
 }
 public async Task<ServiceFieldOption_AddResponse> ServiceFieldOption_AddAsync(ServiceFieldOption_AddRequest request, Actor actor, CancellationToken ct = default)
 {
  Validation.Validate(request);
  var result = await executor.ExecuteAsync(ProcedureCatalog.All["usp_ServiceFieldOption_Add"], ParameterValues.From(request), actor, ct);
  return new ServiceFieldOption_AddResponse { NewID=result.Output<long?>("NewID") };
 }
 public async Task<ServiceFieldOption_UpdateResponse> ServiceFieldOption_UpdateAsync(ServiceFieldOption_UpdateRequest request, Actor actor, CancellationToken ct = default)
 {
  Validation.Validate(request);
  var result = await executor.ExecuteAsync(ProcedureCatalog.All["usp_ServiceFieldOption_Update"], ParameterValues.From(request), actor, ct);
  return new ServiceFieldOption_UpdateResponse {  };
 }
 public async Task<ServiceFieldOption_GetResponse> ServiceFieldOption_GetAsync(ServiceFieldOption_GetRequest request, Actor actor, CancellationToken ct = default)
 {
  Validation.Validate(request);
  var result = await executor.ExecuteAsync(ProcedureCatalog.All["usp_ServiceFieldOption_Get"], ParameterValues.From(request), actor, ct);
  return new ServiceFieldOption_GetResponse { Rows1=result.Rows<ServiceFieldOption_GetRow1>(0) };
 }
 public async Task<DropdownList_CreateResponse> DropdownList_CreateAsync(DropdownList_CreateRequest request, Actor actor, CancellationToken ct = default)
 {
  Validation.Validate(request);
  var result = await executor.ExecuteAsync(ProcedureCatalog.All["usp_DropdownList_Create"], ParameterValues.From(request), actor, ct);
  return new DropdownList_CreateResponse { NewID=result.Output<long?>("NewID") };
 }
 public async Task<DropdownListVersion_CreateResponse> DropdownListVersion_CreateAsync(DropdownListVersion_CreateRequest request, Actor actor, CancellationToken ct = default)
 {
  Validation.Validate(request);
  var result = await executor.ExecuteAsync(ProcedureCatalog.All["usp_DropdownListVersion_Create"], ParameterValues.From(request), actor, ct);
  return new DropdownListVersion_CreateResponse { NewVersionID=result.Output<long?>("NewVersionID") };
 }
 public async Task<DropdownListOption_AddResponse> DropdownListOption_AddAsync(DropdownListOption_AddRequest request, Actor actor, CancellationToken ct = default)
 {
  Validation.Validate(request);
  var result = await executor.ExecuteAsync(ProcedureCatalog.All["usp_DropdownListOption_Add"], ParameterValues.From(request), actor, ct);
  return new DropdownListOption_AddResponse { NewID=result.Output<long?>("NewID") };
 }
 public async Task<DropdownListOption_UpdateResponse> DropdownListOption_UpdateAsync(DropdownListOption_UpdateRequest request, Actor actor, CancellationToken ct = default)
 {
  Validation.Validate(request);
  var result = await executor.ExecuteAsync(ProcedureCatalog.All["usp_DropdownListOption_Update"], ParameterValues.From(request), actor, ct);
  return new DropdownListOption_UpdateResponse {  };
 }
 public async Task<DropdownListVersion_PublishResponse> DropdownListVersion_PublishAsync(DropdownListVersion_PublishRequest request, Actor actor, CancellationToken ct = default)
 {
  Validation.Validate(request);
  var result = await executor.ExecuteAsync(ProcedureCatalog.All["usp_DropdownListVersion_Publish"], ParameterValues.From(request), actor, ct);
  return new DropdownListVersion_PublishResponse {  };
 }
 public async Task<DropdownList_GetPublishedOptionsResponse> DropdownList_GetPublishedOptionsAsync(DropdownList_GetPublishedOptionsRequest request, Actor actor, CancellationToken ct = default)
 {
  Validation.Validate(request);
  var result = await executor.ExecuteAsync(ProcedureCatalog.All["usp_DropdownList_GetPublishedOptions"], ParameterValues.From(request), actor, ct);
  return new DropdownList_GetPublishedOptionsResponse { Rows1=result.Rows<DropdownList_GetPublishedOptionsRow1>(0) };
 }
 public async Task<ServiceWorkflow_SaveResponse> ServiceWorkflow_SaveAsync(ServiceWorkflow_SaveRequest request, Actor actor, CancellationToken ct = default)
 {
  Validation.Validate(request);
  var result = await executor.ExecuteAsync(ProcedureCatalog.All["usp_ServiceWorkflow_Save"], ParameterValues.From(request), actor, ct);
  return new ServiceWorkflow_SaveResponse {  };
 }
 public async Task<ServiceWorkflow_GetResponse> ServiceWorkflow_GetAsync(ServiceWorkflow_GetRequest request, Actor actor, CancellationToken ct = default)
 {
  Validation.Validate(request);
  var result = await executor.ExecuteAsync(ProcedureCatalog.All["usp_ServiceWorkflow_Get"], ParameterValues.From(request), actor, ct);
  return new ServiceWorkflow_GetResponse { Rows1=result.Rows<ServiceWorkflow_GetRow1>(0) };
 }
 public async Task<ServiceNotificationConfig_SaveResponse> ServiceNotificationConfig_SaveAsync(ServiceNotificationConfig_SaveRequest request, Actor actor, CancellationToken ct = default)
 {
  Validation.Validate(request);
  var result = await executor.ExecuteAsync(ProcedureCatalog.All["usp_ServiceNotificationConfig_Save"], ParameterValues.From(request), actor, ct);
  return new ServiceNotificationConfig_SaveResponse {  };
 }
 public async Task<ServiceNotificationConfig_GetResponse> ServiceNotificationConfig_GetAsync(ServiceNotificationConfig_GetRequest request, Actor actor, CancellationToken ct = default)
 {
  Validation.Validate(request);
  var result = await executor.ExecuteAsync(ProcedureCatalog.All["usp_ServiceNotificationConfig_Get"], ParameterValues.From(request), actor, ct);
  return new ServiceNotificationConfig_GetResponse { Rows1=result.Rows<ServiceNotificationConfig_GetRow1>(0) };
 }
 public async Task<NotificationTemplate_CreateDraftResponse> NotificationTemplate_CreateDraftAsync(NotificationTemplate_CreateDraftRequest request, Actor actor, CancellationToken ct = default)
 {
  Validation.Validate(request);
  var result = await executor.ExecuteAsync(ProcedureCatalog.All["usp_NotificationTemplate_CreateDraft"], ParameterValues.From(request), actor, ct);
  return new NotificationTemplate_CreateDraftResponse { NewID=result.Output<long?>("NewID") };
 }
 public async Task<NotificationTemplate_UpdateDraftResponse> NotificationTemplate_UpdateDraftAsync(NotificationTemplate_UpdateDraftRequest request, Actor actor, CancellationToken ct = default)
 {
  Validation.Validate(request);
  var result = await executor.ExecuteAsync(ProcedureCatalog.All["usp_NotificationTemplate_UpdateDraft"], ParameterValues.From(request), actor, ct);
  return new NotificationTemplate_UpdateDraftResponse {  };
 }
 public async Task<NotificationTemplate_PublishResponse> NotificationTemplate_PublishAsync(NotificationTemplate_PublishRequest request, Actor actor, CancellationToken ct = default)
 {
  Validation.Validate(request);
  var result = await executor.ExecuteAsync(ProcedureCatalog.All["usp_NotificationTemplate_Publish"], ParameterValues.From(request), actor, ct);
  return new NotificationTemplate_PublishResponse {  };
 }
 public async Task<NotificationTemplate_GetPublishedResponse> NotificationTemplate_GetPublishedAsync(NotificationTemplate_GetPublishedRequest request, Actor actor, CancellationToken ct = default)
 {
  Validation.Validate(request);
  var result = await executor.ExecuteAsync(ProcedureCatalog.All["usp_NotificationTemplate_GetPublished"], ParameterValues.From(request), actor, ct);
  return new NotificationTemplate_GetPublishedResponse { Rows1=result.Rows<NotificationTemplate_GetPublishedRow1>(0) };
 }
 public async Task<Ticket_CreateResponse> Ticket_CreateAsync(Ticket_CreateRequest request, Actor actor, CancellationToken ct = default)
 {
  Validation.Validate(request);
  var result = await executor.ExecuteAsync(ProcedureCatalog.All["usp_Ticket_Create"], ParameterValues.From(request), actor, ct);
  return new Ticket_CreateResponse { RequestID=result.Output<long?>("RequestID"), TicketNumber=result.Output<string?>("TicketNumber") };
 }
 public async Task<Ticket_ApproveResponse> Ticket_ApproveAsync(Ticket_ApproveRequest request, Actor actor, CancellationToken ct = default)
 {
  Validation.Validate(request);
  var result = await executor.ExecuteAsync(ProcedureCatalog.All["usp_Ticket_Approve"], ParameterValues.From(request), actor, ct);
  return new Ticket_ApproveResponse {  };
 }
 public async Task<Ticket_RejectResponse> Ticket_RejectAsync(Ticket_RejectRequest request, Actor actor, CancellationToken ct = default)
 {
  Validation.Validate(request);
  var result = await executor.ExecuteAsync(ProcedureCatalog.All["usp_Ticket_Reject"], ParameterValues.From(request), actor, ct);
  return new Ticket_RejectResponse {  };
 }
 public async Task<Ticket_UpdateStatusResponse> Ticket_UpdateStatusAsync(Ticket_UpdateStatusRequest request, Actor actor, CancellationToken ct = default)
 {
  Validation.Validate(request);
  var result = await executor.ExecuteAsync(ProcedureCatalog.All["usp_Ticket_UpdateStatus"], ParameterValues.From(request), actor, ct);
  return new Ticket_UpdateStatusResponse {  };
 }
 public async Task<Ticket_GetByIDResponse> Ticket_GetByIDAsync(Ticket_GetByIDRequest request, Actor actor, CancellationToken ct = default)
 {
  Validation.Validate(request);
  var result = await executor.ExecuteAsync(ProcedureCatalog.All["usp_Ticket_GetByID"], ParameterValues.From(request), actor, ct);
  return new Ticket_GetByIDResponse { Rows1=result.Rows<Ticket_GetByIDRow1>(0), Rows2=result.Rows<Ticket_GetByIDRow2>(1) };
 }
 public async Task<Ticket_GetByNumberResponse> Ticket_GetByNumberAsync(Ticket_GetByNumberRequest request, Actor actor, CancellationToken ct = default)
 {
  Validation.Validate(request);
  var result = await executor.ExecuteAsync(ProcedureCatalog.All["usp_Ticket_GetByNumber"], ParameterValues.From(request), actor, ct);
  return new Ticket_GetByNumberResponse { Rows1=result.Rows<Ticket_GetByNumberRow1>(0), Rows2=result.Rows<Ticket_GetByNumberRow2>(1) };
 }
 public async Task<Ticket_SearchResponse> Ticket_SearchAsync(Ticket_SearchRequest request, Actor actor, CancellationToken ct = default)
 {
  Validation.Validate(request);
  var result = await executor.ExecuteAsync(ProcedureCatalog.All["usp_Ticket_Search"], ParameterValues.From(request), actor, ct);
  return new Ticket_SearchResponse { Rows1=result.Rows<Ticket_SearchRow1>(0) };
 }
 public async Task<Ticket_GetRecentByCustomerResponse> Ticket_GetRecentByCustomerAsync(Ticket_GetRecentByCustomerRequest request, Actor actor, CancellationToken ct = default)
 {
  Validation.Validate(request);
  var result = await executor.ExecuteAsync(ProcedureCatalog.All["usp_Ticket_GetRecentByCustomer"], ParameterValues.From(request), actor, ct);
  return new Ticket_GetRecentByCustomerResponse { Rows1=result.Rows<Ticket_GetRecentByCustomerRow1>(0) };
 }
 public async Task<Ticket_GetPendingApprovalResponse> Ticket_GetPendingApprovalAsync(Ticket_GetPendingApprovalRequest request, Actor actor, CancellationToken ct = default)
 {
  Validation.Validate(request);
  var result = await executor.ExecuteAsync(ProcedureCatalog.All["usp_Ticket_GetPendingApproval"], ParameterValues.From(request), actor, ct);
  return new Ticket_GetPendingApprovalResponse { Rows1=result.Rows<Ticket_GetPendingApprovalRow1>(0) };
 }
 public async Task<Ticket_GetPriorityQueueResponse> Ticket_GetPriorityQueueAsync(Ticket_GetPriorityQueueRequest request, Actor actor, CancellationToken ct = default)
 {
  Validation.Validate(request);
  var result = await executor.ExecuteAsync(ProcedureCatalog.All["usp_Ticket_GetPriorityQueue"], ParameterValues.From(request), actor, ct);
  return new Ticket_GetPriorityQueueResponse { Rows1=result.Rows<Ticket_GetPriorityQueueRow1>(0) };
 }
 public async Task<Ticket_GetStatusHistoryResponse> Ticket_GetStatusHistoryAsync(Ticket_GetStatusHistoryRequest request, Actor actor, CancellationToken ct = default)
 {
  Validation.Validate(request);
  var result = await executor.ExecuteAsync(ProcedureCatalog.All["usp_Ticket_GetStatusHistory"], ParameterValues.From(request), actor, ct);
  return new Ticket_GetStatusHistoryResponse { Rows1=result.Rows<Ticket_GetStatusHistoryRow1>(0) };
 }
 public async Task<Ticket_GetActivityHistoryResponse> Ticket_GetActivityHistoryAsync(Ticket_GetActivityHistoryRequest request, Actor actor, CancellationToken ct = default)
 {
  Validation.Validate(request);
  var result = await executor.ExecuteAsync(ProcedureCatalog.All["usp_Ticket_GetActivityHistory"], ParameterValues.From(request), actor, ct);
  return new Ticket_GetActivityHistoryResponse { Rows1=result.Rows<Ticket_GetActivityHistoryRow1>(0) };
 }
 public async Task<FollowUp_AddResponse> FollowUp_AddAsync(FollowUp_AddRequest request, Actor actor, CancellationToken ct = default)
 {
  Validation.Validate(request);
  var result = await executor.ExecuteAsync(ProcedureCatalog.All["usp_FollowUp_Add"], ParameterValues.From(request), actor, ct);
  return new FollowUp_AddResponse { FollowUpID=result.Output<long?>("FollowUpID") };
 }
 public async Task<FollowUp_GetByTicketResponse> FollowUp_GetByTicketAsync(FollowUp_GetByTicketRequest request, Actor actor, CancellationToken ct = default)
 {
  Validation.Validate(request);
  var result = await executor.ExecuteAsync(ProcedureCatalog.All["usp_FollowUp_GetByTicket"], ParameterValues.From(request), actor, ct);
  return new FollowUp_GetByTicketResponse { Rows1=result.Rows<FollowUp_GetByTicketRow1>(0) };
 }
 public async Task<FollowUpSchedule_CreateResponse> FollowUpSchedule_CreateAsync(FollowUpSchedule_CreateRequest request, Actor actor, CancellationToken ct = default)
 {
  Validation.Validate(request);
  var result = await executor.ExecuteAsync(ProcedureCatalog.All["usp_FollowUpSchedule_Create"], ParameterValues.From(request), actor, ct);
  return new FollowUpSchedule_CreateResponse { ScheduleID=result.Output<long?>("ScheduleID") };
 }
 public async Task<FollowUpSchedule_GetByTicketResponse> FollowUpSchedule_GetByTicketAsync(FollowUpSchedule_GetByTicketRequest request, Actor actor, CancellationToken ct = default)
 {
  Validation.Validate(request);
  var result = await executor.ExecuteAsync(ProcedureCatalog.All["usp_FollowUpSchedule_GetByTicket"], ParameterValues.From(request), actor, ct);
  return new FollowUpSchedule_GetByTicketResponse { Rows1=result.Rows<FollowUpSchedule_GetByTicketRow1>(0) };
 }
 public async Task<FollowUpSchedule_GetDueResponse> FollowUpSchedule_GetDueAsync(FollowUpSchedule_GetDueRequest request, Actor actor, CancellationToken ct = default)
 {
  Validation.Validate(request);
  var result = await executor.ExecuteAsync(ProcedureCatalog.All["usp_FollowUpSchedule_GetDue"], ParameterValues.From(request), actor, ct);
  return new FollowUpSchedule_GetDueResponse { Rows1=result.Rows<FollowUpSchedule_GetDueRow1>(0) };
 }
 public async Task<FollowUpSchedule_CompleteResponse> FollowUpSchedule_CompleteAsync(FollowUpSchedule_CompleteRequest request, Actor actor, CancellationToken ct = default)
 {
  Validation.Validate(request);
  var result = await executor.ExecuteAsync(ProcedureCatalog.All["usp_FollowUpSchedule_Complete"], ParameterValues.From(request), actor, ct);
  return new FollowUpSchedule_CompleteResponse {  };
 }
 public async Task<FollowUpSchedule_CancelResponse> FollowUpSchedule_CancelAsync(FollowUpSchedule_CancelRequest request, Actor actor, CancellationToken ct = default)
 {
  Validation.Validate(request);
  var result = await executor.ExecuteAsync(ProcedureCatalog.All["usp_FollowUpSchedule_Cancel"], ParameterValues.From(request), actor, ct);
  return new FollowUpSchedule_CancelResponse {  };
 }
 public async Task<Forwarding_CreateResponse> Forwarding_CreateAsync(Forwarding_CreateRequest request, Actor actor, CancellationToken ct = default)
 {
  Validation.Validate(request);
  var result = await executor.ExecuteAsync(ProcedureCatalog.All["usp_Forwarding_Create"], ParameterValues.From(request), actor, ct);
  return new Forwarding_CreateResponse { ForwardingID=result.Output<long?>("ForwardingID") };
 }
 public async Task<Forwarding_GetByTicketResponse> Forwarding_GetByTicketAsync(Forwarding_GetByTicketRequest request, Actor actor, CancellationToken ct = default)
 {
  Validation.Validate(request);
  var result = await executor.ExecuteAsync(ProcedureCatalog.All["usp_Forwarding_GetByTicket"], ParameterValues.From(request), actor, ct);
  return new Forwarding_GetByTicketResponse { Rows1=result.Rows<Forwarding_GetByTicketRow1>(0) };
 }
 public async Task<Forwarding_GetPendingResponse> Forwarding_GetPendingAsync(Forwarding_GetPendingRequest request, Actor actor, CancellationToken ct = default)
 {
  Validation.Validate(request);
  var result = await executor.ExecuteAsync(ProcedureCatalog.All["usp_Forwarding_GetPending"], ParameterValues.From(request), actor, ct);
  return new Forwarding_GetPendingResponse { Rows1=result.Rows<Forwarding_GetPendingRow1>(0) };
 }
 public async Task<Forwarding_MarkSubmittingResponse> Forwarding_MarkSubmittingAsync(Forwarding_MarkSubmittingRequest request, Actor actor, CancellationToken ct = default)
 {
  Validation.Validate(request);
  var result = await executor.ExecuteAsync(ProcedureCatalog.All["usp_Forwarding_MarkSubmitting"], ParameterValues.From(request), actor, ct);
  return new Forwarding_MarkSubmittingResponse {  };
 }
 public async Task<Forwarding_RecordAcceptedResponse> Forwarding_RecordAcceptedAsync(Forwarding_RecordAcceptedRequest request, Actor actor, CancellationToken ct = default)
 {
  Validation.Validate(request);
  var result = await executor.ExecuteAsync(ProcedureCatalog.All["usp_Forwarding_RecordAccepted"], ParameterValues.From(request), actor, ct);
  return new Forwarding_RecordAcceptedResponse {  };
 }
 public async Task<Forwarding_RecordFailedResponse> Forwarding_RecordFailedAsync(Forwarding_RecordFailedRequest request, Actor actor, CancellationToken ct = default)
 {
  Validation.Validate(request);
  var result = await executor.ExecuteAsync(ProcedureCatalog.All["usp_Forwarding_RecordFailed"], ParameterValues.From(request), actor, ct);
  return new Forwarding_RecordFailedResponse {  };
 }
 public async Task<Communication_CreateResponse> Communication_CreateAsync(Communication_CreateRequest request, Actor actor, CancellationToken ct = default)
 {
  Validation.Validate(request);
  var result = await executor.ExecuteAsync(ProcedureCatalog.All["usp_Communication_Create"], ParameterValues.From(request), actor, ct);
  return new Communication_CreateResponse { CommunicationID=result.Output<long?>("CommunicationID") };
 }
 public async Task<Communication_GetByTicketResponse> Communication_GetByTicketAsync(Communication_GetByTicketRequest request, Actor actor, CancellationToken ct = default)
 {
  Validation.Validate(request);
  var result = await executor.ExecuteAsync(ProcedureCatalog.All["usp_Communication_GetByTicket"], ParameterValues.From(request), actor, ct);
  return new Communication_GetByTicketResponse { Rows1=result.Rows<Communication_GetByTicketRow1>(0) };
 }
 public async Task<Communication_GetByIDResponse> Communication_GetByIDAsync(Communication_GetByIDRequest request, Actor actor, CancellationToken ct = default)
 {
  Validation.Validate(request);
  var result = await executor.ExecuteAsync(ProcedureCatalog.All["usp_Communication_GetByID"], ParameterValues.From(request), actor, ct);
  return new Communication_GetByIDResponse { Rows1=result.Rows<Communication_GetByIDRow1>(0) };
 }
 public async Task<CommunicationOutbox_ClaimResponse> CommunicationOutbox_ClaimAsync(CommunicationOutbox_ClaimRequest request, Actor actor, CancellationToken ct = default)
 {
  Validation.Validate(request);
  var result = await executor.ExecuteAsync(ProcedureCatalog.All["usp_CommunicationOutbox_Claim"], ParameterValues.From(request), actor, ct);
  return new CommunicationOutbox_ClaimResponse { Rows1=result.Rows<CommunicationOutbox_ClaimRow1>(0) };
 }
 public async Task<CommunicationAttempt_StartResponse> CommunicationAttempt_StartAsync(CommunicationAttempt_StartRequest request, Actor actor, CancellationToken ct = default)
 {
  Validation.Validate(request);
  var result = await executor.ExecuteAsync(ProcedureCatalog.All["usp_CommunicationAttempt_Start"], ParameterValues.From(request), actor, ct);
  return new CommunicationAttempt_StartResponse { AttemptID=result.Output<long?>("AttemptID") };
 }
 public async Task<CommunicationAttempt_CompleteResponse> CommunicationAttempt_CompleteAsync(CommunicationAttempt_CompleteRequest request, Actor actor, CancellationToken ct = default)
 {
  Validation.Validate(request);
  var result = await executor.ExecuteAsync(ProcedureCatalog.All["usp_CommunicationAttempt_Complete"], ParameterValues.From(request), actor, ct);
  return new CommunicationAttempt_CompleteResponse {  };
 }
 public async Task<Communication_RecordProviderResultResponse> Communication_RecordProviderResultAsync(Communication_RecordProviderResultRequest request, Actor actor, CancellationToken ct = default)
 {
  Validation.Validate(request);
  var result = await executor.ExecuteAsync(ProcedureCatalog.All["usp_Communication_RecordProviderResult"], ParameterValues.From(request), actor, ct);
  return new Communication_RecordProviderResultResponse {  };
 }
 public async Task<CommunicationOutbox_CompleteResponse> CommunicationOutbox_CompleteAsync(CommunicationOutbox_CompleteRequest request, Actor actor, CancellationToken ct = default)
 {
  Validation.Validate(request);
  var result = await executor.ExecuteAsync(ProcedureCatalog.All["usp_CommunicationOutbox_Complete"], ParameterValues.From(request), actor, ct);
  return new CommunicationOutbox_CompleteResponse {  };
 }
 public async Task<CommunicationOutbox_FailResponse> CommunicationOutbox_FailAsync(CommunicationOutbox_FailRequest request, Actor actor, CancellationToken ct = default)
 {
  Validation.Validate(request);
  var result = await executor.ExecuteAsync(ProcedureCatalog.All["usp_CommunicationOutbox_Fail"], ParameterValues.From(request), actor, ct);
  return new CommunicationOutbox_FailResponse {  };
 }
 public async Task<CommunicationOutbox_ScheduleRetryResponse> CommunicationOutbox_ScheduleRetryAsync(CommunicationOutbox_ScheduleRetryRequest request, Actor actor, CancellationToken ct = default)
 {
  Validation.Validate(request);
  var result = await executor.ExecuteAsync(ProcedureCatalog.All["usp_CommunicationOutbox_ScheduleRetry"], ParameterValues.From(request), actor, ct);
  return new CommunicationOutbox_ScheduleRetryResponse {  };
 }
 public async Task<CommunicationOutbox_ManualRetryResponse> CommunicationOutbox_ManualRetryAsync(CommunicationOutbox_ManualRetryRequest request, Actor actor, CancellationToken ct = default)
 {
  Validation.Validate(request);
  var result = await executor.ExecuteAsync(ProcedureCatalog.All["usp_CommunicationOutbox_ManualRetry"], ParameterValues.From(request), actor, ct);
  return new CommunicationOutbox_ManualRetryResponse {  };
 }
 public async Task<TrackingToken_CreateResponse> TrackingToken_CreateAsync(TrackingToken_CreateRequest request, Actor actor, CancellationToken ct = default)
 {
  Validation.Validate(request);
  var result = await executor.ExecuteAsync(ProcedureCatalog.All["usp_TrackingToken_Create"], ParameterValues.From(request), actor, ct);
  return new TrackingToken_CreateResponse { TrackingTokenID=result.Output<long?>("TrackingTokenID") };
 }
 public async Task<TrackingToken_ValidateResponse> TrackingToken_ValidateAsync(TrackingToken_ValidateRequest request, Actor actor, CancellationToken ct = default)
 {
  Validation.Validate(request);
  var result = await executor.ExecuteAsync(ProcedureCatalog.All["usp_TrackingToken_Validate"], ParameterValues.From(request), actor, ct);
  return new TrackingToken_ValidateResponse { Rows1=result.Rows<TrackingToken_ValidateRow1>(0) };
 }
 public async Task<TrackingToken_RecordAccessResponse> TrackingToken_RecordAccessAsync(TrackingToken_RecordAccessRequest request, Actor actor, CancellationToken ct = default)
 {
  Validation.Validate(request);
  var result = await executor.ExecuteAsync(ProcedureCatalog.All["usp_TrackingToken_RecordAccess"], ParameterValues.From(request), actor, ct);
  return new TrackingToken_RecordAccessResponse {  };
 }
 public async Task<TrackingToken_RevokeResponse> TrackingToken_RevokeAsync(TrackingToken_RevokeRequest request, Actor actor, CancellationToken ct = default)
 {
  Validation.Validate(request);
  var result = await executor.ExecuteAsync(ProcedureCatalog.All["usp_TrackingToken_Revoke"], ParameterValues.From(request), actor, ct);
  return new TrackingToken_RevokeResponse {  };
 }
 public async Task<TrackingToken_ReplaceResponse> TrackingToken_ReplaceAsync(TrackingToken_ReplaceRequest request, Actor actor, CancellationToken ct = default)
 {
  Validation.Validate(request);
  var result = await executor.ExecuteAsync(ProcedureCatalog.All["usp_TrackingToken_Replace"], ParameterValues.From(request), actor, ct);
  return new TrackingToken_ReplaceResponse { NewTrackingTokenID=result.Output<long?>("NewTrackingTokenID") };
 }
 public async Task<Tracking_GetPublicStatusResponse> Tracking_GetPublicStatusAsync(Tracking_GetPublicStatusRequest request, Actor actor, CancellationToken ct = default)
 {
  Validation.Validate(request);
  var result = await executor.ExecuteAsync(ProcedureCatalog.All["usp_Tracking_GetPublicStatus"], ParameterValues.From(request), actor, ct);
  return new Tracking_GetPublicStatusResponse { Rows1=result.Rows<Tracking_GetPublicStatusRow1>(0) };
 }
 public async Task<Tracking_GetPublicTimelineResponse> Tracking_GetPublicTimelineAsync(Tracking_GetPublicTimelineRequest request, Actor actor, CancellationToken ct = default)
 {
  Validation.Validate(request);
  var result = await executor.ExecuteAsync(ProcedureCatalog.All["usp_Tracking_GetPublicTimeline"], ParameterValues.From(request), actor, ct);
  return new Tracking_GetPublicTimelineResponse { Rows1=result.Rows<Tracking_GetPublicTimelineRow1>(0) };
 }
 public async Task<CustomerStatusMapping_GetAllResponse> CustomerStatusMapping_GetAllAsync(CustomerStatusMapping_GetAllRequest request, Actor actor, CancellationToken ct = default)
 {
  Validation.Validate(request);
  var result = await executor.ExecuteAsync(ProcedureCatalog.All["usp_CustomerStatusMapping_GetAll"], ParameterValues.From(request), actor, ct);
  return new CustomerStatusMapping_GetAllResponse { Rows1=result.Rows<CustomerStatusMapping_GetAllRow1>(0) };
 }
 public async Task<CustomerStatusMapping_UpdateResponse> CustomerStatusMapping_UpdateAsync(CustomerStatusMapping_UpdateRequest request, Actor actor, CancellationToken ct = default)
 {
  Validation.Validate(request);
  var result = await executor.ExecuteAsync(ProcedureCatalog.All["usp_CustomerStatusMapping_Update"], ParameterValues.From(request), actor, ct);
  return new CustomerStatusMapping_UpdateResponse {  };
 }
 public async Task<AuthenticationLog_CreateResponse> AuthenticationLog_CreateAsync(AuthenticationLog_CreateRequest request, Actor actor, CancellationToken ct = default)
 {
  Validation.Validate(request);
  var result = await executor.ExecuteAsync(ProcedureCatalog.All["usp_AuthenticationLog_Create"], ParameterValues.From(request), actor, ct);
  return new AuthenticationLog_CreateResponse { AuthenticationLogID=result.Output<long?>("AuthenticationLogID") };
 }
 public async Task<AuditLog_CreateResponse> AuditLog_CreateAsync(AuditLog_CreateRequest request, Actor actor, CancellationToken ct = default)
 {
  Validation.Validate(request);
  var result = await executor.ExecuteAsync(ProcedureCatalog.All["usp_AuditLog_Create"], ParameterValues.From(request), actor, ct);
  return new AuditLog_CreateResponse { AuditID=result.Output<long?>("AuditID") };
 }
 public async Task<IntegrationLog_StartResponse> IntegrationLog_StartAsync(IntegrationLog_StartRequest request, Actor actor, CancellationToken ct = default)
 {
  Validation.Validate(request);
  var result = await executor.ExecuteAsync(ProcedureCatalog.All["usp_IntegrationLog_Start"], ParameterValues.From(request), actor, ct);
  return new IntegrationLog_StartResponse { IntegrationLogID=result.Output<long?>("IntegrationLogID") };
 }
 public async Task<IntegrationLog_CompleteResponse> IntegrationLog_CompleteAsync(IntegrationLog_CompleteRequest request, Actor actor, CancellationToken ct = default)
 {
  Validation.Validate(request);
  var result = await executor.ExecuteAsync(ProcedureCatalog.All["usp_IntegrationLog_Complete"], ParameterValues.From(request), actor, ct);
  return new IntegrationLog_CompleteResponse {  };
 }
 public async Task<AuthenticationLog_SearchResponse> AuthenticationLog_SearchAsync(AuthenticationLog_SearchRequest request, Actor actor, CancellationToken ct = default)
 {
  Validation.Validate(request);
  var result = await executor.ExecuteAsync(ProcedureCatalog.All["usp_AuthenticationLog_Search"], ParameterValues.From(request), actor, ct);
  return new AuthenticationLog_SearchResponse { Rows1=result.Rows<AuthenticationLog_SearchRow1>(0) };
 }
 public async Task<AuditLog_SearchResponse> AuditLog_SearchAsync(AuditLog_SearchRequest request, Actor actor, CancellationToken ct = default)
 {
  Validation.Validate(request);
  var result = await executor.ExecuteAsync(ProcedureCatalog.All["usp_AuditLog_Search"], ParameterValues.From(request), actor, ct);
  return new AuditLog_SearchResponse { Rows1=result.Rows<AuditLog_SearchRow1>(0) };
 }
 public async Task<AuditLog_GetByTicketResponse> AuditLog_GetByTicketAsync(AuditLog_GetByTicketRequest request, Actor actor, CancellationToken ct = default)
 {
  Validation.Validate(request);
  var result = await executor.ExecuteAsync(ProcedureCatalog.All["usp_AuditLog_GetByTicket"], ParameterValues.From(request), actor, ct);
  return new AuditLog_GetByTicketResponse { Rows1=result.Rows<AuditLog_GetByTicketRow1>(0) };
 }
 public async Task<AuditLog_GetByActorResponse> AuditLog_GetByActorAsync(AuditLog_GetByActorRequest request, Actor actor, CancellationToken ct = default)
 {
  Validation.Validate(request);
  var result = await executor.ExecuteAsync(ProcedureCatalog.All["usp_AuditLog_GetByActor"], ParameterValues.From(request), actor, ct);
  return new AuditLog_GetByActorResponse { Rows1=result.Rows<AuditLog_GetByActorRow1>(0) };
 }
 public async Task<IntegrationLog_SearchResponse> IntegrationLog_SearchAsync(IntegrationLog_SearchRequest request, Actor actor, CancellationToken ct = default)
 {
  Validation.Validate(request);
  var result = await executor.ExecuteAsync(ProcedureCatalog.All["usp_IntegrationLog_Search"], ParameterValues.From(request), actor, ct);
  return new IntegrationLog_SearchResponse { Rows1=result.Rows<IntegrationLog_SearchRow1>(0) };
 }
 public async Task<IntegrationLog_GetFailuresResponse> IntegrationLog_GetFailuresAsync(IntegrationLog_GetFailuresRequest request, Actor actor, CancellationToken ct = default)
 {
  Validation.Validate(request);
  var result = await executor.ExecuteAsync(ProcedureCatalog.All["usp_IntegrationLog_GetFailures"], ParameterValues.From(request), actor, ct);
  return new IntegrationLog_GetFailuresResponse { Rows1=result.Rows<IntegrationLog_GetFailuresRow1>(0) };
 }
 public async Task<Ticket_GetDashboardSummaryResponse> Ticket_GetDashboardSummaryAsync(Ticket_GetDashboardSummaryRequest request, Actor actor, CancellationToken ct = default)
 {
  Validation.Validate(request);
  var result = await executor.ExecuteAsync(ProcedureCatalog.All["usp_Ticket_GetDashboardSummary"], ParameterValues.From(request), actor, ct);
  return new Ticket_GetDashboardSummaryResponse { Rows1=result.Rows<Ticket_GetDashboardSummaryRow1>(0) };
 }
 public async Task<Dashboard_SummaryByStatusResponse> Dashboard_SummaryByStatusAsync(Dashboard_SummaryByStatusRequest request, Actor actor, CancellationToken ct = default)
 {
  Validation.Validate(request);
  var result = await executor.ExecuteAsync(ProcedureCatalog.All["usp_Dashboard_SummaryByStatus"], ParameterValues.From(request), actor, ct);
  return new Dashboard_SummaryByStatusResponse { Rows1=result.Rows<Dashboard_SummaryByStatusRow1>(0) };
 }
 public async Task<Dashboard_PendingApprovalCountResponse> Dashboard_PendingApprovalCountAsync(Dashboard_PendingApprovalCountRequest request, Actor actor, CancellationToken ct = default)
 {
  Validation.Validate(request);
  var result = await executor.ExecuteAsync(ProcedureCatalog.All["usp_Dashboard_PendingApprovalCount"], ParameterValues.From(request), actor, ct);
  return new Dashboard_PendingApprovalCountResponse { Rows1=result.Rows<Dashboard_PendingApprovalCountRow1>(0) };
 }
 public async Task<Dashboard_CriticalHighResponse> Dashboard_CriticalHighAsync(Dashboard_CriticalHighRequest request, Actor actor, CancellationToken ct = default)
 {
  Validation.Validate(request);
  var result = await executor.ExecuteAsync(ProcedureCatalog.All["usp_Dashboard_CriticalHigh"], ParameterValues.From(request), actor, ct);
  return new Dashboard_CriticalHighResponse { Rows1=result.Rows<Dashboard_CriticalHighRow1>(0) };
 }
 public async Task<Dashboard_OverdueApprovalResponse> Dashboard_OverdueApprovalAsync(Dashboard_OverdueApprovalRequest request, Actor actor, CancellationToken ct = default)
 {
  Validation.Validate(request);
  var result = await executor.ExecuteAsync(ProcedureCatalog.All["usp_Dashboard_OverdueApproval"], ParameterValues.From(request), actor, ct);
  return new Dashboard_OverdueApprovalResponse { Rows1=result.Rows<Dashboard_OverdueApprovalRow1>(0) };
 }
 public async Task<Dashboard_OverdueResolutionResponse> Dashboard_OverdueResolutionAsync(Dashboard_OverdueResolutionRequest request, Actor actor, CancellationToken ct = default)
 {
  Validation.Validate(request);
  var result = await executor.ExecuteAsync(ProcedureCatalog.All["usp_Dashboard_OverdueResolution"], ParameterValues.From(request), actor, ct);
  return new Dashboard_OverdueResolutionResponse { Rows1=result.Rows<Dashboard_OverdueResolutionRow1>(0) };
 }
 public async Task<Dashboard_SentToBranchResponse> Dashboard_SentToBranchAsync(Dashboard_SentToBranchRequest request, Actor actor, CancellationToken ct = default)
 {
  Validation.Validate(request);
  var result = await executor.ExecuteAsync(ProcedureCatalog.All["usp_Dashboard_SentToBranch"], ParameterValues.From(request), actor, ct);
  return new Dashboard_SentToBranchResponse { Rows1=result.Rows<Dashboard_SentToBranchRow1>(0) };
 }
 public async Task<Dashboard_SentToDivisionResponse> Dashboard_SentToDivisionAsync(Dashboard_SentToDivisionRequest request, Actor actor, CancellationToken ct = default)
 {
  Validation.Validate(request);
  var result = await executor.ExecuteAsync(ProcedureCatalog.All["usp_Dashboard_SentToDivision"], ParameterValues.From(request), actor, ct);
  return new Dashboard_SentToDivisionResponse { Rows1=result.Rows<Dashboard_SentToDivisionRow1>(0) };
 }
 public async Task<Dashboard_CompletedResponse> Dashboard_CompletedAsync(Dashboard_CompletedRequest request, Actor actor, CancellationToken ct = default)
 {
  Validation.Validate(request);
  var result = await executor.ExecuteAsync(ProcedureCatalog.All["usp_Dashboard_Completed"], ParameterValues.From(request), actor, ct);
  return new Dashboard_CompletedResponse { Rows1=result.Rows<Dashboard_CompletedRow1>(0) };
 }
 public async Task<Dashboard_FailedResponse> Dashboard_FailedAsync(Dashboard_FailedRequest request, Actor actor, CancellationToken ct = default)
 {
  Validation.Validate(request);
  var result = await executor.ExecuteAsync(ProcedureCatalog.All["usp_Dashboard_Failed"], ParameterValues.From(request), actor, ct);
  return new Dashboard_FailedResponse { Rows1=result.Rows<Dashboard_FailedRow1>(0) };
 }
 public async Task<Dashboard_PendingFollowUpsResponse> Dashboard_PendingFollowUpsAsync(Dashboard_PendingFollowUpsRequest request, Actor actor, CancellationToken ct = default)
 {
  Validation.Validate(request);
  var result = await executor.ExecuteAsync(ProcedureCatalog.All["usp_Dashboard_PendingFollowUps"], ParameterValues.From(request), actor, ct);
  return new Dashboard_PendingFollowUpsResponse { Rows1=result.Rows<Dashboard_PendingFollowUpsRow1>(0) };
 }
 public async Task<Dashboard_OverdueFollowUpsResponse> Dashboard_OverdueFollowUpsAsync(Dashboard_OverdueFollowUpsRequest request, Actor actor, CancellationToken ct = default)
 {
  Validation.Validate(request);
  var result = await executor.ExecuteAsync(ProcedureCatalog.All["usp_Dashboard_OverdueFollowUps"], ParameterValues.From(request), actor, ct);
  return new Dashboard_OverdueFollowUpsResponse { Rows1=result.Rows<Dashboard_OverdueFollowUpsRow1>(0) };
 }
 public async Task<Dashboard_TicketVolumeByServiceResponse> Dashboard_TicketVolumeByServiceAsync(Dashboard_TicketVolumeByServiceRequest request, Actor actor, CancellationToken ct = default)
 {
  Validation.Validate(request);
  var result = await executor.ExecuteAsync(ProcedureCatalog.All["usp_Dashboard_TicketVolumeByService"], ParameterValues.From(request), actor, ct);
  return new Dashboard_TicketVolumeByServiceResponse { Rows1=result.Rows<Dashboard_TicketVolumeByServiceRow1>(0) };
 }
 public async Task<Dashboard_TicketVolumeByDateResponse> Dashboard_TicketVolumeByDateAsync(Dashboard_TicketVolumeByDateRequest request, Actor actor, CancellationToken ct = default)
 {
  Validation.Validate(request);
  var result = await executor.ExecuteAsync(ProcedureCatalog.All["usp_Dashboard_TicketVolumeByDate"], ParameterValues.From(request), actor, ct);
  return new Dashboard_TicketVolumeByDateResponse { Rows1=result.Rows<Dashboard_TicketVolumeByDateRow1>(0) };
 }
 public async Task<Dashboard_AgentCreatedTicketCountsResponse> Dashboard_AgentCreatedTicketCountsAsync(Dashboard_AgentCreatedTicketCountsRequest request, Actor actor, CancellationToken ct = default)
 {
  Validation.Validate(request);
  var result = await executor.ExecuteAsync(ProcedureCatalog.All["usp_Dashboard_AgentCreatedTicketCounts"], ParameterValues.From(request), actor, ct);
  return new Dashboard_AgentCreatedTicketCountsResponse { Rows1=result.Rows<Dashboard_AgentCreatedTicketCountsRow1>(0) };
 }
 public async Task<Dashboard_ManagerActivitySummaryResponse> Dashboard_ManagerActivitySummaryAsync(Dashboard_ManagerActivitySummaryRequest request, Actor actor, CancellationToken ct = default)
 {
  Validation.Validate(request);
  var result = await executor.ExecuteAsync(ProcedureCatalog.All["usp_Dashboard_ManagerActivitySummary"], ParameterValues.From(request), actor, ct);
  return new Dashboard_ManagerActivitySummaryResponse { Rows1=result.Rows<Dashboard_ManagerActivitySummaryRow1>(0) };
 }
 public async Task<CommunicationOutbox_SetMaxAttemptsResponse> CommunicationOutbox_SetMaxAttemptsAsync(CommunicationOutbox_SetMaxAttemptsRequest request, Actor actor, CancellationToken ct = default)
 {
  Validation.Validate(request);
  var result = await executor.ExecuteAsync(ProcedureCatalog.All["usp_CommunicationOutbox_SetMaxAttempts"], ParameterValues.From(request), actor, ct);
  return new CommunicationOutbox_SetMaxAttemptsResponse {  };
 }
}
