export interface Skill {
  id: number;
  name: string;
  category: string;
  proficiencyLevel: number;
  sortOrder: number;
}

export interface SkillCategory {
  categoryName: string;
  skills: Skill[];
}
