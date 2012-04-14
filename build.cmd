:@echo off
"%~dp0.nuget\nuget.exe" install -OutputDirectory packages .\packages.config
"%~dp0packages\Sake.0.1.0\tools\sake.exe" -C %~dp0 -f makefile.shade %*
