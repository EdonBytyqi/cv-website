export interface Experience {
  id: number;
  jobTitle: string;
  company: string;
  location: string;
  startDate: Date;
  endDate?: Date;
  isCurrentJob: boolean;
  responsibilities: string[];
  sortOrder: number;
}
