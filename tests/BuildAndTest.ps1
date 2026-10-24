#requires -Version 7.0
param([switch]$HttpSmoke)
$ErrorActionPreference='Stop'
$apiRoot=Split-Path $PSScriptRoot -Parent
Push-Location $apiRoot
try {
 dotnet restore Customer360.slnx --disable-parallel -m:1
 if($LASTEXITCODE){throw 'Restore failed; retain NuGet diagnostics'}
 dotnet build Customer360.slnx --no-restore -m:1 -nr:false
 if($LASTEXITCODE){throw 'Build failed'}
 dotnet run --project tests/Customer360.UnitTests --no-build -- docs/UnitTestResults.json
 if($LASTEXITCODE){throw 'Unit tests failed'}
 dotnet src/Customer360.Api/bin/Debug/net10.0/Customer360.Api.dll --export-openapi src/Customer360.Api/OpenApi/openapi.json
 if($LASTEXITCODE){throw 'OpenAPI export failed'}
 if($HttpSmoke){& ./tests/HttpSmoke.ps1 -LoopbackHttp}
 dotnet run --project tests/Customer360.IntegrationTests --no-build -- docs/IntegrationTestResults.json
 if($LASTEXITCODE -eq 2){Write-Host 'Live SQL integration tests BLOCKED, not passed'}elseif($LASTEXITCODE){throw 'Integration runner failed'}
}
finally {Pop-Location}
