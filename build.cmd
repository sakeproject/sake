:@echo off
set EnableNuGetPackageRestore=true
".nuget\NuGet.exe" install .\packages.config -o packages
"packages\Sake.0.1.0-alpha\tools\Sake.exe" -C %~dp0 -I src\Sake.Library\Shared %*
