import { Component, ChangeDetectionStrategy, inject } from '@angular/core';
import { CommonModule, DatePipe } from '@angular/common';
import { CvService } from './services/cv.service';

@Component({
  selector: 'app-root',
  imports: [CommonModule, DatePipe],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css',
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class AppComponent {
  private cvService = inject(CvService);

  // Expose the computed signal from the service
  cvData = this.cvService.cvData;
}
