# 003 — Notebook redesign

Status: Implemented

## Problem

The .NET site still has the old dark sidebar design, while the Angular
version has the notebook redesign. Both versions of the CV should look and
behave the same.

## Direction

Same as the Angular spec `angular-cv/angular-cv/specs/003-notebook-redesign`:
eggshell paper (`#F0EAD6`), ink `#2A2119`, deep teal `#1F5F5B`, marker
`#F2D16B`. Source Serif 4, Caveat and JetBrains Mono are served from
`wwwroot/fonts` (OFL-licensed, no third-party requests). The layout uses a
sticky header with a mobile Menu button, a polaroid hero photo, year-labelled
experience cards with hard ink shadows, index cards for education, sticker
skill tags, a note-paper contact block and a footer.

## Scope

In scope: the CV page, the layout and the error page styling.

Out of scope: content (spec 001), the JSON API (spec 002).

## Acceptance criteria

- **AC-003-01** The first focusable element is a "Skip to content" link whose
  target is the `main` element.
- **AC-003-02** A `header` landmark contains the site name and the main
  navigation.
- **AC-003-03** The header has a "Menu" button rendered with
  `aria-expanded="false"` and `aria-controls` pointing at the navigation
  list.
- **AC-003-04** The hero shows the job title and a note about the current
  role: "currently at <company>, <city>", or "currently: <company line>"
  when the role has no location.
- **AC-003-05** The hero photo is an `img` whose `alt` contains the full name.
- **AC-003-06** Each experience entry shows its start year separately from
  the full date range.
- **AC-003-07** The contact section offers two primary actions: an email link
  and a LinkedIn link.
- **AC-003-08** A `footer` landmark contains links to LinkedIn and GitHub.
- **AC-003-09** Every stylesheet, script and font the page loads comes from
  the site itself (no third-party hosts).

## Manual checks

- The Menu button toggles the navigation, closes it on Escape (focus returns
  to the button) and when a link is chosen; it's hidden above 860px.
- Cards reveal once on scroll; with reduced motion nothing is hidden.
- Hover effects only apply with a fine pointer; buttons scale to 0.97 when
  pressed.
- Colours meet WCAG AA; the page works at 320px without horizontal scroll.
