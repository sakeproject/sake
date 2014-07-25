@echo off
cd %~dp0

setlocal
set EnableNuGetPackageRestore=true
set Configuration=Release

msbuild /fl /v:Q Sake.sln

src\Sake\bin\%Configuration%\sake.exe -I src\Sake.Library\Shared %*
set __sake__=

