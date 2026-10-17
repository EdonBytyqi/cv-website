import { ComponentFixture, TestBed, fakeAsync, tick } from '@angular/core/testing';
import { AppComponent } from './app.component';

describe('AppComponent', () => {
  let fixture: ComponentFixture<AppComponent>;
  let component: AppComponent;
  let compiled: HTMLElement;

  beforeEach(async () => {
    sessionStorage.removeItem('cv-hello-seen');

    await TestBed.configureTestingModule({
      imports: [AppComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(AppComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
    compiled = fixture.nativeElement as HTMLElement;
  });

  afterEach(() => sessionStorage.removeItem('cv-hello-seen'));

  it('renders the name as the page heading', () => {
    expect(compiled.querySelector('h1')?.textContent).toContain('Edon Bytyqi');
  });

  it('links every nav item to an existing section', () => {
    const links = Array.from(compiled.querySelectorAll<HTMLAnchorElement>('.toc-link'));
    expect(links.length).toBeGreaterThan(0);
    for (const link of links) {
      const id = link.getAttribute('href')!.slice(1);
      expect(compiled.querySelector(`section#${id}`)).withContext(id).not.toBeNull();
    }
  });

  it('marks the first section as current initially', () => {
    const current = compiled.querySelectorAll('.toc-link[aria-current="true"]');
    expect(current.length).toBe(1);
    expect(current[0].getAttribute('href')).toBe('#about');
  });

  it('does not render a phone link when no phone number is set', () => {
    expect(compiled.querySelector('a[href^="tel:"]')).toBeNull();
  });

  it('renders the experience entries', () => {
    expect(compiled.querySelectorAll('#experience .entry').length).toBe(2);
  });

  describe('welcome', () => {
    it('can be skipped, which reveals the page and remembers it for the session', () => {
      if (!component.introActive()) pending('reduced motion is on in this browser');
      expect(compiled.querySelector('.hello')).not.toBeNull();
      expect(compiled.querySelector('.layout.ready')).toBeNull();

      component.skipIntro();
      fixture.detectChanges();

      expect(compiled.querySelector('.hello')).toBeNull();
      expect(compiled.querySelector('.layout.ready')).not.toBeNull();
      expect(sessionStorage.getItem('cv-hello-seen')).toBe('1');
    });
  });

  describe('tooltips', () => {
    const hover = (id: string) =>
      component.showTooltip(id, new PointerEvent('pointerenter', { pointerType: 'mouse' }));

    it('delays the first tooltip, then opens neighbours instantly', fakeAsync(() => {
      hover('github');
      tick(300);
      expect(component.openTooltip()).toBeNull();
      tick(200);
      expect(component.openTooltip()).toBe('github');
      expect(component.tooltipInstant()).toBeFalse();

      component.hideTooltip();
      hover('email');
      expect(component.openTooltip()).toBe('email');
      expect(component.tooltipInstant()).toBeTrue();

      component.hideTooltip();
      tick(300);
      hover('linkedin');
      expect(component.openTooltip()).toBeNull();
      tick(500);
      expect(component.openTooltip()).toBe('linkedin');
    }));

    it('ignores touch', fakeAsync(() => {
      component.showTooltip('github', new PointerEvent('pointerenter', { pointerType: 'touch' }));
      tick(1000);
      expect(component.openTooltip()).toBeNull();
    }));
  });

  it('swaps the copy button to its confirmation state', async () => {
    spyOn(navigator.clipboard, 'writeText').and.resolveTo();
    const button = compiled.querySelector<HTMLButtonElement>('button.swap')!;
    expect(button.dataset['state']).toBe('idle');

    await component.copyEmail();
    fixture.detectChanges();

    expect(navigator.clipboard.writeText).toHaveBeenCalledWith('edonbytyqi@outlook.com');
    expect(button.dataset['state']).toBe('copied');
  });
});
