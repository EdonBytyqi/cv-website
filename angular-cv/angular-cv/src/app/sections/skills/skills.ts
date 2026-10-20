import { Component, input } from '@angular/core';
import { SkillCategory } from '../../models/skill.model';
import { Reveal } from '../../shared/reveal';

@Component({
  selector: 'app-skills',
  imports: [Reveal],
  templateUrl: './skills.html',
  styleUrl: './skills.css'
})
export class Skills {
  readonly categories = input.required<SkillCategory[]>();
}
