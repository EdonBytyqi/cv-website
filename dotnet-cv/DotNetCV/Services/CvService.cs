using DotNetCV.Models;
using DotNetCV.ViewModels;

namespace DotNetCV.Services;

/// <summary>
/// Serves the CV content. The content is static, so it is built once and shared.
/// </summary>
public sealed class CvService : ICvService
{
    private static readonly CvViewModel Cv = CreateCv();

    public CvViewModel GetCv() => Cv;

    private static CvViewModel CreateCv() => new()
    {
        AboutMe = new AboutMe
        {
            FullName = "Edon Bytyqi",
            Title = "Software Developer",
            Bio = "Experienced software developer specializing in Avaloq, SAP, and .NET development. Passionate about creating efficient and scalable solutions.",
            ProfileImageUrl = "/images/profile.jpg"
        },
        Experiences =
        [
            new Experience
            {
                JobTitle = "Software Developer - SAP",
                Company = "BSI SA",
                Location = "Lugano, Switzerland",
                StartDate = new DateOnly(2024, 5, 1),
                Responsibilities =
                [
                    "Develop and maintain SAP applications",
                    "Collaborate with cross-functional teams",
                    "Implement new features and optimize existing code",
                    "Participate in code reviews and knowledge sharing"
                ]
            },
            new Experience
            {
                JobTitle = "Software Developer - Avaloq",
                Company = "BSI SA",
                Location = "Lugano, Switzerland",
                StartDate = new DateOnly(2023, 7, 1),
                EndDate = new DateOnly(2024, 4, 30),
                Responsibilities =
                [
                    "Developed banking solutions using Avaloq platform",
                    "Customized workflows and business processes",
                    "Maintained high code quality standards",
                    "Worked in Agile/Scrum environment"
                ]
            }
        ],
        Education =
        [
            new Education
            {
                Degree = "M-Files Solution Course",
                Institution = "M-Files",
                Location = "Helsinki (Online)",
                StartDate = new DateOnly(2025, 1, 1),
                Description = "M-Files solution architecture and implementation"
            },
            new Education
            {
                Degree = "Avaloq Certified Professional",
                Institution = "Avaloq Group AG",
                Location = "Zürich",
                StartDate = new DateOnly(2024, 1, 1),
                Grade = "Diploma",
                Description = "Avaloq platform development and customization"
            },
            new Education
            {
                Degree = "Bachelor in Computer & Engineering Sciences",
                Institution = "UBT - Higher Education Institution",
                Location = "Prishtina, Kosovo",
                StartDate = new DateOnly(2014, 9, 1),
                EndDate = new DateOnly(2018, 6, 30),
                Grade = "7.69/10",
                Description = "Comprehensive computer science and engineering program"
            }
        ],
        SkillCategories =
        [
            new SkillCategory { CategoryName = "ERP & Banking Systems", Skills = ["SAP ABAP", "SAP UI5/Fiori", "SAP HCM/MM", "Avaloq Core Platform"] },
            new SkillCategory { CategoryName = "Development", Skills = ["C#", ".NET", "Angular", "JavaScript", "TypeScript", "VBScript", "PL/SQL"] },
            new SkillCategory { CategoryName = "Databases", Skills = ["Oracle", "MSSQL", "MySQL", "Firebase"] },
            new SkillCategory { CategoryName = "Systems & Integration", Skills = ["M-Files", "SAP ERP", "Neptune DXP", "API Integration"] }
        ],
        Contact = new Contact
        {
            Email = "edonbytyqi@outlook.com",
            Phone = "+41 XX XXX XX XX",
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
                new TechStackItem(".NET", "http://localhost:5000", IsActive: true),
                new TechStackItem("Angular", "http://localhost:4202", IsActive: false)
            ],
            SocialLinks =
            [
                new SocialLink("LinkedIn", "https://linkedin.com/in/edon-bytyqi", "in"),
                new SocialLink("GitHub", "https://github.com/EdonBytyqi", "gh")
            ]
        }
    };
}
