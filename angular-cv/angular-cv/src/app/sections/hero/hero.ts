import { Component, computed, input } from '@angular/core';
import { NgOptimizedImage } from '@angular/common';
import { AboutMe } from '../../models/about-me.model';
import { Experience } from '../../models/experience.model';

@Component({
  selector: 'app-hero',
  imports: [NgOptimizedImage],
  templateUrl: './hero.html',
  styleUrl: './hero.css'
})
export class Hero {
  readonly aboutMe = input.required<AboutMe>();
  readonly currentJob = input<Experience>();

  private readonly nameParts = computed(() => this.aboutMe().fullName.split(' '));
  protected readonly firstName = computed(() => this.nameParts()[0]);
  protected readonly lastName = computed(() => this.nameParts().slice(1).join(' '));

  protected readonly currentlyAt = computed(() => {
    const job = this.currentJob();
    if (!job) return null;

    const city = job.location.split(',')[0];
    return `currently at ${job.company}, ${city}`;
  });
}
