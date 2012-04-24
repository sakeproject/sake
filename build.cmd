:@echo off
set EnableNuGetPackageRestore=true
"%~dp0.nuget\NuGet.exe" install -OutputDirectory packages .\packages.config
"%~dp0packages\Sake.0.1.0-alpha\tools\Sake.exe" -C %~dp0 -I src\Sake.Library\Shared %*
