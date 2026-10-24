# Runs only after the existing approved Windows identity is selected by runas.
$ErrorActionPreference='Stop'
$apiRoot=[IO.Path]::GetFullPath((Join-Path $PSScriptRoot '..'))
$identity=[Security.Principal.WindowsIdentity]::GetCurrent().Name
if($identity -ine 'DESKTOP-BUDV4V7\Customer360ApiAgent'){throw 'Real SQL API must run as DESKTOP-BUDV4V7\Customer360ApiAgent.'}
$env:ASPNETCORE_ENVIRONMENT='Development'
$env:Development__EnableMockDatabase='false'
$env:Development__EnableMockAuth='true'
$env:Development__EnableMockIntegrations='true'
$env:Development__AllowLoopbackHttp='true'
$env:Development__Port='7246'
$env:Testing__AllowLoopbackHttp='false'
$env:DevelopmentTls__PemPath=$null;$env:DevelopmentTls__KeyPath=$null
$env:Workers__Enabled='false'
$env:Database__ExpectedServer='DESKTOP-BUDV4V7'
$env:Database__ExpectedDatabase='JamunaCustomer360'
$env:Database__Connections__AGENT='Server=DESKTOP-BUDV4V7;Database=JamunaCustomer360;Integrated Security=True;Encrypt=False;TrustServerCertificate=True;'
$env:Database__ExpectedUsers__AGENT='Customer360_ApiAgent'
$env:Database__AllowUnencryptedLocalDevelopment='true'
$env:Database__IdentityVerificationReferences__AGENT='User-reviewed local setup 2026-10-17: restricted Agent identity and effective permissions; operator-attested provider isolation; empty outbox before first ticket.'
$env:Database__LiveValidationApproved='true'
$env:ASPNETCORE_URLS='http://localhost:7246'
Write-Host 'Local SQL API: JamunaCustomer360. Synthetic authentication/integrations; workers OFF.'
Write-Host 'Stop this SQL API with Ctrl+C in THIS window. No ticket is created by startup.'
& (Get-Command dotnet -ErrorAction Stop).Source (Join-Path $apiRoot 'src/Customer360.Api/bin/Debug/net10.0/Customer360.Api.dll') --contentRoot (Join-Path $apiRoot 'src/Customer360.Api')
if($LASTEXITCODE){throw "SQL API exited with code $LASTEXITCODE. Keep this window open and retain the error above."}

