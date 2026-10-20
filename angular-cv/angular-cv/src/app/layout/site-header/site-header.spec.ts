import { ComponentFixture, TestBed } from '@angular/core/testing';
import { SiteHeader } from './site-header';
import { CvService } from '../../services/cv.service';

describe('SiteHeader', () => {
  let fixture: ComponentFixture<SiteHeader>;
  let header: HTMLElement;
  let menuButton: HTMLButtonElement;

  beforeEach(async () => {
    await TestBed.configureTestingModule({ imports: [SiteHeader] }).compileComponents();
    fixture = TestBed.createComponent(SiteHeader);
    fixture.componentRef.setInput('site', TestBed.inject(CvService).cvData().sidebar);
    await fixture.whenStable();
    header = fixture.nativeElement as HTMLElement;
    menuButton = header.querySelector('button')!;
    // jsdom ignores media queries, so emulate the narrow layout where the Menu button is shown
    menuButton.style.display = 'block';
  });

  async function openMenu(): Promise<void> {
    menuButton.click();
    await fixture.whenStable();
  }

  it('[AC-003-01] renders a header landmark with the site name and main navigation', () => {
    const landmark = header.querySelector('header')!;
    expect(landmark.textContent).toContain('EDON BYTYQI');
    expect(landmark.querySelector('nav[aria-label="Main navigation"]')).toBeTruthy();
  });

  it('[AC-003-02] toggles aria-expanded on a Menu button that controls the nav list', async () => {
    expect(menuButton.textContent?.trim()).toBe('Menu');
    expect(menuButton.getAttribute('aria-expanded')).toBe('false');
    const controlled = header.querySelector(`#${menuButton.getAttribute('aria-controls')}`);
    expect(controlled?.closest('nav')).toBeTruthy();

    await openMenu();

    expect(menuButton.getAttribute('aria-expanded')).toBe('true');
  });

  it('[AC-003-03] closes on Escape and returns focus to the Menu button', async () => {
    await openMenu();
    header.querySelector<HTMLAnchorElement>('.nav-link')!.focus();

    document.dispatchEvent(new KeyboardEvent('keydown', { key: 'Escape' }));
    await fixture.whenStable();

    expect(menuButton.getAttribute('aria-expanded')).toBe('false');
    expect(document.activeElement).toBe(menuButton);
  });

  it('[AC-003-04] closes when a navigation link is chosen', async () => {
    await openMenu();

    header.querySelector<HTMLAnchorElement>('.nav-link')!.click();
    await fixture.whenStable();

    expect(menuButton.getAttribute('aria-expanded')).toBe('false');
  });
});
