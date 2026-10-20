import { TestBed } from '@angular/core/testing';
import { Contact } from './contact';
import { Contact as ContactDetails } from '../../models/contact.model';

// Made-up details so tests never depend on (or leak) real contact data
const details: ContactDetails = {
  id: 1,
  email: 'someone@example.com',
  location: 'Zürich, Switzerland',
  fullAddress: 'Example Street 1\n8000 Zürich, Switzerland',
  linkedInUrl: 'https://linkedin.com/in/example',
  gitHubUrl: 'https://github.com/example'
};

async function render(contact: ContactDetails): Promise<HTMLElement> {
  const fixture = TestBed.createComponent(Contact);
  fixture.componentRef.setInput('contact', contact);
  await fixture.whenStable();
  return fixture.nativeElement as HTMLElement;
}

describe('Contact', () => {
  beforeEach(async () => {
    await TestBed.configureTestingModule({ imports: [Contact] }).compileComponents();
  });

  it('[AC-001-09] strips whitespace from the phone link when a number is set', async () => {
    const section = await render({ ...details, phone: '044 123 45 67' });
    const link = section.querySelector('a[href^="tel:"]');
    expect(link?.getAttribute('href')).toBe('tel:0441234567');
    expect(link?.textContent?.trim()).toBe('044 123 45 67');
  });

  it('[AC-001-11] shows no phone entry when no number is set', async () => {
    const section = await render(details);
    expect(section.querySelector('a[href^="tel:"]')).toBeNull();
    const terms = [...section.querySelectorAll('dt')].map((term) => term.textContent?.trim());
    expect(terms).not.toContain('Phone');
  });
});
