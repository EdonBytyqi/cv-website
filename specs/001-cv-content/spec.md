# Feature Specification: CV content

**Feature Branch**: `001-cv-content`

**Created**: 2026-10-20

**Status**: Implemented

**Input**: Migrated from the per-app specs `angular-cv/…/specs/001-cv-content` and `dotnet-cv/specs/001-cv-page`.

## User Scenarios & Testing *(mandatory)*

### User Story 1 - Read the CV on one page (Priority: P1)

A recruiter opens the site and reads Edon's profile, work history, education
and skills from top to bottom.

**Why this priority**: This is the reason the site exists.

**Independent Test**: Load the page and check every CV area and entry is shown in order.

**Acceptance Scenarios**:

1. **Given** the CV page, **When** it loads, **Then** the name is the only top-level heading and every CV area has its own section.
2. **Given** several roles, **When** the experience section renders, **Then** the newest role comes first and the current role reads "<Mon yyyy> - Present" in English.
3. **Given** an education entry that is still in progress, **When** it renders, **Then** it reads "since <year>".

---

### User Story 2 - Jump to a section (Priority: P2)

A reader uses the navigation to jump straight to experience, skills or contact.

**Why this priority**: Recruiters skim; navigation saves them time.

**Independent Test**: Every navigation link targets an element that exists.

**Acceptance Scenarios**:

1. **Given** the main navigation, **When** a link is chosen, **Then** the page scrolls to an existing section.

---

### User Story 3 - Get in touch (Priority: P2)

A reader wants to email or call Edon.

**Why this priority**: Contact is the goal of every visit.

**Independent Test**: The contact section exposes working links and hides missing details.

**Acceptance Scenarios**:

1. **Given** contact details, **When** the contact section renders, **Then** the email address is a `mailto:` link.
2. **Given** no phone number, **When** the contact section renders, **Then** no phone entry is shown.

### Edge Cases

- A one-word name still renders (the last-name part is empty).
- The server runs in a non-English culture: dates must stay English.
- An education entry with the same start and end year shows a single year.

## Requirements *(mandatory)*

### Functional Requirements

- **FR-001**: The page MUST have exactly one `h1`; it contains the first name, and the last name is wrapped in its own element.
- **FR-002**: The page MUST have sections with ids `about`, `experience`, `education`, `skills` and `contact`, each with an `h2`.
- **FR-003**: Every link in the main navigation MUST point to an element that exists on the page.
- **FR-004**: Experience MUST be listed newest first; the current role shows its start as "MMM yyyy" in English followed by "Present", whatever the server culture.
- **FR-005**: Each experience entry MUST have a `time` element whose `datetime` is the ISO start date. *(Angular only)*
- **FR-006**: An education entry MUST show its start year, "start - end" when the end year differs, "since <start year>" when ongoing, and its description when set.
- **FR-007**: An education entry with a grade and ECTS MUST show them as "Grade: <grade> | <n> ECTS". *(Angular only)*
- **FR-008**: Every skill MUST be shown under a heading for its category.
- **FR-009**: The email address MUST be a `mailto:` link.
- **FR-010**: When a phone number is set, the phone link MUST be `tel:` followed by the number with all whitespace removed. *(Angular only)*
- **FR-011**: When no phone number is set, the page MUST show no phone entry.
- **FR-012**: Each line of the postal address MUST be rendered on its own line. *(Angular only)*

### Key Entities

- **About me**: full name, title, bio, profile photo.
- **Experience**: job title, company, location, start/end dates, current flag, responsibilities.
- **Education**: degree, institution, location, start/end dates, ongoing flag, grade, ECTS, description.
- **Skill category**: name and list of skills.
- **Contact**: email, optional phone, city-level location, LinkedIn and GitHub URLs.

## Success Criteria *(mandatory)*

### Measurable Outcomes

- **SC-001**: Both apps render the same CV content (same roles, education and skills in the same order).
- **SC-002**: Every requirement above has a passing test in each app it applies to.

## Assumptions

- Content mirrors the latest CV, translated to English.
- The repository is public, so private details (phone, street address, date of birth, permit, licence) are not part of the content (constitution IV).
- FR-005, FR-007, FR-010 and FR-012 are Angular-only because the .NET view does not render those fields (no `time` ISO check, no grade/ECTS, phone as plain text, single-line location).
