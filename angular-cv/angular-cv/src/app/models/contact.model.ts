export interface Contact {
  id: number;
  email: string;
  /** Optional: only rendered when set. Keep real numbers out of the public repo. */
  phone?: string;
  location: string;
  linkedInUrl: string;
  gitHubUrl: string;
  websiteUrl?: string;
}
