#requires -Version 7.0
param([int]$Port=7246,[string]$CertificatePath,[string]$CertificateKeyPath,[switch]$LoopbackHttp)
$ErrorActionPreference='Stop'
$apiRoot=Split-Path $PSScriptRoot -Parent
$runtime=Join-Path $apiRoot 'TestResults/runtime'
New-Item -ItemType Directory -Force $runtime | Out-Null
if(-not $LoopbackHttp){if(-not $CertificatePath){$CertificatePath=Join-Path $runtime 'localhost.crt';dotnet dev-certs https --export-path $CertificatePath --format Pem --no-password | Out-Null;if($LASTEXITCODE){throw 'HTTPS development certificate export failed'};$CertificateKeyPath=Join-Path $runtime 'localhost.key'};$CertificatePath=(Resolve-Path -LiteralPath $CertificatePath).Path;$CertificateKeyPath=(Resolve-Path -LiteralPath $CertificateKeyPath).Path}
$names=@('ASPNETCORE_ENVIRONMENT','ASPNETCORE_URLS','Development__EnableMockDatabase','Development__EnableMockAuth','Development__EnableMockIntegrations','DevelopmentTls__PemPath','DevelopmentTls__KeyPath','DevelopmentTls__Port','Testing__AllowLoopbackHttp','Testing__Port')
$previous=@{};foreach($name in $names){$previous[$name]=[Environment]::GetEnvironmentVariable($name,'Process')}
$process=$null;$results=[Collections.Generic.List[object]]::new();$failed=0
$base=if($LoopbackHttp){"http://localhost:$Port"}else{"https://localhost:$Port"}
function Request([string]$method,[string]$path,$body,$session,$headers=@{}){
 $p=@{Uri=$base+$path;Method=$method;SkipCertificateCheck=$true;SkipHttpErrorCheck=$true;Headers=$headers;TimeoutSec=5;NoProxy=$true}
 # Certificate bypass is solely for this loopback test server, never a provider or SQL connection.
 if($session){[void]$session.Headers.Remove('X-CSRF-TOKEN');$p.WebSession=$session};if($null -ne $body){$p.Body=$body | ConvertTo-Json -Depth 12;$p.ContentType='application/json'}
 Invoke-WebRequest @p
}
function Check([string]$name,$response,[int]$expected){$ok=[int]$response.StatusCode -eq $expected;$results.Add([pscustomobject]@{Name=$name;Status=if($ok){'PASS'}else{'FAIL'};Expected=$expected;Actual=[int]$response.StatusCode});if(-not $ok){$script:failed++;Write-Host "FAIL $name expected $expected got $($response.StatusCode): $($response.Content)"}else{Write-Host "PASS $name"}}
function Login([string]$persona){$session=[Microsoft.PowerShell.Commands.WebRequestSession]::new();$csrf=(Request GET '/api/v1/auth/csrf' $null $session).Content | ConvertFrom-Json;$response=Request POST '/api/v1/auth/development/session' @{persona=$persona} $session @{'X-CSRF-TOKEN'=$csrf.data.token};Check "Development $persona login" $response 200;$csrf=(Request GET '/api/v1/auth/csrf' $null $session).Content | ConvertFrom-Json;return @{Session=$session;Headers=@{'X-CSRF-TOKEN'=$csrf.data.token}}}
try{
 $env:ASPNETCORE_ENVIRONMENT=if($LoopbackHttp){'Testing'}else{'Development'};$env:ASPNETCORE_URLS=$base
 $env:Development__EnableMockDatabase='true';$env:Development__EnableMockAuth='true';$env:Development__EnableMockIntegrations='true'
 if($LoopbackHttp){$env:Testing__AllowLoopbackHttp='true';$env:Testing__Port=$Port.ToString();$env:DevelopmentTls__PemPath=$null;$env:DevelopmentTls__KeyPath=$null}
 else{$env:DevelopmentTls__PemPath=$CertificatePath;$env:DevelopmentTls__KeyPath=$CertificateKeyPath;$env:DevelopmentTls__Port=$Port.ToString()}
 $dll=Join-Path $apiRoot 'src/Customer360.Api/bin/Debug/net10.0/Customer360.Api.dll'
 $process=Start-Process -FilePath (Get-Command dotnet).Source -ArgumentList @('"'+$dll+'"') -WorkingDirectory (Join-Path $apiRoot 'src/Customer360.Api') -PassThru -WindowStyle Hidden -RedirectStandardOutput (Join-Path $runtime 'stdout.log') -RedirectStandardError (Join-Path $runtime 'stderr.log')
 foreach($name in $names){[Environment]::SetEnvironmentVariable($name,$previous[$name],'Process')}
 $healthy=$false;for($i=0;$i -lt 8;$i++){try{if((Request GET '/health' $null $null).StatusCode -eq 200){$healthy=$true;break}}catch{};if($process.HasExited){throw 'Mock API process exited; retain runtime logs'};Start-Sleep -Milliseconds 250};if(-not $healthy){throw 'Mock API did not become available'}
 Check 'Health distinguishes pending SQL' (Request GET '/health' $null $null) 200
 Check 'Development OpenAPI available' (Request GET '/openapi/v1.json' $null $null) 200
 $decision=@{requestID=1;expectedRowVersion='AQAAAAAAAAA='}
 Check 'Anonymous Manager action rejected' (Request POST '/api/v1/tickets/ticket/approve' $decision $null) 401
 Check 'Forged identity headers rejected' (Request POST '/api/v1/tickets/ticket/approve' $decision $null @{'X-Agent-ID'='AGENT001';'X-Role'='MANAGER'}) 401
 Check 'Development login requires CSRF' (Request POST '/api/v1/auth/development/session' @{persona='Agent'} $null) 400
 $agent=Login 'Agent'
 Check 'Agent Manager action denied' (Request POST '/api/v1/tickets/ticket/approve' $decision $agent.Session $agent.Headers) 403
 $create=@{serviceID=1;versionID=1;idempotencyKey=[guid]::NewGuid().ToString();customerReference='SYNTHETIC-CUSTOMER';fields=@(@{fieldID=1;value='Synthetic'});description='Synthetic HTTP test'}
 $forged=$create.Clone();$forged.actorRole='MANAGER'
 Check 'Role body field rejected' (Request POST '/api/v1/tickets' $forged $agent.Session $agent.Headers) 400
 $created=Request POST '/api/v1/tickets' $create $agent.Session $agent.Headers;Check 'Synthetic ticket creation' $created 200
 $ticket=($created.Content | ConvertFrom-Json).data
 Check 'Duplicate request conflicts' (Request POST '/api/v1/tickets' $create $agent.Session $agent.Headers) 409
 Check 'Authenticated mutation requires CSRF' (Request POST '/api/v1/tickets' $create $agent.Session) 400
 Check 'Malformed rowversion rejected' (Request POST '/api/v1/tickets/ticket/approve' @{requestID=$ticket.requestID;expectedRowVersion='AA=='} $agent.Session $agent.Headers) 403
 $manager=Login 'Manager'
 Check 'Empty rejection reason rejected' (Request POST '/api/v1/tickets/ticket/reject' @{requestID=$ticket.requestID;expectedRowVersion='AQAAAAAAAAA=';remarks=''} $manager.Session $manager.Headers) 400
 $decision.requestID=$ticket.requestID
 Check 'Manager approves synthetic ticket' (Request POST '/api/v1/tickets/ticket/approve' $decision $manager.Session $manager.Headers) 200
 Check 'Stale decision conflicts' (Request POST '/api/v1/tickets/ticket/reject' @{requestID=$ticket.requestID;expectedRowVersion='AQAAAAAAAAA=';remarks='Synthetic reason'} $manager.Session $manager.Headers) 409
 Check 'Unknown tracking token is generic unavailable' (Request POST '/api/v1/tracking' @{token=('A'*43)} $null) 404
 Check 'Worker procedure has no HTTP endpoint' (Request POST '/api/v1/communications/communication-outbox/claim' @{} $manager.Session $manager.Headers) 404
 Check 'Real Avaya adapter fails closed' (Request POST '/api/v1/auth/avaya' @{assertion='unverified'} $manager.Session $manager.Headers) 503
 $audit=Login 'Auditor'
 Check 'Auditor denied ticket mutation' (Request POST '/api/v1/tickets/ticket/approve' $decision $audit.Session $audit.Headers) 403
 $configuration=Login 'Configuration'
 Check 'Configuration user cannot Manager approve' (Request POST '/api/v1/tickets/ticket/approve' $decision $configuration.Session $configuration.Headers) 403
 Check 'Manager cannot publish configuration' (Request POST '/api/v1/configuration/service-version/publish-draft' @{versionID=1;expectedRowVersion='AQAAAAAAAAA='} $manager.Session $manager.Headers) 403
}
finally{
 foreach($name in $names){[Environment]::SetEnvironmentVariable($name,$previous[$name],'Process')}
 if($process -and -not $process.HasExited){Stop-Process -Id $process.Id}
 [pscustomobject]@{Suite='API with development doubles, no SQL execution';Transport=if($LoopbackHttp){'All-mock Testing loopback HTTP; production TLS not tested'}else{'HTTPS'};Passed=($results.Count-$failed);Failed=$failed;Results=$results} | ConvertTo-Json -Depth 10 | Set-Content (Join-Path $apiRoot 'docs/HttpSmokeResults.json')
}
if($failed){throw "$failed HTTP smoke checks failed"}
