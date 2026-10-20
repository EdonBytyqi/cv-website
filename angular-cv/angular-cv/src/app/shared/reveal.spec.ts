import { Component } from '@angular/core';
import { TestBed } from '@angular/core/testing';
import { Reveal } from './reveal';

@Component({
  imports: [Reveal],
  template: `<div appReveal="50">revealed content</div>`
})
class Host {}

async function renderTarget(): Promise<HTMLElement> {
  const fixture = TestBed.createComponent(Host);
  await fixture.whenStable();
  return (fixture.nativeElement as HTMLElement).querySelector('div')!;
}

describe('Reveal', () => {
  let observed: Element[];
  let trigger: (entry: Partial<IntersectionObserverEntry>) => void;

  function installObserver(): void {
    observed = [];
    vi.stubGlobal('IntersectionObserver', class {
      constructor(callback: IntersectionObserverCallback) {
        trigger = (entry) => callback([entry as IntersectionObserverEntry], this as unknown as IntersectionObserver);
      }
      observe(element: Element) { observed.push(element); }
      unobserve() {}
      disconnect() {}
    });
  }

  function prefersReducedMotion(matches: boolean): void {
    vi.stubGlobal('matchMedia', (query: string) => ({ matches: matches && query.includes('reduce'), media: query }));
  }

  afterEach(() => vi.unstubAllGlobals());

  it('[004:FR-008] leaves content visible when IntersectionObserver is unavailable', async () => {
    vi.stubGlobal('IntersectionObserver', undefined);
    const target = await renderTarget();
    expect(target.classList).not.toContain('reveal-pending');
  });

  it('[004:FR-008] leaves content visible when the user prefers reduced motion', async () => {
    installObserver();
    prefersReducedMotion(true);
    const target = await renderTarget();
    expect(target.classList).not.toContain('reveal-pending');
    expect(observed).toEqual([]);
  });

  it('hides content until it scrolls into view, then reveals it once', async () => {
    installObserver();
    prefersReducedMotion(false);
    const target = await renderTarget();
    expect(target.classList).toContain('reveal-pending');

    trigger({ isIntersecting: true, target });
    TestBed.tick();

    expect(target.classList).toContain('reveal-visible');
    expect(target.classList).not.toContain('reveal-pending');
    expect(target.style.getPropertyValue('--reveal-delay')).toBe('50ms');
  });
});
