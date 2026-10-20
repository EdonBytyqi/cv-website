# 001 — CV content

Status: Implemented

## Problem

Recruiters need to read Edon's profile, work history, education, skills and
contact details on one page and jump straight to any section.

## Scope

In scope:
- Rendering the data from `CvService` into a single page.
- In-page navigation between sections.

Out of scope:
- Editing content in the browser; content lives in `CvService`.
- The .NET version of the site.

## Acceptance criteria

- **AC-001-01** The page has exactly one `h1`. It contains the first name, and
  the last name is wrapped in its own element so it can be styled separately.
- **AC-001-02** There are sections with ids `about`, `experience`, `education`,
  `skills` and `contact`, each with an `h2`.
- **AC-001-03** Every link in the main navigation points to an element that
  exists on the page.
- **AC-001-04** Experience entries are listed newest first. The current job
  shows its start month and year followed by "Present".
- **AC-001-05** Each experience entry has a `time` element whose `datetime`
  is the ISO start date.
- **AC-001-06** An education entry with both a grade and ECTS shows them as
  "Grade: 7.69/10 | 180 ECTS".
- **AC-001-07** Skills are grouped under one heading per category, and every
  skill from `CvService` is shown.
- **AC-001-08** The email link has `href` `mailto:<email>`.
- **AC-001-09** The phone link has `href` `tel:` followed by the number with
  all whitespace removed.
- **AC-001-10** Each line of the postal address is rendered on its own line.
- **AC-001-11** When no phone number is set in `CvService`, the page shows no
  phone entry and no `tel:` link. AC-001-09 applies only when a number is set.

## Manual checks

- Dates read naturally in the browser locale ("May 2024").
