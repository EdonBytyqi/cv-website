# .NET CV

Edon Bytyqi's CV as an ASP.NET Core 10 MVC site with a read-only JSON API.

## Getting started

```bash
dotnet run --project DotNetCV        # http://localhost:5000 (profiles in launchSettings.json)
dotnet test                          # unit, integration and spec-coverage tests
```

In Development, the OpenAPI document is at `/openapi/v1.json` and Swagger UI
at `/api/docs`.

## How this project is developed

Behaviour is defined in specs first, then tested, then built, using GitHub
Spec Kit. The shared feature specs live in the repository's root
[`specs/`](../specs) folder, the rules in the
[constitution](../.specify/memory/constitution.md), and code conventions in
[`CLAUDE.md`](CLAUDE.md).

## Structure

| Path | Purpose |
| --- | --- |
| `DotNetCV/Services/CvService.cs` | The CV content, built once and registered as a singleton |
| `DotNetCV/Models`, `ViewModels` | Immutable content types |
| `DotNetCV/Controllers` | The CV page (`HomeController`) and `/api/cvapi` |
| `DotNetCV.Tests/Integration` | In-memory app tests for features 001–004 |
| `DotNetCV.Tests/Specs` | The spec coverage check against the root `specs/` |

## Running both versions locally

The stack switch links the two versions when both run on your machine:

```bash
dotnet run --project DotNetCV --launch-profile http   # .NET on http://localhost:5000
cd ../angular-cv/angular-cv && npm start              # Angular on http://localhost:4200
```

In Development the Angular button opens `http://localhost:4200`
(`StackLinks:AngularUrl` in `appsettings.Development.json`); elsewhere it
opens the live GitHub Pages site.
