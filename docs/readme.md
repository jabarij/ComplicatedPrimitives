[![Github All Releases](https://img.shields.io/github/downloads/jabarij/ComplicatedPrimitives/total.svg?maxAge=600)](https://github.com/dotnet/docfx/releases/latest)
[![NuGet](https://img.shields.io/nuget/v/SoterDevelopment.ComplicatedPrimitives.svg)](https://www.nuget.org/packages/SoterDevelopment.ComplicatedPrimitives/)

# ComplicatedPrimitives

## Description
This library contains types that seem to be primitive but are non-trivial and a bit arduous to implement. This means mainly any abstract algebraic structures (like range) or other .NET primitive types wrappers that modify their behaviour (like CaseInsensitiveString).

## How to install
* Using [NuGet client](docs.microsoft.com/nuget/install-nuget-client-tools) install package [SoterDevelopment.ComplicatedPrimitives](https://www.nuget.org/packages/SoterDevelopment.ComplicatedPrimitives/).
  > Under Visual Studio NuGet Manager Console run command `Install-Package SoterDevelopment.ComplicatedPrimitives` or `Install-Package SoterDevelopment.ComplicatedPrimitives -Version X.Y.Z` to install specific version.
* Downloading any desired version from [GitHub repo](https://github.com/jabarij/ComplicatedPrimitives) or one of available [stable releases](https://github.com/jabarij/ComplicatedPrimitives/releases) and compiling it by yourself.
* Getting it on a 3.5" floppy disk from some suspiciously dressed man on a train station (just kidding, don't do this... in fact it's really hard to read a floppy disk nowadays).

## Versioning and compatibility
Semantic Versioning 2.0.0 specification is used in this library development process (see the [specification docs](https://semver.org/spec/v2.0.0.html) for more information). The following version signature pattern is used for this project `X.Y.Z` where:
* `X` is MAJOR version. Releases with different major version are not guaranteed to be compatibile.
* `Y` is MINOR version. Releases with different minor but the same major version are guaranteed (more or less :P) to be compatibile.
* `Z` is PATCH version. When a new patch version is released it often means an API-agnostic fix of some embarassingly stupid bug.

This library is considered to be compatible with .NET Standard 2.0 to assure the widest reasonable compatibility with .NET Core and .NET Standard projects.

## Documentation
[API documentation](api/index.md) for this project is developed using [XML documentation comments](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/xmldoc), so should be available to read directly in your IDE. 