import { Component, ChangeDetectionStrategy, computed, inject } from '@angular/core';
import { DatePipe, NgOptimizedImage } from '@angular/common';
import { CvService } from './services/cv.service';

@Component({
  selector: 'app-root',
  imports: [DatePipe, NgOptimizedImage],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css',
  // OnPush is not the default until Angular v22; keep it explicit while on v19
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class AppComponent {
  private readonly cvService = inject(CvService);

  protected readonly cvData = this.cvService.cvData;

  private readonly nameParts = computed(() => this.cvData().aboutMe.fullName.split(' '));
  protected readonly firstName = computed(() => this.nameParts()[0]);
  protected readonly lastName = computed(() => this.nameParts().slice(1).join(' '));

  protected readonly phoneHref = computed(() => 'tel:' + this.cvData().contact.phone.replace(/\s/g, ''));
  protected readonly addressLines = computed(() => this.cvData().contact.fullAddress?.split('\n') ?? []);
}
