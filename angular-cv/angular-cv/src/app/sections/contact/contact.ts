import { Component, computed, input } from '@angular/core';
import { Contact as ContactDetails } from '../../models/contact.model';
import { Reveal } from '../../shared/reveal';

@Component({
  selector: 'app-contact',
  imports: [Reveal],
  templateUrl: './contact.html',
  styleUrl: './contact.css'
})
export class Contact {
  readonly contact = input.required<ContactDetails>();

  protected readonly phoneHref = computed(() => 'tel:' + this.contact().phone.replace(/\s/g, ''));
  protected readonly addressLines = computed(() => this.contact().fullAddress?.split('\n') ?? []);
}
