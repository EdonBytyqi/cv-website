# Tasks: API and security

**Input**: Design documents from `/specs/003-api-and-security/`

**Prerequisites**: plan.md, spec.md

**Tests**: Required (constitution II). Test names start with `[003:FR-###]`.

## Phase 1: User Story 2 - Safe by default (P1) 🎯 MVP

- [x] T001 [P] [US2] Tests FR-003..FR-005 in `dotnet-cv/DotNetCV.Tests/Integration/ApiAndSecurityTests.cs`
- [x] T002 [US2] Replace the deprecated `X-XSS-Protection` header with CSP `frame-ancestors 'none'`
- [x] T003 [US2] Remove per-action try/catch that returned `ex.Message`
- [x] T004 [US2] Fix found by FR-005: the error page's layout no longer calls the CV service

## Phase 2: User Story 1 - Read the CV as JSON (P2)

- [x] T005 [P] [US1] Tests FR-001, FR-002 in `ApiAndSecurityTests.cs`
- [x] T006 [US1] Typed `ActionResult<T>` endpoints; CORS limited to `GET`
- [x] T007 [US1] Built-in OpenAPI document with Swagger UI at `/api/docs` in Development
