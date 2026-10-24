param([ValidateSet('Start','Stop')][string]$Action='Start',[ValidateSet('Http','Https')][string]$Mode='Http',[int]$Port=7246,[switch]$SkipBuild,[switch]$Rebuild)
$ErrorActionPreference='Stop'
$apiRoot=[IO.Path]::GetFullPath((Join-Path $PSScriptRoot '..'))
$runtime=Join-Path $apiRoot 'TestResults/LocalServices'
$statePath=Join-Path $runtime 'service.json'
New-Item -ItemType Directory -Force $runtime | Out-Null
function Get-DotnetPath {
 $command=Get-Command dotnet -ErrorAction SilentlyContinue
 if($command){return $command.Source}
 foreach($root in @($env:ProgramW6432,$env:ProgramFiles)){if($root){$candidate=Join-Path $root 'dotnet/dotnet.exe';if(Test-Path -LiteralPath $candidate){return $candidate}}}
 throw '.NET 10 SDK/runtime was not found. Install .NET 10 before starting the API.'
}
function Get-OwnedProcess($saved){
 $p=Get-Process -Id $saved.ProcessId -ErrorAction SilentlyContinue
 if(-not $p){return $null}
 if($p.StartTime.ToUniversalTime().Ticks.ToString() -ne [string]$saved.StartTicks){throw 'PID was reused. Refusing to stop an unrelated process.'}
 $expectedDll=Join-Path $apiRoot 'src/Customer360.Api/bin/Debug/net10.0/Customer360.Api.dll'
 if([string]$saved.Dll -ne $expectedDll -or $p.Path -ne (Get-DotnetPath)){throw 'Process executable/state does not match this launcher. No process was stopped.'}
 return $p
}
if($Action -eq 'Stop'){
 if(-not (Test-Path -LiteralPath $statePath)){Write-Host 'No service started by this launcher is recorded.';exit 0}
 $saved=Get-Content -LiteralPath $statePath -Raw | ConvertFrom-Json
 $p=Get-OwnedProcess $saved
 if($p){Stop-Process -Id $p.Id;Write-Host 'Customer360 mock API stopped.'}else{Write-Host 'The recorded API process has already stopped.'}
 Remove-Item -LiteralPath $statePath
 exit 0
}
if($Port -lt 1024 -or $Port -gt 65535){throw 'Choose a port between 1024 and 65535.'}
if(Test-Path -LiteralPath $statePath){$saved=Get-Content -LiteralPath $statePath -Raw | ConvertFrom-Json;$existing=Get-OwnedProcess $saved;if($existing){Write-Host "API is already running: $($saved.BaseUrl)";exit 0}}
if(Get-NetTCPConnection -LocalPort $Port -State Listen -ErrorAction SilentlyContinue){throw "Port $Port is in use. Nothing was stopped. Use StartServices.bat -Port 7247."}
$dotnet=Get-DotnetPath
$env:DOTNET_CLI_HOME=Join-Path $runtime 'dotnet-home'
# Keep package paths out of the deeply nested runtime directory. Some MSBuild
# assembly/copy tasks cannot consume DLL paths longer than MAX_PATH on Windows.
# Preserve an explicitly configured package cache; otherwise use NuGet's usual
# per-user cache independently of the launcher's isolated CLI home.
if([string]::IsNullOrWhiteSpace($env:NUGET_PACKAGES)){
 $env:NUGET_PACKAGES=Join-Path ([Environment]::GetFolderPath('UserProfile')) '.nuget/packages'
}
$env:DOTNET_CLI_TELEMETRY_OPTOUT='1'
$env:DOTNET_NOLOGO='1'
Push-Location $apiRoot
try {
 $dll=Join-Path $apiRoot 'src/Customer360.Api/bin/Debug/net10.0/Customer360.Api.dll'
 $needsBuild=-not (Test-Path -LiteralPath $dll)
 if(-not $needsBuild){$builtAt=(Get-Item -LiteralPath $dll).LastWriteTimeUtc;$needsBuild=@(Get-ChildItem (Join-Path $apiRoot 'src') -File -Recurse | Where-Object {$_.Extension -in '.cs','.csproj' -and $_.FullName -notmatch '[\\/](bin|obj)[\\/]' -and $_.LastWriteTimeUtc -gt $builtAt}).Count -gt 0}
 if(-not $SkipBuild -and ($needsBuild -or $Rebuild)){
  & $dotnet restore Customer360.slnx --force --disable-parallel -m:1 -nr:false
  if($LASTEXITCODE){throw 'Package restore failed. Review the restore messages; no API was started.'}
  & $dotnet build src/Customer360.Api/Customer360.Api.csproj --no-restore -m:1 -nr:false
  if($LASTEXITCODE){throw 'API build failed. No API was started.'}
 }
 if(-not (Test-Path -LiteralPath $dll)){throw 'Built API not found. Run StartServices.bat without -SkipBuild.'}
 $env:ASPNETCORE_ENVIRONMENT=if($Mode -eq 'Http'){'Testing'}else{'Development'}
 $env:Development__EnableMockDatabase='true';$env:Development__EnableMockAuth='true';$env:Development__EnableMockIntegrations='true'
 $env:Workers__Enabled='false';$env:Database__LiveValidationApproved='false'
 $env:Testing__AllowLoopbackHttp=if($Mode -eq 'Http'){'true'}else{'false'}
 $env:Testing__Port=$Port.ToString();$env:DevelopmentTls__PemPath=$null;$env:DevelopmentTls__KeyPath=$null
 $baseUrl=if($Mode -eq 'Http'){"http://localhost:$Port"}else{"https://localhost:$Port"}
 $env:ASPNETCORE_URLS=$baseUrl
 $stdout=Join-Path $runtime 'api.stdout.log';$stderr=Join-Path $runtime 'api.stderr.log'
 $launchArguments=@('"'+$dll+'"','--environment',$env:ASPNETCORE_ENVIRONMENT,'--urls',$baseUrl,
 '--Development:EnableMockDatabase','true','--Development:EnableMockAuth','true','--Development:EnableMockIntegrations','true',
 '--Workers:Enabled','false','--Database:LiveValidationApproved','false','--Testing:AllowLoopbackHttp',$env:Testing__AllowLoopbackHttp,'--Testing:Port',$Port.ToString())
 $launch=@{FilePath=$dotnet;ArgumentList=$launchArguments;WorkingDirectory=(Join-Path $apiRoot 'src/Customer360.Api');WindowStyle='Hidden';PassThru=$true;RedirectStandardOutput=$stdout;RedirectStandardError=$stderr}
 # Normalize duplicate environment-key casing only inside this launcher process.
 # Preserve inherited system settings required by Windows sockets; never change user/machine PATH.
 if($PSVersionTable.PSVersion.Major -lt 6){
  $snapshot=[Environment]::GetEnvironmentVariables('Process')
  $duplicates=@($snapshot.Keys | Group-Object {([string]$_).ToUpperInvariant()} | Where-Object {$_.Count -gt 1})
  foreach($group in $duplicates){
   $key=[string]$group.Group[0];$value=[string]$snapshot[$key]
   if($key -ieq 'PATH'){$value=($group.Group | ForEach-Object {$snapshot[$_]}) -join ';'}
   for($n=0;$n -le $group.Count;$n++){[Environment]::SetEnvironmentVariable($key,$null,'Process')}
   [Environment]::SetEnvironmentVariable($key,$value,'Process')
  }
 }
 $process=Start-Process @launch
 $started=$false
 try {
  $deadline=[DateTime]::UtcNow.AddSeconds(30)
  while([DateTime]::UtcNow -lt $deadline){
   $process.Refresh();if($process.HasExited){throw "API exited during startup. Read $stderr and $stdout"}
   try{$health=Invoke-RestMethod -Uri "$baseUrl/health" -TimeoutSec 2 -UseBasicParsing;if($health.status -eq 'alive' -and $health.mockDatabase -eq $true -and $health.workersEnabled -eq $false){$started=$true;break}}catch{}
   Start-Sleep -Milliseconds 300
  }
  if(-not $started){throw "Health check failed. HTTPS requires a valid trusted local certificate. Logs: $runtime"}
  $process.Refresh()
  [pscustomobject]@{ProcessId=$process.Id;StartTicks=$process.StartTime.ToUniversalTime().Ticks.ToString();Dll=$dll;BaseUrl=$baseUrl;Mode='Local mocks only';WorkersEnabled=$false} | ConvertTo-Json | Set-Content -LiteralPath $statePath
 }catch{if(-not $process.HasExited){Stop-Process -Id $process.Id};throw}
 Write-Host "`nCustomer360 mock services are ready."
 Write-Host "API / Postman baseUrl: $baseUrl"
 Write-Host "Health:               $baseUrl/health"
 Write-Host "OpenAPI:              $baseUrl/openapi/v1.json"
 Write-Host "Agent UI:             $baseUrl/agent?mobile=01712345678"
 Write-Host "Postman collection:   $(Join-Path $apiRoot 'Postman/Customer360.LocalMock.postman_collection.json')"
 Write-Host "Logs:                 $runtime"
 Write-Host 'Stop: double-click StopServices.bat'
 Write-Host 'SQL Server is not required in mock mode. SMS/email workers remain OFF. Agent UI uses this same API origin.'
}finally{Pop-Location}

