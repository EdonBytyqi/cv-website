import {
  Component,
  ChangeDetectionStrategy,
  DestroyRef,
  ElementRef,
  afterNextRender,
  inject,
  signal,
  viewChild
} from '@angular/core';
import { DatePipe } from '@angular/common';
import { CvService } from './services/cv.service';

/** Distance from the viewport top (px) a section must pass to count as current; clears the sticky nav. */
const ACTIVE_SECTION_OFFSET = 120;

/** Hover delay before the first tooltip in a group opens. Later ones open instantly. */
const TOOLTIP_OPEN_DELAY = 500;

/** How long the tooltip group stays "warm" after the pointer leaves a trigger. */
const TOOLTIP_WARM_WINDOW = 300;

const INTRO_SEEN_KEY = 'cv-hello-seen';

/** The welcome normally ends at 3.3s (2.7s delay + 0.6s fade); this is the hard ceiling. */
const INTRO_MAX_DURATION = 4000;

@Component({
  selector: 'app-root',
  imports: [DatePipe],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css',
  changeDetection: ChangeDetectionStrategy.OnPush,
  host: {
    '(document:keydown)': 'skipIntro()'
  }
})
export class AppComponent {
  private cvService = inject(CvService);
  private destroyRef = inject(DestroyRef);
  private host = inject<ElementRef<HTMLElement>>(ElementRef);
  private navList = viewChild<ElementRef<HTMLElement>>('navList');
  private copyResetTimer?: ReturnType<typeof setTimeout>;
  private tooltipOpenTimer?: ReturnType<typeof setTimeout>;
  private tooltipWarmTimer?: ReturnType<typeof setTimeout>;
  private tooltipWarm = false;
  private introFallbackTimer?: ReturnType<typeof setTimeout>;

  cvData = this.cvService.cvData;
  /** Starts on the hero, which has no nav item, so nothing is highlighted until you scroll. */
  activeSection = signal('top');
  emailCopied = signal(false);
  introActive = signal(shouldPlayIntro());
  /** Whether the page entrance may play; held back while the welcome is fully covering it. */
  contentReady = signal(!this.introActive());
  openTooltip = signal<string | null>(null);
  tooltipInstant = signal(false);
  readonly year = new Date().getFullYear();

  constructor() {
    afterNextRender(() => {
      this.observeSections();
      // Safety net: never leave the page hidden if the overlay's animation events don't fire.
      if (this.introActive()) this.introFallbackTimer = setTimeout(() => this.skipIntro(), INTRO_MAX_DURATION);
    });
    this.destroyRef.onDestroy(() => {
      clearTimeout(this.introFallbackTimer);
      clearTimeout(this.copyResetTimer);
      clearTimeout(this.tooltipOpenTimer);
      clearTimeout(this.tooltipWarmTimer);
    });
  }

  // ---------- Welcome "hello" ----------
  // Child animations (the stroke) bubble to the overlay too, so only react to the overlay's own.

  /** The overlay started fading: let the page entrance play underneath it. */
  onIntroAnimationStart(event: AnimationEvent): void {
    if (event.target === event.currentTarget) this.contentReady.set(true);
  }

  /** The overlay finished fading: remove it. */
  onIntroAnimationEnd(event: AnimationEvent): void {
    if (event.target === event.currentTarget) this.finishIntro();
  }

  skipIntro(): void {
    if (this.introActive()) this.finishIntro();
  }

  private finishIntro(): void {
    clearTimeout(this.introFallbackTimer);
    this.contentReady.set(true);
    this.introActive.set(false);
    try {
      sessionStorage.setItem(INTRO_SEEN_KEY, '1');
    } catch {
      // Storage blocked; the intro will simply play again next visit.
    }
  }

  // ---------- Copy email ----------

