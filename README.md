# CV Website — Angular & .NET

Edon Bytyqi's CV, built twice: once as an **Angular 22** single-page app and once
as an **ASP.NET Core 10** MVC site. Both show the same content in the same
"notebook" design, and a **Stack** switch in the header jumps between them.

- **Angular:** <https://edonbytyqi.github.io/cv-website/> (deployed from `main`)
- **.NET:** runs locally for now (see below)

## Running locally

```bash
# Angular — http://localhost:4200
cd angular-cv/angular-cv
npm ci
npm start

# .NET — http://localhost:5000
cd dotnet-cv
dotnet run --project DotNetCV --launch-profile http
```

With both running, the Stack switch links them: Angular's **.NET** button opens
`http://localhost:5000`, and the .NET site's **Angular** button opens
`http://localhost:4200`. In production, the Angular site hides the .NET button
until the .NET site is hosted.

Requirements: Node.js 22 (see `angular-cv/angular-cv/.nvmrc`) and the
.NET 10 SDK (pinned in `dotnet-cv/global.json`).

## Project structure

```
cv-website/
├── specs/                       Shared Spec Kit features (spec, plan, tasks)
├── .specify/                    Spec Kit: constitution, templates, scripts
├── .claude/skills/              /speckit-* commands for Claude Code
├── angular-cv/angular-cv/       Angular 22 app
│   ├── src/app/                 Components per section, CvService (content)
│   └── src/environments/        Stack link URLs per build
├── dotnet-cv/
│   ├── DotNetCV/                ASP.NET Core 10 MVC + read-only JSON API
│   └── DotNetCV.Tests/          Unit, integration and spec-coverage tests
└── .github/                     CI, deployment and Dependabot
```

## How it's built

The project uses **spec-driven development with
[GitHub Spec Kit](https://github.com/github/spec-kit)**. The rules are in the
[constitution](.specify/memory/constitution.md). Both apps implement the same
features, so the specs are shared:

| Feature | What it covers |
| --- | --- |
| [001 CV content](specs/001-cv-content/spec.md) | Sections, ordering, dates, education, skills, contact |
| [002 Accessibility](specs/002-accessibility/spec.md) | Skip link, landmarks, alt text, axe-core |
| [003 API and security](specs/003-api-and-security/spec.md) | JSON API, headers, error handling (.NET only) |
| [004 Notebook redesign](specs/004-notebook-redesign/spec.md) | Header and menu, hero, cards, reveal, stack switch |

Each feature folder has a `spec.md` (what and why, with `FR-###`
requirements), a `plan.md` (how, for both apps) and a `tasks.md`. A
requirement applies to both apps unless marked *(Angular only)* or
*(.NET only)*.

**New work** starts in Claude Code with `/speckit-specify <description>`, then
`/speckit-plan`, `/speckit-tasks` and `/speckit-implement`. Optional helpers:
`/speckit-clarify`, `/speckit-checklist`, `/speckit-analyze`.

**Tests are traceable:** each test's name starts with the requirement it
covers, e.g. `[001:FR-004]`. Both apps fail their build when a requirement
that applies to them has no test.

| | Angular | .NET |
| --- | --- | --- |
| Conventions | [`CLAUDE.md`](angular-cv/angular-cv/CLAUDE.md) | [`CLAUDE.md`](dotnet-cv/CLAUDE.md) |
| Tests | Vitest + jsdom + axe-core (`npm test`) | xUnit + `WebApplicationFactory` + AngleSharp (`dotnet test`) |
| Spec check | `npm run spec:check` | runs inside `dotnet test` |
| CI | spec check, tests and build on every PR | build (warnings as errors) and tests on every PR |
| Deployment | GitHub Pages on every push to `main` | not deployed yet |

## Updating the content

The CV content lives in one file per project. Keep them in sync:

- **Angular:** `angular-cv/angular-cv/src/app/services/cv.service.ts`
- **.NET:** `dotnet-cv/DotNetCV/Services/CvService.cs`

The profile photo is `src/assets/images/profile.jpg` (Angular) and
`wwwroot/images/profile.jpg` (.NET).

**This repository is public.** Never add a phone number, street address, date of
birth, residence permit or driving licence. Location stays at city level.

## Design

A notebook look inspired by [aoricreative.com](https://aoricreative.com/) and
[jlern.com](https://jlern.com/):
- **Look:** eggshell paper (`#F0EAD6`) with ink and deep-teal accents, a
  polaroid hero photo, year-labelled experience cards with hard shadows,
  index cards and a note-paper contact block.
- **Fonts:** Source Serif 4, Caveat and JetBrains Mono, all self-hosted.
- **Accessibility:** the design meets WCAG AA contrast, respects reduced
  motion, and is checked with axe-core.

Colours, fonts and motion are CSS custom properties in
`angular-cv/angular-cv/src/styles.css` and `dotnet-cv/DotNetCV/wwwroot/css/site.css`.

## Tech stack

**Angular:** Angular 22 (standalone components, signals, zoneless), TypeScript,
Vitest.

**.NET:** ASP.NET Core 10 MVC with Razor views, a read-only JSON API at
`/api/cvapi` (OpenAPI document and Swagger UI at `/api/docs` in
Development), xUnit.
