# Feature Specification: Accessibility

**Feature Branch**: `002-accessibility`

**Created**: 2026-10-20

**Status**: Implemented

**Input**: Migrated from `angular-cv/…/specs/002-accessibility` and the accessibility parts of `dotnet-cv/specs/003-notebook-redesign`.

## User Scenarios & Testing *(mandatory)*

### User Story 1 - Keyboard user skips to the content (Priority: P1)

A keyboard user presses Tab once and can jump past the header to the CV.

**Why this priority**: Without it, every visit starts with tabbing through the whole navigation.

**Independent Test**: The first focusable element is a skip link that targets `main`.

**Acceptance Scenarios**:

1. **Given** a fresh page load, **When** the user presses Tab, **Then** "Skip to content" is focused, and activating it moves to the `main` element.

---

### User Story 2 - Screen reader user understands the page (Priority: P1)

A screen reader user navigates by landmarks and headings and hears meaningful
names for images and links.

**Why this priority**: WCAG 2.2 AA is a project requirement (constitution V).

**Independent Test**: Landmarks, headings, alt text and link names are present and correct; axe reports no violations.

**Acceptance Scenarios**:

1. **Given** the page, **When** a screen reader lists landmarks, **Then** the main navigation has a name.
2. **Given** the hero photo, **When** it is announced, **Then** its alt text contains the full name.
3. **Given** a link that opens a new tab, **When** it is announced, **Then** its name says so.

### Edge Cases

- Decorative images (tape, dots) are hidden from assistive technology.
- jsdom cannot compute colour contrast, so contrast is a manual check.

## Requirements *(mandatory)*

### Functional Requirements

- **FR-001**: The first focusable element MUST be a "Skip to content" link whose target is the `main` element.
- **FR-002**: The main navigation MUST be a `nav` element with an accessible name. *(Angular only)*
- **FR-003**: Every `img` MUST have an `alt` attribute, and the hero photo's alt text MUST contain the full name.
- **FR-004**: Links that open in a new tab MUST have `rel` containing `noopener` and an accessible name. *(Angular only)*
- **FR-005**: Heading levels MUST never skip a level going down the page. *(Angular only)*
- **FR-006**: axe-core MUST report no violations for the rendered page (colour contrast excluded; see manual checks). *(Angular only)*

## Success Criteria *(mandatory)*

### Measurable Outcomes

- **SC-001**: Zero axe-core violations on the CV page.
- **SC-002**: Text and accent colours meet WCAG AA contrast (4.5:1 body text, 3:1 large text). *Manual check.*
- **SC-003**: Every interactive element shows a visible focus ring. *Manual check.*

## Assumptions

- FR-002, FR-004, FR-005 and FR-006 are currently enforced by automated tests in the Angular app only; the .NET markup follows the same structure, and adding the same checks there is a candidate follow-up.
