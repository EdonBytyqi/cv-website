# Tasks: Notebook redesign

**Input**: Design documents from `/specs/004-notebook-redesign/`

**Prerequisites**: plan.md, spec.md

**Tests**: Required (constitution II). Test names start with `[004:FR-###]`.

## Phase 1: Setup

- [x] T001 [P] Design tokens and self-hosted fonts (Angular `styles.css` + `@fontsource`; .NET `site.css` + `wwwroot/fonts`)

## Phase 2: User Story 1 - A CV that feels personal (P1) 🎯 MVP

- [x] T002 [P] [US1] Tests FR-005..FR-007, FR-009 in `angular-cv/angular-cv/src/app/app.spec.ts`, `sections/hero/hero.spec.ts`
- [x] T003 [P] [US1] Tests FR-005..FR-007, FR-009, FR-010 in `dotnet-cv/DotNetCV.Tests/Integration/RedesignTests.cs`
- [x] T004 [US1] Hero (polaroid, current-role note), experience year labels, index cards, stickers, note-paper contact, footer
- [x] T005 [P] [US1] Test FR-008 and implement the reveal (`shared/reveal.ts`; .NET `site.js`)

## Phase 3: User Story 2 - Navigate on a phone (P1)

- [x] T006 [P] [US2] Tests FR-001..FR-004 in `layout/site-header/site-header.spec.ts`
- [x] T007 [P] [US2] Tests FR-001, FR-002 in `RedesignTests.cs`
- [x] T008 [US2] Sticky header with Menu button (Angular component; .NET Razor + `site.js`)

## Phase 4: User Story 3 - Switch between implementations (P2)

- [x] T009 [P] [US3] Tests FR-011, FR-012 (`site-header.spec.ts`, `services/cv.service.spec.ts`; .NET `CvContentRulesTests.cs`)
- [x] T010 [US3] Angular: `DOTNET_CV_URL` token from `environment(.development).ts`
- [x] T011 [US3] .NET: `StackLinkOptions` bound to `StackLinks:AngularUrl`, validated at startup

## Phase 5: Polish

- [x] T012 Screenshots at 1440px and 390px; no overflow at 320px; menu verified in a real browser (SC-003, SC-004)
