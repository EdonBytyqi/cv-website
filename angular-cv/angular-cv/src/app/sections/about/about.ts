import { Component, input } from '@angular/core';
import { AboutMe } from '../../models/about-me.model';
import { Reveal } from '../../shared/reveal';

@Component({
  selector: 'app-about',
  imports: [Reveal],
  templateUrl: './about.html',
  styleUrl: './about.css'
})
export class About {
  readonly aboutMe = input.required<AboutMe>();
  readonly location = input.required<string>();
}
