# Tasks: Accessibility

**Input**: Design documents from `/specs/002-accessibility/`

**Prerequisites**: plan.md, spec.md

**Tests**: Required (constitution II). Test names start with `[002:FR-###]`.

## Phase 1: User Story 1 - Skip to the content (P1) 🎯 MVP

- [x] T001 [P] [US1] Test FR-001 in `angular-cv/angular-cv/src/app/app.spec.ts`
- [x] T002 [P] [US1] Test FR-001 in `dotnet-cv/DotNetCV.Tests/Integration/RedesignTests.cs`
- [x] T003 [US1] Skip link and `main#main-content` in both layouts

## Phase 2: User Story 2 - Understand the page (P1)

- [x] T004 [P] [US2] Tests FR-002..FR-006 in `app.spec.ts` (includes the axe-core scan)
- [x] T005 [P] [US2] Test FR-003 in `RedesignTests.cs`
- [x] T006 [US2] Fix found by the axe scan: experience timeline as a real `<ol>/<li>` list
- [x] T007 [US2] Hero photo as `img` with alt text; named new-tab links

## Phase 3: Follow-up

- [ ] T008 [P] Add .NET tests for FR-002, FR-004 and FR-005, then drop their *(Angular only)* markers
