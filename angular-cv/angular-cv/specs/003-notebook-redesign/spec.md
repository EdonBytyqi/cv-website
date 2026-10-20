# 003 — Notebook redesign

Status: Draft

## Problem

The page is a generic dark "developer portfolio" with a fixed sidebar. It
doesn't feel personal, and on phones the sidebar pushes the content far down
the page.

## Direction

Inspired by two references:

- **aoricreative.com:** a stationery/notebook feel. Warm paper colours, a serif
  reading font, a handwritten accent font, a monospace font for labels,
  polaroid-framed photos, a note-paper call to action offering "two ways in",
  and a slow-out ease (`cubic-bezier(.16, 1, .3, 1)`).
- **jlern.com:** a playful, illustrated portfolio. Work is grouped under big
  year labels, and cards have a hard offset ink shadow instead of a soft blur.

Combined with the existing eggshell (`#F0EAD6`) and deep teal palette:

| Token | Value | Use |
| --- | --- | --- |
| Paper | `#F0EAD6` | Page background, with a faint dot grid |
| Card | `#FBF7EC` | Cards, polaroid frame |
| Ink | `#2A2119` | Text, borders, hard shadows |
| Muted ink | `#5C574C` | Secondary text |
| Teal | `#1F5F5B` | Links, accents, buttons |
| Marker | `#F2D16B` | Highlighter stroke behind key words (decorative) |

Fonts are self-hosted from npm, with no third-party requests:
- Source Serif 4 (body and headings)
- Caveat (handwritten notes)
- JetBrains Mono (dates and labels)

## Scope

In scope:
- Replace the fixed sidebar with a sticky top header. On narrow screens it
  collapses into a Menu button.
- Hero: big serif greeting, a highlighted last name, a polaroid photo and a
  handwritten "currently at…" note.
- Experience as a year-labelled timeline with hard-shadow cards.
- Education as pinned index cards; skills as labelled stickers.
- Contact as a note-paper block with two primary actions.
- A one-time scroll reveal on cards.
- Split the single `App` template into one component per section.

Out of scope:
- Content changes in `CvService`.
- Dark mode.
- The .NET site.

## Acceptance criteria

- **AC-003-01** A `header` landmark at the top of the page contains the site
  name and the main navigation.
- **AC-003-02** The header has a "Menu" button with `aria-expanded="false"`
  and `aria-controls` pointing at the navigation list. Activating it sets
  `aria-expanded="true"`.
- **AC-003-03** When the menu is open, pressing Escape closes it and moves
  focus back to the Menu button.
- **AC-003-04** Choosing a navigation link closes the menu.
- **AC-003-05** Each experience entry is labelled with its start year
  (e.g. "2024"), shown separately from the full date range.
- **AC-003-06** The hero shows the job title and a note naming the current
  employer and its city, e.g. "currently at BSI SA, Lugano".
- **AC-003-07** The contact section offers two primary actions: an email link
  and a LinkedIn link.
- **AC-003-08** Content marked for scroll reveal is visible straight away when
  `IntersectionObserver` is unavailable or the user prefers reduced motion.
- **AC-003-09** A `footer` landmark contains links to LinkedIn and GitHub.

## Manual checks

- Ink, muted ink and teal on paper and card colours meet WCAG AA contrast.
- Hover effects only apply on devices with a fine pointer
  (`@media (hover: hover) and (pointer: fine)`).
- Buttons scale to 0.97 when pressed. Hover and press feedback stays under
  200ms.
- Scroll reveal runs once per element (600ms clip-path, 50ms stagger). With
  reduced motion, nothing is hidden or revealed.
- The header and menu work at 320px width without horizontal scroll.
