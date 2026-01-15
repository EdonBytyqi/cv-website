import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CvService } from './services/cv.service';
import { CvData } from './models/cv-data.model';

@Component({
  selector: 'app-root',
  imports: [CommonModule],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent implements OnInit {
  cvData?: CvData;

  constructor(private cvService: CvService) {}

  ngOnInit(): void {
    this.cvService.getCompleteCv().subscribe(data => {
      this.cvData = data;
    });
  }
}
