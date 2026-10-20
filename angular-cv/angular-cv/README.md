# Angular CV

Edon Bytyqi's CV as a single-page Angular 22 app.

## Getting started

```bash
npm ci
npm start          # http://localhost:4200
```

## How this project is developed

Behaviour is defined in specs first, then tested, then built. See
[`specs/README.md`](specs/README.md) for the workflow and
[`CLAUDE.md`](CLAUDE.md) for code conventions.

## Scripts

| Command | Purpose |
| --- | --- |
| `npm start` | Dev server with live reload |
| `npm test` | Unit tests with Vitest (jsdom) |
| `npm run spec:check` | Checks every tracked acceptance criterion has a test |
| `npm run build` | Production build into `dist/angular-cv/browser` |
