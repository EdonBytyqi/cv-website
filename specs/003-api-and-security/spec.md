# Feature Specification: API and security

**Feature Branch**: `003-api-and-security`

**Created**: 2026-10-20

**Status**: Implemented

**Input**: Migrated from `dotnet-cv/specs/002-api-and-security`. Applies to the .NET app only: the Angular app is static and has no server.

## User Scenarios & Testing *(mandatory)*

### User Story 1 - Read the CV as JSON (Priority: P2)

A developer or tool fetches the CV data as JSON.

**Why this priority**: Nice to have; the page is the main product.

**Independent Test**: `GET /api/cvapi` returns every CV section; writes are rejected.

**Acceptance Scenarios**:

1. **Given** the running app, **When** `GET /api/cvapi` is requested, **Then** the JSON contains every CV section.
2. **Given** the read-only API, **When** `POST /api/cvapi` is sent, **Then** the response is 405.

---

### User Story 2 - Safe by default (Priority: P1)

Visitors are protected from clickjacking and content sniffing, and failures
never reveal internals.

**Why this priority**: Security issues on a public site are worse than missing features.

**Independent Test**: Headers are present on pages; a failing service yields a generic 500.

**Acceptance Scenarios**:

1. **Given** any page, **When** it is requested, **Then** the security headers are sent.
2. **Given** the CV service throws, **When** a page or the API is requested, **Then** the response is 500 and does not contain the exception message.

### Edge Cases

- The error page must render even when the CV service is the thing that failed.

## Requirements *(mandatory)*

### Functional Requirements

- **FR-001**: `GET /api/cvapi` MUST return JSON with `aboutMe`, `experiences`, `education`, `skillCategories`, `contact` and `sidebar`. *(.NET only)*
- **FR-002**: The API MUST be read-only: `POST /api/cvapi` returns 405. *(.NET only)*
- **FR-003**: Every page response MUST send `Content-Security-Policy` with `frame-ancestors 'none'`, `X-Content-Type-Options: nosniff` and a `Referrer-Policy`. *(.NET only)*
- **FR-004**: `GET /health` MUST return 200. *(.NET only)*
- **FR-005**: When the CV service fails, the response MUST be a 500 that does not contain the exception message. *(.NET only)*

## Success Criteria *(mandatory)*

### Measurable Outcomes

- **SC-001**: No response ever contains an exception message.
- **SC-002**: `/api/docs` (Swagger UI) works in Development and is absent in Production. *Manual check.*

## Assumptions

- Everything in the API is public; no authentication is needed.
