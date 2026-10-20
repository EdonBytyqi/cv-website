# .NET CV — agent guide

ASP.NET Core 10 MVC + JSON API, tested with xUnit, `WebApplicationFactory`
and AngleSharp.

## Spec-driven workflow (required)

Read [`specs/README.md`](specs/README.md) before changing behaviour.

1. New or changed behaviour starts as a spec in `specs/NNN-name/spec.md`.
2. Tests name the criterion in their display name:
   `[Fact(DisplayName = "[AC-001-04] ...")]`.
3. Code is written to make those tests pass.
4. `dotnet build` (no warnings) and `dotnet test` must pass before a PR;
   `SpecCoverageTests` enforces step 2.

Do not edit or delete acceptance criteria just to make a check pass.

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
