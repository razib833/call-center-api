# Phase 4 build and test execution — 16 October 2026 (Asia/Dhaka)

Development without live SQL was explicitly authorized by the Phase 4 request. No database installation/migration, QA marker activation, real external banking request or actual SMS/email send occurred.

| Check | Actual result |
|---|---|
| .NET SDK / target | Installed 10.0.302 / net10.0 |
| Debug build | PASS; 0 warnings, 0 errors; Build.Debug.log |
| Release build | PASS; 0 warnings, 0 errors; Build.Release.log |
| Database-independent unit tests | 102 PASS, 0 FAIL; UnitTestResults.json and UnitTests.log |
| Running API smoke checks | 24 PASS, 0 FAIL; HttpSmokeResults.json |
| Successful API transport | All-mock Testing, loopback HTTP; actual cookie/CSRF/policy/model binding and mock workflow execution |
| HTTPS smoke | BLOCKED during local certificate/connection setup; no production TLS pass claimed |
| API SQL integration groups | 6 BLOCKED, 0 executed, 0 PASS; default runner exit 2 |
| Existing SQL inventory | 374 pending/BLOCKED, 0 newly executed; not combined with API test counts |
| SQL QA-009 regression | NOT EXECUTED; API role-denial tests do not prove the stored procedure guard |
| Typed procedure contracts | 124 callable methods from 128 procedure definitions; 4 internal helpers excluded |
| HTTP operations | 117; actual endpoint inventory and exported OpenAPI |
| Contract source fingerprints | 124 actual procedure SHA256 values matched at packaging |

Build/test command details are in Testing.md. Builds used the cached driver/dependencies from a local feed because remote NuGet TLS was unavailable; offline restore used NuGetAudit=false. Project configuration does not disable auditing. Vulnerability audit and supported driver patch review are pending.

Tests are real executable .NET/HTTP checks within their stated scope, not static passes or simulated SQL execution. The custom unit runner returns failure on failed assertions; the HTTP harness starts/stops only its own mock process. The SQL integration project is an explicit blocked scaffold with a later opt-in reference smoke, not six passing placeholders.

## Readiness

The solution is compilable and supports continued development/mock-based review. Production readiness is BLOCKED: live SQL identity/permissions, QA-009, transaction/concurrency/workflow validation, trusted external adapters, token-replacement routing/version data, worker retry/reconciliation/forwarding contracts, production TLS/key custody and dependency audit remain unresolved. See KnownGaps.md for exact behavior and next prerequisites. No database schema change was made.

## User-selected application target update

API default ExpectedDatabase is now JamunaCustomer360, as explicitly requested. The 374-case QA suite and integration fixtures retain JamunaCustomer360_Test. The running API held the Debug DLL open; it was not stopped or replaced. An isolated artifacts build passed with 0 warnings/errors and all 102 unit tests passed. No SQL connection or insertion was executed. Restart/configure the API with approved real SQL settings before using the new main-database Postman collection; the mock launcher still forces in-memory operation.


## PKCS vulnerability remediation — 2026-10-16

System.Security.Cryptography.Pkcs was upgraded from transitive 9.0.4 to an explicit 9.0.21 reference. Both GHSA-2fmv-rv2c-59x7 and GHSA-846f-f764-76mx identify 9.0.21 as patched. All three package lock files were regenerated. NuGetAudit=true, NuGetAuditMode=all and TreatWarningsAsErrors=true remain enabled.

Actual verification: all three projects restored successfully with vulnerability auditing; solution build passed with 0 warnings and 0 errors; 102 unit tests passed, 0 failed. The Windows HTTPS stack failed with an SSPI credentials error in this agent session. Verification therefore used cached packages plus the official patched package fetched over certificate-validated Node HTTPS, and a temporary loopback mirror of NuGet's official vulnerability snapshot. No auditing warnings were suppressed. The temporary mirror is not part of application configuration. Snapshot provenance is recorded in DependencyAuditEvidence.json.

The running API was not replaced. StopServices.bat followed by StartServices.bat rebuilds/restarts using the updated source. Existing SQL validation and provider-isolation blockers remain unchanged; no live SQL tests or inserts were executed for this dependency fix.

Sources: https://github.com/advisories/GHSA-2fmv-rv2c-59x7 and https://github.com/advisories/GHSA-846f-f764-76mx

## Launcher package-path repair — 2026-10-17

The isolated DOTNET_CLI_HOME caused NuGet to place packages underneath TestResults/LocalServices/dotnet-home. The reported OpenIdConnect DLL path was 267 characters; the file existed, but MSBuild assembly resolution/copy failed. The launcher now defaults NUGET_PACKAGES explicitly to the standard per-user .nuget/packages directory, while preserving an explicit caller override. Restore uses --force to regenerate package references after the cache-path change. No cache or database records are deleted.

