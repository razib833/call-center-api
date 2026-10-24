# API stored procedure mapping

All 128 SQL source files reviewed; four internal helpers have no public data-access method or endpoint. The 124 callable contracts have typed request/response and data-access methods. Rows derive from actual SELECT ASTs/table definitions, not earlier prose summaries. SQL binding remains pending. All endpoints use POST to keep customer filters/tokens out of URLs.

| Procedure | Capability | Parameters (OUTPUT noted) | Result sets | HTTP path or service only |
|---|---|---|---|---|
| usp_Priority_GetAll | READ |  | 1 | /api/v1/reference/priority/get-all |
| usp_Priority_GetActive | READ |  | 1 | /api/v1/reference/priority/get-active |
| usp_Priority_Create | CONFIG | PriorityCode, PriorityName, PriorityDescription, PriorityRank, DisplayOrder, IsActive, NewID OUTPUT | 0 | /api/v1/reference/priority/create |
| usp_Priority_Update | CONFIG | PriorityID, PriorityName, PriorityDescription, PriorityRank, DisplayOrder, ExpectedRowVersion | 0 | /api/v1/reference/priority/update |
| usp_Priority_SetActive | CONFIG | PriorityID, IsActive, ExpectedRowVersion | 0 | /api/v1/reference/priority/set-active |
| usp_RequestSource_GetAll | READ |  | 1 | /api/v1/reference/request-source/get-all |
| usp_RequestSource_GetActive | READ |  | 1 | /api/v1/reference/request-source/get-active |
| usp_RequestSource_Create | CONFIG | RequestSourceCode, RequestSourceName, SourceDescription, DisplayOrder, IsActive, NewID OUTPUT | 0 | /api/v1/reference/request-source/create |
| usp_RequestSource_Update | CONFIG | RequestSourceCode, RequestSourceName, SourceDescription, DisplayOrder, ExpectedRowVersion | 0 | /api/v1/reference/request-source/update |
| usp_RequestSource_SetActive | CONFIG | RequestSourceCode, IsActive, ExpectedRowVersion | 0 | /api/v1/reference/request-source/set-active |
| usp_TicketStatus_GetAll | READ |  | 1 | /api/v1/reference/ticket-status/get-all |
| usp_TicketStatus_GetActive | READ |  | 1 | /api/v1/reference/ticket-status/get-active |
| usp_TicketStatusTransition_GetAllowed | READ | RequestID | 1 | /api/v1/reference/ticket-status-transition/get-allowed |
| usp_ServiceCategory_Create | CONFIG | CategoryCode, CategoryName, CategoryDescription, DisplayOrder, IsActive, NewID OUTPUT | 0 | /api/v1/configuration/service-category/create |
| usp_ServiceCategory_Update | CONFIG | CategoryID, CategoryName, CategoryDescription, DisplayOrder | 0 | /api/v1/configuration/service-category/update |
| usp_ServiceCategory_SetActive | CONFIG | CategoryID, IsActive | 0 | /api/v1/configuration/service-category/set-active |
| usp_ServiceCategory_GetByID | READ | ID | 1 | /api/v1/configuration/service-category/get-by-id |
| usp_ServiceCategory_List | READ | Page, PageSize | 1 | /api/v1/configuration/service-category/list |
| usp_Service_Create | CONFIG | CategoryID, ServiceCode, ServiceName, ShortName, VeryShortName, ServiceTitle, Description, IsActive, NewID OUTPUT | 0 | /api/v1/configuration/service/create |
| usp_Service_Update | CONFIG | ServiceID, CategoryID, ServiceName, ShortName, VeryShortName, ServiceTitle, Description | 0 | /api/v1/configuration/service/update |
| usp_Service_SetActive | CONFIG | ServiceID, IsActive | 0 | /api/v1/configuration/service/set-active |
| usp_Service_GetByID | READ | ID | 1 | /api/v1/configuration/service/get-by-id |
| usp_Service_List | READ | Page, PageSize | 1 | /api/v1/configuration/service/list |
| usp_ServiceVersion_CreateDraft | CONFIG | ServiceID, NewVersionID OUTPUT | 0 | /api/v1/configuration/service-version/create-draft |
| usp_ServiceVersion_UpdateDraft | CONFIG | VersionID, CategoryID, VersionServiceName, VersionShortName, VersionVeryShortName, VersionServiceTitle, VersionDescription, ChangeRemarks, ExpectedVersionRowVersion | 0 | /api/v1/configuration/service-version/update-draft |
| usp_ServiceVersion_GetDraft | READ | ServiceID | 1 | /api/v1/configuration/service-version/get-draft |
| usp_ServiceVersion_GetPublished | READ | ServiceID | 1 | /api/v1/configuration/service-version/get-published |
| usp_ServiceVersion_GetHistory | READ | ServiceID | 1 | /api/v1/configuration/service-version/get-history |
| usp_ServiceVersion_ClonePublished | CONFIG | ServiceID, SourceVersionID, NewVersionID OUTPUT | 0 | /api/v1/configuration/service-version/clone-published |
| usp_ServiceVersion_PublishDraft | CONFIG | VersionID, ExpectedRowVersion | 0 | /api/v1/configuration/service-version/publish-draft |
| usp_ServiceVersion_RetirePublished | CONFIG | VersionID, ExpectedRowVersion | 0 | /api/v1/configuration/service-version/retire-published |
| usp_ServiceField_Add | CONFIG | VersionID, FieldKey, FieldLabel, FieldType, DropdownSourceType, DropdownListVersionID, Placeholder, HelpText, DefaultValue, IsRequired, IsReadOnly, IsVisible, MinLength, MaxLength, ValidationPattern, DataSourceKey, DisplayOrder, IsActive, NewID OUTPUT, ExpectedVersionRowVersion | 0 | /api/v1/configuration/service-field/add |
| usp_ServiceField_Update | CONFIG | FieldID, FieldLabel, FieldType, DropdownSourceType, DropdownListVersionID, Placeholder, HelpText, DefaultValue, IsRequired, IsReadOnly, IsVisible, MinLength, MaxLength, ValidationPattern, DataSourceKey, DisplayOrder, IsActive, ExpectedVersionRowVersion | 0 | /api/v1/configuration/service-field/update |
| usp_ServiceField_Deactivate | CONFIG | FieldID, ExpectedVersionRowVersion | 0 | /api/v1/configuration/service-field/deactivate |
| usp_ServiceField_GetByVersion | READ | VersionID | 1 | /api/v1/configuration/service-field/get-by-version |
| usp_ServiceFieldOption_Add | CONFIG | FieldID, OptionLabel, OptionValue, DisplayOrder, IsDefault, IsActive, NewID OUTPUT, ExpectedVersionRowVersion | 0 | /api/v1/configuration/service-field-option/add |
| usp_ServiceFieldOption_Update | CONFIG | OptionID, OptionLabel, OptionValue, DisplayOrder, IsDefault, IsActive, ExpectedVersionRowVersion | 0 | /api/v1/configuration/service-field-option/update |
| usp_ServiceFieldOption_Get | READ | FieldID | 1 | /api/v1/configuration/service-field-option/get |
| usp_DropdownList_Create | CONFIG | ListCode, ListName, Description, IsActive, NewID OUTPUT | 0 | /api/v1/configuration/dropdown-list/create |
| usp_DropdownListVersion_Create | CONFIG | DropdownListID, NewVersionID OUTPUT | 0 | /api/v1/configuration/dropdown-list-version/create |
| usp_DropdownListOption_Add | CONFIG | DropdownListVersionID, OptionLabel, OptionValue, DisplayOrder, IsDefault, IsActive, NewID OUTPUT, ExpectedVersionRowVersion | 0 | /api/v1/configuration/dropdown-list-option/add |
| usp_DropdownListOption_Update | CONFIG | DropdownOptionID, OptionLabel, OptionValue, DisplayOrder, IsDefault, IsActive, ExpectedVersionRowVersion | 0 | /api/v1/configuration/dropdown-list-option/update |
| usp_DropdownListVersion_Publish | CONFIG | DropdownListVersionID, ExpectedRowVersion | 0 | /api/v1/configuration/dropdown-list-version/publish |
| usp_DropdownList_GetPublishedOptions | READ | DropdownListID | 1 | /api/v1/configuration/dropdown-list/get-published-options |
| usp_ServiceWorkflow_Save | CONFIG | VersionID, DefaultPriorityID, ApprovalTargetMinutes, ResolutionTargetMinutes, AllowForwardToBranch, AllowForwardToDivision, FollowUpRequired, DefaultFollowUpMinutes, ExpectedVersionRowVersion | 0 | /api/v1/configuration/service-workflow/save |
| usp_ServiceWorkflow_Get | READ | VersionID | 1 | /api/v1/configuration/service-workflow/get |
| usp_ServiceNotificationConfig_Save | CONFIG | VersionID, EventCode, ChannelCode, TemplateID, IsEnabled, ExpectedVersionRowVersion | 0 | /api/v1/configuration/service-notification-config/save |
| usp_ServiceNotificationConfig_Get | READ | VersionID | 1 | /api/v1/configuration/service-notification-config/get |
| usp_NotificationTemplate_CreateDraft | CONFIG | TemplateCode, TemplateVersion, TemplateName, ChannelCode, TemplateSubject, TemplateBody, Description, NewID OUTPUT | 0 | /api/v1/communications/notification-template/create-draft |
| usp_NotificationTemplate_UpdateDraft | CONFIG | TemplateID, TemplateName, TemplateSubject, TemplateBody, Description, ExpectedRowVersion | 0 | /api/v1/communications/notification-template/update-draft |
| usp_NotificationTemplate_Publish | CONFIG | TemplateID, ExpectedRowVersion | 0 | /api/v1/communications/notification-template/publish |
| usp_NotificationTemplate_GetPublished | READ | TemplateCode | 1 | /api/v1/communications/notification-template/get-published |
| usp_Ticket_Create | AGENT | IdempotencyKey, ServiceID, VersionID, AvayaInteractionID, FieldValuesJson, TrackingTokenHash, EncryptedDeliveryPayload, PayloadKeyReference, PayloadExpiresAt, RequestID OUTPUT, TicketNumber OUTPUT, RequestDescription, CustomerName, CustomerMobile, CBS_CIF, AccountNumber, CardClientID, CardCIF, CardLast4, InternetBankingUserID | 0 | Application / internal worker service only |
| usp_Ticket_Approve | MANAGER | RequestID, ExpectedRowVersion, Remarks | 0 | /api/v1/tickets/ticket/approve |
| usp_Ticket_Reject | MANAGER | RequestID, ExpectedRowVersion, Remarks | 0 | /api/v1/tickets/ticket/reject |
| usp_Ticket_UpdateStatus | MANAGER | RequestID, ToStatusID, ExpectedRowVersion, Remarks | 0 | /api/v1/tickets/ticket/update-status |
| usp_Ticket_GetByID | READ | RequestID | 2 | /api/v1/tickets/ticket/get-by-id |
| usp_Ticket_GetByNumber | READ | TicketNumber | 2 | /api/v1/tickets/ticket/get-by-number |
| usp_Ticket_Search | READ | TicketNumber, CustomerMobile, CBS_CIF, AccountNumber, CardClientID, CardCIF, CardLast4, InternetBankingUserID, CustomerName, ServiceID, PriorityID, StatusID, FromDate, ToDate, Page, PageSize | 1 | /api/v1/tickets/ticket/search |
| usp_Ticket_GetRecentByCustomer | READ | CustomerMobile, CBS_CIF, Page, PageSize | 1 | /api/v1/tickets/ticket/get-recent-by-customer |
| usp_Ticket_GetPendingApproval | MANAGER | FromDate, ToDate, Page, PageSize | 1 | /api/v1/tickets/ticket/get-pending-approval |
| usp_Ticket_GetPriorityQueue | MANAGER | FromDate, ToDate, Page, PageSize | 1 | /api/v1/tickets/ticket/get-priority-queue |
| usp_Ticket_GetStatusHistory | READ | RequestID | 1 | /api/v1/tickets/ticket/get-status-history |
| usp_Ticket_GetActivityHistory | READ | RequestID | 1 | /api/v1/tickets/ticket/get-activity-history |
| usp_FollowUp_Add | MANAGER | RequestID, FollowUpType, Note, FollowUpID OUTPUT, RelatedForwardingID | 0 | /api/v1/follow/follow-up/add |
| usp_FollowUp_GetByTicket | MANAGER | RequestID | 1 | /api/v1/follow/follow-up/get-by-ticket |
| usp_FollowUpSchedule_Create | MANAGER | RequestID, Subject, ScheduledAt, ScheduleID OUTPUT, Description, AssignedManagerID, RelatedFollowUpID, RelatedForwardingID | 0 | /api/v1/follow/follow-up-schedule/create |
| usp_FollowUpSchedule_GetByTicket | MANAGER | RequestID | 1 | /api/v1/follow/follow-up-schedule/get-by-ticket |
| usp_FollowUpSchedule_GetDue | MANAGER | Until, Page, PageSize | 1 | /api/v1/follow/follow-up-schedule/get-due |
| usp_FollowUpSchedule_Complete | MANAGER | ScheduleID, ExpectedRowVersion, Remarks | 0 | /api/v1/follow/follow-up-schedule/complete |
| usp_FollowUpSchedule_Cancel | MANAGER | ScheduleID, ExpectedRowVersion, Remarks | 0 | /api/v1/follow/follow-up-schedule/cancel |
| usp_Forwarding_Create | MANAGER | RequestID, ExpectedRowVersion, ForwardingType, DestinationName, ToAddresses, Subject, Body, ForwardingID OUTPUT, CcAddresses, Remarks, TemplateID | 0 | /api/v1/follow/forwarding/create |
| usp_Forwarding_GetByTicket | MANAGER | RequestID | 1 | /api/v1/follow/forwarding/get-by-ticket |
| usp_Forwarding_GetPending | WORKER | Page, PageSize | 1 | Application / internal worker service only |
| usp_Forwarding_MarkSubmitting | WORKER | ForwardingID, ExpectedRowVersion, LeaseID | 0 | Application / internal worker service only |
| usp_Forwarding_RecordAccepted | WORKER | ForwardingID, ProviderMessageID, LeaseID | 0 | Application / internal worker service only |
| usp_Forwarding_RecordFailed | WORKER | ForwardingID, LeaseID, Reason | 0 | Application / internal worker service only |
| usp_Communication_Create | MANAGER | RequestID, EventCode, CommunicationID OUTPUT, EncryptedPayload, KeyReference, PayloadExpiresAt | 0 | Application / internal worker service only |
| usp_Communication_GetByTicket | MANAGER | ID | 1 | /api/v1/communications/communication/get-by-ticket |
| usp_Communication_GetByID | MANAGER | ID | 1 | /api/v1/communications/communication/get-by-id |
| usp_CommunicationOutbox_Claim | WORKER | BatchSize, LeaseSeconds | 1 | Application / internal worker service only |
| usp_CommunicationAttempt_Start | WORKER | CommunicationID, LeaseID, ProviderCode, ProviderRequestID, AttemptID OUTPUT | 0 | Application / internal worker service only |
| usp_CommunicationAttempt_Complete | WORKER | AttemptID, LeaseID, ProviderRequestID, Result, ProviderMessageID, ErrorCode, SanitizedError, IsRetryable | 0 | Application / internal worker service only |
| usp_Communication_RecordProviderResult | WORKER | AttemptID, LeaseID, ProviderRequestID, Result, ProviderMessageID, ErrorCode, SanitizedError, IsRetryable | 0 | Application / internal worker service only |
| usp_CommunicationOutbox_Complete | WORKER | CommunicationID, ExpectedRowVersion | 0 | Application / internal worker service only |
| usp_CommunicationOutbox_Fail | WORKER | CommunicationID, ExpectedRowVersion | 0 | Application / internal worker service only |
| usp_CommunicationOutbox_ScheduleRetry | WORKER | CommunicationID, ExpectedRowVersion, NextRetryAt | 0 | Application / internal worker service only |
| usp_CommunicationOutbox_ManualRetry | MANAGER | CommunicationID, ExpectedRowVersion, Reason | 0 | /api/v1/communications/communication-outbox/manual-retry |
| usp_TrackingToken_Create | MANAGER | RequestID, TokenHash, TrackingTokenID OUTPUT | 0 | Application / internal worker service only |
| usp_TrackingToken_Validate | PUBLIC | TokenHash | 1 | Application / internal worker service only |
| usp_TrackingToken_RecordAccess | PUBLIC | TokenHash | 0 | Application / internal worker service only |
| usp_TrackingToken_Revoke | MANAGER | RequestID, ExpectedRowVersion, Reason | 0 | Application / internal worker service only |
| usp_TrackingToken_Replace | MANAGER | RequestID, ExpectedRowVersion, Reason, NewTokenHash, EncryptedDeliveryPayload, PayloadKeyReference, PayloadExpiresAt, NewTrackingTokenID OUTPUT | 0 | Application / internal worker service only |
| usp_Tracking_GetPublicStatus | PUBLIC | TokenHash | 1 | Application / internal worker service only |
| usp_Tracking_GetPublicTimeline | PUBLIC | TokenHash | 1 | Application / internal worker service only |
| usp_CustomerStatusMapping_GetAll | CONFIG |  | 1 | /api/v1/track/customer-status-mapping/get-all |
| usp_CustomerStatusMapping_Update | CONFIG | StatusMappingID, PublicStatusCode, PublicStatusName, PublicDescription, DisplayOrder, IsVisible, IsActive | 0 | /api/v1/track/customer-status-mapping/update |
| usp_AuthenticationLog_Create | TRUSTED | AuthenticationMethod, AuthenticationResult, AuthenticationLogID OUTPUT, FailureReasonCode, SanitizedFailure, SessionIDHash, ExternalSessionReference, ClientIPAddress, ClientUserAgent, EnvironmentCode | 0 | Application / internal worker service only |
| usp_AuditLog_Create | TRUSTED | EventCode, EventCategory, EntityType, AuditID OUTPUT, EntityID, RequestID, Reason, OldValueJson, NewValueJson, RelatedActivityID | 0 | Application / internal worker service only |
| usp_IntegrationLog_Start | TRUSTED | IntegrationCode, OperationCode, IntegrationType, EndpointCode, EnvironmentCode, IntegrationLogID OUTPUT, RequestID, HttpMethod, ExternalCorrelationID | 0 | Application / internal worker service only |
| usp_IntegrationLog_Complete | TRUSTED | IntegrationLogID, Result, HttpStatusCode, ProviderReferenceID, ErrorCode, SanitizedError, IsRetryable | 0 | Application / internal worker service only |
| usp_AuthenticationLog_Search | AUDIT | FromDate, ToDate, SearchCorrelationID, Page, PageSize | 1 | /api/v1/audit/authentication-log/search |
| usp_AuditLog_Search | AUDIT | FromDate, ToDate, SearchCorrelationID, Page, PageSize | 1 | /api/v1/audit/audit-log/search |
| usp_AuditLog_GetByTicket | AUDIT | RequestID, FromDate, ToDate, SearchCorrelationID, Page, PageSize | 1 | /api/v1/audit/audit-log/get-by-ticket |
| usp_AuditLog_GetByActor | AUDIT | SearchActorID, FromDate, ToDate, SearchCorrelationID, Page, PageSize | 1 | /api/v1/audit/audit-log/get-by-actor |
| usp_IntegrationLog_Search | AUDIT | FromDate, ToDate, SearchCorrelationID, Page, PageSize | 1 | /api/v1/audit/integration-log/search |
| usp_IntegrationLog_GetFailures | AUDIT | FromDate, ToDate, SearchCorrelationID, Page, PageSize | 1 | /api/v1/audit/integration-log/get-failures |
| usp_Ticket_GetDashboardSummary | MANAGER | FromDate, ToDate | 1 | /api/v1/tickets/ticket/get-dashboard-summary |
| usp_Dashboard_SummaryByStatus | MANAGER | FromDate, ToDate | 1 | /api/v1/report/dashboard/summary-by-status |
| usp_Dashboard_PendingApprovalCount | MANAGER | FromDate, ToDate | 1 | /api/v1/report/dashboard/pending-approval-count |
| usp_Dashboard_CriticalHigh | MANAGER | FromDate, ToDate, Page, PageSize | 1 | /api/v1/report/dashboard/critical-high |
| usp_Dashboard_OverdueApproval | MANAGER | FromDate, ToDate, Page, PageSize | 1 | /api/v1/report/dashboard/overdue-approval |
| usp_Dashboard_OverdueResolution | MANAGER | FromDate, ToDate, Page, PageSize | 1 | /api/v1/report/dashboard/overdue-resolution |
| usp_Dashboard_SentToBranch | MANAGER | FromDate, ToDate, Page, PageSize | 1 | /api/v1/report/dashboard/sent-to-branch |
| usp_Dashboard_SentToDivision | MANAGER | FromDate, ToDate, Page, PageSize | 1 | /api/v1/report/dashboard/sent-to-division |
| usp_Dashboard_Completed | MANAGER | FromDate, ToDate, Page, PageSize | 1 | /api/v1/report/dashboard/completed |
| usp_Dashboard_Failed | MANAGER | FromDate, ToDate, Page, PageSize | 1 | /api/v1/report/dashboard/failed |
| usp_Dashboard_PendingFollowUps | MANAGER | FromDate, ToDate, Page, PageSize | 1 | /api/v1/report/dashboard/pending-follow-ups |
| usp_Dashboard_OverdueFollowUps | MANAGER | FromDate, ToDate, Page, PageSize | 1 | /api/v1/report/dashboard/overdue-follow-ups |
| usp_Dashboard_TicketVolumeByService | MANAGER | FromDate, ToDate, Page, PageSize | 1 | /api/v1/report/dashboard/ticket-volume-by-service |
| usp_Dashboard_TicketVolumeByDate | MANAGER | FromDate, ToDate, Page, PageSize | 1 | /api/v1/report/dashboard/ticket-volume-by-date |
| usp_Dashboard_AgentCreatedTicketCounts | MANAGER | FromDate, ToDate, Page, PageSize | 1 | /api/v1/report/dashboard/agent-created-ticket-counts |
| usp_Dashboard_ManagerActivitySummary | MANAGER | FromDate, ToDate, Page, PageSize | 1 | /api/v1/report/dashboard/manager-activity-summary |
| usp_CommunicationOutbox_SetMaxAttempts | CONFIG | CommunicationID, ExpectedRowVersion, MaxAttempts, Reason | 0 | /api/v1/communications/communication-outbox/set-max-attempts |