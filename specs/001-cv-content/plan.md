# Implementation Plan: CV content

**Branch**: `001-cv-content` | **Date**: 2026-10-20 | **Spec**: [spec.md](spec.md)

**Input**: Feature specification from `/specs/001-cv-content/spec.md`

## Summary

Render the CV from a single, static content source per app into semantic
sections (hero, about, experience, education, skills, contact). Formatting
rules (name split, date ranges, "since", phone link) live in code, not in
templates, so they can be unit tested.

## Technical Context

**Language/Version**: TypeScript 6 / Angular 22; C# 14 / ASP.NET Core 10

**Primary Dependencies**: Angular (standalone components, signals); ASP.NET Core MVC (Razor)

**Storage**: N/A — content is static in `CvService` (one per app)

**Testing**: Vitest + jsdom (Angular); xUnit + `WebApplicationFactory` + AngleSharp (.NET)

**Target Platform**: Modern browsers; GitHub Pages (Angular), Kestrel (.NET)

**Project Type**: Web application, two independent implementations

**Performance Goals**: Content available on first render; no network calls for content

**Constraints**: Dates formatted in English regardless of culture; no private data (constitution IV)

**Scale/Scope**: One page, ~5 roles, ~4 education entries, ~10 skill categories

## Constitution Check

- I. Spec first: this spec defines the content behaviour — ✅
- II. Traceable tests: every FR has tests tagged `[001:FR-###]` — ✅
- III. Two implementations: Angular-only FRs are justified in the spec's Assumptions — ✅
- IV. Privacy: content excludes private details; tests use made-up contact data — ✅
- V/VI: semantic markup, logic in code — ✅

## Project Structure

### Documentation (this feature)

```text
specs/001-cv-content/
├── spec.md
├── plan.md
└── tasks.md
```

### Source Code (repository root)

```text
angular-cv/angular-cv/src/app/
├── services/cv.service.ts            # content
├── models/                           # content types
├── sections/{hero,about,experience,education,skills,contact}/
└── app.spec.ts, sections/*/*.spec.ts # tests

dotnet-cv/
├── DotNetCV/Services/CvService.cs    # content
├── DotNetCV/Models, ViewModels/      # immutable content types
├── DotNetCV/Views/Home/Index.cshtml  # page
└── DotNetCV.Tests/Integration/CvPageTests.cs, CvContentRulesTests.cs
```

**Structure Decision**: Each app owns its content file and rendering; the
shared spec keeps them in step.
