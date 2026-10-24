# Environment configuration

Defaults in `src/Customer360.Api/appsettings.json` are inert: all mocks off, workers off, no credentials, no SQL identity approval and no external endpoint URLs. Do not commit secrets or key files. Use secret storage/environment variables (development User Secrets is configured); do not put credentials into source templates.

| Key | Meaning |
|---|---|
| ASPNETCORE_ENVIRONMENT | Development, Testing or the actual deployment environment |
| Development:EnableMockDatabase | Explicit development/test-only in-memory procedure double |
| Development:EnableMockAuth | Explicit local fixed-persona session endpoint |
| Development:EnableMockIntegrations | Synthetic banking/provider/envelope doubles |
| Database:ExpectedServer | Exact approved SqlConnection DataSource; default DESKTOP-BUDV4V7 |
| Database:ExpectedDatabase | Exact approved catalog; default JamunaCustomer360 (user-selected local development target) |
| Database:Connections:{capability} | Secret connection string for that approved restricted actor identity |
| Database:ExpectedUsers:{capability} | Independently verified database user; not a claim that a connection string attests it |
| Database:IdentityVerificationReferences:{capability} | Reference to retained independent live permissions/identity evidence |
| Database:LiveValidationApproved | False until exact target, identity and security integration are approved |
| Security:DataProtectionKeyPath | Approved persistent key directory; protected with Windows DPAPI |
| Tracking:PublicBaseUri | Approved HTTPS public origin, no query parameters; never an invented provider endpoint |
| Workers:Enabled | Must remain false; true is rejected while worker contracts are unresolved |
| Testing:AllowLoopbackHttp / Testing:Port | All-mock Testing only, loopback listener; never for real SQL/provider execution |
| DevelopmentTls:PemPath / KeyPath / Port | Optional development/test PEM listener; private keys stay outside source |

ADO.NET forces encrypted SQL transport, certificate verification, no MARS and no pooling. The configured DataSource and catalog must exactly match the approved values. No TrustServerCertificate bypass is used for real SQL. The application does not create SQL logins/users, grant roles, set QA markers, clear queues, or modify the schema.

Use different secret connection entries and actual restricted users per capability. API activation is separate from the SQL QA coordinator role; approved QA activation scripts must not be reused to grant application db_owner. Keep external provider delivery disconnected during isolated SQL QA.

The build used cached Microsoft.Data.SqlClient 6.1.0 because the environment could not establish a trusted NuGet TLS connection. Restore on a normal connected development machine, complete vulnerability auditing, review the supported driver patch and regenerate the package lock before release. Audit was disabled only for the recorded offline restore, not globally in project configuration. .NET SDK 10.0.302/net10.0 was the installed suitable LTS target; see https://dotnet.microsoft.com/en-us/platform/support/policy/dotnet-core .


## PKCS vulnerability remediation — 2026-10-16

System.Security.Cryptography.Pkcs was upgraded from transitive 9.0.4 to an explicit 9.0.21 reference. Both GHSA-2fmv-rv2c-59x7 and GHSA-846f-f764-76mx identify 9.0.21 as patched. All three package lock files were regenerated. NuGetAudit=true, NuGetAuditMode=all and TreatWarningsAsErrors=true remain enabled.

Actual verification: all three projects restored successfully with vulnerability auditing; solution build passed with 0 warnings and 0 errors; 102 unit tests passed, 0 failed. The Windows HTTPS stack failed with an SSPI credentials error in this agent session. Verification therefore used cached packages plus the official patched package fetched over certificate-validated Node HTTPS, and a temporary loopback mirror of NuGet's official vulnerability snapshot. No auditing warnings were suppressed. The temporary mirror is not part of application configuration. Snapshot provenance is recorded in DependencyAuditEvidence.json.

The running API was not replaced. StopServices.bat followed by StartServices.bat rebuilds/restarts using the updated source. Existing SQL validation and provider-isolation blockers remain unchanged; no live SQL tests or inserts were executed for this dependency fix.

Sources: https://github.com/advisories/GHSA-2fmv-rv2c-59x7 and https://github.com/advisories/GHSA-846f-f764-76mx
