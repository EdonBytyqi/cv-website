# Specs

This project uses **spec-driven development**: every user-visible behaviour is
described in a spec *before* it is built, and every acceptance criterion in an
approved spec is covered by at least one test.

## Workflow

1. **Write the spec.** Copy [`_template.md`](_template.md) into a new folder
   `specs/NNN-short-name/spec.md` with `Status: Draft`. Describe the problem,
   the scope and the acceptance criteria. Open a PR with only the spec if the
   change is big enough to discuss first.
2. **Approve it.** Set `Status: Approved` once the spec is agreed. From now on
   `npm run spec:check` requires a test for every criterion.
3. **Write the tests.** Put the criterion ID at the start of the test name:
   `it('[AC-001-03] shows "Present" for the current job', ...)`. They fail.
4. **Implement** until the tests pass.
5. **Mark it done.** Set `Status: Implemented` in the same PR as the code.

Changing behaviour later means changing the spec first, in the same PR as the
code. Never delete a criterion just to make the check pass.

## Rules for acceptance criteria

- IDs are `AC-<spec number>-<two digit index>`, e.g. `AC-002-04`. Never reuse
  an ID; mark removed criteria as `~~AC-002-04~~ (removed: reason)`.
- Each criterion is observable from the outside (rendered DOM, URL, network)
  and testable. Write "the email link has `href` `mailto:<email>`", not
  "contact details are handled correctly".
- Criteria that cannot run in jsdom (colour contrast, real-device feel) go
  under **Manual checks** and are not tracked by `spec:check`.

## Tooling

| Command | What it does |
| --- | --- |
| `npm run spec:check` | Fails if an Approved/Implemented criterion has no test, or a test names an unknown ID |
| `npm test` | Runs the Vitest suite |

## Index

| Spec | Status |
| --- | --- |
| [001 CV content](001-cv-content/spec.md) | Implemented |
| [002 Accessibility](002-accessibility/spec.md) | Implemented |
| [003 Notebook redesign](003-notebook-redesign/spec.md) | Implemented |
