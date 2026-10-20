import { TestBed } from '@angular/core/testing';
import { Education } from './education';
import { Education as EducationEntry } from '../../models/education.model';

// Made-up entries so the tests don't change whenever the CV does
const degree: EducationEntry = {
  id: 1,
  degree: 'BSc Example Studies',
  institution: 'Example University',
  location: 'Bern',
  startDate: new Date(2010, 8, 1),
  endDate: new Date(2013, 5, 30),
  grade: '5.2/6',
  ects: 180,
  sortOrder: 1
};

const courses: EducationEntry = {
  id: 2,
  degree: 'Online courses',
  institution: 'Example Academy',
  location: 'Online',
  startDate: new Date(2022, 0, 1),
  ongoing: true,
  description: 'Cloud and data courses',
  sortOrder: 2
};

async function render(entries: EducationEntry[]): Promise<HTMLElement> {
  const fixture = TestBed.createComponent(Education);
  fixture.componentRef.setInput('education', entries);
  await fixture.whenStable();
  return fixture.nativeElement as HTMLElement;
}

function cardText(section: HTMLElement, degreeName: string): string {
  const card = [...section.querySelectorAll('article')].find((article) => article.textContent?.includes(degreeName));
  return card?.textContent?.replace(/\s+/g, ' ').trim() ?? '';
}

describe('Education', () => {
  beforeEach(async () => {
    await TestBed.configureTestingModule({ imports: [Education] }).compileComponents();
  });

  it('[001:FR-007] shows grade and ECTS together as "Grade: x | n ECTS"', async () => {
    const section = await render([degree]);
    expect(cardText(section, degree.degree)).toContain('Grade: 5.2/6 | 180 ECTS');
  });

  it('[001:FR-006] shows "since <year>" for ongoing entries and the description when set', async () => {
    const section = await render([degree, courses]);
    const ongoing = cardText(section, courses.degree);
    expect(ongoing).toContain('since 2022');
    expect(ongoing).toContain('Cloud and data courses');
    expect(cardText(section, degree.degree)).toContain('2010 – 2013');
  });
});
