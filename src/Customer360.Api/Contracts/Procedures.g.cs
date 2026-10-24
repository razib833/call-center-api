#nullable enable
// Generated from SQL source; do not hand-edit. Result columns extracted with ScriptDom.
using System.ComponentModel.DataAnnotations;
namespace Customer360.Contracts;
public sealed record Priority_GetAllRequest
{
}
public sealed record Priority_GetAllRow1
{
 public int? PriorityID { get; init; }
 public string? PriorityCode { get; init; }
 public string? PriorityName { get; init; }
 public string? PriorityDescription { get; init; }
 public byte? PriorityRank { get; init; }
 public int? DisplayOrder { get; init; }
 public bool? IsActive { get; init; }
 public string? CreatedBy { get; init; }
 public DateTime? CreatedAt { get; init; }
 public string? UpdatedBy { get; init; }
 public DateTime? UpdatedAt { get; init; }
 public byte[]? RowVersion { get; init; }
}
public sealed record Priority_GetAllResponse
{
 public IReadOnlyList<Priority_GetAllRow1> Rows1 { get; init; } = [];
}
public sealed record Priority_GetActiveRequest
{
}
public sealed record Priority_GetActiveRow1
{
 public int? PriorityID { get; init; }
 public string? PriorityCode { get; init; }
 public string? PriorityName { get; init; }
 public string? PriorityDescription { get; init; }
 public byte? PriorityRank { get; init; }
 public int? DisplayOrder { get; init; }
 public bool? IsActive { get; init; }
 public string? CreatedBy { get; init; }
 public DateTime? CreatedAt { get; init; }
 public string? UpdatedBy { get; init; }
 public DateTime? UpdatedAt { get; init; }
 public byte[]? RowVersion { get; init; }
}
public sealed record Priority_GetActiveResponse
{
 public IReadOnlyList<Priority_GetActiveRow1> Rows1 { get; init; } = [];
}
public sealed record Priority_CreateRequest
{
 [Required] public required string PriorityCode { get; init; }
 [Required] public required string PriorityName { get; init; }
 public string? PriorityDescription { get; init; }
 public required byte PriorityRank { get; init; }
 public required int DisplayOrder { get; init; }
 public required bool IsActive { get; init; }
}
public sealed record Priority_CreateResponse
{
 public int? NewID { get; init; }
}
public sealed record Priority_UpdateRequest
{
 public required int PriorityID { get; init; }
 [Required] public required string PriorityName { get; init; }
 public string? PriorityDescription { get; init; }
 public required byte PriorityRank { get; init; }
 public required int DisplayOrder { get; init; }
 [MinLength(8), MaxLength(8)] public required byte[] ExpectedRowVersion { get; init; }
}
public sealed record Priority_UpdateResponse
{
}
public sealed record Priority_SetActiveRequest
{
 public required int PriorityID { get; init; }
 public required bool IsActive { get; init; }
 [MinLength(8), MaxLength(8)] public required byte[] ExpectedRowVersion { get; init; }
}
public sealed record Priority_SetActiveResponse
{
}
public sealed record RequestSource_GetAllRequest
{
}
public sealed record RequestSource_GetAllRow1
{
 public string? RequestSourceCode { get; init; }
 public string? RequestSourceName { get; init; }
 public string? SourceDescription { get; init; }
 public int? DisplayOrder { get; init; }
 public bool? IsActive { get; init; }
 public string? CreatedBy { get; init; }
 public DateTime? CreatedAt { get; init; }
 public string? UpdatedBy { get; init; }
 public DateTime? UpdatedAt { get; init; }
 public byte[]? RowVersion { get; init; }
}
public sealed record RequestSource_GetAllResponse
{
 public IReadOnlyList<RequestSource_GetAllRow1> Rows1 { get; init; } = [];
}
public sealed record RequestSource_GetActiveRequest
{
}
public sealed record RequestSource_GetActiveRow1
{
 public string? RequestSourceCode { get; init; }
 public string? RequestSourceName { get; init; }
 public string? SourceDescription { get; init; }
 public int? DisplayOrder { get; init; }
 public bool? IsActive { get; init; }
 public string? CreatedBy { get; init; }
 public DateTime? CreatedAt { get; init; }
 public string? UpdatedBy { get; init; }
 public DateTime? UpdatedAt { get; init; }
 public byte[]? RowVersion { get; init; }
}
public sealed record RequestSource_GetActiveResponse
{
 public IReadOnlyList<RequestSource_GetActiveRow1> Rows1 { get; init; } = [];
}
public sealed record RequestSource_CreateRequest
{
 [Required] public required string RequestSourceCode { get; init; }
 [Required] public required string RequestSourceName { get; init; }
 public string? SourceDescription { get; init; }
 public required int DisplayOrder { get; init; }
 public required bool IsActive { get; init; }
}
public sealed record RequestSource_CreateResponse
{
 public string? NewID { get; init; }
}
public sealed record RequestSource_UpdateRequest
{
 [Required] [MaxLength(30)] public required string RequestSourceCode { get; init; }
 [Required] public required string RequestSourceName { get; init; }
 public string? SourceDescription { get; init; }
 public required int DisplayOrder { get; init; }
 [MinLength(8), MaxLength(8)] public required byte[] ExpectedRowVersion { get; init; }
}
public sealed record RequestSource_UpdateResponse
{
}
public sealed record RequestSource_SetActiveRequest
{
 [Required] [MaxLength(30)] public required string RequestSourceCode { get; init; }
 public required bool IsActive { get; init; }
 [MinLength(8), MaxLength(8)] public required byte[] ExpectedRowVersion { get; init; }
}
public sealed record RequestSource_SetActiveResponse
{
}
public sealed record TicketStatus_GetAllRequest
{
}
public sealed record TicketStatus_GetAllRow1
{
 public int? StatusID { get; init; }
 public string? StatusCode { get; init; }
 public string? StatusName { get; init; }
 public string? StatusDescription { get; init; }
 public int? DisplayOrder { get; init; }
 public bool? IsTerminal { get; init; }
 public bool? IsActive { get; init; }
}
public sealed record TicketStatus_GetAllResponse
{
 public IReadOnlyList<TicketStatus_GetAllRow1> Rows1 { get; init; } = [];
}
public sealed record TicketStatus_GetActiveRequest
{
}
public sealed record TicketStatus_GetActiveRow1
{
 public int? StatusID { get; init; }
 public string? StatusCode { get; init; }
 public string? StatusName { get; init; }
 public string? StatusDescription { get; init; }
 public int? DisplayOrder { get; init; }
 public bool? IsTerminal { get; init; }
 public bool? IsActive { get; init; }
}
public sealed record TicketStatus_GetActiveResponse
{
 public IReadOnlyList<TicketStatus_GetActiveRow1> Rows1 { get; init; } = [];
}
public sealed record TicketStatusTransition_GetAllowedRequest
{
 public required long RequestID { get; init; }
}
public sealed record TicketStatusTransition_GetAllowedRow1
{
 public int? TransitionID { get; init; }
 public int? FromStatusID { get; init; }
 public int? ToStatusID { get; init; }
 public string? StatusCode { get; init; }
 public string? StatusName { get; init; }
 public bool? RequiresApproval { get; init; }
 public bool? RequiresRemarks { get; init; }
 public bool? RequiresForwardingConfirmation { get; init; }
}
public sealed record TicketStatusTransition_GetAllowedResponse
{
 public IReadOnlyList<TicketStatusTransition_GetAllowedRow1> Rows1 { get; init; } = [];
}
public sealed record ServiceCategory_CreateRequest
{
 [Required] public required string CategoryCode { get; init; }
 [Required] public required string CategoryName { get; init; }
 public string? CategoryDescription { get; init; }
 public required int DisplayOrder { get; init; }
 public required bool IsActive { get; init; }
}
public sealed record ServiceCategory_CreateResponse
{
 public int? NewID { get; init; }
}
public sealed record ServiceCategory_UpdateRequest
{
 public required int CategoryID { get; init; }
 [Required] public required string CategoryName { get; init; }
 public string? CategoryDescription { get; init; }
 public required int DisplayOrder { get; init; }
}
public sealed record ServiceCategory_UpdateResponse
{
}
public sealed record ServiceCategory_SetActiveRequest
{
 public required int CategoryID { get; init; }
 public required bool IsActive { get; init; }
}
public sealed record ServiceCategory_SetActiveResponse
{
}
public sealed record ServiceCategory_GetByIDRequest
{
 public required int ID { get; init; }
}
public sealed record ServiceCategory_GetByIDRow1
{
 public int? CategoryID { get; init; }
 public string? CategoryCode { get; init; }
 public string? CategoryName { get; init; }
 public string? CategoryDescription { get; init; }
 public int? DisplayOrder { get; init; }
 public bool? IsActive { get; init; }
 public string? CreatedBy { get; init; }
 public DateTime? CreatedAt { get; init; }
 public string? UpdatedBy { get; init; }
 public DateTime? UpdatedAt { get; init; }
}
public sealed record ServiceCategory_GetByIDResponse
{
 public IReadOnlyList<ServiceCategory_GetByIDRow1> Rows1 { get; init; } = [];
}
public sealed record ServiceCategory_ListRequest
{
 [Range(1, int.MaxValue)] public int Page { get; init; } = 1;
 [Range(1, 200)] public int PageSize { get; init; } = 50;
}
public sealed record ServiceCategory_ListRow1
{
 public int? CategoryID { get; init; }
 public string? CategoryCode { get; init; }
 public string? CategoryName { get; init; }
 public string? CategoryDescription { get; init; }
 public int? DisplayOrder { get; init; }
 public bool? IsActive { get; init; }
 public string? CreatedBy { get; init; }
 public DateTime? CreatedAt { get; init; }
 public string? UpdatedBy { get; init; }
 public DateTime? UpdatedAt { get; init; }
 public long? TotalRows { get; init; }
}
public sealed record ServiceCategory_ListResponse
{
 public IReadOnlyList<ServiceCategory_ListRow1> Rows1 { get; init; } = [];
}
public sealed record Service_CreateRequest
{
 public required int CategoryID { get; init; }
 [Required] public required string ServiceCode { get; init; }
 [Required] public required string ServiceName { get; init; }
 [Required] public required string ShortName { get; init; }
 [Required] public required string VeryShortName { get; init; }
 [Required] public required string ServiceTitle { get; init; }
 public string? Description { get; init; }
 public required bool IsActive { get; init; }
}
public sealed record Service_CreateResponse
{
 public long? NewID { get; init; }
}
public sealed record Service_UpdateRequest
{
 public required long ServiceID { get; init; }
 public required int CategoryID { get; init; }
 [Required] public required string ServiceName { get; init; }
 [Required] public required string ShortName { get; init; }
 [Required] public required string VeryShortName { get; init; }
 [Required] public required string ServiceTitle { get; init; }
 public string? Description { get; init; }
}
public sealed record Service_UpdateResponse
{
}
public sealed record Service_SetActiveRequest
{
 public required long ServiceID { get; init; }
 public required bool IsActive { get; init; }
}
public sealed record Service_SetActiveResponse
{
}
public sealed record Service_GetByIDRequest
{
 public required long ID { get; init; }
}
public sealed record Service_GetByIDRow1
{
 public long? ServiceID { get; init; }
 public int? CategoryID { get; init; }
 public string? ServiceCode { get; init; }
 public string? ServiceName { get; init; }
 public string? ShortName { get; init; }
 public string? VeryShortName { get; init; }
 public string? ServiceTitle { get; init; }
 public string? Description { get; init; }
 public bool? IsActive { get; init; }
 public string? CreatedBy { get; init; }
 public string? UpdatedBy { get; init; }
 public DateTime? CreatedAt { get; init; }
 public DateTime? UpdatedAt { get; init; }
}
public sealed record Service_GetByIDResponse
{
 public IReadOnlyList<Service_GetByIDRow1> Rows1 { get; init; } = [];
}
public sealed record Service_ListRequest
{
 [Range(1, int.MaxValue)] public int Page { get; init; } = 1;
 [Range(1, 200)] public int PageSize { get; init; } = 50;
}
public sealed record Service_ListRow1
{
 public long? ServiceID { get; init; }
 public int? CategoryID { get; init; }
 public string? ServiceCode { get; init; }
 public string? ServiceName { get; init; }
 public string? ShortName { get; init; }
 public string? VeryShortName { get; init; }
 public string? ServiceTitle { get; init; }
 public string? Description { get; init; }
 public bool? IsActive { get; init; }
 public string? CreatedBy { get; init; }
 public string? UpdatedBy { get; init; }
 public DateTime? CreatedAt { get; init; }
 public DateTime? UpdatedAt { get; init; }
 public long? TotalRows { get; init; }
}
public sealed record Service_ListResponse
{
 public IReadOnlyList<Service_ListRow1> Rows1 { get; init; } = [];
}
public sealed record ServiceVersion_CreateDraftRequest
{
 public required long ServiceID { get; init; }
}
public sealed record ServiceVersion_CreateDraftResponse
{
 public long? NewVersionID { get; init; }
}
public sealed record ServiceVersion_UpdateDraftRequest
{
 public required long VersionID { get; init; }
 public required int CategoryID { get; init; }
 [Required] public required string VersionServiceName { get; init; }
 [Required] public required string VersionShortName { get; init; }
 [Required] public required string VersionVeryShortName { get; init; }
 [Required] public required string VersionServiceTitle { get; init; }
 public string? VersionDescription { get; init; }
 public string? ChangeRemarks { get; init; }
 [MinLength(8), MaxLength(8)] public required byte[] ExpectedVersionRowVersion { get; init; }
}
public sealed record ServiceVersion_UpdateDraftResponse
{
}
public sealed record ServiceVersion_GetDraftRequest
{
 public required long ServiceID { get; init; }
}
public sealed record ServiceVersion_GetDraftRow1
{
 public long? VersionID { get; init; }
 public long? ServiceID { get; init; }
 public int? VersionNumber { get; init; }
 public string? VersionStatus { get; init; }
 public int? CategoryID { get; init; }
 public string? VersionServiceName { get; init; }
 public string? VersionShortName { get; init; }
 public string? VersionVeryShortName { get; init; }
 public string? VersionServiceTitle { get; init; }
 public string? VersionDescription { get; init; }
 public string? CreatedBy { get; init; }
 public DateTime? CreatedAt { get; init; }
 public string? PublishedBy { get; init; }
 public DateTime? PublishedAt { get; init; }
 public string? RetiredBy { get; init; }
 public DateTime? RetiredAt { get; init; }
 public string? ChangeRemarks { get; init; }
 public byte[]? RowVersion { get; init; }
}
public sealed record ServiceVersion_GetDraftResponse
{
 public IReadOnlyList<ServiceVersion_GetDraftRow1> Rows1 { get; init; } = [];
}
public sealed record ServiceVersion_GetPublishedRequest
{
 public required long ServiceID { get; init; }
}
public sealed record ServiceVersion_GetPublishedRow1
{
 public long? VersionID { get; init; }
 public long? ServiceID { get; init; }
 public int? VersionNumber { get; init; }
 public string? VersionStatus { get; init; }
 public int? CategoryID { get; init; }
 public string? VersionServiceName { get; init; }
 public string? VersionShortName { get; init; }
 public string? VersionVeryShortName { get; init; }
 public string? VersionServiceTitle { get; init; }
 public string? VersionDescription { get; init; }
 public string? CreatedBy { get; init; }
 public DateTime? CreatedAt { get; init; }
 public string? PublishedBy { get; init; }
 public DateTime? PublishedAt { get; init; }
 public string? RetiredBy { get; init; }
 public DateTime? RetiredAt { get; init; }
 public string? ChangeRemarks { get; init; }
 public byte[]? RowVersion { get; init; }
}
public sealed record ServiceVersion_GetPublishedResponse
{
 public IReadOnlyList<ServiceVersion_GetPublishedRow1> Rows1 { get; init; } = [];
}
public sealed record ServiceVersion_GetHistoryRequest
{
 public required long ServiceID { get; init; }
}
public sealed record ServiceVersion_GetHistoryRow1
{
 public long? VersionID { get; init; }
 public long? ServiceID { get; init; }
 public int? VersionNumber { get; init; }
 public string? VersionStatus { get; init; }
 public int? CategoryID { get; init; }
 public string? VersionServiceName { get; init; }
 public string? VersionShortName { get; init; }
 public string? VersionVeryShortName { get; init; }
 public string? VersionServiceTitle { get; init; }
 public string? VersionDescription { get; init; }
 public string? CreatedBy { get; init; }
 public DateTime? CreatedAt { get; init; }
 public string? PublishedBy { get; init; }
 public DateTime? PublishedAt { get; init; }
 public string? RetiredBy { get; init; }
 public DateTime? RetiredAt { get; init; }
 public string? ChangeRemarks { get; init; }
 public byte[]? RowVersion { get; init; }
}
public sealed record ServiceVersion_GetHistoryResponse
{
 public IReadOnlyList<ServiceVersion_GetHistoryRow1> Rows1 { get; init; } = [];
}
public sealed record ServiceVersion_ClonePublishedRequest
{
 public required long ServiceID { get; init; }
 public required long SourceVersionID { get; init; }
}
public sealed record ServiceVersion_ClonePublishedResponse
{
 public long? NewVersionID { get; init; }
}
public sealed record ServiceVersion_PublishDraftRequest
{
 public required long VersionID { get; init; }
 [MinLength(8), MaxLength(8)] public required byte[] ExpectedRowVersion { get; init; }
}
public sealed record ServiceVersion_PublishDraftResponse
{
}
public sealed record ServiceVersion_RetirePublishedRequest
{
 public required long VersionID { get; init; }
 [MinLength(8), MaxLength(8)] public required byte[] ExpectedRowVersion { get; init; }
}
public sealed record ServiceVersion_RetirePublishedResponse
{
}
public sealed record ServiceField_AddRequest
{
 public required long VersionID { get; init; }
 [Required] public required string FieldKey { get; init; }
 [Required] public required string FieldLabel { get; init; }
 [Required] public required string FieldType { get; init; }
 public string? DropdownSourceType { get; init; }
 public long? DropdownListVersionID { get; init; }
 public string? Placeholder { get; init; }
 public string? HelpText { get; init; }
 public string? DefaultValue { get; init; }
 public required bool IsRequired { get; init; }
 public required bool IsReadOnly { get; init; }
 public required bool IsVisible { get; init; }
 public int? MinLength { get; init; }
 public int? MaxLength { get; init; }
 public string? ValidationPattern { get; init; }
 public string? DataSourceKey { get; init; }
 public required int DisplayOrder { get; init; }
 public required bool IsActive { get; init; }
 [MinLength(8), MaxLength(8)] public required byte[] ExpectedVersionRowVersion { get; init; }
}
public sealed record ServiceField_AddResponse
{
 public long? NewID { get; init; }
}
public sealed record ServiceField_UpdateRequest
{
 public required long FieldID { get; init; }
 [Required] public required string FieldLabel { get; init; }
 [Required] public required string FieldType { get; init; }
 public string? DropdownSourceType { get; init; }
 public long? DropdownListVersionID { get; init; }
 public string? Placeholder { get; init; }
 public string? HelpText { get; init; }
 public string? DefaultValue { get; init; }
 public required bool IsRequired { get; init; }
 public required bool IsReadOnly { get; init; }
 public required bool IsVisible { get; init; }
 public int? MinLength { get; init; }
 public int? MaxLength { get; init; }
 public string? ValidationPattern { get; init; }
 public string? DataSourceKey { get; init; }
 public required int DisplayOrder { get; init; }
 public required bool IsActive { get; init; }
 [MinLength(8), MaxLength(8)] public required byte[] ExpectedVersionRowVersion { get; init; }
}
public sealed record ServiceField_UpdateResponse
{
}
public sealed record ServiceField_DeactivateRequest
{
 public required long FieldID { get; init; }
 [MinLength(8), MaxLength(8)] public required byte[] ExpectedVersionRowVersion { get; init; }
}
public sealed record ServiceField_DeactivateResponse
{
}
public sealed record ServiceField_GetByVersionRequest
{
 public required long VersionID { get; init; }
}
public sealed record ServiceField_GetByVersionRow1
{
 public long? FieldID { get; init; }
 public long? VersionID { get; init; }
 public string? FieldKey { get; init; }
 public string? FieldLabel { get; init; }
 public string? FieldType { get; init; }
 public string? DropdownSourceType { get; init; }
 public long? DropdownListVersionID { get; init; }
 public string? Placeholder { get; init; }
 public string? HelpText { get; init; }
 public string? DefaultValue { get; init; }
 public bool? IsRequired { get; init; }
 public bool? IsReadOnly { get; init; }
 public bool? IsVisible { get; init; }
 public int? MinLength { get; init; }
 public int? MaxLength { get; init; }
 public string? ValidationPattern { get; init; }
 public string? DataSourceKey { get; init; }
 public int? DisplayOrder { get; init; }
 public bool? IsActive { get; init; }
 public DateTime? CreatedAt { get; init; }
 public DateTime? UpdatedAt { get; init; }
}
public sealed record ServiceField_GetByVersionResponse
{
 public IReadOnlyList<ServiceField_GetByVersionRow1> Rows1 { get; init; } = [];
}
public sealed record ServiceFieldOption_AddRequest
{
 public required long FieldID { get; init; }
 [Required] public required string OptionLabel { get; init; }
 [Required] public required string OptionValue { get; init; }
 public required int DisplayOrder { get; init; }
 public required bool IsDefault { get; init; }
 public required bool IsActive { get; init; }
 [MinLength(8), MaxLength(8)] public required byte[] ExpectedVersionRowVersion { get; init; }
}
public sealed record ServiceFieldOption_AddResponse
{
 public long? NewID { get; init; }
}
public sealed record ServiceFieldOption_UpdateRequest
{
 public required long OptionID { get; init; }
 [Required] public required string OptionLabel { get; init; }
 [Required] public required string OptionValue { get; init; }
 public required int DisplayOrder { get; init; }
 public required bool IsDefault { get; init; }
 public required bool IsActive { get; init; }
 [MinLength(8), MaxLength(8)] public required byte[] ExpectedVersionRowVersion { get; init; }
}
public sealed record ServiceFieldOption_UpdateResponse
{
}
public sealed record ServiceFieldOption_GetRequest
{
 public required long FieldID { get; init; }
}
public sealed record ServiceFieldOption_GetRow1
{
 public long? OptionID { get; init; }
 public long? FieldID { get; init; }
 public string? OptionLabel { get; init; }
 public string? OptionValue { get; init; }
 public int? DisplayOrder { get; init; }
 public bool? IsDefault { get; init; }
 public bool? IsActive { get; init; }
 public DateTime? CreatedAt { get; init; }
}
public sealed record ServiceFieldOption_GetResponse
{
 public IReadOnlyList<ServiceFieldOption_GetRow1> Rows1 { get; init; } = [];
}
public sealed record DropdownList_CreateRequest
{
 [Required] public required string ListCode { get; init; }
 [Required] public required string ListName { get; init; }
 public string? Description { get; init; }
 public required bool IsActive { get; init; }
}
public sealed record DropdownList_CreateResponse
{
 public long? NewID { get; init; }
}
public sealed record DropdownListVersion_CreateRequest
{
 public required long DropdownListID { get; init; }
}
public sealed record DropdownListVersion_CreateResponse
{
 public long? NewVersionID { get; init; }
}
public sealed record DropdownListOption_AddRequest
{
 public required long DropdownListVersionID { get; init; }
 [Required] public required string OptionLabel { get; init; }
 [Required] public required string OptionValue { get; init; }
 public required int DisplayOrder { get; init; }
 public required bool IsDefault { get; init; }
 public required bool IsActive { get; init; }
 [MinLength(8), MaxLength(8)] public required byte[] ExpectedVersionRowVersion { get; init; }
}
public sealed record DropdownListOption_AddResponse
{
 public long? NewID { get; init; }
}
public sealed record DropdownListOption_UpdateRequest
{
 public required long DropdownOptionID { get; init; }
 [Required] public required string OptionLabel { get; init; }
 [Required] public required string OptionValue { get; init; }
 public required int DisplayOrder { get; init; }
 public required bool IsDefault { get; init; }
 public required bool IsActive { get; init; }
 [MinLength(8), MaxLength(8)] public required byte[] ExpectedVersionRowVersion { get; init; }
}
public sealed record DropdownListOption_UpdateResponse
{
}
public sealed record DropdownListVersion_PublishRequest
{
 public required long DropdownListVersionID { get; init; }
 [MinLength(8), MaxLength(8)] public required byte[] ExpectedRowVersion { get; init; }
}
public sealed record DropdownListVersion_PublishResponse
{
}
public sealed record DropdownList_GetPublishedOptionsRequest
{
 public required long DropdownListID { get; init; }
}
public sealed record DropdownList_GetPublishedOptionsRow1
{
 public long? DropdownOptionID { get; init; }
 public long? DropdownListVersionID { get; init; }
 public string? OptionLabel { get; init; }
 public string? OptionValue { get; init; }
 public int? DisplayOrder { get; init; }
 public bool? IsDefault { get; init; }
}
public sealed record DropdownList_GetPublishedOptionsResponse
{
 public IReadOnlyList<DropdownList_GetPublishedOptionsRow1> Rows1 { get; init; } = [];
}
public sealed record ServiceWorkflow_SaveRequest
{
 public required long VersionID { get; init; }
 public required int DefaultPriorityID { get; init; }
 public required int ApprovalTargetMinutes { get; init; }
 public required int ResolutionTargetMinutes { get; init; }
 public required bool AllowForwardToBranch { get; init; }
 public required bool AllowForwardToDivision { get; init; }
 public required bool FollowUpRequired { get; init; }
 public int? DefaultFollowUpMinutes { get; init; }
 [MinLength(8), MaxLength(8)] public required byte[] ExpectedVersionRowVersion { get; init; }
}
public sealed record ServiceWorkflow_SaveResponse
{
}
public sealed record ServiceWorkflow_GetRequest
{
 public required long VersionID { get; init; }
}
public sealed record ServiceWorkflow_GetRow1
{
 public long? WorkflowID { get; init; }
 public long? VersionID { get; init; }
 public int? DefaultPriorityID { get; init; }
 public int? ApprovalTargetMinutes { get; init; }
 public int? ResolutionTargetMinutes { get; init; }
 public bool? AllowForwardToBranch { get; init; }
 public bool? AllowForwardToDivision { get; init; }
 public bool? FollowUpRequired { get; init; }
 public int? DefaultFollowUpMinutes { get; init; }
 public DateTime? CreatedAt { get; init; }
 public DateTime? UpdatedAt { get; init; }
}
public sealed record ServiceWorkflow_GetResponse
{
 public IReadOnlyList<ServiceWorkflow_GetRow1> Rows1 { get; init; } = [];
}
public sealed record ServiceNotificationConfig_SaveRequest
{
 public required long VersionID { get; init; }
 [Required] public required string EventCode { get; init; }
 [Required] public required string ChannelCode { get; init; }
 public long? TemplateID { get; init; }
 public required bool IsEnabled { get; init; }
 [MinLength(8), MaxLength(8)] public required byte[] ExpectedVersionRowVersion { get; init; }
}
public sealed record ServiceNotificationConfig_SaveResponse
{
}
public sealed record ServiceNotificationConfig_GetRequest
{
 public required long VersionID { get; init; }
}
public sealed record ServiceNotificationConfig_GetRow1
{
 public long? NotificationConfigID { get; init; }
 public long? VersionID { get; init; }
 public string? EventCode { get; init; }
 public string? ChannelCode { get; init; }
 public long? TemplateID { get; init; }
 public bool? IsEnabled { get; init; }
 public DateTime? CreatedAt { get; init; }
 public DateTime? UpdatedAt { get; init; }
}
public sealed record ServiceNotificationConfig_GetResponse
{
 public IReadOnlyList<ServiceNotificationConfig_GetRow1> Rows1 { get; init; } = [];
}
public sealed record NotificationTemplate_CreateDraftRequest
{
 [Required] public required string TemplateCode { get; init; }
 public required int TemplateVersion { get; init; }
 [Required] public required string TemplateName { get; init; }
 [Required] public required string ChannelCode { get; init; }
 public string? TemplateSubject { get; init; }
 [Required] public required string TemplateBody { get; init; }
 public string? Description { get; init; }
}
public sealed record NotificationTemplate_CreateDraftResponse
{
 public long? NewID { get; init; }
}
public sealed record NotificationTemplate_UpdateDraftRequest
{
 public required long TemplateID { get; init; }
 [Required] public required string TemplateName { get; init; }
 public string? TemplateSubject { get; init; }
 [Required] public required string TemplateBody { get; init; }
 public string? Description { get; init; }
 [MinLength(8), MaxLength(8)] public required byte[] ExpectedRowVersion { get; init; }
}
public sealed record NotificationTemplate_UpdateDraftResponse
{
}
public sealed record NotificationTemplate_PublishRequest
{
 public required long TemplateID { get; init; }
 [MinLength(8), MaxLength(8)] public required byte[] ExpectedRowVersion { get; init; }
}
public sealed record NotificationTemplate_PublishResponse
{
}
public sealed record NotificationTemplate_GetPublishedRequest
{
 [Required] public required string TemplateCode { get; init; }
}
public sealed record NotificationTemplate_GetPublishedRow1
{
 public long? TemplateID { get; init; }
 public string? TemplateCode { get; init; }
 public int? TemplateVersion { get; init; }
 public string? TemplateName { get; init; }
 public string? ChannelCode { get; init; }
 public string? TemplateSubject { get; init; }
 public string? TemplateBody { get; init; }
 public string? TemplateStatus { get; init; }
 public string? Description { get; init; }
 public string? CreatedByManagerID { get; init; }
 public DateTime? CreatedAt { get; init; }
 public string? PublishedByManagerID { get; init; }
 public DateTime? PublishedAt { get; init; }
 public DateTime? RetiredAt { get; init; }
 public byte[]? RowVersion { get; init; }
}
public sealed record NotificationTemplate_GetPublishedResponse
{
 public IReadOnlyList<NotificationTemplate_GetPublishedRow1> Rows1 { get; init; } = [];
}
public sealed record Ticket_CreateRequest
{
 public required Guid IdempotencyKey { get; init; }
 public required long ServiceID { get; init; }
 public required long VersionID { get; init; }
 [Required] public required string AvayaInteractionID { get; init; }
 [Required] public required string FieldValuesJson { get; init; }
 public required byte[] TrackingTokenHash { get; init; }
 public required byte[] EncryptedDeliveryPayload { get; init; }
 [Required] public required string PayloadKeyReference { get; init; }
 public required DateTime PayloadExpiresAt { get; init; }
 public string? RequestDescription { get; init; }
 public string? CustomerName { get; init; }
 public string? CustomerMobile { get; init; }
 public string? CBS_CIF { get; init; }
 public string? AccountNumber { get; init; }
 public string? CardClientID { get; init; }
 public string? CardCIF { get; init; }
 public string? CardLast4 { get; init; }
 public string? InternetBankingUserID { get; init; }
}
public sealed record Ticket_CreateResponse
{
 public long? RequestID { get; init; }
 public string? TicketNumber { get; init; }
}
public sealed record Ticket_ApproveRequest
{
 public required long RequestID { get; init; }
 [MinLength(8), MaxLength(8)] public required byte[] ExpectedRowVersion { get; init; }
 public string? Remarks { get; init; }
}
public sealed record Ticket_ApproveResponse
{
}
public sealed record Ticket_RejectRequest
{
 public required long RequestID { get; init; }
 [MinLength(8), MaxLength(8)] public required byte[] ExpectedRowVersion { get; init; }
 [Required] public required string Remarks { get; init; }
}
public sealed record Ticket_RejectResponse
{
}
public sealed record Ticket_UpdateStatusRequest
{
 public required long RequestID { get; init; }
 public required int ToStatusID { get; init; }
 [MinLength(8), MaxLength(8)] public required byte[] ExpectedRowVersion { get; init; }
 public string? Remarks { get; init; }
}
public sealed record Ticket_UpdateStatusResponse
{
}
public sealed record Ticket_GetByIDRequest
{
 public required long RequestID { get; init; }
}
public sealed record Ticket_GetByIDRow1
{
 public long? RequestID { get; init; }
 public string? TicketNumber { get; init; }
 public long? ServiceID { get; init; }
 public long? VersionID { get; init; }
 public string? RequestTitle { get; init; }
 public string? RequestSourceCode { get; init; }
 public string? CardLast4 { get; init; }
 public int? PriorityID { get; init; }
 public int? CurrentStatusID { get; init; }
 public string? ApprovalStatus { get; init; }
 public DateTime? ApprovalDueAt { get; init; }
 public DateTime? ResolutionDueAt { get; init; }
 public DateTime? ApprovedAt { get; init; }
 public DateTime? CompletedAt { get; init; }
 public DateTime? ClosedAt { get; init; }
 public string? CreatedByAgentName { get; init; }
 public DateTime? CreatedAt { get; init; }
 public string? LastUpdatedByRole { get; init; }
 public DateTime? LastUpdatedAt { get; init; }
 public DateTime? NextFollowUpAt { get; init; }
 public bool? FollowUpRequired { get; init; }
 public byte[]? RowVersion { get; init; }
 public string? MaskedCustomerMobile { get; init; }
}
public sealed record Ticket_GetByIDRow2
{
 public long? RequestFieldValueID { get; init; }
 public long? FieldID { get; init; }
 public string? FieldKeySnapshot { get; init; }
 public string? FieldLabelSnapshot { get; init; }
 public string? FieldTypeSnapshot { get; init; }
}
public sealed record Ticket_GetByIDResponse
{
 public IReadOnlyList<Ticket_GetByIDRow1> Rows1 { get; init; } = [];
 public IReadOnlyList<Ticket_GetByIDRow2> Rows2 { get; init; } = [];
}
public sealed record Ticket_GetByNumberRequest
{
 [Required] public required string TicketNumber { get; init; }
}
public sealed record Ticket_GetByNumberRow1
{
 public long? RequestID { get; init; }
 public string? TicketNumber { get; init; }
 public long? ServiceID { get; init; }
 public long? VersionID { get; init; }
 public string? RequestTitle { get; init; }
 public string? RequestSourceCode { get; init; }
 public string? CardLast4 { get; init; }
 public int? PriorityID { get; init; }
 public int? CurrentStatusID { get; init; }
 public string? ApprovalStatus { get; init; }
 public DateTime? ApprovalDueAt { get; init; }
 public DateTime? ResolutionDueAt { get; init; }
 public DateTime? ApprovedAt { get; init; }
 public DateTime? CompletedAt { get; init; }
 public DateTime? ClosedAt { get; init; }
 public string? CreatedByAgentName { get; init; }
 public DateTime? CreatedAt { get; init; }
 public string? LastUpdatedByRole { get; init; }
 public DateTime? LastUpdatedAt { get; init; }
 public DateTime? NextFollowUpAt { get; init; }
 public bool? FollowUpRequired { get; init; }
 public byte[]? RowVersion { get; init; }
 public string? MaskedCustomerMobile { get; init; }
}
public sealed record Ticket_GetByNumberRow2
{
 public long? RequestFieldValueID { get; init; }
 public long? FieldID { get; init; }
 public string? FieldKeySnapshot { get; init; }
 public string? FieldLabelSnapshot { get; init; }
 public string? FieldTypeSnapshot { get; init; }
}
public sealed record Ticket_GetByNumberResponse
{
 public IReadOnlyList<Ticket_GetByNumberRow1> Rows1 { get; init; } = [];
 public IReadOnlyList<Ticket_GetByNumberRow2> Rows2 { get; init; } = [];
}
public sealed record Ticket_SearchRequest
{
 public string? TicketNumber { get; init; }
 public string? CustomerMobile { get; init; }
 public string? CBS_CIF { get; init; }
 public string? AccountNumber { get; init; }
 public string? CardClientID { get; init; }
 public string? CardCIF { get; init; }
 public string? CardLast4 { get; init; }
 public string? InternetBankingUserID { get; init; }
 public string? CustomerName { get; init; }
 public long? ServiceID { get; init; }
 public int? PriorityID { get; init; }
 public int? StatusID { get; init; }
 public DateTime? FromDate { get; init; }
 public DateTime? ToDate { get; init; }
 [Range(1, int.MaxValue)] public int Page { get; init; } = 1;
 [Range(1, 200)] public int PageSize { get; init; } = 50;
}
public sealed record Ticket_SearchRow1
{
 public long? RequestID { get; init; }
 public string? TicketNumber { get; init; }
 public string? RequestTitle { get; init; }
 public long? ServiceID { get; init; }
 public int? PriorityID { get; init; }
 public int? CurrentStatusID { get; init; }
 public string? ApprovalStatus { get; init; }
 public DateTime? CreatedAt { get; init; }
 public DateTime? ApprovalDueAt { get; init; }
 public DateTime? ResolutionDueAt { get; init; }
 public DateTime? NextFollowUpAt { get; init; }
 public byte[]? RowVersion { get; init; }
 public long? TotalRows { get; init; }
}
public sealed record Ticket_SearchResponse
{
 public IReadOnlyList<Ticket_SearchRow1> Rows1 { get; init; } = [];
}
public sealed record Ticket_GetRecentByCustomerRequest
{
 public string? CustomerMobile { get; init; }
 public string? CBS_CIF { get; init; }
 [Range(1, int.MaxValue)] public int Page { get; init; } = 1;
 [Range(1, 200)] public int PageSize { get; init; } = 50;
}
public sealed record Ticket_GetRecentByCustomerRow1
{
 public long? RequestID { get; init; }
 public string? TicketNumber { get; init; }
 public string? RequestTitle { get; init; }
 public long? ServiceID { get; init; }
 public int? PriorityID { get; init; }
 public int? CurrentStatusID { get; init; }
 public string? ApprovalStatus { get; init; }
 public DateTime? CreatedAt { get; init; }
 public DateTime? ApprovalDueAt { get; init; }
 public DateTime? ResolutionDueAt { get; init; }
 public DateTime? NextFollowUpAt { get; init; }
 public byte[]? RowVersion { get; init; }
 public long? TotalRows { get; init; }
}
public sealed record Ticket_GetRecentByCustomerResponse
{
 public IReadOnlyList<Ticket_GetRecentByCustomerRow1> Rows1 { get; init; } = [];
}
public sealed record Ticket_GetPendingApprovalRequest
{
 public DateTime? FromDate { get; init; }
 public DateTime? ToDate { get; init; }
 [Range(1, int.MaxValue)] public int Page { get; init; } = 1;
 [Range(1, 200)] public int PageSize { get; init; } = 50;
}
public sealed record Ticket_GetPendingApprovalRow1
{
 public long? RequestID { get; init; }
 public string? TicketNumber { get; init; }
 public string? RequestTitle { get; init; }
 public int? PriorityID { get; init; }
 public string? PriorityCode { get; init; }
 public int? CurrentStatusID { get; init; }
 public DateTime? CreatedAt { get; init; }
 public DateTime? ApprovalDueAt { get; init; }
 public DateTime? ResolutionDueAt { get; init; }
 public byte[]? RowVersion { get; init; }
 public long? TotalRows { get; init; }
}
public sealed record Ticket_GetPendingApprovalResponse
{
 public IReadOnlyList<Ticket_GetPendingApprovalRow1> Rows1 { get; init; } = [];
}
public sealed record Ticket_GetPriorityQueueRequest
{
 public DateTime? FromDate { get; init; }
 public DateTime? ToDate { get; init; }
 [Range(1, int.MaxValue)] public int Page { get; init; } = 1;
 [Range(1, 200)] public int PageSize { get; init; } = 50;
}
public sealed record Ticket_GetPriorityQueueRow1
{
 public long? RequestID { get; init; }
 public string? TicketNumber { get; init; }
 public string? RequestTitle { get; init; }
 public int? PriorityID { get; init; }
 public string? PriorityCode { get; init; }
 public int? CurrentStatusID { get; init; }
 public DateTime? CreatedAt { get; init; }
 public DateTime? ApprovalDueAt { get; init; }
 public DateTime? ResolutionDueAt { get; init; }
 public byte[]? RowVersion { get; init; }
 public long? TotalRows { get; init; }
}
public sealed record Ticket_GetPriorityQueueResponse
{
 public IReadOnlyList<Ticket_GetPriorityQueueRow1> Rows1 { get; init; } = [];
}
public sealed record Ticket_GetStatusHistoryRequest
{
 public required long RequestID { get; init; }
}
public sealed record Ticket_GetStatusHistoryRow1
{
 public long? StatusHistoryID { get; init; }
 public int? FromStatusID { get; init; }
 public int? ToStatusID { get; init; }
 public string? ChangedByRole { get; init; }
 public DateTime? ChangedAt { get; init; }
}
public sealed record Ticket_GetStatusHistoryResponse
{
 public IReadOnlyList<Ticket_GetStatusHistoryRow1> Rows1 { get; init; } = [];
}
public sealed record Ticket_GetActivityHistoryRequest
{
 public required long RequestID { get; init; }
}
public sealed record Ticket_GetActivityHistoryRow1
{
 public long? ActivityID { get; init; }
 public string? ActivityType { get; init; }
 public string? ActivityTitle { get; init; }
 public string? PerformedByRole { get; init; }
 public DateTime? ActivityAt { get; init; }
}
public sealed record Ticket_GetActivityHistoryResponse
{
 public IReadOnlyList<Ticket_GetActivityHistoryRow1> Rows1 { get; init; } = [];
}
public sealed record FollowUp_AddRequest
{
 public required long RequestID { get; init; }
 [Required] public required string FollowUpType { get; init; }
 [Required] public required string Note { get; init; }
 public long? RelatedForwardingID { get; init; }
}
public sealed record FollowUp_AddResponse
{
 public long? FollowUpID { get; init; }
}
public sealed record FollowUp_GetByTicketRequest
{
 public required long RequestID { get; init; }
}
public sealed record FollowUp_GetByTicketRow1
{
 public long? FollowUpID { get; init; }
 public long? RequestID { get; init; }
 public string? FollowUpType { get; init; }
 public string? FollowUpNote { get; init; }
 public long? RelatedForwardingID { get; init; }
 public string? CreatedByManagerID { get; init; }
 public string? CreatedByManagerName { get; init; }
 public string? CreatedByManagerRole { get; init; }
 public string? AuthenticationSource { get; init; }
 public DateTime? CreatedAt { get; init; }
}
public sealed record FollowUp_GetByTicketResponse
{
 public IReadOnlyList<FollowUp_GetByTicketRow1> Rows1 { get; init; } = [];
}
public sealed record FollowUpSchedule_CreateRequest
{
 public required long RequestID { get; init; }
 [Required] public required string Subject { get; init; }
 public required DateTime ScheduledAt { get; init; }
 public string? Description { get; init; }
 public string? AssignedManagerID { get; init; }
 public long? RelatedFollowUpID { get; init; }
 public long? RelatedForwardingID { get; init; }
}
public sealed record FollowUpSchedule_CreateResponse
{
 public long? ScheduleID { get; init; }
}
public sealed record FollowUpSchedule_GetByTicketRequest
{
 public required long RequestID { get; init; }
}
public sealed record FollowUpSchedule_GetByTicketRow1
{
 public long? FollowUpScheduleID { get; init; }
 public long? RequestID { get; init; }
 public long? RelatedFollowUpID { get; init; }
 public long? RelatedForwardingID { get; init; }
 public string? FollowUpSubject { get; init; }
 public string? FollowUpDescription { get; init; }
 public DateTime? ScheduledAt { get; init; }
 public string? ReminderStatus { get; init; }
 public string? AssignedManagerID { get; init; }
 public string? CreatedByManagerID { get; init; }
 public string? CreatedByManagerRole { get; init; }
 public DateTime? CreatedAt { get; init; }
 public string? CompletedByManagerID { get; init; }
 public DateTime? CompletedAt { get; init; }
 public string? CompletionRemarks { get; init; }
 public string? CancelledByManagerID { get; init; }
 public DateTime? CancelledAt { get; init; }
 public string? CancellationReason { get; init; }
 public byte[]? RowVersion { get; init; }
}
public sealed record FollowUpSchedule_GetByTicketResponse
{
 public IReadOnlyList<FollowUpSchedule_GetByTicketRow1> Rows1 { get; init; } = [];
}
public sealed record FollowUpSchedule_GetDueRequest
{
 public DateTime? Until { get; init; }
 [Range(1, int.MaxValue)] public int Page { get; init; } = 1;
 [Range(1, 200)] public int PageSize { get; init; } = 50;
}
public sealed record FollowUpSchedule_GetDueRow1
{
 public long? FollowUpScheduleID { get; init; }
 public long? RequestID { get; init; }
 public string? FollowUpSubject { get; init; }
 public DateTime? ScheduledAt { get; init; }
 public string? AssignedManagerID { get; init; }
 public byte[]? RowVersion { get; init; }
 public int? IsOverdue { get; init; }
 public long? TotalRows { get; init; }
}
public sealed record FollowUpSchedule_GetDueResponse
{
 public IReadOnlyList<FollowUpSchedule_GetDueRow1> Rows1 { get; init; } = [];
}
public sealed record FollowUpSchedule_CompleteRequest
{
 public required long ScheduleID { get; init; }
 [MinLength(8), MaxLength(8)] public required byte[] ExpectedRowVersion { get; init; }
 public string? Remarks { get; init; }
}
public sealed record FollowUpSchedule_CompleteResponse
{
}
public sealed record FollowUpSchedule_CancelRequest
{
 public required long ScheduleID { get; init; }
 [MinLength(8), MaxLength(8)] public required byte[] ExpectedRowVersion { get; init; }
 [Required] public required string Remarks { get; init; }
}
public sealed record FollowUpSchedule_CancelResponse
{
}
public sealed record Forwarding_CreateRequest
{
 public required long RequestID { get; init; }
 [MinLength(8), MaxLength(8)] public required byte[] ExpectedRowVersion { get; init; }
 [Required] public required string ForwardingType { get; init; }
 [Required] public required string DestinationName { get; init; }
 [Required] public required string ToAddresses { get; init; }
 [Required] public required string Subject { get; init; }
 [Required] public required string Body { get; init; }
 public string? CcAddresses { get; init; }
 public string? Remarks { get; init; }
 public long? TemplateID { get; init; }
}
public sealed record Forwarding_CreateResponse
{
 public long? ForwardingID { get; init; }
}
public sealed record Forwarding_GetByTicketRequest
{
 public required long RequestID { get; init; }
}
public sealed record Forwarding_GetByTicketRow1
{
 public long? ForwardingID { get; init; }
 public long? RequestID { get; init; }
 public string? ForwardingType { get; init; }
 public string? DestinationName { get; init; }
 public string? ToAddresses { get; init; }
 public string? CcAddresses { get; init; }
 public string? EmailSubject { get; init; }
 public string? EmailBody { get; init; }
 public string? ForwardingRemarks { get; init; }
 public string? SubmissionStatus { get; init; }
 public string? EmailProviderMessageID { get; init; }
 public int? SubmissionAttemptCount { get; init; }
 public DateTime? LastAttemptAt { get; init; }
 public DateTime? AcceptedAt { get; init; }
 public string? FailureReason { get; init; }
 public string? ForwardedByManagerID { get; init; }
 public string? ForwardedByManagerName { get; init; }
 public string? ForwardedByManagerRole { get; init; }
 public string? AuthenticationSource { get; init; }
 public DateTime? CreatedAt { get; init; }
 public byte[]? RowVersion { get; init; }
}
public sealed record Forwarding_GetByTicketResponse
{
 public IReadOnlyList<Forwarding_GetByTicketRow1> Rows1 { get; init; } = [];
}
public sealed record Forwarding_GetPendingRequest
{
 [Range(1, int.MaxValue)] public int Page { get; init; } = 1;
 [Range(1, 200)] public int PageSize { get; init; } = 50;
}
public sealed record Forwarding_GetPendingRow1
{
 public long? ForwardingID { get; init; }
 public long? RequestID { get; init; }
 public string? ForwardingType { get; init; }
 public string? SubmissionStatus { get; init; }
 public int? SubmissionAttemptCount { get; init; }
 public DateTime? CreatedAt { get; init; }
 public byte[]? RowVersion { get; init; }
 public long? TotalRows { get; init; }
}
public sealed record Forwarding_GetPendingResponse
{
 public IReadOnlyList<Forwarding_GetPendingRow1> Rows1 { get; init; } = [];
}
public sealed record Forwarding_MarkSubmittingRequest
{
 public required long ForwardingID { get; init; }
 [MinLength(8), MaxLength(8)] public required byte[] ExpectedRowVersion { get; init; }
 [Required] [MaxLength(100)] public required string LeaseID { get; init; }
}
public sealed record Forwarding_MarkSubmittingResponse
{
}
public sealed record Forwarding_RecordAcceptedRequest
{
 public required long ForwardingID { get; init; }
 [Required] public required string ProviderMessageID { get; init; }
 [Required] [MaxLength(100)] public required string LeaseID { get; init; }
}
public sealed record Forwarding_RecordAcceptedResponse
{
}
public sealed record Forwarding_RecordFailedRequest
{
 public required long ForwardingID { get; init; }
 [Required] [MaxLength(100)] public required string LeaseID { get; init; }
 [Required] public required string Reason { get; init; }
}
public sealed record Forwarding_RecordFailedResponse
{
}
public sealed record Communication_CreateRequest
{
 public required long RequestID { get; init; }
 [Required] [MaxLength(50)] public required string EventCode { get; init; }
 public byte[]? EncryptedPayload { get; init; }
 [MaxLength(200)] public string? KeyReference { get; init; }
 public DateTime? PayloadExpiresAt { get; init; }
}
public sealed record Communication_CreateResponse
{
 public long? CommunicationID { get; init; }
}
public sealed record Communication_GetByTicketRequest
{
 public required long ID { get; init; }
}
public sealed record Communication_GetByTicketRow1
{
 public long? CommunicationID { get; init; }
 public long? RequestID { get; init; }
 public long? RelatedForwardingID { get; init; }
 public string? ChannelCode { get; init; }
 public string? EventCode { get; init; }
 public string? RecipientType { get; init; }
 public string? CommunicationStatus { get; init; }
 public string? ProviderMessageID { get; init; }
 public Guid? CorrelationID { get; init; }
 public DateTime? CreatedAt { get; init; }
 public DateTime? AcceptedAt { get; init; }
 public DateTime? DeliveredAt { get; init; }
 public DateTime? FailedAt { get; init; }
 public string? LastErrorCode { get; init; }
 public byte[]? RowVersion { get; init; }
}
public sealed record Communication_GetByTicketResponse
{
 public IReadOnlyList<Communication_GetByTicketRow1> Rows1 { get; init; } = [];
}
public sealed record Communication_GetByIDRequest
{
 public required long ID { get; init; }
}
public sealed record Communication_GetByIDRow1
{
 public long? CommunicationID { get; init; }
 public long? RequestID { get; init; }
 public long? RelatedForwardingID { get; init; }
 public string? ChannelCode { get; init; }
 public string? EventCode { get; init; }
 public string? RecipientType { get; init; }
 public string? CommunicationStatus { get; init; }
 public string? ProviderMessageID { get; init; }
 public Guid? CorrelationID { get; init; }
 public DateTime? CreatedAt { get; init; }
 public DateTime? AcceptedAt { get; init; }
 public DateTime? DeliveredAt { get; init; }
 public DateTime? FailedAt { get; init; }
 public string? LastErrorCode { get; init; }
 public byte[]? RowVersion { get; init; }
}
public sealed record Communication_GetByIDResponse
{
 public IReadOnlyList<Communication_GetByIDRow1> Rows1 { get; init; } = [];
}
public sealed record CommunicationOutbox_ClaimRequest
{
 public int BatchSize { get; init; } = 20;
 public int LeaseSeconds { get; init; } = 60;
}
public sealed record CommunicationOutbox_ClaimRow1
{
 public long? OutboxID { get; init; }
 public long? CommunicationID { get; init; }
 public string? LeaseID { get; init; }
 public DateTime? LockedUntil { get; init; }
 public byte[]? RowVersion { get; init; }
 public string? ChannelCode { get; init; }
 public string? EventCode { get; init; }
 public string? RecipientAddress { get; init; }
 public string? CcAddresses { get; init; }
 public string? MessageSubject { get; init; }
 public string? MessageBody { get; init; }
 public Guid? CorrelationID { get; init; }
 public byte[]? EncryptedDeliveryPayload { get; init; }
 public string? PayloadKeyReference { get; init; }
 public DateTime? PayloadExpiresAt { get; init; }
}
public sealed record CommunicationOutbox_ClaimResponse
{
 public IReadOnlyList<CommunicationOutbox_ClaimRow1> Rows1 { get; init; } = [];
}
public sealed record CommunicationAttempt_StartRequest
{
 public required long CommunicationID { get; init; }
 [Required] [MaxLength(100)] public required string LeaseID { get; init; }
 [Required] public required string ProviderCode { get; init; }
 [Required] public required string ProviderRequestID { get; init; }
}
public sealed record CommunicationAttempt_StartResponse
{
 public long? AttemptID { get; init; }
}
public sealed record CommunicationAttempt_CompleteRequest
{
 public required long AttemptID { get; init; }
 [Required] [MaxLength(100)] public required string LeaseID { get; init; }
 [Required] public required string ProviderRequestID { get; init; }
 [Required] [MaxLength(25)] public required string Result { get; init; }
 public string? ProviderMessageID { get; init; }
 public string? ErrorCode { get; init; }
 public string? SanitizedError { get; init; }
 public bool? IsRetryable { get; init; }
}
public sealed record CommunicationAttempt_CompleteResponse
{
}
public sealed record Communication_RecordProviderResultRequest
{
 public required long AttemptID { get; init; }
 [Required] [MaxLength(100)] public required string LeaseID { get; init; }
 [Required] public required string ProviderRequestID { get; init; }
 [Required] [MaxLength(25)] public required string Result { get; init; }
 public string? ProviderMessageID { get; init; }
 public string? ErrorCode { get; init; }
 public string? SanitizedError { get; init; }
 public bool? IsRetryable { get; init; }
}
public sealed record Communication_RecordProviderResultResponse
{
}
public sealed record CommunicationOutbox_CompleteRequest
{
 public required long CommunicationID { get; init; }
 [MinLength(8), MaxLength(8)] public required byte[] ExpectedRowVersion { get; init; }
}
public sealed record CommunicationOutbox_CompleteResponse
{
}
public sealed record CommunicationOutbox_FailRequest
{
 public required long CommunicationID { get; init; }
 [MinLength(8), MaxLength(8)] public required byte[] ExpectedRowVersion { get; init; }
}
public sealed record CommunicationOutbox_FailResponse
{
}
public sealed record CommunicationOutbox_ScheduleRetryRequest
{
 public required long CommunicationID { get; init; }
 [MinLength(8), MaxLength(8)] public required byte[] ExpectedRowVersion { get; init; }
 public required DateTime NextRetryAt { get; init; }
}
public sealed record CommunicationOutbox_ScheduleRetryResponse
{
}
public sealed record CommunicationOutbox_ManualRetryRequest
{
 public required long CommunicationID { get; init; }
 [MinLength(8), MaxLength(8)] public required byte[] ExpectedRowVersion { get; init; }
 [Required] public required string Reason { get; init; }
}
public sealed record CommunicationOutbox_ManualRetryResponse
{
}
public sealed record TrackingToken_CreateRequest
{
 public required long RequestID { get; init; }
 public required byte[] TokenHash { get; init; }
}
public sealed record TrackingToken_CreateResponse
{
 public long? TrackingTokenID { get; init; }
}
public sealed record TrackingToken_ValidateRequest
{
 public required byte[] TokenHash { get; init; }
}
public sealed record TrackingToken_ValidateRow1
{
 public bool? IsValid { get; init; }
}
public sealed record TrackingToken_ValidateResponse
{
 public IReadOnlyList<TrackingToken_ValidateRow1> Rows1 { get; init; } = [];
}
public sealed record TrackingToken_RecordAccessRequest
{
 public required byte[] TokenHash { get; init; }
}
public sealed record TrackingToken_RecordAccessResponse
{
}
public sealed record TrackingToken_RevokeRequest
{
 public required long RequestID { get; init; }
 [MinLength(8), MaxLength(8)] public required byte[] ExpectedRowVersion { get; init; }
 [Required] public required string Reason { get; init; }
}
public sealed record TrackingToken_RevokeResponse
{
}
public sealed record TrackingToken_ReplaceRequest
{
 public required long RequestID { get; init; }
 [MinLength(8), MaxLength(8)] public required byte[] ExpectedRowVersion { get; init; }
 [Required] public required string Reason { get; init; }
 public required byte[] NewTokenHash { get; init; }
 public required byte[] EncryptedDeliveryPayload { get; init; }
 [Required] public required string PayloadKeyReference { get; init; }
 public required DateTime PayloadExpiresAt { get; init; }
}
public sealed record TrackingToken_ReplaceResponse
{
 public long? NewTrackingTokenID { get; init; }
}
public sealed record Tracking_GetPublicStatusRequest
{
 public required byte[] TokenHash { get; init; }
}
public sealed record Tracking_GetPublicStatusRow1
{
 public string? TicketNumber { get; init; }
 public string? ServiceDisplayName { get; init; }
 public DateTime? CreatedAt { get; init; }
 public string? PublicStatusCode { get; init; }
 public string? PublicStatusName { get; init; }
 public string? PublicDescription { get; init; }
}
public sealed record Tracking_GetPublicStatusResponse
{
 public IReadOnlyList<Tracking_GetPublicStatusRow1> Rows1 { get; init; } = [];
}
public sealed record Tracking_GetPublicTimelineRequest
{
 public required byte[] TokenHash { get; init; }
}
public sealed record Tracking_GetPublicTimelineRow1
{
 public DateTime? ChangedAt { get; init; }
 public string? PublicStatusCode { get; init; }
 public string? PublicStatusName { get; init; }
 public string? PublicDescription { get; init; }
}
public sealed record Tracking_GetPublicTimelineResponse
{
 public IReadOnlyList<Tracking_GetPublicTimelineRow1> Rows1 { get; init; } = [];
}
public sealed record CustomerStatusMapping_GetAllRequest
{
}
public sealed record CustomerStatusMapping_GetAllRow1
{
 public int? StatusMappingID { get; init; }
 public int? InternalStatusID { get; init; }
 public string? PublicStatusCode { get; init; }
 public string? PublicStatusName { get; init; }
 public string? PublicDescription { get; init; }
 public int? DisplayOrder { get; init; }
 public bool? IsTerminal { get; init; }
 public bool? IsVisible { get; init; }
 public bool? IsActive { get; init; }
 public DateTime? CreatedAt { get; init; }
 public DateTime? UpdatedAt { get; init; }
 public string? UpdatedByActorID { get; init; }
}
public sealed record CustomerStatusMapping_GetAllResponse
{
 public IReadOnlyList<CustomerStatusMapping_GetAllRow1> Rows1 { get; init; } = [];
}
public sealed record CustomerStatusMapping_UpdateRequest
{
 public required int StatusMappingID { get; init; }
 [Required] public required string PublicStatusCode { get; init; }
 [Required] public required string PublicStatusName { get; init; }
 public string? PublicDescription { get; init; }
 public int DisplayOrder { get; init; } = 0;
 public bool IsVisible { get; init; } = true;
 public bool IsActive { get; init; } = true;
}
public sealed record CustomerStatusMapping_UpdateResponse
{
}
public sealed record AuthenticationLog_CreateRequest
{
 [Required] public required string AuthenticationMethod { get; init; }
 [Required] [MaxLength(20)] public required string AuthenticationResult { get; init; }
 public string? FailureReasonCode { get; init; }
 public string? SanitizedFailure { get; init; }
 public byte[]? SessionIDHash { get; init; }
 public string? ExternalSessionReference { get; init; }
 public string? ClientIPAddress { get; init; }
 public string? ClientUserAgent { get; init; }
 [Required] public string EnvironmentCode { get; init; } = "UNSPECIFIED";
}
public sealed record AuthenticationLog_CreateResponse
{
 public long? AuthenticationLogID { get; init; }
}
public sealed record AuditLog_CreateRequest
{
 [Required] public required string EventCode { get; init; }
 [Required] public required string EventCategory { get; init; }
 [Required] public required string EntityType { get; init; }
 public string? EntityID { get; init; }
 public long? RequestID { get; init; }
 public string? Reason { get; init; }
 public string? OldValueJson { get; init; }
 public string? NewValueJson { get; init; }
 public long? RelatedActivityID { get; init; }
}
public sealed record AuditLog_CreateResponse
{
 public long? AuditID { get; init; }
}
public sealed record IntegrationLog_StartRequest
{
 [Required] public required string IntegrationCode { get; init; }
 [Required] public required string OperationCode { get; init; }
 [Required] public required string IntegrationType { get; init; }
 [Required] public required string EndpointCode { get; init; }
 [Required] public required string EnvironmentCode { get; init; }
 public long? RequestID { get; init; }
 public string? HttpMethod { get; init; }
 public string? ExternalCorrelationID { get; init; }
}
public sealed record IntegrationLog_StartResponse
{
 public long? IntegrationLogID { get; init; }
}
public sealed record IntegrationLog_CompleteRequest
{
 public required long IntegrationLogID { get; init; }
 [Required] [MaxLength(20)] public required string Result { get; init; }
 public short? HttpStatusCode { get; init; }
 public string? ProviderReferenceID { get; init; }
 public string? ErrorCode { get; init; }
 public string? SanitizedError { get; init; }
 public bool? IsRetryable { get; init; }
}
public sealed record IntegrationLog_CompleteResponse
{
}
public sealed record AuthenticationLog_SearchRequest
{
 public required DateTime FromDate { get; init; }
 public required DateTime ToDate { get; init; }
 public Guid? SearchCorrelationID { get; init; }
 [Range(1, int.MaxValue)] public int Page { get; init; } = 1;
 [Range(1, 200)] public int PageSize { get; init; } = 50;
}
public sealed record AuthenticationLog_SearchRow1
{
 public long? AuthenticationLogID { get; init; }
 public string? ActorType { get; init; }
 public string? ActorRole { get; init; }
 public string? AuthenticationSource { get; init; }
 public string? AuthenticationMethod { get; init; }
 public string? AuthenticationResult { get; init; }
 public string? FailureReasonCode { get; init; }
 public Guid? CorrelationID { get; init; }
 public string? ApplicationCode { get; init; }
 public string? EnvironmentCode { get; init; }
 public DateTime? AuthenticatedAt { get; init; }
 public long? TotalRows { get; init; }
}
public sealed record AuthenticationLog_SearchResponse
{
 public IReadOnlyList<AuthenticationLog_SearchRow1> Rows1 { get; init; } = [];
}
public sealed record AuditLog_SearchRequest
{
 public required DateTime FromDate { get; init; }
 public required DateTime ToDate { get; init; }
 public Guid? SearchCorrelationID { get; init; }
 [Range(1, int.MaxValue)] public int Page { get; init; } = 1;
 [Range(1, 200)] public int PageSize { get; init; } = 50;
}
public sealed record AuditLog_SearchRow1
{
 public long? AuditID { get; init; }
 public string? EventCode { get; init; }
 public string? EventCategory { get; init; }
 public string? EntityType { get; init; }
 public string? EntityID { get; init; }
 public long? RequestID { get; init; }
 public string? EventResult { get; init; }
 public DateTime? OccurredAt { get; init; }
 public string? ActorType { get; init; }
 public string? ActorRole { get; init; }
 public string? ApplicationCode { get; init; }
 public Guid? CorrelationID { get; init; }
 public long? TotalRows { get; init; }
}
public sealed record AuditLog_SearchResponse
{
 public IReadOnlyList<AuditLog_SearchRow1> Rows1 { get; init; } = [];
}
public sealed record AuditLog_GetByTicketRequest
{
 public required long RequestID { get; init; }
 public required DateTime FromDate { get; init; }
 public required DateTime ToDate { get; init; }
 public Guid? SearchCorrelationID { get; init; }
 [Range(1, int.MaxValue)] public int Page { get; init; } = 1;
 [Range(1, 200)] public int PageSize { get; init; } = 50;
}
public sealed record AuditLog_GetByTicketRow1
{
 public long? AuditID { get; init; }
 public string? EventCode { get; init; }
 public string? EventCategory { get; init; }
 public string? EntityType { get; init; }
 public string? EntityID { get; init; }
 public long? RequestID { get; init; }
 public string? EventResult { get; init; }
 public DateTime? OccurredAt { get; init; }
 public string? ActorType { get; init; }
 public string? ActorRole { get; init; }
 public string? ApplicationCode { get; init; }
 public Guid? CorrelationID { get; init; }
 public long? TotalRows { get; init; }
}
public sealed record AuditLog_GetByTicketResponse
{
 public IReadOnlyList<AuditLog_GetByTicketRow1> Rows1 { get; init; } = [];
}
public sealed record AuditLog_GetByActorRequest
{
 [Required] public required string SearchActorID { get; init; }
 public required DateTime FromDate { get; init; }
 public required DateTime ToDate { get; init; }
 public Guid? SearchCorrelationID { get; init; }
 [Range(1, int.MaxValue)] public int Page { get; init; } = 1;
 [Range(1, 200)] public int PageSize { get; init; } = 50;
}
public sealed record AuditLog_GetByActorRow1
{
 public long? AuditID { get; init; }
 public string? EventCode { get; init; }
 public string? EventCategory { get; init; }
 public string? EntityType { get; init; }
 public string? EntityID { get; init; }
 public long? RequestID { get; init; }
 public string? EventResult { get; init; }
 public DateTime? OccurredAt { get; init; }
 public string? ActorType { get; init; }
 public string? ActorRole { get; init; }
 public string? ApplicationCode { get; init; }
 public Guid? CorrelationID { get; init; }
 public long? TotalRows { get; init; }
}
public sealed record AuditLog_GetByActorResponse
{
 public IReadOnlyList<AuditLog_GetByActorRow1> Rows1 { get; init; } = [];
}
public sealed record IntegrationLog_SearchRequest
{
 public required DateTime FromDate { get; init; }
 public required DateTime ToDate { get; init; }
 public Guid? SearchCorrelationID { get; init; }
 [Range(1, int.MaxValue)] public int Page { get; init; } = 1;
 [Range(1, 200)] public int PageSize { get; init; } = 50;
}
public sealed record IntegrationLog_SearchRow1
{
 public long? IntegrationLogID { get; init; }
 public string? IntegrationCode { get; init; }
 public string? OperationCode { get; init; }
 public string? IntegrationType { get; init; }
 public long? RequestID { get; init; }
 public Guid? CorrelationID { get; init; }
 public string? EndpointCode { get; init; }
 public short? HttpStatusCode { get; init; }
 public string? IntegrationResult { get; init; }
 public DateTime? StartedAt { get; init; }
 public DateTime? CompletedAt { get; init; }
 public int? DurationMs { get; init; }
 public string? ErrorCode { get; init; }
 public bool? IsRetryable { get; init; }
 public long? TotalRows { get; init; }
}
public sealed record IntegrationLog_SearchResponse
{
 public IReadOnlyList<IntegrationLog_SearchRow1> Rows1 { get; init; } = [];
}
public sealed record IntegrationLog_GetFailuresRequest
{
 public required DateTime FromDate { get; init; }
 public required DateTime ToDate { get; init; }
 public Guid? SearchCorrelationID { get; init; }
 [Range(1, int.MaxValue)] public int Page { get; init; } = 1;
 [Range(1, 200)] public int PageSize { get; init; } = 50;
}
public sealed record IntegrationLog_GetFailuresRow1
{
 public long? IntegrationLogID { get; init; }
 public string? IntegrationCode { get; init; }
 public string? OperationCode { get; init; }
 public string? IntegrationType { get; init; }
 public long? RequestID { get; init; }
 public Guid? CorrelationID { get; init; }
 public string? EndpointCode { get; init; }
 public short? HttpStatusCode { get; init; }
 public string? IntegrationResult { get; init; }
 public DateTime? StartedAt { get; init; }
 public DateTime? CompletedAt { get; init; }
 public int? DurationMs { get; init; }
 public string? ErrorCode { get; init; }
 public bool? IsRetryable { get; init; }
 public long? TotalRows { get; init; }
}
public sealed record IntegrationLog_GetFailuresResponse
{
 public IReadOnlyList<IntegrationLog_GetFailuresRow1> Rows1 { get; init; } = [];
}
public sealed record Ticket_GetDashboardSummaryRequest
{
 public required DateTime FromDate { get; init; }
 public required DateTime ToDate { get; init; }
}
public sealed record Ticket_GetDashboardSummaryRow1
{
 public string? StatusCode { get; init; }
 public string? StatusName { get; init; }
 public long? TicketCount { get; init; }
}
public sealed record Ticket_GetDashboardSummaryResponse
{
 public IReadOnlyList<Ticket_GetDashboardSummaryRow1> Rows1 { get; init; } = [];
}
public sealed record Dashboard_SummaryByStatusRequest
{
 public required DateTime FromDate { get; init; }
 public required DateTime ToDate { get; init; }
}
public sealed record Dashboard_SummaryByStatusRow1
{
 public string? StatusCode { get; init; }
 public string? StatusName { get; init; }
 public long? TicketCount { get; init; }
}
public sealed record Dashboard_SummaryByStatusResponse
{
 public IReadOnlyList<Dashboard_SummaryByStatusRow1> Rows1 { get; init; } = [];
}
public sealed record Dashboard_PendingApprovalCountRequest
{
 public required DateTime FromDate { get; init; }
 public required DateTime ToDate { get; init; }
}
public sealed record Dashboard_PendingApprovalCountRow1
{
 public long? PendingApprovalCount { get; init; }
}
public sealed record Dashboard_PendingApprovalCountResponse
{
 public IReadOnlyList<Dashboard_PendingApprovalCountRow1> Rows1 { get; init; } = [];
}
public sealed record Dashboard_CriticalHighRequest
{
 public required DateTime FromDate { get; init; }
 public required DateTime ToDate { get; init; }
 [Range(1, int.MaxValue)] public int Page { get; init; } = 1;
 [Range(1, 200)] public int PageSize { get; init; } = 50;
}
public sealed record Dashboard_CriticalHighRow1
{
 public long? RequestID { get; init; }
 public string? TicketNumber { get; init; }
 public string? RequestTitle { get; init; }
 public long? ServiceID { get; init; }
 public string? PriorityCode { get; init; }
 public string? StatusCode { get; init; }
 public DateTime? CreatedAt { get; init; }
 public DateTime? ApprovalDueAt { get; init; }
 public DateTime? ResolutionDueAt { get; init; }
 public long? TotalRows { get; init; }
}
public sealed record Dashboard_CriticalHighResponse
{
 public IReadOnlyList<Dashboard_CriticalHighRow1> Rows1 { get; init; } = [];
}
public sealed record Dashboard_OverdueApprovalRequest
{
 public required DateTime FromDate { get; init; }
 public required DateTime ToDate { get; init; }
 [Range(1, int.MaxValue)] public int Page { get; init; } = 1;
 [Range(1, 200)] public int PageSize { get; init; } = 50;
}
public sealed record Dashboard_OverdueApprovalRow1
{
 public long? RequestID { get; init; }
 public string? TicketNumber { get; init; }
 public string? RequestTitle { get; init; }
 public long? ServiceID { get; init; }
 public string? PriorityCode { get; init; }
 public string? StatusCode { get; init; }
 public DateTime? CreatedAt { get; init; }
 public DateTime? ApprovalDueAt { get; init; }
 public DateTime? ResolutionDueAt { get; init; }
 public long? TotalRows { get; init; }
}
public sealed record Dashboard_OverdueApprovalResponse
{
 public IReadOnlyList<Dashboard_OverdueApprovalRow1> Rows1 { get; init; } = [];
}
public sealed record Dashboard_OverdueResolutionRequest
{
 public required DateTime FromDate { get; init; }
 public required DateTime ToDate { get; init; }
 [Range(1, int.MaxValue)] public int Page { get; init; } = 1;
 [Range(1, 200)] public int PageSize { get; init; } = 50;
}
public sealed record Dashboard_OverdueResolutionRow1
{
 public long? RequestID { get; init; }
 public string? TicketNumber { get; init; }
 public string? RequestTitle { get; init; }
 public long? ServiceID { get; init; }
 public string? PriorityCode { get; init; }
 public string? StatusCode { get; init; }
 public DateTime? CreatedAt { get; init; }
 public DateTime? ApprovalDueAt { get; init; }
 public DateTime? ResolutionDueAt { get; init; }
 public long? TotalRows { get; init; }
}
public sealed record Dashboard_OverdueResolutionResponse
{
 public IReadOnlyList<Dashboard_OverdueResolutionRow1> Rows1 { get; init; } = [];
}
public sealed record Dashboard_SentToBranchRequest
{
 public required DateTime FromDate { get; init; }
 public required DateTime ToDate { get; init; }
 [Range(1, int.MaxValue)] public int Page { get; init; } = 1;
 [Range(1, 200)] public int PageSize { get; init; } = 50;
}
public sealed record Dashboard_SentToBranchRow1
{
 public long? RequestID { get; init; }
 public string? TicketNumber { get; init; }
 public string? RequestTitle { get; init; }
 public long? ServiceID { get; init; }
 public string? PriorityCode { get; init; }
 public string? StatusCode { get; init; }
 public DateTime? CreatedAt { get; init; }
 public DateTime? ApprovalDueAt { get; init; }
 public DateTime? ResolutionDueAt { get; init; }
 public long? TotalRows { get; init; }
}
public sealed record Dashboard_SentToBranchResponse
{
 public IReadOnlyList<Dashboard_SentToBranchRow1> Rows1 { get; init; } = [];
}
public sealed record Dashboard_SentToDivisionRequest
{
 public required DateTime FromDate { get; init; }
 public required DateTime ToDate { get; init; }
 [Range(1, int.MaxValue)] public int Page { get; init; } = 1;
 [Range(1, 200)] public int PageSize { get; init; } = 50;
}
public sealed record Dashboard_SentToDivisionRow1
{
 public long? RequestID { get; init; }
 public string? TicketNumber { get; init; }
 public string? RequestTitle { get; init; }
 public long? ServiceID { get; init; }
 public string? PriorityCode { get; init; }
 public string? StatusCode { get; init; }
 public DateTime? CreatedAt { get; init; }
 public DateTime? ApprovalDueAt { get; init; }
 public DateTime? ResolutionDueAt { get; init; }
 public long? TotalRows { get; init; }
}
public sealed record Dashboard_SentToDivisionResponse
{
 public IReadOnlyList<Dashboard_SentToDivisionRow1> Rows1 { get; init; } = [];
}
public sealed record Dashboard_CompletedRequest
{
 public required DateTime FromDate { get; init; }
 public required DateTime ToDate { get; init; }
 [Range(1, int.MaxValue)] public int Page { get; init; } = 1;
 [Range(1, 200)] public int PageSize { get; init; } = 50;
}
public sealed record Dashboard_CompletedRow1
{
 public long? RequestID { get; init; }
 public string? TicketNumber { get; init; }
 public string? RequestTitle { get; init; }
 public long? ServiceID { get; init; }
 public string? PriorityCode { get; init; }
 public string? StatusCode { get; init; }
 public DateTime? CreatedAt { get; init; }
 public DateTime? ApprovalDueAt { get; init; }
 public DateTime? ResolutionDueAt { get; init; }
 public long? TotalRows { get; init; }
}
public sealed record Dashboard_CompletedResponse
{
 public IReadOnlyList<Dashboard_CompletedRow1> Rows1 { get; init; } = [];
}
public sealed record Dashboard_FailedRequest
{
 public required DateTime FromDate { get; init; }
 public required DateTime ToDate { get; init; }
 [Range(1, int.MaxValue)] public int Page { get; init; } = 1;
 [Range(1, 200)] public int PageSize { get; init; } = 50;
}
public sealed record Dashboard_FailedRow1
{
 public long? RequestID { get; init; }
 public string? TicketNumber { get; init; }
 public string? RequestTitle { get; init; }
 public long? ServiceID { get; init; }
 public string? PriorityCode { get; init; }
 public string? StatusCode { get; init; }
 public DateTime? CreatedAt { get; init; }
 public DateTime? ApprovalDueAt { get; init; }
 public DateTime? ResolutionDueAt { get; init; }
 public long? TotalRows { get; init; }
}
public sealed record Dashboard_FailedResponse
{
 public IReadOnlyList<Dashboard_FailedRow1> Rows1 { get; init; } = [];
}
public sealed record Dashboard_PendingFollowUpsRequest
{
 public required DateTime FromDate { get; init; }
 public required DateTime ToDate { get; init; }
 [Range(1, int.MaxValue)] public int Page { get; init; } = 1;
 [Range(1, 200)] public int PageSize { get; init; } = 50;
}
public sealed record Dashboard_PendingFollowUpsRow1
{
 public long? FollowUpScheduleID { get; init; }
 public long? RequestID { get; init; }
 public string? FollowUpSubject { get; init; }
 public DateTime? ScheduledAt { get; init; }
 public long? TotalRows { get; init; }
}
public sealed record Dashboard_PendingFollowUpsResponse
{
 public IReadOnlyList<Dashboard_PendingFollowUpsRow1> Rows1 { get; init; } = [];
}
public sealed record Dashboard_OverdueFollowUpsRequest
{
 public required DateTime FromDate { get; init; }
 public required DateTime ToDate { get; init; }
 [Range(1, int.MaxValue)] public int Page { get; init; } = 1;
 [Range(1, 200)] public int PageSize { get; init; } = 50;
}
public sealed record Dashboard_OverdueFollowUpsRow1
{
 public long? FollowUpScheduleID { get; init; }
 public long? RequestID { get; init; }
 public string? FollowUpSubject { get; init; }
 public DateTime? ScheduledAt { get; init; }
 public long? TotalRows { get; init; }
}
public sealed record Dashboard_OverdueFollowUpsResponse
{
 public IReadOnlyList<Dashboard_OverdueFollowUpsRow1> Rows1 { get; init; } = [];
}
public sealed record Dashboard_TicketVolumeByServiceRequest
{
 public required DateTime FromDate { get; init; }
 public required DateTime ToDate { get; init; }
 [Range(1, int.MaxValue)] public int Page { get; init; } = 1;
 [Range(1, 200)] public int PageSize { get; init; } = 50;
}
public sealed record Dashboard_TicketVolumeByServiceRow1
{
 public long? GroupKey { get; init; }
 public long? TicketCount { get; init; }
}
public sealed record Dashboard_TicketVolumeByServiceResponse
{
 public IReadOnlyList<Dashboard_TicketVolumeByServiceRow1> Rows1 { get; init; } = [];
}
public sealed record Dashboard_TicketVolumeByDateRequest
{
 public required DateTime FromDate { get; init; }
 public required DateTime ToDate { get; init; }
 [Range(1, int.MaxValue)] public int Page { get; init; } = 1;
 [Range(1, 200)] public int PageSize { get; init; } = 50;
}
public sealed record Dashboard_TicketVolumeByDateRow1
{
 public DateTime? GroupKey { get; init; }
 public long? TicketCount { get; init; }
}
public sealed record Dashboard_TicketVolumeByDateResponse
{
 public IReadOnlyList<Dashboard_TicketVolumeByDateRow1> Rows1 { get; init; } = [];
}
public sealed record Dashboard_AgentCreatedTicketCountsRequest
{
 public required DateTime FromDate { get; init; }
 public required DateTime ToDate { get; init; }
 [Range(1, int.MaxValue)] public int Page { get; init; } = 1;
 [Range(1, 200)] public int PageSize { get; init; } = 50;
}
public sealed record Dashboard_AgentCreatedTicketCountsRow1
{
 public string? GroupKey { get; init; }
 public long? TicketCount { get; init; }
}
public sealed record Dashboard_AgentCreatedTicketCountsResponse
{
 public IReadOnlyList<Dashboard_AgentCreatedTicketCountsRow1> Rows1 { get; init; } = [];
}
public sealed record Dashboard_ManagerActivitySummaryRequest
{
 public required DateTime FromDate { get; init; }
 public required DateTime ToDate { get; init; }
 [Range(1, int.MaxValue)] public int Page { get; init; } = 1;
 [Range(1, 200)] public int PageSize { get; init; } = 50;
}
public sealed record Dashboard_ManagerActivitySummaryRow1
{
 public string? ManagerID { get; init; }
 public string? ActivityType { get; init; }
 public long? ActivityCount { get; init; }
}
public sealed record Dashboard_ManagerActivitySummaryResponse
{
 public IReadOnlyList<Dashboard_ManagerActivitySummaryRow1> Rows1 { get; init; } = [];
}
public sealed record CommunicationOutbox_SetMaxAttemptsRequest
{
 public required long CommunicationID { get; init; }
 [MinLength(8), MaxLength(8)] public required byte[] ExpectedRowVersion { get; init; }
 public required int MaxAttempts { get; init; }
 [Required] public required string Reason { get; init; }
}
public sealed record CommunicationOutbox_SetMaxAttemptsResponse
{
}
