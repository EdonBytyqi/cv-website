# CV Website Constitution

## Core Principles

### I. Spec First (NON-NEGOTIABLE)

Every user-visible behaviour is defined in a feature spec under `specs/` before
it is built, following the Spec Kit flow: `/speckit-specify` → (`/speckit-clarify`)
→ `/speckit-plan` → `/speckit-tasks` → `/speckit-implement`. Changing behaviour
means changing the spec first, in the same pull request as the code. A
requirement is never deleted or weakened just to make a check pass.

### II. Traceable Tests

Every functional requirement in a spec with status **Approved** or
**Implemented** has at least one test per app it applies to. The test name
starts with the requirement's key, e.g. `[001:FR-004]` (feature number and
requirement). Tests are written first and must fail before the code exists.
Coverage gates enforce this in both apps: `npm run spec:check` (Angular) and
`SpecCoverageTests` inside `dotnet test` (.NET).

### III. One CV, Two Implementations

The Angular app (`angular-cv/angular-cv`) and the ASP.NET Core app
(`dotnet-cv`) present the same CV with the same design. Specs are shared:
a requirement applies to both apps unless it is marked *(Angular only)* or
*(.NET only)*, and such markers must be justified by a real platform
difference. CV content is kept identical in both apps.

### IV. Privacy by Default

This repository is public. Source, tests, specs and commit history never
contain a phone number, street address, date of birth, residence permit or
driving licence. Location stays at city level. Tests use made-up contact data.

### V. Accessible, Fast and Calm UI

Pages meet WCAG 2.2 AA (contrast, focus, landmarks, alt text) and pass axe
checks. Motion animates only `transform`, `opacity` or `clip-path`, respects
`prefers-reduced-motion`, and never hides content without a way to reveal it.
Fonts and assets are self-hosted; no third-party requests.

### VI. Simple, Idiomatic Code

Each app follows its framework's current guidance: Angular 22 (standalone,
signals, zoneless, `@Service`, native control flow) and the .NET Framework
Design Guidelines (immutable models, primary constructors, no swallow-and-log
try/catch). Prefer the simplest solution; remove unused code with approval.
Project-specific rules live in each app's `CLAUDE.md`.

## Technology Constraints

- **Angular**: Angular 22, TypeScript (strict), Vitest + jsdom + axe-core,
  deployed to GitHub Pages from `main`.
- **.NET**: ASP.NET Core 10 MVC, xUnit + `WebApplicationFactory` + AngleSharp,
  builds with warnings as errors.
- Locally the apps link to each other through the stack switch
  (`http://localhost:4200` ↔ `http://localhost:5000`); production builds
  never link to `localhost`.

## Development Workflow

- One feature per branch and pull request; large changes are split into
  stacked PRs with small commits.
- CI must pass before merging: spec coverage, tests and production build for
  each app touched.
- Reviews check the change against this constitution and the feature's spec.

## Governance

This constitution supersedes other practices. Amendments are made through a
pull request that updates this file, bumps the version (semantic versioning:
MAJOR for removed or redefined principles, MINOR for new principles or
sections, PATCH for wording) and updates any affected templates or specs.

**Version**: 1.0.0 | **Ratified**: 2026-10-20 | **Last Amended**: 2026-10-20
