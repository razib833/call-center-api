# Test execution

Run from the Api directory with .NET 10 and PowerShell 7:

```powershell
dotnet restore Customer360.slnx
dotnet build Customer360.slnx --no-restore -m:1
dotnet run --project tests/Customer360.UnitTests --no-build -- docs/UnitTestResults.json
pwsh ./tests/HttpSmoke.ps1 -LoopbackHttp
dotnet run --project tests/Customer360.IntegrationTests --no-build -- docs/IntegrationTestResults.json
```

`tests/BuildAndTest.ps1 -HttpSmoke` performs this sequence. The dependency-light test projects are executable test harnesses, not xUnit/Test SDK projects; `dotnet test` does not execute them. Unit failures return 1, and the SQL integration runner returns 2 when live groups are BLOCKED. A build or blocked inventory is not a passed integration test.

The unit harness exercises allowed/denied capability combinations; environment/mock/worker gates; unconfigured trusted authentication rejection; ticket/service/rowversion/rejection/follow-up/forwarding validation; trusted customer/Avaya request mapping; mock mandatory approval/idempotency/concurrency; random tokens/public projection; encrypted envelope handling; safe error mapping; SQL parameter metadata; unknown provider outcome safety and sanitized integration audit. These tests use in-memory data and prove API behavior within that scope only.

The HTTP harness starts an isolated child process with all three doubles enabled, exercises cookie/CSRF/policy/model-binding/error behavior and synthetic creation/approval, then stops only its own process. The successful recorded run uses the explicitly gated all-mock Testing loopback HTTP transport. That does not verify production HTTPS, certificates or external authentication. The optional default HTTPS mode requires a working development certificate/client stack and did not complete in this environment. Test runtime logs/key files are not source deliverables.

## Live SQL prerequisites

Keep all existing 374 database cases pending until actual execution. Do not rerun installation/migrations. Resolve QA coordinator identity, restricted actor permissions, the owner-dependent legacy harness, actual provider/worker disconnection, empty outbox, markers, dataset manifests and exact committed workload approval through the existing QA activation plan. Do not set markers or clear queues from the API.

For the API, separately configure approved connectable capability identities, target and encrypted SQL connections. Verify actual current login/user, role tokens, table-denial and restricted procedure behavior under every identity, including QA-009. Retain this independent evidence before setting `LiveValidationApproved` and identity verification references. The supplied QA WITHOUT LOGIN actors cannot authenticate API connections.

The integration project has six explicit pending groups. Only the reference-procedure smoke is implemented for optional later live execution:

```powershell
$env:C360_INTEGRATION_CONFIG='<absolute path to secret QA DatabaseOptions JSON>'
$env:C360_INTEGRATION_APPROVED='true'
dotnet run --project tests/Customer360.IntegrationTests -- --execute-reference-smoke docs/IntegrationTestResults.json
```

This executes only `usp_Priority_GetActive` using the approved Agent connection and checks real typed seed rows. It never represents a complete functional/security suite. Permissions, transaction, independent-session approval/outbox, and tracking lifecycle groups remain BLOCKED until their isolated fixture implementations and approvals are completed. Never substitute mocked concurrency or one sequential connection for genuine SQL multi-session tests.

Complete bank-approved workflow/template oracles, middleware/provider/KMS integration, worker read-contract gaps, production TLS/secret/key management, dependency auditing, operational performance and end-to-end tests before production readiness.
