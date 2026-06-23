# EVE ISK per Hour

This repository contains a classic VB.NET Windows Forms application targeting `.NET Framework 4.6.1` and `x86`.

## Development build

Build the solution with MSBuild from Visual Studio:

```powershell
& 'C:\Program Files\Microsoft Visual Studio\18\Professional\MSBuild\Current\Bin\MSBuild.exe' `
  'EVE Isk per Hour.sln' `
  /t:Build `
  /p:Configuration=Debug `
  /p:Platform=x86 `
  /m
```

## Run locally

The main app's `Debug|x86` output path is configured as `..\Root Directory`, so the executable is written to `E:\Root Directory\` when the repo lives at `E:\EVE-IPH`.

Launch it with:

```powershell
Start-Process -FilePath 'E:\Root Directory\EVE Isk per Hour.exe' -WorkingDirectory 'E:\Root Directory'
```

## Notes

- The solution also builds `EVEIPH Updater` and `EVEIPH SQLite DLL Updater`.
- NuGet packages are committed under `packages/`, so a restore step was not required in this environment.
- If you want the build output to stay inside the repo, update the `Debug|x86` `OutputPath` in `EVE Isk per Hour.vbproj`.
