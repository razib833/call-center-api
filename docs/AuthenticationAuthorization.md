# Authentication and authorization

Production Avaya authentication accepts an opaque assertion that must be independently validated by `IAvayaIntegration`. The unconfigured implementation rejects access. The Agent ID, role and interaction reference never come from arbitrary headers or the ticket request. Avaya results must have AGENT/AVAYA capability/source and a verified interaction reference. There is no second production Agent credential login.

Manager credentials pass solely to `IManagerAuthentication`; no local credential table or password validation scheme is invented. The returned identity must have a separately verified MANAGER or CONFIG capability and EXTERNAL_MANAGER source. The real wire endpoint, session verification and role allowlist must be supplied before implementing an external adapter. Session lifetimes are capped at 15 minutes and checked on each request. External revocation behavior still requires integration design/testing.

Cookies are protected by ASP.NET Core Data Protection, HttpOnly, Secure, SameSite=Strict, fixed expiry and no sliding refresh. Mutations/authentication bootstrap require antiforgery cookie plus `X-CSRF-TOKEN`; obtain a fresh token after login. Bodies containing ActorRole or other undocumented fields are rejected, headers cannot create an authenticated principal, and incoming correlation IDs are ignored. The all-mock Testing loopback transport is explicitly gated and uses a distinct test cookie; production security is unchanged.

| Policy | Accepted capability |
|---|---|
| AGENT | Agent only |
| MANAGER | Manager only |
| CONFIG | Configuration administrator only |
| READ | Agent, Manager, configuration administrator |
| WORKER | Internal service worker; no HTTP procedure routes |
| AUDIT | Auditor only |
| PUBLIC | Tracking SQL principal only; HTTP takes raw token and hashes server-side |
| TRUSTED | Agent, Manager, configuration, worker logging operations; not exposed as generic HTTP execution |

Application authorization precedes the DAL, and the SQL guard must independently bind verified context to the actual restricted database principal. Configure different approved SQL identities for AGENT/MANAGER/CONFIG/WORKER/PUBLIC/AUDIT. Never use Risha/CONTROL SERVER, sa, sysadmin, db_owner or one multi-capability identity as an application login. QA WITHOUT LOGIN users are impersonation subjects for SQL testing; they cannot be used as API connection logins.

Because no existing application procedure returns a runtime identity attestation, no direct SELECT is added to C#. `Database.LiveValidationApproved` defaults false, and every configured identity requires an independent live verification reference. Those settings are operator evidence gates, not runtime proof of identity. Verify effective role memberships, Windows groups, cross-database rights and the QA-009 regression outside the API before approval. Integrated Security in one process normally uses one process Windows identity: a multi-role deployment requires approved separately authenticated SQL connections or isolated service processes; changing application names does not create separate principals.

Production requires protected persistent Data Protection keys (current host implementation uses Windows DPAPI). Multi-node/shared-key deployment and real external session revocation need separate approved configuration. Workers cannot be activated merely by toggling a flag in this build.
