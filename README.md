# Jamuna Bank Customer 360 — Phase 4 API

ASP.NET Core / C# on .NET 10. Development was explicitly authorized while live SQL testing is pending. This build does not establish production readiness.

The solution contains the Web API, executable database-independent unit tests, a real loopback API smoke harness and a separate gated SQL integration project. ADO.NET methods cover the 124 callable procedures; the four internal helpers have no application endpoint. The schema and migrations are unchanged.

```powershell
dotnet restore Customer360.slnx
dotnet build Customer360.slnx --no-restore -m:1
dotnet run --project tests/Customer360.UnitTests --no-build -- docs/UnitTestResults.json
pwsh ./tests/HttpSmoke.ps1 -LoopbackHttp
```

The test projects are dependency-light executable runners: use the commands above, not `dotnet test`. They return nonzero on failure and emit JSON evidence. The SQL integration runner returns 2 when cases are BLOCKED; that is not a passing suite.

Run the development API only after explicitly selecting the desired doubles:

```powershell
$env:ASPNETCORE_ENVIRONMENT='Development'
$env:Development__EnableMockDatabase='true'
$env:Development__EnableMockAuth='true'
$env:Development__EnableMockIntegrations='true'
dotnet run --project src/Customer360.Api -- --urls https://localhost:7246
```

Configure/trust a local development HTTPS certificate in your own environment if needed. OpenAPI is available at `/openapi/v1.json` in Development/Testing only. Fetch `/api/v1/auth/csrf` with a cookie jar before each authentication or mutation request; refetch after signing in. Submit the returned token in `X-CSRF-TOKEN`. Local mock personas: Agent (`AGENT001`), Manager (`MGR001`), Configuration and Auditor. The mock session endpoint accepts these fixed personas only on a loopback connection with mock authentication explicitly enabled. It is unavailable in production. No production Agent login or locally stored Manager password is introduced.

All mocks default OFF. Both communication workers are unregistered and activation is rejected. No real SMS/email or live banking request is made by the supplied doubles. `Testing:AllowLoopbackHttp` exists solely for the all-mock loopback smoke harness, cannot be used outside Testing, and does not verify production TLS.

- [Architecture](docs/Architecture.md)
- [Endpoints](docs/EndpointInventory.md)
- [Models](docs/Models.md)
- [Procedure mapping](docs/StoredProcedureMapping.md)
- [Authentication and authorization](docs/AuthenticationAuthorization.md)
- [External integration contracts](docs/IntegrationContracts.md)
- [Configuration](docs/EnvironmentConfiguration.md)
- [Testing](docs/Testing.md)
- [Build and test evidence](docs/BuildTestReport.md)
- [Unresolved requirements](docs/KnownGaps.md)

Token replacement, full automatic worker retry/reconciliation/forwarding finalization, and several live integration contracts remain blocked as documented. Those paths fail closed; a compilable project is not evidence that these external/database requirements have been fulfilled.

Postman examples: import [the local mock collection](Postman/Customer360.LocalMock.postman_collection.json) and run requests 01–12 in order. See [Postman setup](Postman/README.md).

Windows launchers: double-click StartServices.bat, then use http://localhost:7246 in Postman. StopServices.bat stops the recorded API. See [launcher options](docs/LocalLauncher.md).


User-selected SQL application target: JamunaCustomer360. See [main-database Postman setup](Postman/JamunaCustomer360Setup.md). Mock launchers still use in-memory data. The separate QA suite remains on JamunaCustomer360_Test.


Agent UI first increment: see docs/AgentUi.md. Launch /agent?mobile=01700000000&agentId=AGENT001 on the API origin. No side menu or separate frontend server.


React landing page now replaces the earlier static UI at /agent. Frontend defaults to fictional MOCK mode independently of the host API SQL setting. Source: ../AgentUi; see docs/ReactAgentBuildReport.md and ../AgentUi/API_MAPPING.md.

