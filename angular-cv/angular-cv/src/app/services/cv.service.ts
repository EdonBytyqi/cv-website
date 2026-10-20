import { Service, signal } from '@angular/core';
import { CvData } from '../models/cv-data.model';
import { AboutMe } from '../models/about-me.model';
import { Experience } from '../models/experience.model';
import { Education } from '../models/education.model';
import { SkillCategory } from '../models/skill.model';
import { Contact } from '../models/contact.model';
import { SidebarData } from '../models/sidebar.model';

// Content mirrors the CV (translated to English). This repository is public:
// never add a phone number, street address, date of birth or permit details.
@Service()
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
      title: 'Software Developer · SAP · GenAI',
      bio: 'Software developer with 6+ years in enterprise IT: SAP development, API integration and production GenAI systems. '
        + 'I specialise in SAP (Fiori, SAPUI5, ABAP OO, OData), the Avaloq Core Platform, C#/.NET and production AI solutions '
        + 'with LangChain, RAG, agentic AI, multi-agent orchestration and MCP. I design RAG architectures, work on prompt '
        + 'engineering and guardrails, and connect LLMs to real workflows. I like numbers and data analysis, work in a '
        + 'structured way that keeps to agreed timelines, and am a friendly, helpful team player.',
      profileImageUrl: 'assets/images/profile.jpg',
      createdAt: new Date(),
      updatedAt: new Date()
    };
  }

  private createExperiences(): Experience[] {
    return [
      {
        id: 1,
        jobTitle: 'Professional Development & Independent Projects',
        company: 'AI integration, software development',
        location: '',
        startDate: new Date(2025, 3, 1),
        isCurrentJob: true,
        responsibilities: [
          'Anthropic Academy certification programme 2026 (Anthropic / Skilljar): Claude 101, Claude Platform 101, '
            + 'Claude Code 101, Claude Code in Action, Introduction to Model Context Protocol, Model Context Protocol: Advanced Topics',
          'Designed and built RAG systems for knowledge management: chunking, embeddings and retrieval with LangChain, '
            + 'vector databases (Pinecone/Weaviate) and the Claude API',
          'Built multi-agent systems with orchestrated workflows: agent delegation, tool use and coordination via the '
            + 'Model Context Protocol (MCP)',
          'Built AI automations with n8n, e.g. RSS aggregation with LLM-generated structured digests, including error '
            + 'handling and cost control',
          'Prompt engineering, guardrails (NeMo) and model evaluation as everyday practice'
        ],
        sortOrder: 1
      },
      {
        id: 2,
        jobTitle: 'Junior Avaloq Customization Professional',
        company: 'Luzerner Kantonalbank AG',
        location: 'Lucerne, Switzerland',
        startDate: new Date(2023, 8, 1),
        endDate: new Date(2025, 2, 31),
        isCurrentJob: false,
        responsibilities: [
          'Main responsibility for operating and technically parameterising the Avaloq core banking platform',
          'Developed and optimised complex SQL queries on Oracle for reporting, data analysis and quality assurance',
          'Derived requirements and specifications from business departments (Compliance, Operations, Controlling) '
            + 'and translated them into technical solutions',
          'Provided application support for incidents and change requests (1st and 2nd level)',
          'Accompanied several go-lives from planning to production rollout'
        ],
        sortOrder: 2
      },
      {
        id: 3,
        jobTitle: 'SAP ERP Developer',
        company: 'Ringier AG',
        location: 'Zofingen, Switzerland',
        startDate: new Date(2021, 11, 1),
        endDate: new Date(2023, 7, 31),
        isCurrentJob: false,
        responsibilities: [
          'In-depth SAP ABAP development in an ERP environment',
          'REST API integration between SAP and Coupa (B2B), modernised from file transfer to API and running '
            + 'error-free in production',
          'Worked with web technologies (REST APIs, JavaScript, TypeScript)',
          'Defined developer guidelines for Neptune DXP (SAP low-code)',
          'Built Google Apps Script automations for status notifications and IT account validation'
        ],
        sortOrder: 3
      },
      {
        id: 4,
        jobTitle: 'Junior SAPUI5 Fiori Developer',
        company: 'Axel Springer',
        location: 'Berlin, Germany',
        startDate: new Date(2020, 9, 1),
        endDate: new Date(2021, 5, 30),
        isCurrentJob: false,
        responsibilities: [
          'Developed Fiori applications for 2,000+ users internationally',
          'Implemented backend OData services for UI5 applications',
          'Advised business departments: gathered requirements, translated them into technical concepts and '
            + 'estimated effort',
          'Worked with scalable architectures for enterprise applications',
          'Ensured application quality through testing'
        ],
        sortOrder: 4
      },
      {
        id: 5,
        jobTitle: 'Junior SAP Developer',
        company: 'abat AG',
        location: 'Bremen, Germany',
        startDate: new Date(2019, 0, 1),
        endDate: new Date(2020, 8, 30),
        isCurrentJob: false,
        responsibilities: [
          'Learned SAP ABAP from the ground up in an enterprise environment',
          'Developed structured, maintainable solutions',
          'SAP development in manufacturing for Mercedes-Benz: logistics, production, MM/SD',
          'Delivered SAP S/4HANA implementation projects in manufacturing (automotive/logistics, Mercedes-Benz)'
        ],
        sortOrder: 5
      }
    ];
  }

  private createEducation(): Education[] {
    return [
      {
        id: 1,
        degree: 'M-Files Solution Course',
        institution: 'M-Files',
        location: 'Helsinki (online)',
        startDate: new Date(2025, 0, 1),
        description: 'Python development and C# desktop development: built desktop applications with C#/.NET and WPF',
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
        degree: 'SAP, M-Files and other courses',
        institution: 'udemy.com',
        location: 'online',
        startDate: new Date(2022, 0, 1),
        ongoing: true,
        sortOrder: 3
      },
      {
        id: 4,
        degree: 'Bachelor in Computer and Engineering Sciences',
        institution: 'UBT – Higher Education Institution',
        location: 'Prishtina, Kosovo',
        startDate: new Date(2014, 8, 1),
        endDate: new Date(2018, 5, 30),
        sortOrder: 4
      }
    ];
  }

  private createSkillCategories(): SkillCategory[] {
    let nextId = 1;
    const category = (categoryName: string, names: string[]): SkillCategory => ({
      categoryName,
      skills: names.map((name, index) => ({ id: nextId++, name, category: categoryName, sortOrder: index + 1 }))
    });

    return [
      category('AI Implementation', [
        'LLM integration (Claude API, OpenAI API)', 'Prompt engineering', 'RAG',
        'Vector databases (Pinecone, Weaviate)', 'LangChain', 'n8n', 'NeMo Guardrails',
        'Model Context Protocol (MCP servers & tools)', 'Agent-based systems',
        'Multi-agent orchestration / AI swarm architecture', 'LLM orchestration', 'Model evaluation',
        'AI output validation'
      ]),
      category('ERP & Banking Systems', [
        'ABAP OO', 'SAP Fiori', 'SAPUI5', 'OData', 'S/4HANA', 'Neptune DXP', 'Avaloq Core Platform'
      ]),
      category('Development', [
        'C#', '.NET', 'WPF', 'Angular', 'TypeScript', 'JavaScript', 'Python', 'PL/SQL',
        'PHP (basic)', 'WordPress (basic)', 'VBScript (basic)'
      ]),
      category('Databases', ['Oracle', 'MSSQL', 'MySQL', 'Firebase']),
      category('Application Management', [
        'Requirements analysis (IREB)', 'Backlog management', 'Release coordination', 'Testing/QA'
      ]),
      category('Automation & CI/CD', [
        'GitHub Actions (automated builds & tests)', 'GdUnit4 unit tests', 'Test-driven development'
      ]),
      category('Systems & Integration', ['M-Files', 'SAP ERP', 'Neptune DXP', 'API integration']),
      category('Support & Methods', [
        '2nd/3rd-level support', 'Error analysis', 'Incident resolution', 'Scrum', 'Kanban', 'Waterfall',
        'Requirements engineering'
      ]),
      category('Linux', [
        'Arch Linux (daily driver, KDE Plasma on Wayland)', 'Linux administration', 'Bash scripting',
        'System configuration', 'Package management'
      ]),
      category('Languages', ['German (C1, accent-free)', 'English (B2/C1)', 'Albanian (native)'])
    ];
  }

  private createContact(): Contact {
    return {
      id: 1,
      email: 'edonbytyqi@outlook.com',
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
      profileImageUrl: 'assets/images/profile.jpg',
      navigationItems: [
        { label: 'HOME', href: '#home', isActive: true },
        { label: 'ABOUT', href: '#about', isActive: false },
        { label: 'EXPERIENCE', href: '#experience', isActive: false },
        { label: 'EDUCATION', href: '#education', isActive: false },
        { label: 'SKILLS', href: '#skills', isActive: false },
        { label: 'CONTACT', href: '#contact', isActive: false }
      ],
      techStackItems: [
        // The .NET site isn't hosted yet, so link to its source. Swap in its URL once it is deployed.
        {
          label: '.NET',
          url: 'https://github.com/EdonBytyqi/cv-website/tree/main/dotnet-cv',
          isActive: false,
          description: '.NET version: source code on GitHub (opens in a new tab)',
          opensInNewTab: true
        },
        { label: 'Angular', url: './', isActive: true, description: 'Angular version of this CV (this page)', opensInNewTab: false }
      ],
      socialLinks: [
        { label: 'LinkedIn', url: 'https://linkedin.com/in/edon-bytyqi', icon: 'in' },
        { label: 'GitHub', url: 'https://github.com/EdonBytyqi', icon: 'gh' }
      ]
    };
  }
}
