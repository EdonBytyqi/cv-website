export interface NavigationItem {
  label: string;
  /** Id of the section this item scrolls to, without the leading '#'. */
  sectionId: string;
}

export interface SocialLink {
  label: string;
  url: string;
  icon: 'linkedin' | 'github';
}

export interface SidebarData {
  name: string;
  profileImageUrl: string;
  navigationItems: NavigationItem[];
  socialLinks: SocialLink[];
}
