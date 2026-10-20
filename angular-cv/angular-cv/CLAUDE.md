# Angular CV — agent guide

Single-page CV built with Angular 22 (standalone, zoneless, signals) and tested
with Vitest.

## Spec-driven workflow (required)

This repo uses GitHub Spec Kit. Follow the constitution
(`/.specify/memory/constitution.md`) and the shared feature specs in the
repository's root `specs/` folder (both apps implement the same features).

1. New or changed behaviour starts with `/speckit-specify`, then
   `/speckit-plan`, `/speckit-tasks` and `/speckit-implement`.
2. Tests name the requirement they cover: `it('[001:FR-004] ...')`
   (feature number and requirement).
3. Requirements apply to both apps unless marked *(Angular only)* or
   *(.NET only)*; `npm run spec:check` enforces the ones that apply here.
4. `npm run spec:check && npm test && npm run build` must pass before a PR.

Do not edit or delete requirements just to make a check pass.

## Commands

| Command | Purpose |
| --- | --- |
| `npm start` | Dev server on http://localhost:4200 |
| `npm test` | Vitest unit tests (jsdom, no browser needed) |
| `npm run spec:check` | Every Angular requirement in `specs/` has a test |
| `npm run build` | Production build into `dist/angular-cv/browser` |

## Code conventions

- Standalone components; never set `standalone: true`.
- OnPush is the default in v22; do not set `changeDetection`.
- Signals for state, `computed()` for derived values, `input()`/`output()`
  instead of decorators, `inject()` instead of constructor injection.
- `@Service()` for singleton services.
- Native control flow (`@if`, `@for`); `class`/`style` bindings instead of
  `ngClass`/`ngStyle`; import only the pipes and directives a template uses.
- No logic in templates beyond simple expressions; move it into `computed()`.
- `NgOptimizedImage` for static images.
- Logic in `.ts`, markup in `.html`, styles in `.css`.
- Colours, fonts and motion come from the CSS custom properties in
  `src/styles.css`; do not hard-code new values in component CSS.

## Accessibility

Must pass axe and WCAG 2.2 AA (see `specs/002-accessibility`). Animate only `transform` and
`opacity`, and respect `prefers-reduced-motion`.
