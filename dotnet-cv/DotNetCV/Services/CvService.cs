using DotNetCV.Models;
using DotNetCV.ViewModels;

namespace DotNetCV.Services;

public class CvService : ICvService
{
    // For now, this returns hardcoded data. Later we'll replace this with Supabase calls

    public Task<AboutMe> GetAboutMeAsync()
    {
        var aboutMe = new AboutMe
        {
            Id = 1,
            FullName = "Edon Bytyqi",
            Title = "Software Developer",
            Bio = "Experienced software developer specializing in Avaloq, SAP, and .NET development. Passionate about creating efficient and scalable solutions.",
            ProfileImageUrl = "/assets/images/profile.jpg",
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        return Task.FromResult(aboutMe);
    }

    public Task<List<Experience>> GetExperiencesAsync()
    {
        var experiences = new List<Experience>
        {
            new Experience
            {
                Id = 1,
                JobTitle = "Software Developer - SAP",
                Company = "BSI SA",
                Location = "Lugano, Switzerland",
                StartDate = new DateTime(2024, 5, 1),
                EndDate = null,
                IsCurrentJob = true,
                Responsibilities = new List<string>
                {
                    "Develop and maintain SAP applications",
                    "Collaborate with cross-functional teams",
                    "Implement new features and optimize existing code",
                    "Participate in code reviews and knowledge sharing"
                },
                SortOrder = 1
            },
            new Experience
            {
                Id = 2,
                JobTitle = "Software Developer - Avaloq",
                Company = "BSI SA",
                Location = "Lugano, Switzerland",
                StartDate = new DateTime(2023, 7, 1),
                EndDate = new DateTime(2024, 4, 30),
                IsCurrentJob = false,
                Responsibilities = new List<string>
                {
                    "Developed banking solutions using Avaloq platform",
                    "Customized workflows and business processes",
                    "Maintained high code quality standards",
                    "Worked in Agile/Scrum environment"
                },
                SortOrder = 2
            }
        };

        return Task.FromResult(experiences);
    }

    public Task<List<Education>> GetEducationAsync()
    {
        var education = new List<Education>
        {
            new Education
            {
                Id = 1,
                Degree = "Computer Science Apprenticeship",
                Institution = "SPAI - Scuola Professionale Artigianale e Industriale",
                Location = "Lugano, Switzerland",
                StartDate = new DateTime(2019, 8, 1),
                EndDate = new DateTime(2023, 6, 30),
                Grade = "Excellent",
                Description = "Comprehensive program covering software development, databases, and system design",
                SortOrder = 1
            }
        };

        return Task.FromResult(education);
    }

    public Task<List<SkillCategory>> GetSkillCategoriesAsync()
    {
        var skillCategories = new List<SkillCategory>
        {
            new SkillCategory
            {
                CategoryName = "Programming Languages",
                Skills = new List<Skill>
                {
                    new Skill { Id = 1, Name = "C#", Category = "Programming Languages", ProficiencyLevel = 5, SortOrder = 1 },
                    new Skill { Id = 2, Name = "TypeScript", Category = "Programming Languages", ProficiencyLevel = 4, SortOrder = 2 },
                    new Skill { Id = 3, Name = "JavaScript", Category = "Programming Languages", ProficiencyLevel = 4, SortOrder = 3 },
                    new Skill { Id = 4, Name = "SQL", Category = "Programming Languages", ProficiencyLevel = 4, SortOrder = 4 }
                }
            },
            new SkillCategory
            {
                CategoryName = "Frameworks & Technologies",
                Skills = new List<Skill>
                {
                    new Skill { Id = 5, Name = ".NET Core", Category = "Frameworks & Technologies", ProficiencyLevel = 5, SortOrder = 1 },
                    new Skill { Id = 6, Name = "Angular", Category = "Frameworks & Technologies", ProficiencyLevel = 4, SortOrder = 2 },
                    new Skill { Id = 7, Name = "Entity Framework", Category = "Frameworks & Technologies", ProficiencyLevel = 4, SortOrder = 3 },
                    new Skill { Id = 8, Name = "ASP.NET MVC", Category = "Frameworks & Technologies", ProficiencyLevel = 5, SortOrder = 4 }
                }
            },
            new SkillCategory
            {
                CategoryName = "Tools & Platforms",
                Skills = new List<Skill>
                {
                    new Skill { Id = 9, Name = "Git", Category = "Tools & Platforms", ProficiencyLevel = 4, SortOrder = 1 },
                    new Skill { Id = 10, Name = "Azure", Category = "Tools & Platforms", ProficiencyLevel = 3, SortOrder = 2 },
                    new Skill { Id = 11, Name = "Docker", Category = "Tools & Platforms", ProficiencyLevel = 3, SortOrder = 3 },
                    new Skill { Id = 12, Name = "Visual Studio", Category = "Tools & Platforms", ProficiencyLevel = 5, SortOrder = 4 }
                }
            },
            new SkillCategory
            {
                CategoryName = "Domain Expertise",
                Skills = new List<Skill>
                {
                    new Skill { Id = 13, Name = "Avaloq", Category = "Domain Expertise", ProficiencyLevel = 4, SortOrder = 1 },
                    new Skill { Id = 14, Name = "SAP", Category = "Domain Expertise", ProficiencyLevel = 4, SortOrder = 2 },
                    new Skill { Id = 15, Name = "Banking Systems", Category = "Domain Expertise", ProficiencyLevel = 4, SortOrder = 3 }
                }
            }
        };

        return Task.FromResult(skillCategories);
    }

    public Task<Contact> GetContactAsync()
    {
        var contact = new Contact
        {
            Id = 1,
            Email = "edonbytyqi@outlook.com",
            Phone = "+41 XX XXX XX XX",
            Location = "Lugano, Switzerland",
            LinkedInUrl = "https://linkedin.com/in/edon-bytyqi",
            GitHubUrl = "https://github.com/EdonBytyqi",
            WebsiteUrl = "https://edonbytyqi.com"
        };

        return Task.FromResult(contact);
    }

    public Task<SidebarViewModel> GetSidebarAsync()
    {
        var sidebar = new SidebarViewModel
        {
            Name = "EDON BYTYQI",
            ProfileImageUrl = "/assets/images/profile.jpg",
            NavigationItems = new List<NavigationItem>
            {
                new NavigationItem { Label = "HOME", Href = "#home", IsActive = true },
                new NavigationItem { Label = "ABOUT", Href = "#about", IsActive = false },
                new NavigationItem { Label = "EXPERIENCE", Href = "#experience", IsActive = false },
                new NavigationItem { Label = "EDUCATION", Href = "#education", IsActive = false },
                new NavigationItem { Label = "SKILLS", Href = "#skills", IsActive = false },
                new NavigationItem { Label = "CONTACT", Href = "#contact", IsActive = false }
            },
            TechStackItems = new List<TechStackItem>
            {
                new TechStackItem { Label = ".NET", Url = "http://localhost:5000", IsActive = true },
                new TechStackItem { Label = "Angular", Url = "http://localhost:4202", IsActive = false }
            },
            SocialLinks = new List<SocialLink>
            {
                new SocialLink { Label = "LinkedIn", Url = "https://linkedin.com/in/edon-bytyqi", Icon = "in" },
                new SocialLink { Label = "GitHub", Url = "https://github.com/EdonBytyqi", Icon = "gh" }
            }
        };

        return Task.FromResult(sidebar);
    }

    public async Task<CvViewModel> GetCompleteCvAsync()
    {
        var viewModel = new CvViewModel
        {
            AboutMe = await GetAboutMeAsync(),
            Experiences = await GetExperiencesAsync(),
            Education = await GetEducationAsync(),
            SkillCategories = await GetSkillCategoriesAsync(),
            Contact = await GetContactAsync(),
            Sidebar = await GetSidebarAsync()
        };

        return viewModel;
    }
}
