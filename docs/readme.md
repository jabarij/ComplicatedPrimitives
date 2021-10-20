[![Latest version](https://img.shields.io/nuget/v/SoterDevelopment.ComplicatedPrimitives.svg)](https://www.nuget.org/packages/SoterDevelopment.ComplicatedPrimitives/)
[![Downloads](https://img.shields.io/nuget/dt/SoterDevelopment.ComplicatedPrimitives.svg)](https://www.nuget.org/packages/SoterDevelopment.ComplicatedPrimitives/)

# ComplicatedPrimitives

## What is ComplicatedPrimitives?
Generally speaking, every general purpose algebraic type or type wrapping other .NET primitive type hiding or widening it's original behaviour can be placed in this library. In other words this library contains types that seem to be primitive but are non-trivial and a bit arduous to implement. The best example is a range of comparable values, which seems to be just a pair of two values with a few methods, but when added open/closed boundaries, methods imitating set theory operators and some aggreagation operations it turns out to be quite tricky.

## How to install
* Using [NuGet client](docs.microsoft.com/nuget/install-nuget-client-tools) install package [SoterDevelopment.ComplicatedPrimitives](https://www.nuget.org/packages/SoterDevelopment.ComplicatedPrimitives/).
  > Under Visual Studio NuGet Manager Console run command `Install-Package SoterDevelopment.ComplicatedPrimitives` or `Install-Package SoterDevelopment.ComplicatedPrimitives -Version X.Y.Z` to install specific version.
* Downloading any desired version from [GitHub repo](https://github.com/jabarij/ComplicatedPrimitives) or one of available [stable releases](https://github.com/jabarij/ComplicatedPrimitives/releases) and compiling it by yourself.
* Getting it on a 3.5" floppy disk from some suspiciously dressed man on a train station (just kidding, don't do this... in fact it's really hard to read a floppy disk nowadays).

## Compatibility
This library is considered to be compatible with .NET Standard 2.0 to assure the widest reasonable compatibility with .NET Core and .NET Standard projects.

Versioning and releasing instructions are available [here](release.md).

## Documentation
See the [documentation](index.md) section.