Actual verification: launcher Start -Port 7248 -Rebuild completed audited restore, built the API with zero warnings/errors, and passed its health check. An independent GET /health returned status=alive, mockDatabase=true, workersEnabled=false. The test instance was then stopped. For this agent session only, verification used a shorter workspace package-cache override and the temporary official-data audit mirror described above because native HTTPS is unavailable. The normal launcher ships neither override nor mirror configuration. Live SQL was not exercised.
# Local transport update — 2026-10-17

The user selected certificate-free local development. This supersedes HTTPS/certificate requirements below for this local mode only. No SQL Server instance settings, certificates, identity permissions or database objects were changed by this source update.

Enable Development:AllowLoopbackHttp=true and Development:Port=7246 in Development. The API binds only localhost and uses HTTP-compatible authentication/CSRF cookies. Set Database:AllowUnencryptedLocalDevelopment=true and use this AGENT connection string:

Server=DESKTOP-BUDV4V7;Database=JamunaCustomer360;Integrated Security=True;Encrypt=False;TrustServerCertificate=True;

The API must run as DESKTOP-BUDV4V7\Customer360ApiAgent. This SQL option is rejected outside Development or when targeting another machine. SQL Server Force Encryption, if enabled, can still require encrypted transport; this change does not override server policy. SQL login negotiation may still use SQL Server's automatically generated certificate, so this does not eliminate every internal certificate use. Application token-payload encryption and authentication protection remain intact.

The REVIEW_REQUIRED JSON template contains the local transport options but is not automatically loaded. Identity verification, provider isolation, LiveValidationApproved and retained verification references remain prerequisites. StartServices.bat still intentionally launches mocks; it does not enable SQL. Postman now accepts HTTP or HTTPS on localhost/127.0.0.1, defaults to http://localhost:7246, and retains prerequisite and mock-backend guards.

Verification: solution build passed with zero warnings/errors; 102 existing unit tests passed. The new Development HTTP listener started on localhost:7251 and GET /health succeeded using mocks. No live SQL connection or ticket insert was tested.

---


## Agent UI first increment — 2026-10-17
Build PASS (0 warnings/errors), existing unit tests 102 PASS, HTTP checks 10 PASS against isolated all-mock API. Browser/visual/interactions BLOCKED by browser launch EPERM; no SQL tests or ticket inserts executed. See AgentUi.md and AgentUiTestResults.json.


## Landing reference update — 17 October 2026

The Agent page now follows the user-supplied Landing-page.png layout: blue call header, mobile search, full-width customer information, banking relationship table and last five service requests. There is no side menu. Customer selection, restricted session checks, CSRF and explicit ticket creation remain. Changing the search mobile preserves the original launch mobile in the incoming-call header. Call duration is shown as Not connected because no verified Avaya call-time events exist; it is not a page-open timer.

Core Banking, Card System and Internet Banking rows load existing integration summaries independently; each Details button shows only the existing summary contract. Errors display Unavailable rather than claiming no relationship. The history defaults to five records, View All Requests switches to paged 20-record history, and View reads the existing authorized get-by-id API. Approval state is labelled accurately: APPROVED does not imply ticket completion. The list API has no status name, account number or remarks, so it does not invent them. Gender, DOB, NID, parent names, email and address are unavailable in the current CustomerProfile contract and are labelled accordingly. The API contracts and SQL objects were not changed.

Verified: JavaScript syntax, unique HTML IDs/static DOM references, all four landing sections and HTTP 200 with updated content from running /agent, /agent/landing.css and /agent/agent.js. Five checks passed; browser visual/interaction validation remains BLOCKED by prior browser launch EPERM. No new sessions, customer API requests, SQL writes or ticket inserts were performed during these checks. Refresh the existing browser with Ctrl+F5; these are static asset changes and need no API restart when running from the source content root.

## Development landing defaults — 18 October 2026

The development mobile now defaults to 01712345678 in the page and DevelopmentIntegrations lookup/profile fixture. The Agent ID input is removed; authenticated Agent identity comes from /auth/session. /agent works with no query. /agent?mobile=01712345678 is the explicit launch URL. If an external caller supplies agentId in the query, it is checked against the authenticated session, never used to create identity. Development login remains an explicit action and returns the server's fixed AGENT001; it cannot impersonate another optional URL Agent ID.

The blue reference layout has a mobile-only search row, consistent vector search/person/bank/card/monitor/document icons, an avatar illustration and refined spacing. Missing demographic values and live call duration remain accurately labelled unavailable/disconnected; no sample-person banking data is fabricated. Existing SQL rows are unchanged. Tickets previously stored under 01700000000 remain there and will not appear under 01712345678 history unless searched by the old number; no data migration was performed.

Validation: solution build 0 warnings/errors, 103 unit tests passed (including lookup/profile default-mobile consistency), JavaScript syntax check passed, five static/read-only HTTP landing checks passed. Browser visual/interaction validation remains BLOCKED. No SQL writes or new tickets were executed. Static UI changes require Ctrl+F5. The backend fixture change requires rebuilding as Risha and restarting in the existing restricted Agent window using the previously documented real-SQL environment. Do not restart via StartServices.bat for real SQL; it forces mocks.
