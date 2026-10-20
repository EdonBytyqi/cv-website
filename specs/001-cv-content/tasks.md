# Tasks: CV content

**Input**: Design documents from `/specs/001-cv-content/`

**Prerequisites**: plan.md, spec.md

**Tests**: Required (constitution II). Test names start with `[001:FR-###]`.

## Phase 1: Setup

- [x] T001 Content models in `angular-cv/angular-cv/src/app/models/` and `dotnet-cv/DotNetCV/Models/`
- [x] T002 Static content in both `CvService` files, translated from the latest CV (no private details)

## Phase 2: User Story 1 - Read the CV on one page (P1) 🎯 MVP

- [x] T003 [P] [US1] Tests for FR-001, FR-002, FR-004, FR-005, FR-006, FR-007, FR-008 in `angular-cv/angular-cv/src/app/app.spec.ts` and `sections/education/education.spec.ts`
- [x] T004 [P] [US1] Tests for FR-001, FR-002, FR-004, FR-006, FR-008 in `dotnet-cv/DotNetCV.Tests/Integration/CvPageTests.cs` and `CvContentRulesTests.cs`
- [x] T005 [US1] Section components (Angular) and `Index.cshtml` (.NET); `AboutMe.FirstName/LastName`, `Education.YearRange`

## Phase 3: User Story 2 - Jump to a section (P2)

- [x] T006 [P] [US2] Tests for FR-003 in both apps
- [x] T007 [US2] Navigation rendered from the sidebar/site model

## Phase 4: User Story 3 - Get in touch (P2)

- [x] T008 [P] [US3] Tests for FR-009..FR-012 (Angular `sections/contact/contact.spec.ts`, `app.spec.ts`; .NET `CvContentRulesTests.cs`)
- [x] T009 [US3] Contact section: `mailto:` link, optional phone, address lines

## Phase 5: Polish

- [x] T010 Dates in English regardless of culture (.NET `CultureInfo.InvariantCulture`)
- [x] T011 Remove the placeholder phone number from both content files
