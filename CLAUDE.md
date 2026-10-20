# CV website — agent guide

Two implementations of the same CV: `angular-cv/angular-cv` (Angular 22) and
`dotnet-cv` (ASP.NET Core 10). Read the app's own `CLAUDE.md` before changing it.

## Spec-driven development (GitHub Spec Kit)

- Rules: `.specify/memory/constitution.md`. They override everything else.
- Features: `specs/NNN-name/` with `spec.md`, `plan.md` and `tasks.md`, shared
  by both apps. A requirement applies to both apps unless marked
  *(Angular only)* or *(.NET only)*.
- New work: `/speckit-specify` → (`/speckit-clarify`) → `/speckit-plan` →
  `/speckit-tasks` → `/speckit-implement`.
- Tests start with the requirement key, e.g. `[001:FR-004]`. Coverage checks:
  `npm run spec:check` (Angular) and `SpecCoverageTests` in `dotnet test` (.NET).

## Never

- Add a phone number, street address, date of birth, residence permit or
  driving licence anywhere: this repository is public.
- Branch from a stale local `main`; branch from `origin/main`.
