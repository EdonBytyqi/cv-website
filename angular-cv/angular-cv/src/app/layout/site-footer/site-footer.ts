import { Component, input } from '@angular/core';
import { SocialLink } from '../../models/sidebar.model';

@Component({
  selector: 'app-site-footer',
  templateUrl: './site-footer.html',
  styleUrl: './site-footer.css'
})
export class SiteFooter {
  readonly name = input.required<string>();
  readonly socialLinks = input.required<SocialLink[]>();
}
