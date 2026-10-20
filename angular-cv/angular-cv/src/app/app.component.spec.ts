import { TestBed } from '@angular/core/testing';
import { AppComponent } from './app.component';
import { CvService } from './services/cv.service';

describe('AppComponent', () => {
  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AppComponent],
    }).compileComponents();
  });

  it('should create the app', () => {
    const fixture = TestBed.createComponent(AppComponent);
    expect(fixture.componentInstance).toBeTruthy();
  });

  it('should render the full name in the hero heading', () => {
    const fixture = TestBed.createComponent(AppComponent);
    fixture.detectChanges();
    const compiled = fixture.nativeElement as HTMLElement;
    expect(compiled.querySelector('h1')?.textContent).toContain('Edon');
    expect(compiled.querySelector('h1 .highlight')?.textContent).toContain('Bytyqi');
  });

  it('should strip spaces from the phone link', () => {
    const fixture = TestBed.createComponent(AppComponent);
    fixture.detectChanges();
    const compiled = fixture.nativeElement as HTMLElement;
    expect(compiled.querySelector('a[href^="tel:"]')?.getAttribute('href')).toBe('tel:' + TestBed.inject(CvService).cvData().contact.phone.replace(/\s/g, ''));
  });
});