  async copyEmail(): Promise<void> {
    try {
      await navigator.clipboard.writeText(this.cvData().contact.email);
    } catch {
      return; // Clipboard blocked (insecure context or denied); the mailto link still works.
    }
    this.emailCopied.set(true);
    clearTimeout(this.copyResetTimer);
    this.copyResetTimer = setTimeout(() => this.emailCopied.set(false), 2000);
  }

  // ---------- Tooltips ----------

  /**
   * The first tooltip waits before opening to avoid accidental triggers. Once one has been
   * shown, moving to a neighbour opens it immediately and without animation.
   */
  showTooltip(id: string, event: PointerEvent | FocusEvent): void {
    if (event instanceof PointerEvent && event.pointerType !== 'mouse') return;
    if (event instanceof FocusEvent && !(event.target as HTMLElement).matches(':focus-visible')) return;

    clearTimeout(this.tooltipOpenTimer);
    clearTimeout(this.tooltipWarmTimer);

    if (this.openTooltip() !== null || this.tooltipWarm) {
      this.tooltipInstant.set(true);
      this.openTooltip.set(id);
    } else {
      this.tooltipInstant.set(false);
      this.tooltipOpenTimer = setTimeout(() => this.openTooltip.set(id), TOOLTIP_OPEN_DELAY);
    }
  }

  hideTooltip(): void {
    clearTimeout(this.tooltipOpenTimer);
    if (this.openTooltip() === null) return;

    // Leaving the group animates out; hopping to a neighbour flips back to instant.
    this.tooltipInstant.set(false);
    this.openTooltip.set(null);
    this.tooltipWarm = true;
    clearTimeout(this.tooltipWarmTimer);
    this.tooltipWarmTimer = setTimeout(() => (this.tooltipWarm = false), TOOLTIP_WARM_WINDOW);
  }

  // ---------- Section tracking ----------

  /**
   * Highlights the last section whose top has scrolled past the sticky nav.
   * At the very bottom the last section wins, since a short final section never reaches the top.
   */
  private observeSections(): void {
    const sections = Array.from(this.host.nativeElement.querySelectorAll<HTMLElement>('main section[id]'));
    if (sections.length === 0) return;

    let frame = 0;
    const update = () => {
      frame = 0;
      const atBottom = window.innerHeight + window.scrollY >= document.documentElement.scrollHeight - 2;
      let current = sections[0].id;
      for (const section of sections) {
        if (section.getBoundingClientRect().top <= ACTIVE_SECTION_OFFSET) current = section.id;
      }
      this.setActive(atBottom ? sections[sections.length - 1].id : current);
    };
    const schedule = () => {
      if (!frame) frame = requestAnimationFrame(update);
    };

    window.addEventListener('scroll', schedule, { passive: true });
    window.addEventListener('resize', schedule, { passive: true });
    update();
    this.destroyRef.onDestroy(() => {
      window.removeEventListener('scroll', schedule);
      window.removeEventListener('resize', schedule);
      cancelAnimationFrame(frame);
    });
  }

  private setActive(sectionId: string): void {
    if (this.activeSection() === sectionId) return;
    this.activeSection.set(sectionId);

    // On narrow screens the nav is a horizontal strip; keep the active item in view.
    const list = this.navList()?.nativeElement;
    const item = list?.querySelector<HTMLElement>(`[data-section="${sectionId}"]`);
    if (list && item && list.scrollWidth > list.clientWidth) {
      const reduceMotion = matchMedia('(prefers-reduced-motion: reduce)').matches;
      list.scrollTo({ left: item.offsetLeft - 16, behavior: reduceMotion ? 'auto' : 'smooth' });
    }
  }
}

/** Play the welcome once per browser session, and never for people who asked for less motion. */
function shouldPlayIntro(): boolean {
  if (typeof window === 'undefined') return false;
  if (window.matchMedia?.('(prefers-reduced-motion: reduce)').matches) return false;
  try {
    return sessionStorage.getItem(INTRO_SEEN_KEY) === null;
  } catch {
    return true;
  }
}
