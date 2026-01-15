export interface Education {
  id: number;
  degree: string;
  institution: string;
  location: string;
  startDate: Date;
  endDate?: Date;
  grade?: string;
  description?: string;
  certificationType?: string;
  ects?: number;
  sortOrder: number;
}
