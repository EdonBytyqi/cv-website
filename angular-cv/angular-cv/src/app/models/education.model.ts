export interface Education {
  id: number;
  degree: string;
  institution: string;
  location: string;
  startDate: Date;
  endDate?: Date;
  /** Still in progress; shown as "since <start year>" */
  ongoing?: boolean;
  grade?: string;
  description?: string;
  certificationType?: string;
  ects?: number;
  sortOrder: number;
}
