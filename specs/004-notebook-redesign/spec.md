# Feature Specification: Notebook redesign

**Feature Branch**: `004-notebook-redesign`

**Created**: 2026-10-20

**Status**: Implemented

**Input**: Migrated from the per-app specs `003-notebook-redesign` (Angular and .NET) and the stack-link requirements of `dotnet-cv/specs/001-cv-page`.

## User Scenarios & Testing *(mandatory)*

### User Story 1 - A CV that feels personal (Priority: P1)

A recruiter lands on a warm, notebook-styled page instead of a generic dark
portfolio: eggshell paper, a polaroid photo, a highlighted name and a
handwritten note about the current role.

**Why this priority**: First impressions decide whether the CV is read.

**Independent Test**: The hero shows title, photo and current-role note; experience shows year labels; contact offers two clear actions.

**Acceptance Scenarios**:

1. **Given** a current role with a location, **When** the hero renders, **Then** it reads "currently at <company>, <city>".
2. **Given** a current role without a location, **When** the hero renders, **Then** it reads "currently: <company line>".
3. **Given** the experience list, **When** it renders, **Then** each entry shows its start year separately from the date range.

---

### User Story 2 - Navigate on a phone (Priority: P1)

On a narrow screen the navigation collapses behind a Menu button that works
with touch, keyboard and screen readers.

**Why this priority**: Most first visits come from a phone.

**Independent Test**: The Menu button exposes `aria-expanded`/`aria-controls`, closes on Escape (returning focus) and when a link is chosen.

**Acceptance Scenarios**:

1. **Given** the collapsed menu, **When** Menu is activated, **Then** `aria-expanded` becomes `true`.
2. **Given** the open menu, **When** Escape is pressed, **Then** it closes and focus returns to the Menu button.

---

### User Story 3 - Switch between the two implementations (Priority: P2)

A visitor (or Edon, locally) uses the Stack switch to open the other version
of the CV.

**Why this priority**: Showing the same CV in Angular and .NET is the point of the project.

**Independent Test**: The current version is marked; the other opens in a new tab and points to the right place for the environment.

**Acceptance Scenarios**:

1. **Given** local development, **When** the other version's button is chosen, **Then** it opens that app's local dev server.
2. **Given** a production build, **When** the switch renders, **Then** no link points to `localhost`.

### Edge Cases

- Without JavaScript (.NET) the navigation simply stays open.
- Without `IntersectionObserver` or with reduced motion, nothing is hidden by the scroll reveal.
- The .NET site isn't hosted yet, so the Angular production build hides its .NET button.

## Requirements *(mandatory)*

### Functional Requirements

- **FR-001**: A `header` landmark at the top of the page MUST contain the site name and the main navigation.
- **FR-002**: The header MUST have a "Menu" button with `aria-expanded="false"` and `aria-controls` pointing at the navigation list; activating it sets `aria-expanded="true"`.
- **FR-003**: When the menu is open, pressing Escape MUST close it and move focus back to the Menu button. *(Angular only)*
- **FR-004**: Choosing a navigation link MUST close the menu. *(Angular only)*
- **FR-005**: Each experience entry MUST show its start year separately from the full date range.
- **FR-006**: The hero MUST show the job title and a note about the current role: "currently at <company>, <city>", or "currently: <company line>" when the role has no location.
- **FR-007**: The contact section MUST offer two primary actions: an email link and a LinkedIn link.
- **FR-008**: Content marked for scroll reveal MUST be visible straight away when `IntersectionObserver` is unavailable or the user prefers reduced motion. *(Angular only)*
- **FR-009**: A `footer` landmark MUST contain links to LinkedIn and GitHub.
- **FR-010**: Every stylesheet, script and font the page loads MUST come from the site itself. *(.NET only)*
- **FR-011**: The stack switch MUST mark the app's own version as current (`aria-current="page"`); an entry that leads to another site opens in a new tab with `rel="noopener noreferrer"` and an accessible name that says so.
- **FR-012**: In development the other version's stack link MUST point to its local dev server (Angular → `http://localhost:5000`, .NET → `http://localhost:4200`); in production no stack link points to `localhost` (the Angular build hides the .NET entry until that site is hosted; the .NET site links to the live Angular site).

### Key Entities

- **Stack link**: label, URL, current flag, accessible description, opens-in-new-tab flag.

## Success Criteria *(mandatory)*

### Measurable Outcomes

- **SC-001**: Ink, muted ink and teal on paper and card colours meet WCAG AA contrast. *Manual check.*
- **SC-002**: Hover effects only apply with a fine pointer; press feedback stays under 200ms. *Manual check.*
- **SC-003**: The page works at 320px width without horizontal scroll. *Manual check.*
- **SC-004**: On .NET the Menu button closes on Escape and on link choice in a real browser. *Manual check (vanilla JS, not covered by jsdom).*

## Assumptions

- Design direction: notebook look inspired by aoricreative.com (stationery, handwritten accents, polaroids) and jlern.com (year labels, hard ink shadows) on an eggshell `#F0EAD6` / deep teal `#1F5F5B` palette.
- FR-003, FR-004 and FR-008 run in Angular's component tests; the .NET equivalents live in `site.js` and are covered by SC-004.
- FR-010 is .NET-only because the Angular build bundles its assets; Angular also self-hosts its fonts.
