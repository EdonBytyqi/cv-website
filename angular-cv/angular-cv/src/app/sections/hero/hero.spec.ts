import { TestBed } from '@angular/core/testing';
import { Hero } from './hero';
import { AboutMe } from '../../models/about-me.model';
import { Experience } from '../../models/experience.model';

const aboutMe: AboutMe = {
  id: 1,
  fullName: 'Alex Example',
  title: 'Software Developer',
  bio: '',
  profileImageUrl: 'assets/images/profile.jpg',
  createdAt: new Date(2026, 0, 1),
  updatedAt: new Date(2026, 0, 1)
};

const job: Experience = {
  id: 1,
  jobTitle: 'Developer',
  company: 'Example AG',
  location: 'Zürich, Switzerland',
  startDate: new Date(2024, 0, 1),
  isCurrentJob: true,
  responsibilities: [],
  sortOrder: 1
};

async function heroText(currentJob: Experience): Promise<string> {
  const fixture = TestBed.createComponent(Hero);
  fixture.componentRef.setInput('aboutMe', aboutMe);
  fixture.componentRef.setInput('currentJob', currentJob);
  await fixture.whenStable();
  return (fixture.nativeElement as HTMLElement).textContent?.replace(/\s+/g, ' ') ?? '';
}

describe('Hero', () => {
  beforeEach(async () => {
    await TestBed.configureTestingModule({ imports: [Hero] }).compileComponents();
  });

  it('[004:FR-006] names employer and city when the current role has a location', async () => {
    const text = await heroText(job);
    expect(text).toContain('Software Developer');
    expect(text).toContain('currently at Example AG, Zürich');
  });

  it('[004:FR-006] falls back to the company line when the current role has no location', async () => {
    const text = await heroText({ ...job, company: 'AI integration, software development', location: '' });
    expect(text).toContain('currently: AI integration, software development');
  });
});
