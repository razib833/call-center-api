# Postman samples

Import Customer360.LocalMock.postman_collection.json. Start the mock API using the commands in ../README.md. baseUrl defaults to https://localhost:7246. Keep Postman cookies enabled, choose No Auth (cookie authentication is automatic) and run requests 01–12 in order. Collection scripts save csrfToken, ticketId and rowVersion. Refresh CSRF after switching personas. Every sample is synthetic and uses the in-memory database, not SQL Server.

If your development certificate is not trusted, trust your local certificate or disable certificate verification for this localhost request only. API HTTPS was not verified in the agent environment. An unreachable endpoint means the local API must first be started; /health is the first connectivity check.

Do not reuse installation/migration scripts. Do not enable workers/providers or live SQL just to run these mock requests. Real external authentication endpoints are deliberately unconfigured; use the explicit development persona endpoint for this collection. The mock data resets when the server restarts.

For the user-selected JamunaCustomer360 database, use Customer360.JamunaCustomer360.postman_collection.json and JamunaCustomer360Setup.md. The earlier RealSqlQA collection remains specifically for JamunaCustomer360_Test. Neither prepared SQL collection has been live-executed.

