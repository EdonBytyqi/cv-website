using DotNetCV.Configuration;
using DotNetCV.Models;
using DotNetCV.ViewModels;
using Microsoft.Extensions.Options;

namespace DotNetCV.Services;

/// <summary>
/// Serves the CV content. The content is static, so it is built once per (singleton) instance.
/// </summary>
/// <remarks>
/// Content mirrors the CV (translated to English) and the Angular version. This repository
/// is public: never add a phone number, street address, date of birth or permit details.
/// </remarks>
public sealed class CvService(IOptions<StackLinkOptions> stackLinks) : ICvService
{
    private readonly CvViewModel _cv = CreateCv(
        (stackLinks ?? throw new ArgumentNullException(nameof(stackLinks))).Value.AngularUrl);

    public CvViewModel GetCv() => _cv;

    private static CvViewModel CreateCv(string angularUrl) => new()
    {
        AboutMe = new AboutMe
        {
            FullName = "Edon Bytyqi",
            Title = "Software Developer · SAP · GenAI",
            Bio = "Software developer with 6+ years in enterprise IT: SAP development, API integration and production GenAI systems. "
                + "I specialise in SAP (Fiori, SAPUI5, ABAP OO, OData), the Avaloq Core Platform, C#/.NET and production AI solutions "
                + "with LangChain, RAG, agentic AI, multi-agent orchestration and MCP. I design RAG architectures, work on prompt "
                + "engineering and guardrails, and connect LLMs to real workflows. I like numbers and data analysis, work in a "
                + "structured way that keeps to agreed timelines, and am a friendly, helpful team player.",
            ProfileImageUrl = "/images/profile.jpg"
        },
        Experiences =
        [
            new Experience
            {
                JobTitle = "Professional Development & Independent Projects",
                Company = "AI integration, software development",
                Location = "",
                StartDate = new DateOnly(2025, 4, 1),
                Responsibilities =
                [
                    "Anthropic Academy certification programme 2026 (Anthropic / Skilljar): Claude 101, Claude Platform 101, "
                        + "Claude Code 101, Claude Code in Action, Introduction to Model Context Protocol, Model Context Protocol: Advanced Topics",
                    "Designed and built RAG systems for knowledge management: chunking, embeddings and retrieval with LangChain, "
                        + "vector databases (Pinecone/Weaviate) and the Claude API",
                    "Built multi-agent systems with orchestrated workflows: agent delegation, tool use and coordination via the "
                        + "Model Context Protocol (MCP)",
                    "Built AI automations with n8n, e.g. RSS aggregation with LLM-generated structured digests, including error "
                        + "handling and cost control",
                    "Prompt engineering, guardrails (NeMo) and model evaluation as everyday practice"
                ]
            },
            new Experience
            {
                JobTitle = "Junior Avaloq Customization Professional",
                Company = "Luzerner Kantonalbank AG",
                Location = "Lucerne, Switzerland",
                StartDate = new DateOnly(2023, 9, 1),
                EndDate = new DateOnly(2025, 3, 31),
                Responsibilities =
                [
                    "Main responsibility for operating and technically parameterising the Avaloq core banking platform",
                    "Developed and optimised complex SQL queries on Oracle for reporting, data analysis and quality assurance",
                    "Derived requirements and specifications from business departments (Compliance, Operations, Controlling) "
                        + "and translated them into technical solutions",
                    "Provided application support for incidents and change requests (1st and 2nd level)",
                    "Accompanied several go-lives from planning to production rollout"
                ]
            },
            new Experience
            {
                JobTitle = "SAP ERP Developer",
                Company = "Ringier AG",
                Location = "Zofingen, Switzerland",
                StartDate = new DateOnly(2021, 12, 1),
                EndDate = new DateOnly(2023, 8, 31),
                Responsibilities =
                [
                    "In-depth SAP ABAP development in an ERP environment",
                    "REST API integration between SAP and Coupa (B2B), modernised from file transfer to API and running "
                        + "error-free in production",
                    "Worked with web technologies (REST APIs, JavaScript, TypeScript)",
                    "Defined developer guidelines for Neptune DXP (SAP low-code)",
                    "Built Google Apps Script automations for status notifications and IT account validation"
                ]
            },
            new Experience
            {
                JobTitle = "Junior SAPUI5 Fiori Developer",
                Company = "Axel Springer",
                Location = "Berlin, Germany",
                StartDate = new DateOnly(2020, 10, 1),
                EndDate = new DateOnly(2021, 6, 30),
                Responsibilities =
                [
                    "Developed Fiori applications for 2,000+ users internationally",
                    "Implemented backend OData services for UI5 applications",
                    "Advised business departments: gathered requirements, translated them into technical concepts and "
                        + "estimated effort",
                    "Worked with scalable architectures for enterprise applications",
                    "Ensured application quality through testing"
                ]
            },
            new Experience
            {
                JobTitle = "Junior SAP Developer",
                Company = "abat AG",
                Location = "Bremen, Germany",
                StartDate = new DateOnly(2019, 1, 1),
                EndDate = new DateOnly(2020, 9, 30),
                Responsibilities =
                [
                    "Learned SAP ABAP from the ground up in an enterprise environment",
                    "Developed structured, maintainable solutions",
                    "SAP development in manufacturing for Mercedes-Benz: logistics, production, MM/SD",
                    "Delivered SAP S/4HANA implementation projects in manufacturing (automotive/logistics, Mercedes-Benz)"
                ]
            }
        ],
        Education =
        [
            new Education
            {
                Degree = "M-Files Solution Course",
                Institution = "M-Files",
                Location = "Helsinki (online)",
                StartDate = new DateOnly(2025, 1, 1),
                Description = "Python development and C# desktop development: built desktop applications with C#/.NET and WPF"
            },
            new Education
            {
                Degree = "Avaloq Certified Professional",
                Institution = "Avaloq Group AG",
                Location = "Zürich",
                StartDate = new DateOnly(2024, 1, 1),
                Grade = "Diploma"
            },
            new Education
            {
                Degree = "SAP, M-Files and other courses",
                Institution = "udemy.com",
                Location = "online",
                StartDate = new DateOnly(2022, 1, 1),
                IsOngoing = true
            },
            new Education
            {
                Degree = "Bachelor in Computer and Engineering Sciences",
                Institution = "UBT – Higher Education Institution",
                Location = "Prishtina, Kosovo",
                StartDate = new DateOnly(2014, 9, 1),
                EndDate = new DateOnly(2018, 6, 30)
            }
        ],
        SkillCategories =
        [
            new SkillCategory
            {
                CategoryName = "AI Implementation",
                Skills =
                [
                    "LLM integration (Claude API, OpenAI API)", "Prompt engineering", "RAG",
                    "Vector databases (Pinecone, Weaviate)", "LangChain", "n8n", "NeMo Guardrails",
                    "Model Context Protocol (MCP servers & tools)", "Agent-based systems",
                    "Multi-agent orchestration / AI swarm architecture", "LLM orchestration", "Model evaluation",
                    "AI output validation"
                ]
            },
            new SkillCategory
            {
                CategoryName = "ERP & Banking Systems",
                Skills = ["ABAP OO", "SAP Fiori", "SAPUI5", "OData", "S/4HANA", "Neptune DXP", "Avaloq Core Platform"]
            },
            new SkillCategory
            {
                CategoryName = "Development",
                Skills =
                [
                    "C#", ".NET", "WPF", "Angular", "TypeScript", "JavaScript", "Python", "PL/SQL",
                    "PHP (basic)", "WordPress (basic)", "VBScript (basic)"
                ]
            },
            new SkillCategory { CategoryName = "Databases", Skills = ["Oracle", "MSSQL", "MySQL", "Firebase"] },
            new SkillCategory
            {
                CategoryName = "Application Management",
                Skills = ["Requirements analysis (IREB)", "Backlog management", "Release coordination", "Testing/QA"]
            },
            new SkillCategory
            {
                CategoryName = "Automation & CI/CD",
                Skills = ["GitHub Actions (automated builds & tests)", "GdUnit4 unit tests", "Test-driven development"]
            },
            new SkillCategory
            {
                CategoryName = "Systems & Integration",
                Skills = ["M-Files", "SAP ERP", "Neptune DXP", "API integration"]
            },
            new SkillCategory
            {
                CategoryName = "Support & Methods",
                Skills =
                [
                    "2nd/3rd-level support", "Error analysis", "Incident resolution", "Scrum", "Kanban", "Waterfall",
                    "Requirements engineering"
                ]
            },
            new SkillCategory
            {
                CategoryName = "Linux",
                Skills =
                [
                    "Arch Linux (daily driver, KDE Plasma on Wayland)", "Linux administration", "Bash scripting",
                    "System configuration", "Package management"
                ]
            },
            new SkillCategory
            {
                CategoryName = "Languages",
                Skills = ["German (C1, accent-free)", "English (B2/C1)", "Albanian (native)"]
            }
        ],
        Contact = new Contact
        {
            Email = "edonbytyqi@outlook.com",
            Location = "6020 Emmenbrücke, Switzerland",
            LinkedInUrl = "https://linkedin.com/in/edon-bytyqi",
            GitHubUrl = "https://github.com/EdonBytyqi",
            WebsiteUrl = "https://edonbytyqi.com"
        },
        Sidebar = new SidebarViewModel
        {
            Name = "EDON BYTYQI",
            NavigationItems =
            [
                new NavigationItem("HOME", "#home"),
                new NavigationItem("ABOUT", "#about"),
                new NavigationItem("EXPERIENCE", "#experience"),
                new NavigationItem("EDUCATION", "#education"),
                new NavigationItem("SKILLS", "#skills"),
                new NavigationItem("CONTACT", "#contact")
            ],
            TechStackItems =
            [
                new TechStackItem(".NET", "/", IsActive: true, ".NET version of this CV (this page)", OpensInNewTab: false),
                new TechStackItem(
                    "Angular",
                    angularUrl,
                    IsActive: false,
                    "Angular version of this CV (opens in a new tab)",
                    OpensInNewTab: true)
            ],
            SocialLinks =
            [
                new SocialLink("LinkedIn", "https://linkedin.com/in/edon-bytyqi", "in"),
                new SocialLink("GitHub", "https://github.com/EdonBytyqi", "gh")
            ]
        }
    };
}
