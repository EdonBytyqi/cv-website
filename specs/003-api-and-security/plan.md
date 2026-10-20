# Implementation Plan: API and security

**Branch**: `003-api-and-security` | **Date**: 2026-10-20 | **Spec**: [spec.md](spec.md)

**Input**: Feature specification from `/specs/003-api-and-security/spec.md`

## Summary

`CvApiController` exposes read-only `GET` endpoints over the same `ICvService`
the page uses. A middleware adds security headers; unhandled errors go to the
exception handler, whose error page no longer depends on the CV service.

## Technical Context

**Language/Version**: C# 14 / ASP.NET Core 10

**Primary Dependencies**: ASP.NET Core MVC, Microsoft.AspNetCore.OpenApi, Swashbuckle Swagger UI (Development only)

**Storage**: N/A

**Testing**: xUnit + `WebApplicationFactory` (in-memory app, Production environment for error tests)

**Target Platform**: Kestrel

**Project Type**: Web service inside the MVC app

**Performance Goals**: N/A

**Constraints**: CORS limited to `GET`; no exception details in responses

**Scale/Scope**: 7 endpoints

## Constitution Check

- III. *(.NET only)* is justified: the Angular app has no server — ✅
- II. Traceable tests: `[003:FR-###]` in `ApiAndSecurityTests.cs` — ✅
- VI. No swallow-and-log try/catch in controllers — ✅

## Project Structure

### Documentation (this feature)

```text
specs/003-api-and-security/
├── spec.md
├── plan.md
└── tasks.md
```

### Source Code (repository root)

```text
dotnet-cv/DotNetCV/Controllers/Api/CvApiController.cs
dotnet-cv/DotNetCV/Program.cs                         # headers, CORS, health, OpenAPI
dotnet-cv/DotNetCV/Views/Shared/_Layout.cshtml, Error.cshtml
dotnet-cv/DotNetCV.Tests/Integration/ApiAndSecurityTests.cs
```

**Structure Decision**: Server concerns stay in the .NET app.
