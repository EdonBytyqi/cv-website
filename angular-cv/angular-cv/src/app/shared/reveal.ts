import { DestroyRef, Directive, ElementRef, afterNextRender, inject, input, numberAttribute, signal } from '@angular/core';

type RevealState = 'static' | 'pending' | 'visible';

/**
 * Reveals an element with a clip-path wipe the first time it scrolls into view.
 * Content stays visible when IntersectionObserver is missing or the user prefers
 * reduced motion, so nothing is ever hidden without a way to show it.
 * Usage: `<li appReveal="100">` where the value is a stagger delay in ms.
 */
@Directive({
  selector: '[appReveal]',
  host: {
    '[class.reveal-pending]': 'state() === "pending"',
    '[class.reveal-visible]': 'state() === "visible"',
    '[style.--reveal-delay.ms]': 'delay()'
  }
})
export class Reveal {
  readonly delay = input(0, { alias: 'appReveal', transform: (value: unknown) => numberAttribute(value, 0) });

  protected readonly state = signal<RevealState>('static');

  private readonly element = inject<ElementRef<HTMLElement>>(ElementRef).nativeElement;
  private readonly destroyRef = inject(DestroyRef);

  constructor() {
    afterNextRender(() => this.observe());
  }

  private observe(): void {
    if (typeof IntersectionObserver === 'undefined') return;
    if (window.matchMedia?.('(prefers-reduced-motion: reduce)').matches) return;

    const observer = new IntersectionObserver(
      ([entry]) => {
        if (!entry.isIntersecting) return;
        this.state.set('visible');
        observer.disconnect();
      },
      { rootMargin: '0px 0px -10% 0px' }
    );

    this.state.set('pending');
    observer.observe(this.element);
    this.destroyRef.onDestroy(() => observer.disconnect());
  }
}
