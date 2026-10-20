import { Component, ElementRef, input, signal, viewChild } from '@angular/core';
import { SidebarData } from '../../models/sidebar.model';

@Component({
  selector: 'app-site-header',
  templateUrl: './site-header.html',
  styleUrl: './site-header.css',
  host: {
    '(document:keydown.escape)': 'closeAndRefocus()'
  }
})
export class SiteHeader {
  readonly site = input.required<SidebarData>();

  protected readonly menuOpen = signal(false);

  private readonly menuButton = viewChild.required<ElementRef<HTMLButtonElement>>('menuButton');

  protected toggleMenu(): void {
    this.menuOpen.update((open) => !open);
  }

  protected closeMenu(): void {
    this.menuOpen.set(false);
  }

  protected closeAndRefocus(): void {
    if (!this.menuOpen()) return;

    this.menuOpen.set(false);
    this.menuButton().nativeElement.focus();
  }
}
