import { Component, computed, inject } from '@angular/core';
import { DatePipe, NgOptimizedImage } from '@angular/common';
import { CvService } from './services/cv.service';

@Component({
  selector: 'app-root',
  imports: [DatePipe, NgOptimizedImage],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App {
  private readonly cvService = inject(CvService);

  protected readonly cvData = this.cvService.cvData;

  private readonly nameParts = computed(() => this.cvData().aboutMe.fullName.split(' '));
  protected readonly firstName = computed(() => this.nameParts()[0]);
  protected readonly lastName = computed(() => this.nameParts().slice(1).join(' '));

  protected readonly phoneHref = computed(() => 'tel:' + this.cvData().contact.phone.replace(/\s/g, ''));
  protected readonly addressLines = computed(() => this.cvData().contact.fullAddress?.split('\n') ?? []);
}
