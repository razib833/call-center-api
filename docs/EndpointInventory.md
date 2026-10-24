# Endpoint inventory

Generated from actual controller signatures and policies. All procedure endpoints use typed POST bodies. A documented route does not imply a live database/provider is configured; see KnownGaps.md. Workers/internal helpers have no general HTTP execution endpoint. Health is a liveness-only endpoint outside this inventory; OpenAPI is development/test-only.

| Method | Path | Authorization | Request model |
|---|---|---|---|
| POST | /api/v1/audit/authentication-log/search | Required capability: AUDIT | AuthenticationLog_SearchRequest |
| POST | /api/v1/audit/audit-log/search | Required capability: AUDIT | AuditLog_SearchRequest |
| POST | /api/v1/audit/audit-log/get-by-ticket | Required capability: AUDIT | AuditLog_GetByTicketRequest |
| POST | /api/v1/audit/audit-log/get-by-actor | Required capability: AUDIT | AuditLog_GetByActorRequest |
| POST | /api/v1/audit/integration-log/search | Required capability: AUDIT | IntegrationLog_SearchRequest |
| POST | /api/v1/audit/integration-log/get-failures | Required capability: AUDIT | IntegrationLog_GetFailuresRequest |
| GET | /api/v1/auth/csrf | Public or trusted-verifier entry point. No caller-supplied role is trusted. | None |
| POST | /api/v1/auth/avaya | Public or trusted-verifier entry point. No caller-supplied role is trusted. | AvayaAssertion |
| POST | /api/v1/auth/manager | Public or trusted-verifier entry point. No caller-supplied role is trusted. | ManagerCredentials |
| POST | /api/v1/auth/development/session | Public or trusted-verifier entry point. No caller-supplied role is trusted. | DevelopmentSession |
| GET | /api/v1/auth/session | Required capability: authenticated session | None |
| POST | /api/v1/auth/logout | Required capability: authenticated session | None |
| POST | /api/v1/communications/notification-template/create-draft | Required capability: CONFIG | NotificationTemplate_CreateDraftRequest |
| POST | /api/v1/communications/notification-template/update-draft | Required capability: CONFIG | NotificationTemplate_UpdateDraftRequest |
| POST | /api/v1/communications/notification-template/publish | Required capability: CONFIG | NotificationTemplate_PublishRequest |
| POST | /api/v1/communications/notification-template/get-published | Required capability: READ | NotificationTemplate_GetPublishedRequest |
| POST | /api/v1/communications/communication/get-by-ticket | Required capability: MANAGER | Communication_GetByTicketRequest |
| POST | /api/v1/communications/communication/get-by-id | Required capability: MANAGER | Communication_GetByIDRequest |
| POST | /api/v1/communications/communication-outbox/manual-retry | Required capability: MANAGER | CommunicationOutbox_ManualRetryRequest |
| POST | /api/v1/communications/communication-outbox/set-max-attempts | Required capability: CONFIG | CommunicationOutbox_SetMaxAttemptsRequest |
| POST | /api/v1/configuration/service-category/create | Required capability: CONFIG | ServiceCategory_CreateRequest |
| POST | /api/v1/configuration/service-category/update | Required capability: CONFIG | ServiceCategory_UpdateRequest |
| POST | /api/v1/configuration/service-category/set-active | Required capability: CONFIG | ServiceCategory_SetActiveRequest |
| POST | /api/v1/configuration/service-category/get-by-id | Required capability: READ | ServiceCategory_GetByIDRequest |
| POST | /api/v1/configuration/service-category/list | Required capability: READ | ServiceCategory_ListRequest |
| POST | /api/v1/configuration/service/create | Required capability: CONFIG | Service_CreateRequest |
| POST | /api/v1/configuration/service/update | Required capability: CONFIG | Service_UpdateRequest |
| POST | /api/v1/configuration/service/set-active | Required capability: CONFIG | Service_SetActiveRequest |
| POST | /api/v1/configuration/service/get-by-id | Required capability: READ | Service_GetByIDRequest |
| POST | /api/v1/configuration/service/list | Required capability: READ | Service_ListRequest |
| POST | /api/v1/configuration/service-version/create-draft | Required capability: CONFIG | ServiceVersion_CreateDraftRequest |
| POST | /api/v1/configuration/service-version/update-draft | Required capability: CONFIG | ServiceVersion_UpdateDraftRequest |
| POST | /api/v1/configuration/service-version/get-draft | Required capability: READ | ServiceVersion_GetDraftRequest |
| POST | /api/v1/configuration/service-version/get-published | Required capability: READ | ServiceVersion_GetPublishedRequest |
| POST | /api/v1/configuration/service-version/get-history | Required capability: READ | ServiceVersion_GetHistoryRequest |
| POST | /api/v1/configuration/service-version/clone-published | Required capability: CONFIG | ServiceVersion_ClonePublishedRequest |
| POST | /api/v1/configuration/service-version/publish-draft | Required capability: CONFIG | ServiceVersion_PublishDraftRequest |
| POST | /api/v1/configuration/service-version/retire-published | Required capability: CONFIG | ServiceVersion_RetirePublishedRequest |
| POST | /api/v1/configuration/service-field/add | Required capability: CONFIG | ServiceField_AddRequest |
| POST | /api/v1/configuration/service-field/update | Required capability: CONFIG | ServiceField_UpdateRequest |
| POST | /api/v1/configuration/service-field/deactivate | Required capability: CONFIG | ServiceField_DeactivateRequest |
| POST | /api/v1/configuration/service-field/get-by-version | Required capability: READ | ServiceField_GetByVersionRequest |
| POST | /api/v1/configuration/service-field-option/add | Required capability: CONFIG | ServiceFieldOption_AddRequest |
| POST | /api/v1/configuration/service-field-option/update | Required capability: CONFIG | ServiceFieldOption_UpdateRequest |
| POST | /api/v1/configuration/service-field-option/get | Required capability: READ | ServiceFieldOption_GetRequest |
| POST | /api/v1/configuration/dropdown-list/create | Required capability: CONFIG | DropdownList_CreateRequest |
| POST | /api/v1/configuration/dropdown-list-version/create | Required capability: CONFIG | DropdownListVersion_CreateRequest |
| POST | /api/v1/configuration/dropdown-list-option/add | Required capability: CONFIG | DropdownListOption_AddRequest |
| POST | /api/v1/configuration/dropdown-list-option/update | Required capability: CONFIG | DropdownListOption_UpdateRequest |
| POST | /api/v1/configuration/dropdown-list-version/publish | Required capability: CONFIG | DropdownListVersion_PublishRequest |
| POST | /api/v1/configuration/dropdown-list/get-published-options | Required capability: READ | DropdownList_GetPublishedOptionsRequest |
| POST | /api/v1/configuration/service-workflow/save | Required capability: CONFIG | ServiceWorkflow_SaveRequest |
| POST | /api/v1/configuration/service-workflow/get | Required capability: READ | ServiceWorkflow_GetRequest |
| POST | /api/v1/configuration/service-notification-config/save | Required capability: CONFIG | ServiceNotificationConfig_SaveRequest |
| POST | /api/v1/configuration/service-notification-config/get | Required capability: READ | ServiceNotificationConfig_GetRequest |
| POST | /api/v1/customers/search | Required capability: READ | CustomerSearch |
| POST | /api/v1/customers/profile | Required capability: READ | CustomerReference |
| POST | /api/v1/customers/accounts | Required capability: READ | CustomerReference |
| POST | /api/v1/customers/account-details | Required capability: READ | CustomerReference |
| POST | /api/v1/customers/cards | Required capability: READ | CustomerReference |
| POST | /api/v1/customers/card-transactions | Required capability: READ | CustomerReference |
| POST | /api/v1/customers/internet-banking | Required capability: READ | CustomerReference |
| POST | /api/v1/follow/follow-up/add | Required capability: MANAGER | FollowUp_AddRequest |
| POST | /api/v1/follow/follow-up/get-by-ticket | Required capability: MANAGER | FollowUp_GetByTicketRequest |
| POST | /api/v1/follow/follow-up-schedule/create | Required capability: MANAGER | FollowUpSchedule_CreateRequest |
| POST | /api/v1/follow/follow-up-schedule/get-by-ticket | Required capability: MANAGER | FollowUpSchedule_GetByTicketRequest |
| POST | /api/v1/follow/follow-up-schedule/get-due | Required capability: MANAGER | FollowUpSchedule_GetDueRequest |
| POST | /api/v1/follow/follow-up-schedule/complete | Required capability: MANAGER | FollowUpSchedule_CompleteRequest |
| POST | /api/v1/follow/follow-up-schedule/cancel | Required capability: MANAGER | FollowUpSchedule_CancelRequest |
| POST | /api/v1/follow/forwarding/create | Required capability: MANAGER | Forwarding_CreateRequest |
| POST | /api/v1/follow/forwarding/get-by-ticket | Required capability: MANAGER | Forwarding_GetByTicketRequest |
| POST | /api/v1/reference/priority/get-all | Required capability: READ | Priority_GetAllRequest |
| POST | /api/v1/reference/priority/get-active | Required capability: READ | Priority_GetActiveRequest |
| POST | /api/v1/reference/priority/create | Required capability: CONFIG | Priority_CreateRequest |
| POST | /api/v1/reference/priority/update | Required capability: CONFIG | Priority_UpdateRequest |
| POST | /api/v1/reference/priority/set-active | Required capability: CONFIG | Priority_SetActiveRequest |
| POST | /api/v1/reference/request-source/get-all | Required capability: READ | RequestSource_GetAllRequest |
| POST | /api/v1/reference/request-source/get-active | Required capability: READ | RequestSource_GetActiveRequest |
| POST | /api/v1/reference/request-source/create | Required capability: CONFIG | RequestSource_CreateRequest |
| POST | /api/v1/reference/request-source/update | Required capability: CONFIG | RequestSource_UpdateRequest |
| POST | /api/v1/reference/request-source/set-active | Required capability: CONFIG | RequestSource_SetActiveRequest |
| POST | /api/v1/reference/ticket-status/get-all | Required capability: READ | TicketStatus_GetAllRequest |
| POST | /api/v1/reference/ticket-status/get-active | Required capability: READ | TicketStatus_GetActiveRequest |
| POST | /api/v1/reference/ticket-status-transition/get-allowed | Required capability: READ | TicketStatusTransition_GetAllowedRequest |
| POST | /api/v1/report/dashboard/summary-by-status | Required capability: MANAGER | Dashboard_SummaryByStatusRequest |
| POST | /api/v1/report/dashboard/pending-approval-count | Required capability: MANAGER | Dashboard_PendingApprovalCountRequest |
| POST | /api/v1/report/dashboard/critical-high | Required capability: MANAGER | Dashboard_CriticalHighRequest |
| POST | /api/v1/report/dashboard/overdue-approval | Required capability: MANAGER | Dashboard_OverdueApprovalRequest |
| POST | /api/v1/report/dashboard/overdue-resolution | Required capability: MANAGER | Dashboard_OverdueResolutionRequest |
| POST | /api/v1/report/dashboard/sent-to-branch | Required capability: MANAGER | Dashboard_SentToBranchRequest |
| POST | /api/v1/report/dashboard/sent-to-division | Required capability: MANAGER | Dashboard_SentToDivisionRequest |
| POST | /api/v1/report/dashboard/completed | Required capability: MANAGER | Dashboard_CompletedRequest |
| POST | /api/v1/report/dashboard/failed | Required capability: MANAGER | Dashboard_FailedRequest |
| POST | /api/v1/report/dashboard/pending-follow-ups | Required capability: MANAGER | Dashboard_PendingFollowUpsRequest |
| POST | /api/v1/report/dashboard/overdue-follow-ups | Required capability: MANAGER | Dashboard_OverdueFollowUpsRequest |
| POST | /api/v1/report/dashboard/ticket-volume-by-service | Required capability: MANAGER | Dashboard_TicketVolumeByServiceRequest |
| POST | /api/v1/report/dashboard/ticket-volume-by-date | Required capability: MANAGER | Dashboard_TicketVolumeByDateRequest |
| POST | /api/v1/report/dashboard/agent-created-ticket-counts | Required capability: MANAGER | Dashboard_AgentCreatedTicketCountsRequest |
| POST | /api/v1/report/dashboard/manager-activity-summary | Required capability: MANAGER | Dashboard_ManagerActivitySummaryRequest |
| POST | /api/v1/tickets/ticket/approve | Required capability: MANAGER | Ticket_ApproveRequest |
| POST | /api/v1/tickets/ticket/reject | Required capability: MANAGER | Ticket_RejectRequest |
| POST | /api/v1/tickets/ticket/update-status | Required capability: MANAGER | Ticket_UpdateStatusRequest |
| POST | /api/v1/tickets/ticket/get-by-id | Required capability: READ | Ticket_GetByIDRequest |
| POST | /api/v1/tickets/ticket/get-by-number | Required capability: READ | Ticket_GetByNumberRequest |
| POST | /api/v1/tickets/ticket/search | Required capability: READ | Ticket_SearchRequest |
| POST | /api/v1/tickets/ticket/get-recent-by-customer | Required capability: READ | Ticket_GetRecentByCustomerRequest |
| POST | /api/v1/tickets/ticket/get-pending-approval | Required capability: MANAGER | Ticket_GetPendingApprovalRequest |
| POST | /api/v1/tickets/ticket/get-priority-queue | Required capability: MANAGER | Ticket_GetPriorityQueueRequest |
| POST | /api/v1/tickets/ticket/get-status-history | Required capability: READ | Ticket_GetStatusHistoryRequest |
| POST | /api/v1/tickets/ticket/get-activity-history | Required capability: READ | Ticket_GetActivityHistoryRequest |
| POST | /api/v1/tickets/ticket/get-dashboard-summary | Required capability: MANAGER | Ticket_GetDashboardSummaryRequest |
| POST | /api/v1/track/customer-status-mapping/get-all | Required capability: CONFIG | CustomerStatusMapping_GetAllRequest |
| POST | /api/v1/track/customer-status-mapping/update | Required capability: CONFIG | CustomerStatusMapping_UpdateRequest |
| POST | /api/v1/tickets | Required capability: AGENT | CreateTicketRequest |
| POST | /api/v1/tracking | Public or trusted-verifier entry point. No caller-supplied role is trusted. | PublicTrackingRequest |
| POST | /api/v1/tracking-management/replace | Required capability: MANAGER | TokenReplacementRequest |
| POST | /api/v1/tracking-management/revoke | Required capability: MANAGER | TrackingToken_RevokeRequest |
