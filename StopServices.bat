@echo off
setlocal
cd /d "%~dp0"
set "C360_PS=%SystemRoot%\System32\WindowsPowerShell\v1.0\powershell.exe"
where pwsh.exe >nul 2>&1
if not errorlevel 1 set "C360_PS=pwsh.exe"
if exist "%ProgramW6432%\PowerShell\7\pwsh.exe" set "C360_PS=%ProgramW6432%\PowerShell\7\pwsh.exe"
if exist "%ProgramFiles%\PowerShell\7\pwsh.exe" set "C360_PS=%ProgramFiles%\PowerShell\7\pwsh.exe"
"%C360_PS%" -NoProfile -ExecutionPolicy Bypass -File "%~dp0tools\LocalServices.ps1" -Action Stop
set "STOP_EXIT=%ERRORLEVEL%"
echo.
if not defined C360_NO_PAUSE pause
exit /b %STOP_EXIT%
