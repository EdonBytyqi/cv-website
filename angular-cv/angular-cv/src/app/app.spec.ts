import { TestBed } from '@angular/core/testing';
import axe from 'axe-core';
import { App } from './app';
import { CvService } from './services/cv.service';

async function render(): Promise<HTMLElement> {
  const fixture = TestBed.createComponent(App);
  await fixture.whenStable();
  return fixture.nativeElement as HTMLElement;
}

function monthYear(date: Date): string {
  return date.toLocaleDateString('en-US', { month: 'short', year: 'numeric' });
}

function text(element: Element | null | undefined): string {
  return element?.textContent?.replace(/\s+/g, ' ').trim() ?? '';
}

describe('App', () => {
  let page: HTMLElement;
  let cv: ReturnType<CvService['cvData']>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({ imports: [App] }).compileComponents();
    cv = TestBed.inject(CvService).cvData();
    page = await render();
  });

  describe('001 — CV content', () => {
    it('[001:FR-001] has one h1 with the first name and a separately wrapped last name', () => {
      const headings = page.querySelectorAll('h1');
      expect(headings.length).toBe(1);
      expect(text(headings[0])).toContain('Edon');
      const lastName = [...headings[0].querySelectorAll('*')].find((el) => text(el) === 'Bytyqi');
      expect(lastName).toBeTruthy();
    });

    it('[001:FR-002] has a section with an h2 for every CV area', () => {
      for (const id of ['about', 'experience', 'education', 'skills', 'contact']) {
        const section = page.querySelector(`section#${id}`);
        expect(section, `section #${id}`).toBeTruthy();
        expect(section?.querySelector('h2'), `h2 in #${id}`).toBeTruthy();
      }
    });

    it('[001:FR-003] only links the main navigation to elements that exist', () => {
      const links = [...page.querySelectorAll<HTMLAnchorElement>('nav a[href^="#"]')];
      expect(links.length).toBeGreaterThan(0);
      for (const link of links) {
        const id = link.getAttribute('href')!.slice(1);
        expect(page.querySelector(`[id="${id}"]`), link.getAttribute('href')!).toBeTruthy();
      }
    });

    it('[001:FR-004] lists experience newest first and shows "Present" for the current job', () => {
      const entries = page.querySelectorAll('#experience article');
      expect(entries.length).toBe(cv.experiences.length);
      expect(text(entries[0])).toContain(cv.experiences[0].jobTitle);
      expect(text(entries[0].querySelector('time'))).toMatch(new RegExp(`${monthYear(cv.experiences[0].startDate)}\\s*[-–]\\s*Present`));
    });

    it('[001:FR-005] gives every experience entry a time element with the ISO start date', () => {
      const times = page.querySelectorAll('#experience article time');
      cv.experiences.forEach((experience, index) => {
        expect(times[index].getAttribute('datetime')).toBe(experience.startDate.toISOString());
      });
    });

    it('[001:FR-008] groups every skill under a heading for its category', () => {
      const skills = page.querySelector('#skills')!;
      for (const category of cv.skillCategories) {
        const heading = [...skills.querySelectorAll('h3')].find((h) => text(h) === category.categoryName);
        expect(heading, category.categoryName).toBeTruthy();
        for (const skill of category.skills) {
          expect(text(skills)).toContain(skill.name);
        }
      }
    });

    it('[001:FR-009] links the email address with mailto:', () => {
      expect(page.querySelector(`a[href="mailto:${cv.contact.email}"]`)).toBeTruthy();
    });

    it('[001:FR-012] renders each address line on its own line', () => {
      const lines = cv.contact.fullAddress!.split('\n');
      const address = [...page.querySelectorAll('#contact p, #contact address')]
        .find((el) => text(el).includes(lines[0]))!;
      expect(address.querySelectorAll('br').length).toBe(lines.length - 1);
      expect(text(address)).toBe(lines.join(' '));
    });
  });

  describe('002 — accessibility', () => {
    it('[002:FR-001] starts with a skip link that targets main', () => {
      const firstFocusable = page.querySelector('a[href], button, input, [tabindex]:not([tabindex="-1"])');
      expect(text(firstFocusable)).toBe('Skip to content');
      const targetId = firstFocusable!.getAttribute('href')!.slice(1);
      expect(page.querySelector(`main#${targetId}`)).toBeTruthy();
    });

    it('[002:FR-002] has a labelled nav landmark', () => {
      const nav = page.querySelector('nav');
      expect(nav?.getAttribute('aria-label') || nav?.getAttribute('aria-labelledby')).toBeTruthy();
    });

    it('[002:FR-003] gives every image alt text and names Edon in the hero photo', () => {
      const images = [...page.querySelectorAll('img')];
      expect(images.length).toBeGreaterThan(0);
      for (const image of images) expect(image.hasAttribute('alt')).toBe(true);
      expect(images.some((image) => image.alt.includes(cv.aboutMe.fullName))).toBe(true);
    });

    it('[002:FR-004] protects and names links that open a new tab', () => {
      for (const link of page.querySelectorAll('a[target="_blank"]')) {
        expect(link.getAttribute('rel')).toContain('noopener');
        expect(link.getAttribute('aria-label') || text(link)).toBeTruthy();
      }
    });

    it('[002:FR-005] never skips a heading level going down', () => {
      const levels = [...page.querySelectorAll('h1, h2, h3, h4, h5, h6')].map((h) => Number(h.tagName[1]));
      levels.forEach((level, index) => {
        if (index > 0) expect(level - levels[index - 1]).toBeLessThanOrEqual(1);
      });
    });

    it('[002:FR-006] has no axe-core violations', async () => {
      const results = await axe.run(page, { rules: { 'color-contrast': { enabled: false } } });
      const summary = results.violations.map((v) => `${v.id}: ${v.nodes.length} node(s)`);
      expect(summary).toEqual([]);
    });
  });

  describe('004 — notebook redesign', () => {
    it('[004:FR-005] labels each experience entry with its start year', () => {
      const entries = [...page.querySelectorAll('#experience article')];
      cv.experiences.forEach((experience, index) => {
        const year = String(experience.startDate.getFullYear());
        const label = [...entries[index].querySelectorAll('*')].find((el) => text(el) === year);
        expect(label, year).toBeTruthy();
        expect(label?.closest('time')).toBeNull();
      });
    });

    it('[004:FR-007] offers email and LinkedIn as the two ways to get in touch', () => {
      const contact = page.querySelector('#contact')!;
      expect(contact.querySelector(`a.button[href="mailto:${cv.contact.email}"]`)).toBeTruthy();
      expect(contact.querySelector(`a.button[href="${cv.contact.linkedInUrl}"]`)).toBeTruthy();
    });

    it('[004:FR-009] has a footer landmark linking to LinkedIn and GitHub', () => {
      const footer = page.querySelector('footer')!;
      expect(footer.closest('main')).toBeNull();
      expect(footer.querySelector('a[href*="linkedin.com"]')).toBeTruthy();
      expect(footer.querySelector('a[href*="github.com"]')).toBeTruthy();
    });
  });
});
