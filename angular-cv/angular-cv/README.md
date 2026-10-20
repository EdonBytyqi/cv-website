# Angular CV

Edon Bytyqi's CV as a single-page Angular 22 app.

## Getting started

```bash
npm ci
npm start          # http://localhost:4200
```

## How this project is developed

Behaviour is defined in specs first, then tested, then built, using GitHub
Spec Kit. The shared feature specs live in the repository's root
[`specs/`](../../specs) folder, the rules in the
[constitution](../../.specify/memory/constitution.md), and code conventions in
[`CLAUDE.md`](CLAUDE.md).

## Scripts

| Command | Purpose |
| --- | --- |
| `npm start` | Dev server with live reload |
| `npm test` | Unit tests with Vitest (jsdom) |
| `npm run spec:check` | Checks every Angular requirement in the root `specs/` has a test |
| `npm run build` | Production build into `dist/angular-cv/browser` |

## Running both versions locally

The stack switch links the two versions when both run on your machine:

```bash
npm start                                        # Angular on http://localhost:4200
dotnet run --project ../../dotnet-cv/DotNetCV     # .NET on http://localhost:5000
```

Development builds link to `http://localhost:5000` (see
`src/environments/environment.development.ts`). Production builds hide the
.NET button until that site is hosted.
