import { Component, computed, input } from '@angular/core';
import { Education as EducationEntry } from '../../models/education.model';
import { Reveal } from '../../shared/reveal';

@Component({
  selector: 'app-education',
  imports: [Reveal],
  templateUrl: './education.html',
  styleUrl: './education.css'
})
export class Education {
  readonly education = input.required<EducationEntry[]>();

  protected readonly entries = computed(() =>
    this.education().map((entry) => ({
      ...entry,
      years: formatYears(entry),
      institutionLine: [entry.institution, entry.location].filter(Boolean).join(', ')
        + (entry.certificationType ? ` (${entry.certificationType})` : ''),
      results: [entry.grade && `Grade: ${entry.grade}`, entry.ects && `${entry.ects} ECTS`].filter(Boolean).join(' | ')
    }))
  );
}

function formatYears({ startDate, endDate, ongoing }: EducationEntry): string {
  const start = startDate.getFullYear();
  if (ongoing) return `since ${start}`;

  const end = endDate?.getFullYear();
  return end && end !== start ? `${start} – ${end}` : `${start}`;
}
