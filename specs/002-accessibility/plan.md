# Implementation Plan: Accessibility

**Branch**: `002-accessibility` | **Date**: 2026-10-20 | **Spec**: [spec.md](spec.md)

**Input**: Feature specification from `/specs/002-accessibility/spec.md`

## Summary

Semantic landmarks (`header`, `nav`, `main`, `footer`), a skip link, real
`img` elements with alt text, and named links. The Angular test suite runs
axe-core against the rendered app; both apps test the skip link and alt text.

## Technical Context

**Language/Version**: TypeScript 6 / Angular 22; C# 14 / ASP.NET Core 10

**Primary Dependencies**: axe-core (Angular tests); AngleSharp (.NET tests)

**Storage**: N/A

**Testing**: Vitest + jsdom + axe-core (Angular); xUnit + `WebApplicationFactory` + AngleSharp (.NET)

**Target Platform**: Modern browsers and screen readers

**Project Type**: Web application, two implementations

**Performance Goals**: N/A

**Constraints**: WCAG 2.2 AA (constitution V)

**Scale/Scope**: One page plus the .NET error page

## Constitution Check

- V. Accessible UI: this feature is the enforcement point — ✅
- II. Traceable tests: `[002:FR-###]` tags in both apps — ✅
- III. Angular-only FRs are listed as a follow-up in the spec's Assumptions — ✅

## Project Structure

### Documentation (this feature)

```text
specs/002-accessibility/
├── spec.md
├── plan.md
└── tasks.md
```

### Source Code (repository root)

```text
angular-cv/angular-cv/src/app/app.html, app.spec.ts   # skip link, axe test
dotnet-cv/DotNetCV/Views/Shared/_Layout.cshtml        # skip link, landmarks
dotnet-cv/DotNetCV.Tests/Integration/RedesignTests.cs # skip link, alt text
```

**Structure Decision**: Accessibility checks live next to each app's page tests.
