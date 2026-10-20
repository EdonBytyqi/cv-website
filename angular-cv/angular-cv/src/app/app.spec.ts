import { TestBed } from '@angular/core/testing';
import axe from 'axe-core';
import { App } from './app';
import { CvService } from './services/cv.service';

async function render(): Promise<HTMLElement> {
  const fixture = TestBed.createComponent(App);
  await fixture.whenStable();
  return fixture.nativeElement as HTMLElement;
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

  describe('spec 001 — CV content', () => {
    it('[AC-001-01] has one h1 with the first name and a separately wrapped last name', () => {
      const headings = page.querySelectorAll('h1');
      expect(headings.length).toBe(1);
      expect(text(headings[0])).toContain('Edon');
      const lastName = [...headings[0].querySelectorAll('*')].find((el) => text(el) === 'Bytyqi');
      expect(lastName).toBeTruthy();
    });

    it('[AC-001-02] has a section with an h2 for every CV area', () => {
      for (const id of ['about', 'experience', 'education', 'skills', 'contact']) {
        const section = page.querySelector(`section#${id}`);
        expect(section, `section #${id}`).toBeTruthy();
        expect(section?.querySelector('h2'), `h2 in #${id}`).toBeTruthy();
      }
    });

    it('[AC-001-03] only links the main navigation to elements that exist', () => {
      const links = [...page.querySelectorAll<HTMLAnchorElement>('nav a[href^="#"]')];
      expect(links.length).toBeGreaterThan(0);
      for (const link of links) {
        const id = link.getAttribute('href')!.slice(1);
        expect(page.querySelector(`[id="${id}"]`), link.getAttribute('href')!).toBeTruthy();
      }
    });

    it('[AC-001-04] lists experience newest first and shows "Present" for the current job', () => {
      const entries = page.querySelectorAll('#experience article');
      expect(entries.length).toBe(cv.experiences.length);
      expect(text(entries[0])).toContain(cv.experiences[0].jobTitle);
      expect(text(entries[0].querySelector('time'))).toMatch(/May 2024\s*[-–]\s*Present/);
    });

    it('[AC-001-05] gives every experience entry a time element with the ISO start date', () => {
      const times = page.querySelectorAll('#experience article time');
      cv.experiences.forEach((experience, index) => {
        expect(times[index].getAttribute('datetime')).toBe(experience.startDate.toISOString());
      });
    });

    it('[AC-001-06] shows grade and ECTS together as "Grade: x | n ECTS"', () => {
      expect(text(page.querySelector('#education'))).toContain('Grade: 7.69/10 | 180 ECTS');
    });

    it('[AC-001-07] groups every skill under a heading for its category', () => {
      const skills = page.querySelector('#skills')!;
      for (const category of cv.skillCategories) {
        const heading = [...skills.querySelectorAll('h3')].find((h) => text(h) === category.categoryName);
        expect(heading, category.categoryName).toBeTruthy();
        for (const skill of category.skills) {
          expect(text(skills)).toContain(skill.name);
        }
      }
    });

    it('[AC-001-08] links the email address with mailto:', () => {
      expect(page.querySelector(`a[href="mailto:${cv.contact.email}"]`)).toBeTruthy();
    });

    it('[AC-001-10] renders each address line on its own line', () => {
      const lines = cv.contact.fullAddress!.split('\n');
      const address = [...page.querySelectorAll('#contact p, #contact address')]
        .find((el) => text(el).includes(lines[0]))!;
      expect(address.querySelectorAll('br').length).toBe(lines.length - 1);
      expect(text(address)).toBe(lines.join(' '));
    });
  });

  describe('spec 002 — accessibility', () => {
    it('[AC-002-01] starts with a skip link that targets main', () => {
      const firstFocusable = page.querySelector('a[href], button, input, [tabindex]:not([tabindex="-1"])');
      expect(text(firstFocusable)).toBe('Skip to content');
      const targetId = firstFocusable!.getAttribute('href')!.slice(1);
      expect(page.querySelector(`main#${targetId}`)).toBeTruthy();
    });

    it('[AC-002-02] has a labelled nav landmark', () => {
      const nav = page.querySelector('nav');
      expect(nav?.getAttribute('aria-label') || nav?.getAttribute('aria-labelledby')).toBeTruthy();
    });

    it('[AC-002-03] gives every image alt text and names Edon in the hero photo', () => {
      const images = [...page.querySelectorAll('img')];
      expect(images.length).toBeGreaterThan(0);
      for (const image of images) expect(image.hasAttribute('alt')).toBe(true);
      expect(images.some((image) => image.alt.includes(cv.aboutMe.fullName))).toBe(true);
    });

    it('[AC-002-04] protects and names links that open a new tab', () => {
      for (const link of page.querySelectorAll('a[target="_blank"]')) {
        expect(link.getAttribute('rel')).toContain('noopener');
        expect(link.getAttribute('aria-label') || text(link)).toBeTruthy();
      }
    });

    it('[AC-002-05] never skips a heading level going down', () => {
      const levels = [...page.querySelectorAll('h1, h2, h3, h4, h5, h6')].map((h) => Number(h.tagName[1]));
      levels.forEach((level, index) => {
        if (index > 0) expect(level - levels[index - 1]).toBeLessThanOrEqual(1);
      });
    });

    it('[AC-002-06] has no axe-core violations', async () => {
      const results = await axe.run(page, { rules: { 'color-contrast': { enabled: false } } });
      const summary = results.violations.map((v) => `${v.id}: ${v.nodes.length} node(s)`);
      expect(summary).toEqual([]);
    });
  });

  describe('spec 003 — notebook redesign', () => {
    it('[AC-003-05] labels each experience entry with its start year', () => {
      const entries = [...page.querySelectorAll('#experience article')];
      cv.experiences.forEach((experience, index) => {
        const year = String(experience.startDate.getFullYear());
        const label = [...entries[index].querySelectorAll('*')].find((el) => text(el) === year);
        expect(label, year).toBeTruthy();
        expect(label?.closest('time')).toBeNull();
      });
    });

    it('[AC-003-06] shows the job title and where Edon currently works', () => {
      const hero = page.querySelector('#home')!;
      expect(text(hero)).toContain(cv.aboutMe.title);
      expect(text(hero)).toContain('currently at BSI SA, Lugano');
    });

    it('[AC-003-07] offers email and LinkedIn as the two ways to get in touch', () => {
      const contact = page.querySelector('#contact')!;
      expect(contact.querySelector(`a.button[href="mailto:${cv.contact.email}"]`)).toBeTruthy();
      expect(contact.querySelector(`a.button[href="${cv.contact.linkedInUrl}"]`)).toBeTruthy();
    });

    it('[AC-003-09] has a footer landmark linking to LinkedIn and GitHub', () => {
      const footer = page.querySelector('footer')!;
      expect(footer.closest('main')).toBeNull();
      expect(footer.querySelector('a[href*="linkedin.com"]')).toBeTruthy();
      expect(footer.querySelector('a[href*="github.com"]')).toBeTruthy();
    });
  });
});
