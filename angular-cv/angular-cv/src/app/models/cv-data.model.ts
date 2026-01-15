import { AboutMe } from './about-me.model';
import { Experience } from './experience.model';
import { Education } from './education.model';
import { SkillCategory } from './skill.model';
import { Contact } from './contact.model';
import { SidebarData } from './sidebar.model';

export interface CvData {
  aboutMe: AboutMe;
  experiences: Experience[];
  education: Education[];
  skillCategories: SkillCategory[];
  contact: Contact;
  sidebar: SidebarData;
}
