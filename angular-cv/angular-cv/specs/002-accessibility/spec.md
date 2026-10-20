# 002 — Accessibility

Status: Implemented

## Problem

The site must work for keyboard and screen reader users and meet WCAG 2.2 AA,
as required by the project guidelines (see `CLAUDE.md`).

## Scope

In scope:
- Landmarks, headings, links, images and focus handling on the CV page.

Out of scope:
- Content translations.

## Acceptance criteria

- **AC-002-01** The first focusable element is a "Skip to content" link whose
  target is the `main` element.
- **AC-002-02** The main navigation is a `nav` element with an accessible name.
- **AC-002-03** Every `img` has an `alt` attribute; the hero photo's alt text
  contains the full name.
- **AC-002-04** Links that open in a new tab have `rel` containing `noopener`
  and an accessible name.
- **AC-002-05** Heading levels never skip a level going down the page
  (e.g. no `h2` followed directly by an `h4`).
- **AC-002-06** axe-core reports no violations for the rendered page (colour
  contrast is excluded because jsdom cannot compute it; see manual checks).

## Manual checks

- Text and accent colours meet WCAG AA contrast (4.5:1 for body text, 3:1 for
  large text). Verify with the browser's contrast checker or Lighthouse.
- Every interactive element shows a visible focus ring.
- With `prefers-reduced-motion: reduce`, nothing moves; only opacity/colour
  may change.
