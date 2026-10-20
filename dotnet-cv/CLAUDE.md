# .NET CV — agent guide

ASP.NET Core 10 MVC + JSON API, tested with xUnit, `WebApplicationFactory`
and AngleSharp.

## Spec-driven workflow (required)

This repo uses GitHub Spec Kit. Follow the constitution
(`/.specify/memory/constitution.md`) and the shared feature specs in the
repository's root `specs/` folder (both apps implement the same features).

1. New or changed behaviour starts with `/speckit-specify`, then
   `/speckit-plan`, `/speckit-tasks` and `/speckit-implement`.
2. Tests name the requirement in their display name:
   `[Fact(DisplayName = "[001:FR-004] ...")]` (feature number and requirement).
3. Requirements apply to both apps unless marked *(Angular only)* or
   *(.NET only)*; `SpecCoverageTests` enforces the ones that apply here.
4. `dotnet build` (no warnings) and `dotnet test` must pass before a PR.

Do not edit or delete requirements just to make a check pass.

## Code conventions

Follow the .NET Framework Design Guidelines:
- PascalCase for types and members, camelCase for parameters; `Id`, `Email`.
- Immutable models: `required`/`init` properties and `IReadOnlyList<T>`.
- Primary constructors for dependency injection; validate public arguments.
- No try/catch that only logs and rethrows; let the exception handler deal
  with failures, and never return exception messages to clients.
- Views stay simple: ordering and formatting rules live in the service or
  model (e.g. `AboutMe.FirstName`); dates use `CultureInfo.InvariantCulture`.

## Content

The repository is public. Never add a phone number, street address, date of
birth, residence permit or driving licence. Location stays at city level.
