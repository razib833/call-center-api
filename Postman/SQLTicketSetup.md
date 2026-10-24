# Real SQL QA ticket collection

Import **Customer360.RealSqlQA.postman_collection.json**. This collection is prepared, not live-executed. It targets an independently verified API connection to **DESKTOP-BUDV4V7 / JamunaCustomer360_Test**. Never run it against the original JamunaCustomer360 database or production.

## Current blocker

StartServices.bat deliberately forces an in-memory mock database. Its SYNTHETIC tickets are not SQL rows. This collection cannot change backend mode, provision a SQL login or publish a service. The API currently has no supplied approved Agent connection or live identity-verification evidence. Real insertion remains blocked until these are configured and the QA prerequisites are reviewed.

## Backend prerequisites

- Existing, approved connectable non-admin Agent SQL identity mapped only to the required Customer360_AgentExecRole grants. No sysadmin/db_owner/CONTROL SERVER; do not use Risha as a shortcut. QA_Agent WITHOUT LOGIN cannot authenticate API connections. Do not create identities/passwords from this collection.
- Exact server/catalog, encrypted SQL connection with a trusted certificate, expected database user and independent identity verification reference. All secret connection values stay on the API server, never in Postman.
- Verified local isolated QA, disabled/disconnected external providers/workers and an already empty outbox before the committed test. No outbox clearing or marker activation is performed. Retain explicit approval/evidence for this committed synthetic ticket workload; the earlier creation approval alone did not certify these conditions.
- Real published active service/version, workflow, priority/category, active AVAYA_AGENT request source, valid dynamic fields and published mandatory TICKET_CREATED SMS template/configuration. Deployment metadata/seed existence alone does not establish this operational configuration. Do not invent IDs, publish bank templates or alter transitions to force insertion.
- A working local HTTPS API endpoint. Testing:AllowLoopbackHttp cannot be used with a real database; the existing HTTP launcher is all-mock only.

Use appsettings.SqlQA.REVIEW_REQUIRED.json as a configuration checklist only: it is not automatically loaded and contains no connection credentials. Keep LiveValidationApproved false until actual independent verification and approval; do not flip it merely to bypass the API gate. Supply reviewed values through User Secrets or the approved environment/secret store. For isolated synthetic QA only, mock Agent authentication and synthetic middleware/envelope protection may remain enabled in Development while EnableMockDatabase is false. This produces real committed SQL rows for synthetic data; it is not live Avaya/customer/KMS integration. Workers stay false.

After configuration and approval, start the API manually in Development over approved HTTPS with:

- Development:EnableMockDatabase=false
- Development:EnableMockAuth=true (synthetic local QA only)
- Development:EnableMockIntegrations=true (synthetic local QA only)
- Workers:Enabled=false
- Testing:AllowLoopbackHttp=false

Do not use StartServices.bat for this SQL mode: it overrides those settings back to mocks. No ready-to-run real-SQL launcher is supplied before the missing credentials/verification are resolved.

## Request sequence

1. GET /health: mockDatabase must be false and workersEnabled false. databaseIntegration remains PENDING in this build: this endpoint does not attest SQL connectivity, database name, current user, permissions or successful tests.
2. GET /api/v1/auth/csrf. Cookies remain enabled.
3. After prerequisite review, set sqlQaPrerequisitesReviewed=YES in collection variables. This is a client-side acknowledgement, not SQL environment authorization or evidence. POST development/session creates a synthetic Agent session; its authentication logging already writes through SQL.
4. Refresh Agent CSRF.
5. List actual services. Select the intended serviceId manually; the collection intentionally has no assumed default ID.
6. Retrieve its published version. The response script records versionId only when a published version exists.
7. Inspect published fields. Set fieldValuesJson to an actual JSON array using those fields, e.g. [{"fieldID":123,"value":"Synthetic QA value"}]. 123 is illustrative, not a verified field ID. Include required values. Middleware-backed read-only fields are derived on the server.
8. Create ticket. This calls dbo.usp_Ticket_Create, inserting dbo.ServiceRequest plus the procedure's related field/history/token/communication/outbox records transactionally. Each Send uses a new {{$guid}} idempotency key. The collection expects a J360- ticket number and saves the real returned ID.
9. Read it back through dbo.usp_Ticket_GetByID. It must match the created ID/number and remain PENDING approval. No banking transaction or provider delivery is performed.

Every POST is default-blocked until the review acknowledgement is set and performs an additional health request to reject a mock backend. Scripts use Postman's documented pre-request skipRequest mechanism; skipped requests are NOT passing tests. Use a Postman version that supports it. Source: https://learning.postman.com/docs/tests-and-scripts/write-scripts/postman-sandbox-reference/pm-execution/ . Health checks and collection settings do not replace server-side permissions/target verification or make an unverified environment safe.

Optional independent read-only SQL verification: use VerifyInsertedTicket.sql in SSMS with the returned requestID. Retain Postman responses and SQL Results/Messages. Do not mark any insertion or test successful until actual execution evidence exists.
