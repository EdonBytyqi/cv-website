# 001 — CV page

Status: Implemented

## Problem

The server-rendered CV page must show the same content as the Angular version
and let readers jump to any section.

## Scope

In scope: the page rendered by `HomeController.Index`, including the layout.

Out of scope: visual design (see later specs), the JSON API (spec 002).

## Acceptance criteria

- **AC-001-01** The page has exactly one `h1`. It contains the first name, and
  the last name is wrapped in its own element.
- **AC-001-02** There are sections with ids `about`, `experience`, `education`,
  `skills` and `contact`, each with an `h2`.
- **AC-001-03** Every link in the main navigation points to an element that
  exists on the page.
- **AC-001-04** Experience is listed newest first. The current job shows its
  start as "MMM yyyy" in English followed by "Present", whatever the server's
  culture.
- **AC-001-05** An education entry shows its start year, and "start - end"
  only when the end year differs.
- **AC-001-06** Every skill is shown under a heading for its category.
- **AC-001-07** The email address is a `mailto:` link.
- **AC-001-08** When no phone number is set, the page shows no phone entry.
- **AC-001-09** An education entry marked as ongoing shows "since <start
  year>", and an entry's description is shown when set.
- **AC-001-10** The .NET entry in the stack switch is marked current
  (`aria-current="page"`). An entry that leads to another site opens in a new
  tab with `rel="noopener noreferrer"`, and its accessible name says so.
- **AC-001-11** In the Development environment, the Angular entry links to the
  local Angular dev server (`http://localhost:4200`). In other environments
  it links to the live Angular site and no stack link points to `localhost`.
  The URL comes from configuration (`StackLinks:AngularUrl`).

## Content rules

- Content mirrors the latest CV, translated to English, and matches the
  Angular version.
- The repository is public. Never put a phone number, street address, date
  of birth, residence permit or driving licence in source, tests or specs.
  Location stays at city level.

## Manual checks

- The page looks right at desktop and phone widths.
