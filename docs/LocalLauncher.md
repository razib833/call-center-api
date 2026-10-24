# Windows service launcher

Double-click `StartServices.bat` inside the Api directory. It starts the API and in-process synthetic integrations, verifies `/health`, and prints the URLs. Requires .NET 10. The launcher finds PowerShell 7 on PATH or in its standard installation directory and falls back to built-in Windows PowerShell 5.1; PowerShell 7 installation is not required for these launchers. If the API binary is missing or C# source is newer, it restores/builds first; package access is required for an initial restore. It otherwise uses the current build. Double-click `StopServices.bat` to stop only the recorded API process, checking its PID, creation time and executable before stopping. No administrator permission or SQL Server startup is required.

Default Postman baseUrl: **http://localhost:7246**. Change the imported collection's baseUrl from HTTPS to HTTP. Default transport is the existing all-mock Testing loopback mode; no real database/provider is enabled. Workers stay disabled. There is no React frontend in Phase 4.

Options from a command prompt:

```bat
StartServices.bat -Port 7247
StartServices.bat -Rebuild
StartServices.bat -SkipBuild
StartServices.bat -Mode Https
```

HTTPS requires a working trusted development certificate and has not been verified in the agent environment. The default HTTP mode is loopback-only and permitted only with all three mocks enabled in Testing. Runtime logs and process metadata are stored under `TestResults/LocalServices`. If a port is occupied, the launcher stops without terminating its owner. Never move the batch files away from their project/helper files.

Verification on 16 October 2026: actual batch startup, health verification and batch stop succeeded on port 7247 with exit code 0. A first Windows PowerShell launch hit a duplicate PATH issue, so the launcher explicitly uses PowerShell 7. A WMI permission dependency was removed; stop uses owned process metadata without administrator/WMI rights. The test API was stopped after verification.

Compatibility correction: Windows PowerShell 5.1 startup, health and stop were actually verified on port 7247 with exit 0. Duplicate case variants of environment variables are normalized inside the launcher process only; inherited Windows socket settings are preserved. No user/machine PATH change or administrative permission is required. Update StartServices.bat, StopServices.bat and tools/LocalServices.ps1 together (or extract the updated API ZIP).

