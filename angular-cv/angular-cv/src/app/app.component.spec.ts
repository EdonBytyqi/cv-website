import { TestBed } from '@angular/core/testing';
import { AppComponent } from './app.component';

describe('AppComponent', () => {
  let compiled: HTMLElement;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AppComponent],
    }).compileComponents();

    const fixture = TestBed.createComponent(AppComponent);
    fixture.detectChanges();
    compiled = fixture.nativeElement as HTMLElement;
  });

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
});
