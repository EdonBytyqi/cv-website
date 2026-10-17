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

@Component({
  selector: 'app-root',
  imports: [DatePipe],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css',
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class AppComponent {
  private cvService = inject(CvService);
  private destroyRef = inject(DestroyRef);
  private host = inject<ElementRef<HTMLElement>>(ElementRef);
  private navList = viewChild<ElementRef<HTMLElement>>('navList');
  private copyResetTimer?: ReturnType<typeof setTimeout>;

  cvData = this.cvService.cvData;
  activeSection = signal(this.cvData().sidebar.navigationItems[0].sectionId);
  emailCopied = signal(false);
  readonly year = new Date().getFullYear();

  constructor() {
    afterNextRender(() => this.observeSections());
    this.destroyRef.onDestroy(() => clearTimeout(this.copyResetTimer));
  }

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
