# Request and response models

The full typed contracts are in `Contracts/Procedures.g.cs` and `docs/StoredProcedureContracts.json`. `OpenApi/openapi.json` is generated from actual controller signatures and DTOs. The mapping document links every procedure to exact parameter names, output parameters and ordered typed result sets. `Rows1`, `Rows2` preserve multiple SQL result sets; outputs are separate response properties. SQL error details are never returned.

Every API response uses `success`, `message`, `data`, `errors`, `correlationId`. API DTO properties use camelCase; IDs are numbers (long for SQL bigint); rowversions are base64 eight-byte values; dates must represent UTC. Dynamic field IDs/values remain typed records and are serialized to the exact SQL JSON array contract on the backend.

Synthetic ticket creation:

```json
{
 "serviceID": 1,
 "versionID": 1,
 "idempotencyKey": "8d55e634-e59c-476f-87dd-471184f9d13e",
 "customerReference": "SYNTHETIC-CUSTOMER",
 "fields": [{"fieldID": 1, "value": "Synthetic value"}],
 "description": "Synthetic test request"
}
```

`POST /api/v1/tickets` derives verified Avaya interaction/actor information from the session and customer snapshots from middleware. It generates the tracking token/hash and encrypted delivery server-side. It returns only `requestID` and `ticketNumber`; raw tokens, envelope keys, plaintext delivery and caller-supplied Manager roles are not accepted/exposed.

Synthetic Manager approval:

```json
{"requestID": 1001, "expectedRowVersion": "AQAAAAAAAAA=", "remarks": "Synthetic approval"}
```

Use `/api/v1/tickets/ticket/approve`; rejection uses `/api/v1/tickets/ticket/reject` and requires a nonempty reason in `remarks`. The sample rowversion belongs only to the in-memory mock convention: obtain the real current version from an actual permitted read, never manufacture it for SQL.

Public tracking accepts a 43-character canonical base64url token in a POST body. The backend hashes its decoded 32 random bytes and uses only public tracking procedures. The response exposes public status/timeline fields from the exact SQL projections, not customer PII or internal notes. Invalid/unknown/revoked/expired tokens receive generic unavailable responses.

Forwarding content remains editable before submission through the create request. Destinations are BRANCH/DIVISION; recipient lists are comma/semicolon-separated addresses without CR/LF or display names, subject has no CR/LF, and body is plain text. No API call sets a ticket to Sent to Branch/Division before provider acceptance. Reminder creation requires a future UTC timestamp. Follow-up notes have no update endpoint.

Customer middleware DTOs (`CustomerProfile`, `AccountSummary`, `AccountDetails`, `CardSummary`, `CardTransaction`, `InternetBankingProfile`) are internal application contracts, not invented provider formats. Actual authorization, field mappings, balances and transaction data require approved middleware documentation.
