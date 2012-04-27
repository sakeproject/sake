
# CS Make - Sake

Welcome to Sake, a C# language enabled make system.

## Getting Sake from source and building

This also a way you can see Sake in action - it downloads itself from the Nuget gallery to build.

    git clone https://github.com/loudej/sake.git && cd sake && build

Or to build on Mac or Linux

    git clone https://github.com/loudej/sake.git && cd sake && ./build


## Getting Sake on your machine
### Via Chocolatey

This is a convenient if you want to run `sake` in any directory from a command line.

    cinst sake -pre

First, you'll need to get chocolatey if you don't already have it. From a Package Manager Console

    Install-Package chocolatey
    Initialize-Chocolatey
    Uninstall-Package chocolatey

Or there are other ways of getting chocolatey, see https://github.com/chocolatey/chocolatey/wiki/Installation


## Getting Sake in your project's build
### Via build.cmd bootstrapper

This is another nice technique if you want to use Sake in your project and don't to need people to install anything machine-wide. What you do is use Nuget.exe to download SAKE from the gallery in a pre-build step. This takes three files, `package.config`, `build.cmd`, and `makefile.shade`.

#### package.config
    <packages>
      <package id="Sake" version="0.1.0-alpha" />
      <!-- add other packages used as build tools, like nunit or xunit-runner -->
    </packages>

#### build.cmd
    @echo off
    ".nuget\NuGet.exe" install -OutputDirectory packages .\packages.config
    "packages\Sake.0.1.0-alpha\tools\sake.exe" %*

#### makefile.shade
    #default
      log info="Hello world!"

