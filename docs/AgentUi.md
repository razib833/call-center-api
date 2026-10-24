# Agent UI — first increment

Launch on the same origin as the API:

    http://localhost:7246/agent?mobile=01700000000&agentId=AGENT001

The page has no side menu. It includes customer profile, customer selection, paged recent tickets, and Create ticket using an active service's published configuration. It is static HTML/CSS/JavaScript served by ASP.NET Core; there is no frontend installation or second service. Assets live in src/Customer360.Api/wwwroot/agent. /agent is the human-facing entry route ("headless" is interpreted here as an embedded, navigation-free workspace).

## Running the updated build

1. Stop the current API with Ctrl+C in its existing Agent window. Keep that window open to retain its environment variables.
2. Build as Risha (the restricted Agent has read/execute folder access): from the Api folder, run `dotnet build src/Customer360.Api/Customer360.Api.csproj -m:1 -nr:false`.
3. In the existing Customer360ApiAgent window with the previously configured real-SQL environment, from the Api folder run `dotnet src\Customer360.Api\bin\Debug\net10.0\Customer360.Api.dll --contentRoot "%CD%\src\Customer360.Api"`.
4. Open the launch URL above. If needed, explicitly choose Use development Agent001. This creates the existing synthetic Agent session and writes its normal authentication log. It does not silently sign in from the URL. Live Avaya validation remains unconfigured.

Do not use StartServices.bat to start real SQL: it forces mocks. A new command window requires restoring the reviewed environment settings from JamunaCustomer360Setup.md. /health remains a mode indicator, not a live readiness attestation.

## Identity and data behavior

The launch agentId must match the signed-in server Agent ID and capability. A mismatch blocks the workspace. Mobile is lookup input, not authorization. No browser-supplied actor identity is submitted with ticket creation. The URL query is removed from the current address using replaceState, no customer/session data is placed in localStorage, and DOM text is rendered with textContent. Initial URLs can still be recorded by servers, proxies and the calling system; a production Avaya integration should use a short-lived opaque handoff rather than raw mobile in URLs. Mobile '+' must be URL encoded as %2B.

Existing API cookies, CSRF, policies and rate limits are reused on the same origin. A session expiry is shown as an error, never an automatic identity substitution. Production authentication and browser entitlement tests remain outstanding.

Current customer integration returns Synthetic Customer (01700000000) for searches. The UI labels it synthetic. If the returned mobile differs from the launch mobile, explicit customer selection is required. It never relabels synthetic data as the input customer. Ticket history uses the selected profile's mobile. The current SQL configuration has LOCAL DEV TEST serviceID=2/versionID=2/optional text fieldID=2; these IDs are not hardcoded in the UI.

Ticket creation requires an explicit form submission and confirmation, saves to the API's configured database, and may queue a notification. It cannot approve requests or activate workers. Each form has a fresh UUID; an uncertain-result retry retains the identical payload/key and freezes its inputs. Do not close/reopen and resubmit an uncertain request before checking history. Successful creation displays the returned ticket number/request ID and reloads history. The UI makes no notification delivery claims.

## Current scope and limits

TEXT and TEXTAREA fields are supported. Services containing dropdown fields are blocked with an explicit explanation pending option-source integration. Selection lists more than 10,000 services require a future server search/paging selector. Customer account/card/transaction details, ticket-detail drilldown, live Avaya handoff, Agent-ID parameter authentication, Manager actions, automatic session renewal and notification delivery are outside this first increment. The first page displays profile, tickets and creation only.

Verification: solution build passed with zero warnings/errors; 102 existing unit tests passed; JavaScript syntax check passed. See AgentUiTestResults.json for executed HTTP checks and their exact mock scope. Browser/visual checks were blocked by Chrome process launch permissions; no browser layout, screenshot, interaction or accessibility PASS is claimed. No SQL connection or ticket creation was executed for UI validation. Earlier user evidence for ticket 3 remains separate; full SQL QA/QA-009/concurrency readiness remains outstanding.

## Landing reference update — 17 October 2026

The Agent page now follows the user-supplied Landing-page.png layout: blue call header, mobile search, full-width customer information, banking relationship table and last five service requests. There is no side menu. Customer selection, restricted session checks, CSRF and explicit ticket creation remain. Changing the search mobile preserves the original launch mobile in the incoming-call header. Call duration is shown as Not connected because no verified Avaya call-time events exist; it is not a page-open timer.

Core Banking, Card System and Internet Banking rows load existing integration summaries independently; each Details button shows only the existing summary contract. Errors display Unavailable rather than claiming no relationship. The history defaults to five records, View All Requests switches to paged 20-record history, and View reads the existing authorized get-by-id API. Approval state is labelled accurately: APPROVED does not imply ticket completion. The list API has no status name, account number or remarks, so it does not invent them. Gender, DOB, NID, parent names, email and address are unavailable in the current CustomerProfile contract and are labelled accordingly. The API contracts and SQL objects were not changed.

Verified: JavaScript syntax, unique HTML IDs/static DOM references, all four landing sections and HTTP 200 with updated content from running /agent, /agent/landing.css and /agent/agent.js. Five checks passed; browser visual/interaction validation remains BLOCKED by prior browser launch EPERM. No new sessions, customer API requests, SQL writes or ticket inserts were performed during these checks. Refresh the existing browser with Ctrl+F5; these are static asset changes and need no API restart when running from the source content root.

## Development landing defaults — 18 October 2026

The development mobile now defaults to 01712345678 in the page and DevelopmentIntegrations lookup/profile fixture. The Agent ID input is removed; authenticated Agent identity comes from /auth/session. /agent works with no query. /agent?mobile=01712345678 is the explicit launch URL. If an external caller supplies agentId in the query, it is checked against the authenticated session, never used to create identity. Development login remains an explicit action and returns the server's fixed AGENT001; it cannot impersonate another optional URL Agent ID.

The blue reference layout has a mobile-only search row, consistent vector search/person/bank/card/monitor/document icons, an avatar illustration and refined spacing. Missing demographic values and live call duration remain accurately labelled unavailable/disconnected; no sample-person banking data is fabricated. Existing SQL rows are unchanged. Tickets previously stored under 01700000000 remain there and will not appear under 01712345678 history unless searched by the old number; no data migration was performed.

Validation: solution build 0 warnings/errors, 103 unit tests passed (including lookup/profile default-mobile consistency), JavaScript syntax check passed, five static/read-only HTTP landing checks passed. Browser visual/interaction validation remains BLOCKED. No SQL writes or new tickets were executed. Static UI changes require Ctrl+F5. The backend fixture change requires rebuilding as Risha and restarting in the existing restricted Agent window using the previously documented real-SQL environment. Do not restart via StartServices.bat for real SQL; it forces mocks.
