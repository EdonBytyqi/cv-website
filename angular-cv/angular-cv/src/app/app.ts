import { Component, computed, inject } from '@angular/core';
import { CvService } from './services/cv.service';
import { SiteHeader } from './layout/site-header/site-header';
import { SiteFooter } from './layout/site-footer/site-footer';
import { Hero } from './sections/hero/hero';
import { About } from './sections/about/about';
import { Experience } from './sections/experience/experience';
import { Education } from './sections/education/education';
import { Skills } from './sections/skills/skills';
import { Contact } from './sections/contact/contact';

@Component({
  selector: 'app-root',
  imports: [SiteHeader, SiteFooter, Hero, About, Experience, Education, Skills, Contact],
  templateUrl: './app.html'
})
export class App {
  protected readonly cvData = inject(CvService).cvData;

  protected readonly currentJob = computed(() => this.cvData().experiences.find((job) => job.isCurrentJob));
}
