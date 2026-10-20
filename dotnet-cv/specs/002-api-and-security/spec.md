# 002 — API and security

Status: Implemented

## Problem

The CV is also available as JSON, and the site must not leak internals or be
embeddable by other sites.

## Scope

In scope: `/api/cvapi/*`, `/health`, response headers, error handling.

Out of scope: authentication (everything here is public).

## Acceptance criteria

- **AC-002-01** `GET /api/cvapi` returns JSON with `aboutMe`, `experiences`,
  `education`, `skillCategories`, `contact` and `sidebar`.
- **AC-002-02** The API is read-only: `POST /api/cvapi` returns 405.
- **AC-002-03** Every page response sends `Content-Security-Policy` with
  `frame-ancestors 'none'`, `X-Content-Type-Options: nosniff` and a
  `Referrer-Policy`.
- **AC-002-04** `GET /health` returns 200.
- **AC-002-05** When the CV service fails, the response is a 500 that does not
  contain the exception message.

## Manual checks

- `/api/docs` (Swagger UI) works in Development and is absent in Production.
