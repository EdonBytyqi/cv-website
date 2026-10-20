# Specs

The .NET CV follows the same **spec-driven development** workflow as the
Angular app (see `angular-cv/angular-cv/specs/README.md`): behaviour is
described in a spec before it is built, and every acceptance criterion in an
approved spec is covered by at least one test.

## Workflow

1. **Spec:** copy [`_template.md`](_template.md) to `specs/NNN-short-name/spec.md`
   with `Status: Draft`.
2. **Approve:** set `Status: Approved`. From then on the coverage check
   requires a test for every criterion.
3. **Test:** put the criterion ID at the start of the test's display name:
   `[Fact(DisplayName = "[AC-001-04] Lists experience newest first")]`.
4. **Implement** until the tests pass, then set `Status: Implemented`.

Change the spec in the same PR as the behaviour. Never delete a criterion just
to make the check pass.

## Coverage check

`SpecCoverageTests` (in `DotNetCV.Tests`) runs with `dotnet test`. It fails
when an Approved or Implemented criterion has no test, or when a test names an
ID that no spec defines.

## Index

| Spec | Status |
| --- | --- |
| [001 CV page](001-cv-page/spec.md) | Implemented |
| [002 API and security](002-api-and-security/spec.md) | Implemented |
| [003 Notebook redesign](003-notebook-redesign/spec.md) | Draft |
