# Implementation Plan: Notebook redesign

**Branch**: `004-notebook-redesign` | **Date**: 2026-10-20 | **Spec**: [spec.md](spec.md)

**Input**: Feature specification from `/specs/004-notebook-redesign/spec.md`

## Summary

Shared design tokens (colours, fonts, shadows, motion) drive a sticky header
with a mobile menu, a polaroid hero, year-labelled experience cards, index
cards, sticker tags, note-paper contact and a footer — implemented once as
Angular components and once as Razor views with a small vanilla-JS file.
Stack links come from configuration per environment.

## Technical Context

**Language/Version**: TypeScript 6 / Angular 22; C# 14 / ASP.NET Core 10; vanilla JS (.NET)

**Primary Dependencies**: `@fontsource` (Angular); fonts copied to `wwwroot/fonts` (.NET)

**Storage**: N/A

**Testing**: Vitest + jsdom (Angular component tests incl. menu and reveal); xUnit + AngleSharp (.NET markup)

**Target Platform**: Modern browsers, 320px and up

**Project Type**: Web application, two implementations

**Performance Goals**: Hover/press feedback under 200ms; reveal 600ms once per element

**Constraints**: Animate only `transform`/`opacity`/`clip-path`; respect reduced motion; no third-party requests; production never links to `localhost`

**Scale/Scope**: 1 layout, 6 sections

## Constitution Check

- V. Accessible, calm UI: pointer-gated hover, reduced motion, landmarks — ✅
- III. Same design in both apps; Angular-only FRs are covered by a manual check for .NET (SC-004) — ✅
- II. Traceable tests: `[004:FR-###]` in both apps — ✅

## Project Structure

### Documentation (this feature)

```text
specs/004-notebook-redesign/
├── spec.md
├── plan.md
└── tasks.md
```

### Source Code (repository root)

```text
angular-cv/angular-cv/src/
├── styles.css                                  # tokens + shared styles
├── environments/environment(.development).ts   # stack link URL per build
└── app/layout/site-header, site-footer; sections/*; shared/reveal.ts

dotnet-cv/DotNetCV/
├── wwwroot/css/site.css, js/site.js, fonts/
├── Views/Shared/_Layout.cshtml, Views/Home/Index.cshtml
├── Configuration/StackLinkOptions.cs           # StackLinks:AngularUrl
└── appsettings(.Development).json
```

**Structure Decision**: Same tokens and markup structure in both apps; each
app keeps its own idiomatic implementation.
