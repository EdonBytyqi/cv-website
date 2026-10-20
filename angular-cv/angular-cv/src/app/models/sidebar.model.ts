export interface NavigationItem {
  label: string;
  href: string;
  isActive: boolean;
}

export interface TechStackItem {
  label: string;
  url: string;
  isActive: boolean;
  /** Accessible name that says where the link goes */
  description: string;
  opensInNewTab: boolean;
}

export interface SocialLink {
  label: string;
  url: string;
  icon: string;
}

export interface SidebarData {
  name: string;
  profileImageUrl: string;
  navigationItems: NavigationItem[];
  techStackItems: TechStackItem[];
  socialLinks: SocialLink[];
}
