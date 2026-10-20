import { Injectable, signal } from '@angular/core';
import { CvData } from '../models/cv-data.model';
import { AboutMe } from '../models/about-me.model';
import { Experience } from '../models/experience.model';
import { Education } from '../models/education.model';
import { SkillCategory } from '../models/skill.model';
import { Contact } from '../models/contact.model';
import { SidebarData } from '../models/sidebar.model';

@Injectable({
  providedIn: 'root'
})
export class CvService {
  // The CV content is static, so a single read-only signal is enough
  readonly cvData = signal<CvData>({
    aboutMe: this.createAboutMe(),
    experiences: this.createExperiences(),
    education: this.createEducation(),
    skillCategories: this.createSkillCategories(),
    contact: this.createContact(),
    sidebar: this.createSidebar()
  }).asReadonly();

  private createAboutMe(): AboutMe {
    return {
      id: 1,
      fullName: 'Edon Bytyqi',
      title: 'Software Developer',
      bio: 'Experienced software developer specializing in Avaloq, SAP, and .NET development. Passionate about creating efficient and scalable solutions.',
      profileImageUrl: '/assets/images/profile.jpg',
      createdAt: new Date(),
      updatedAt: new Date()
    };
  }

  private createExperiences(): Experience[] {
    return [
      {
        id: 1,
        jobTitle: 'Software Developer - SAP',
        company: 'BSI SA',
        location: 'Lugano, Switzerland',
        startDate: new Date(2024, 4, 1),
        endDate: undefined,
        isCurrentJob: true,
        responsibilities: [
          'Develop and maintain SAP applications',
          'Collaborate with cross-functional teams',
          'Implement new features and optimize existing code',
          'Participate in code reviews and knowledge sharing'
        ],
        sortOrder: 1
      },
      {
        id: 2,
        jobTitle: 'Software Developer - Avaloq',
        company: 'BSI SA',
        location: 'Lugano, Switzerland',
        startDate: new Date(2023, 6, 1),
        endDate: new Date(2024, 3, 30),
        isCurrentJob: false,
        responsibilities: [
          'Developed banking solutions using Avaloq platform',
          'Customized workflows and business processes',
          'Maintained high code quality standards',
          'Worked in Agile/Scrum environment'
        ],
        sortOrder: 2
      }
    ];
  }

  private createEducation(): Education[] {
    return [
      {
        id: 1,
        degree: 'M-Files Solution Course',
        institution: 'M-Files',
        location: 'Helsinki (Online)',
        startDate: new Date(2025, 0, 1),
        certificationType: undefined,
        sortOrder: 1
      },
      {
        id: 2,
        degree: 'Avaloq Certified Professional',
        institution: 'Avaloq Group AG',
        location: 'Zürich',
        startDate: new Date(2024, 0, 1),
        certificationType: 'Diploma',
        sortOrder: 2
      },
      {
        id: 3,
        degree: 'Bachelor in Computer & Engineering Sciences',
        institution: 'UBT - Higher Education Institution',
        location: 'Prishtina, Kosovo',
        startDate: new Date(2014, 8, 1),
        endDate: new Date(2018, 5, 30),
        grade: '7.69/10',
        ects: 180,
        sortOrder: 3
      }
    ];
  }

  private createSkillCategories(): SkillCategory[] {
    return [
      {
        categoryName: 'ERP & Banking Systems',
        skills: [
          { id: 1, name: 'SAP ABAP', category: 'ERP & Banking Systems', proficiencyLevel: 5, sortOrder: 1 },
          { id: 2, name: 'SAP UI5/Fiori', category: 'ERP & Banking Systems', proficiencyLevel: 5, sortOrder: 2 },
          { id: 3, name: 'SAP HCM/MM', category: 'ERP & Banking Systems', proficiencyLevel: 4, sortOrder: 3 },
          { id: 4, name: 'Avaloq Core Platform', category: 'ERP & Banking Systems', proficiencyLevel: 4, sortOrder: 4 }
        ]
      },
      {
        categoryName: 'Development',
        skills: [
          { id: 5, name: 'C#', category: 'Development', proficiencyLevel: 5, sortOrder: 1 },
          { id: 6, name: '.NET', category: 'Development', proficiencyLevel: 5, sortOrder: 2 },
          { id: 7, name: 'Angular', category: 'Development', proficiencyLevel: 4, sortOrder: 3 },
          { id: 8, name: 'JavaScript', category: 'Development', proficiencyLevel: 4, sortOrder: 4 },
          { id: 9, name: 'TypeScript', category: 'Development', proficiencyLevel: 4, sortOrder: 5 },
          { id: 10, name: 'VBScript', category: 'Development', proficiencyLevel: 3, sortOrder: 6 },
          { id: 11, name: 'PL/SQL', category: 'Development', proficiencyLevel: 4, sortOrder: 7 }
        ]
      },
      {
        categoryName: 'Databases',
        skills: [
          { id: 12, name: 'Oracle', category: 'Databases', proficiencyLevel: 4, sortOrder: 1 },
          { id: 13, name: 'MSSQL', category: 'Databases', proficiencyLevel: 4, sortOrder: 2 },
          { id: 14, name: 'MySQL', category: 'Databases', proficiencyLevel: 4, sortOrder: 3 },
          { id: 15, name: 'Firebase', category: 'Databases', proficiencyLevel: 3, sortOrder: 4 }
        ]
      },
      {
        categoryName: 'Systems & Integration',
        skills: [
          { id: 16, name: 'M-Files', category: 'Systems & Integration', proficiencyLevel: 4, sortOrder: 1 },
          { id: 17, name: 'SAP ERP', category: 'Systems & Integration', proficiencyLevel: 5, sortOrder: 2 },
          { id: 18, name: 'Neptune DXP', category: 'Systems & Integration', proficiencyLevel: 4, sortOrder: 3 },
          { id: 19, name: 'API Integration', category: 'Systems & Integration', proficiencyLevel: 4, sortOrder: 4 }
        ]
      }
    ];
  }

  private createContact(): Contact {
    return {
      id: 1,
      email: 'edonbytyqi@outlook.com',
      phone: '+41 XX XXX XX XX',
      location: '6020 Emmenbrücke, Switzerland',
      fullAddress: '6020 Emmenbrücke, Switzerland',
      linkedInUrl: 'https://linkedin.com/in/edon-bytyqi',
      gitHubUrl: 'https://github.com/EdonBytyqi',
      websiteUrl: 'https://edonbytyqi.com'
    };
  }

  private createSidebar(): SidebarData {
    return {
      name: 'EDON BYTYQI',
      profileImageUrl: '/assets/images/profile.jpg',
      navigationItems: [
        { label: 'HOME', href: '#home', isActive: true },
        { label: 'ABOUT', href: '#about', isActive: false },
        { label: 'EXPERIENCE', href: '#experience', isActive: false },
        { label: 'EDUCATION', href: '#education', isActive: false },
        { label: 'SKILLS', href: '#skills', isActive: false },
        { label: 'CONTACT', href: '#contact', isActive: false }
      ],
      techStackItems: [
        { label: '.NET', url: 'http://localhost:5000', isActive: false },
        { label: 'Angular', url: 'http://localhost:4202', isActive: true }
      ],
      socialLinks: [
        { label: 'LinkedIn', url: 'https://linkedin.com/in/edon-bytyqi', icon: 'in' },
        { label: 'GitHub', url: 'https://github.com/EdonBytyqi', icon: 'gh' }
      ]
    };
  }
}
