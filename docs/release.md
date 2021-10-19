## Versioning
Semantic Versioning 2.0.0 specification is used in this library development process (see the [specification docs](https://semver.org/spec/v2.0.0.html) for more information). The following version signature pattern is used for this project `X.Y.Z` where:
* `X` is MAJOR version. Releases with different major version are not guaranteed to be compatibile.
* `Y` is MINOR version. Releases with different minor but the same major version are guaranteed (more or less :P) to be compatibile.
* `Z` is PATCH version. When a new patch version is released it often means an API-agnostic fix of some embarassingly stupid bug.

## Releasing
1. Create release branch named `release/X.Y.Z`.
1. Create pull request from `release/X.Y.Z` to `master`.
1. Check and merge to `master`.
1. Draft a new release in GitHub as follows:
    * tag version: 'vX.Y.Z';
    * target: master;
    * release title: 'Release version X.Y.Z';
    * description with release notes formatted as line-per-issue of pattern `* description of the single-issue job done (issue #N);` where #N is the [issue number](https://github.com/jabarij/ComplicatedPrimitives/issues);
1. Check and refresh NuGet API key if neede.
1. Publish release.
