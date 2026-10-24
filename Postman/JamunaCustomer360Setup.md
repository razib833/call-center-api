# Local transport update — 2026-10-17

The user selected certificate-free local development. This supersedes HTTPS/certificate requirements below for this local mode only. No SQL Server instance settings, certificates, identity permissions or database objects were changed by this source update.

Enable Development:AllowLoopbackHttp=true and Development:Port=7246 in Development. The API binds only localhost and uses HTTP-compatible authentication/CSRF cookies. Set Database:AllowUnencryptedLocalDevelopment=true and use this AGENT connection string:

Server=DESKTOP-BUDV4V7;Database=JamunaCustomer360;Integrated Security=True;Encrypt=False;TrustServerCertificate=True;

The API must run as DESKTOP-BUDV4V7\Customer360ApiAgent. This SQL option is rejected outside Development or when targeting another machine. SQL Server Force Encryption, if enabled, can still require encrypted transport; this change does not override server policy. SQL login negotiation may still use SQL Server's automatically generated certificate, so this does not eliminate every internal certificate use. Application token-payload encryption and authentication protection remain intact.

The REVIEW_REQUIRED JSON template contains the local transport options but is not automatically loaded. Identity verification, provider isolation, LiveValidationApproved and retained verification references remain prerequisites. StartServices.bat still intentionally launches mocks; it does not enable SQL. Postman now accepts HTTP or HTTPS on localhost/127.0.0.1, defaults to http://localhost:7246, and retains prerequisite and mock-backend guards.

Verification: solution build passed with zero warnings/errors; 102 existing unit tests passed. The new Development HTTP listener started on localhost:7251 and GET /health succeeded using mocks. No live SQL connection or ticket insert was tested.

---
# SQL ticket insertion into JamunaCustomer360

The user explicitly selected DESKTOP-BUDV4V7 / JamunaCustomer360 for ordinary local-development API ticket creation. API ExpectedDatabase defaults now match that choice. No database connection, object/permission change or ticket insert was executed for this configuration change. The isolated 374-case test suite and its integration/concurrency fixtures remain targeted at JamunaCustomer360_Test; do not repoint those tests at this database.

Import Customer360.JamunaCustomer360.postman_collection.json. It contains 9 requests and blocks a mock backend. Use a local HTTPS API baseUrl, keep cookies enabled, and follow the request order. Configure serviceId from actual service results, versionId from the published version response, and fieldValuesJson from actual published field IDs. No service/version ID is assumed. Ticket creation calls dbo.usp_Ticket_Create and persists dbo.ServiceRequest plus related workflow/field/token/outbox data; readback uses dbo.usp_Ticket_GetByID. A successful SQL ticket number uses J360-, not the in-memory SYNTHETIC prefix.

## API server configuration required

- Database:ExpectedServer = DESKTOP-BUDV4V7
- Database:ExpectedDatabase = JamunaCustomer360
- Database:Connections:AGENT = the secret connection string whose DataSource is DESKTOP-BUDV4V7 and Initial Catalog is JamunaCustomer360; encrypted transport with a valid trusted server certificate is mandatory.
- Database:ExpectedUsers:AGENT and IdentityVerificationReferences:AGENT = actual independently verified restricted database user and retained verification reference for this exact database.
- Database:LiveValidationApproved remains false until identity/permissions/target evidence and application execution are reviewed and approved. Merely setting it true is not verification.
- Development:EnableMockDatabase = false.
- For synthetic local development only: Development:EnableMockAuth = true and Development:EnableMockIntegrations = true. This uses a development Agent and synthetic customer/envelope, while ticket rows are real SQL rows. It does not validate real Avaya, banking middleware or KMS integration.
- ASPNETCORE_ENVIRONMENT = Development, Workers:Enabled = false, Testing:AllowLoopbackHttp = false. Serve over working local HTTPS.

Use appsettings.JamunaCustomer360.REVIEW_REQUIRED.json as a checklist only. It is not automatically loaded, contains no credentials, and does not activate execution. Use approved secret storage for the actual connection. Do not use StartServices.bat for this mode: it explicitly forces all mocks. That launcher is unchanged.

## Remaining blockers

No approved connectable restricted Agent identity has been supplied for JamunaCustomer360. Risha previously showed CONTROL SERVER and is not an acceptable least-privilege application account. QA WITHOUT LOGIN users cannot log in and are not application connection identities. Do not invent an identity/password or grant db_owner.

Before committing synthetic ticket data to this local-development database, verify provider/worker disconnection and authorization for the exact workload; creating a ticket also queues communications. Do not clear queues, add QA markers to this database, change published bank configuration or run destructive fixtures. Published services/workflows and mandatory published TICKET_CREATED SMS configuration must already be valid; metadata deployment counts alone are insufficient.

The collection acknowledgement sqlPrerequisitesReviewed defaults NO. Set YES only after actual prerequisite review. Each POST also checks /health for mockDatabase=false and workersEnabled=false. These client checks prevent accidental mock requests; they do not prove which SQL database/user the backend is using. Review the server configuration and actual SQL identity independently. Skipped requests are not PASS (Postman skipRequest documentation: https://learning.postman.com/docs/tests-and-scripts/write-scripts/postman-sandbox-reference/pm-execution/).

After an actual successful insert, use VerifyJamunaCustomer360Ticket.sql in SSMS with the returned requestID for independent read-only confirmation. No SQL/Postman execution success is claimed by these prepared files.

