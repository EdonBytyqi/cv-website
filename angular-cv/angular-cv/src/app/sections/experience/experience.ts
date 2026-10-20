import { Component, input } from '@angular/core';
import { DatePipe } from '@angular/common';
import { Experience as ExperienceEntry } from '../../models/experience.model';
import { Reveal } from '../../shared/reveal';

@Component({
  selector: 'app-experience',
  imports: [DatePipe, Reveal],
  templateUrl: './experience.html',
  styleUrl: './experience.css'
})
export class Experience {
  readonly experiences = input.required<ExperienceEntry[]>();
}
