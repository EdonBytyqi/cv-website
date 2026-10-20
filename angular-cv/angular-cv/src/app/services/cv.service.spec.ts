import { TestBed } from '@angular/core/testing';
import { CvService, DOTNET_CV_URL } from './cv.service';
import { environment as production } from '../../environments/environment';
import { environment as development } from '../../environments/environment.development';

function stackLinks(dotnetCvUrl: string | null) {
  TestBed.configureTestingModule({ providers: [{ provide: DOTNET_CV_URL, useValue: dotnetCvUrl }] });
  return TestBed.inject(CvService).cvData().sidebar.techStackItems;
}

describe('CvService', () => {
  it('[004:FR-012] links .NET to the local dev server in development builds', () => {
    expect(development.dotnetCvUrl).toBe('http://localhost:5000');

    const dotnet = stackLinks(development.dotnetCvUrl).find((item) => item.label === '.NET');

    expect(dotnet?.url).toBe('http://localhost:5000');
    expect(dotnet?.opensInNewTab).toBe(true);
    expect(dotnet?.description).toContain('opens in a new tab');
  });

  it('[004:FR-012] hides .NET and never links to localhost in production builds', () => {
    expect(production.dotnetCvUrl).toBeNull();

    const links = stackLinks(production.dotnetCvUrl);

    expect(links.map((item) => item.label)).toEqual(['Angular']);
    expect(links.some((item) => item.url.includes('localhost'))).toBe(false);
  });
});
