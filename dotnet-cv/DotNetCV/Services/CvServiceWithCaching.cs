using DotNetCV.Configuration;
using DotNetCV.Models;
using DotNetCV.ViewModels;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;

namespace DotNetCV.Services;

/// <summary>
/// CV service implementation with memory caching and error handling
/// </summary>
public sealed class CvServiceWithCaching : ICvService
{
    private readonly IMemoryCache _cache;
    private readonly ILogger<CvServiceWithCaching> _logger;
    private readonly CvOptions _options;

    private const string AboutMeCacheKey = "cv_aboutme";
    private const string ExperiencesCacheKey = "cv_experiences";
    private const string EducationCacheKey = "cv_education";
    private const string SkillsCacheKey = "cv_skills";
    private const string ContactCacheKey = "cv_contact";
    private const string SidebarCacheKey = "cv_sidebar";
    private const string CompleteCvCacheKey = "cv_complete";

    public CvServiceWithCaching(
        IMemoryCache cache,
        ILogger<CvServiceWithCaching> logger,
        IOptions<CvOptions> options)
    {
        ArgumentNullException.ThrowIfNull(cache);
        ArgumentNullException.ThrowIfNull(logger);
        ArgumentNullException.ThrowIfNull(options);

        _cache = cache;
        _logger = logger;
        _options = options.Value;
    }

    public async Task<AboutMe> GetAboutMeAsync()
    {
        try
        {
            return await GetOrCreateCachedAsync(AboutMeCacheKey, CreateAboutMeAsync);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving AboutMe data");
            throw;
        }
    }

    public async Task<List<Experience>> GetExperiencesAsync()
    {
        try
        {
            return await GetOrCreateCachedAsync(ExperiencesCacheKey, CreateExperiencesAsync);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving Experiences data");
            throw;
        }
    }

    public async Task<List<Education>> GetEducationAsync()
    {
        try
        {
            return await GetOrCreateCachedAsync(EducationCacheKey, CreateEducationAsync);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving Education data");
            throw;
        }
    }

    public async Task<List<SkillCategory>> GetSkillCategoriesAsync()
    {
        try
        {
            return await GetOrCreateCachedAsync(SkillsCacheKey, CreateSkillCategoriesAsync);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving Skills data");
            throw;
        }
    }

    public async Task<Contact> GetContactAsync()
    {
        try
        {
            return await GetOrCreateCachedAsync(ContactCacheKey, CreateContactAsync);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving Contact data");
            throw;
        }
    }

    public async Task<SidebarViewModel> GetSidebarAsync()
    {
        try
        {
            return await GetOrCreateCachedAsync(SidebarCacheKey, CreateSidebarAsync);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving Sidebar data");
            throw;
        }
    }

    public async Task<CvViewModel> GetCompleteCvAsync()
    {
        try
        {
            return await GetOrCreateCachedAsync(CompleteCvCacheKey, CreateCompleteCvAsync);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving complete CV data");
            throw;
        }
    }

    private async Task<T> GetOrCreateCachedAsync<T>(string cacheKey, Func<Task<T>> factory)
    {
        if (!_options.EnableCaching)
        {
            _logger.LogDebug("Caching disabled, fetching fresh data for {CacheKey}", cacheKey);
            return await factory();
        }

        return await _cache.GetOrCreateAsync(cacheKey, async entry =>
        {
            entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(_options.CacheDurationMinutes);
            _logger.LogDebug("Cache miss for {CacheKey}, creating new entry", cacheKey);
            return await factory();
        }) ?? await factory();
    }

    private Task<AboutMe> CreateAboutMeAsync()
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

    private Task<List<Experience>> CreateExperiencesAsync()
    {
        var experiences = new List<Experience>
        {
            new()
            {
                Id = 1,
                JobTitle = "Software Developer - SAP",
                Company = "BSI SA",
                Location = "Lugano, Switzerland",
                StartDate = new DateTime(2024, 5, 1),
                EndDate = null,
                IsCurrentJob = true,
                Responsibilities =
                [
                    "Develop and maintain SAP applications",
                    "Collaborate with cross-functional teams",
                    "Implement new features and optimize existing code",
                    "Participate in code reviews and knowledge sharing"
                ],
                SortOrder = 1
            },
            new()
            {
                Id = 2,
                JobTitle = "Software Developer - Avaloq",
                Company = "BSI SA",
                Location = "Lugano, Switzerland",
                StartDate = new DateTime(2023, 7, 1),
                EndDate = new DateTime(2024, 4, 30),
                IsCurrentJob = false,
                Responsibilities =
                [
                    "Developed banking solutions using Avaloq platform",
                    "Customized workflows and business processes",
                    "Maintained high code quality standards",
                    "Worked in Agile/Scrum environment"
                ],
                SortOrder = 2
            }
        };

        return Task.FromResult(experiences);
    }

    private Task<List<Education>> CreateEducationAsync()
    {
        var education = new List<Education>
        {
            new()
            {
                Id = 1,
                Degree = "M-Files Solution Course",
                Institution = "M-Files",
                Location = "Helsinki (Online)",
                StartDate = new DateTime(2025, 1, 1),
                EndDate = null,
                Grade = null,
                Description = "M-Files solution architecture and implementation",
                SortOrder = 1
            },
            new()
            {
                Id = 2,
                Degree = "Avaloq Certified Professional",
                Institution = "Avaloq Group AG",
                Location = "Zürich",
                StartDate = new DateTime(2024, 1, 1),
                EndDate = null,
                Grade = "Diploma",
                Description = "Avaloq platform development and customization",
                SortOrder = 2
            },
            new()
            {
                Id = 3,
                Degree = "Bachelor in Computer & Engineering Sciences",
                Institution = "UBT - Higher Education Institution",
                Location = "Prishtina, Kosovo",
                StartDate = new DateTime(2014, 9, 1),
                EndDate = new DateTime(2018, 6, 30),
                Grade = "7.69/10",
                Description = "Comprehensive computer science and engineering program",
                SortOrder = 3
            }
        };

        return Task.FromResult(education);
    }

    private Task<List<SkillCategory>> CreateSkillCategoriesAsync()
    {
        var skillCategories = new List<SkillCategory>
        {
            new()
            {
                CategoryName = "ERP & Banking Systems",
                Skills =
                [
                    new Skill { Id = 1, Name = "SAP ABAP", Category = "ERP & Banking Systems", ProficiencyLevel = 5, SortOrder = 1 },
                    new Skill { Id = 2, Name = "SAP UI5/Fiori", Category = "ERP & Banking Systems", ProficiencyLevel = 5, SortOrder = 2 },
                    new Skill { Id = 3, Name = "SAP HCM/MM", Category = "ERP & Banking Systems", ProficiencyLevel = 4, SortOrder = 3 },
                    new Skill { Id = 4, Name = "Avaloq Core Platform", Category = "ERP & Banking Systems", ProficiencyLevel = 4, SortOrder = 4 }
                ]
            },
            new()
            {
                CategoryName = "Development",
                Skills =
                [
                    new Skill { Id = 5, Name = "C#", Category = "Development", ProficiencyLevel = 5, SortOrder = 1 },
                    new Skill { Id = 6, Name = ".NET", Category = "Development", ProficiencyLevel = 5, SortOrder = 2 },
                    new Skill { Id = 7, Name = "Angular", Category = "Development", ProficiencyLevel = 4, SortOrder = 3 },
                    new Skill { Id = 8, Name = "JavaScript", Category = "Development", ProficiencyLevel = 4, SortOrder = 4 },
                    new Skill { Id = 9, Name = "TypeScript", Category = "Development", ProficiencyLevel = 4, SortOrder = 5 },
                    new Skill { Id = 10, Name = "VBScript", Category = "Development", ProficiencyLevel = 3, SortOrder = 6 },
                    new Skill { Id = 11, Name = "PL/SQL", Category = "Development", ProficiencyLevel = 4, SortOrder = 7 }
                ]
            },
            new()
            {
                CategoryName = "Databases",
                Skills =
                [
                    new Skill { Id = 12, Name = "Oracle", Category = "Databases", ProficiencyLevel = 4, SortOrder = 1 },
                    new Skill { Id = 13, Name = "MSSQL", Category = "Databases", ProficiencyLevel = 4, SortOrder = 2 },
                    new Skill { Id = 14, Name = "MySQL", Category = "Databases", ProficiencyLevel = 4, SortOrder = 3 },
                    new Skill { Id = 15, Name = "Firebase", Category = "Databases", ProficiencyLevel = 3, SortOrder = 4 }
                ]
            },
            new()
            {
                CategoryName = "Systems & Integration",
                Skills =
                [
                    new Skill { Id = 16, Name = "M-Files", Category = "Systems & Integration", ProficiencyLevel = 4, SortOrder = 1 },
                    new Skill { Id = 17, Name = "SAP ERP", Category = "Systems & Integration", ProficiencyLevel = 5, SortOrder = 2 },
                    new Skill { Id = 18, Name = "Neptune DXP", Category = "Systems & Integration", ProficiencyLevel = 4, SortOrder = 3 },
                    new Skill { Id = 19, Name = "API Integration", Category = "Systems & Integration", ProficiencyLevel = 4, SortOrder = 4 }
                ]
            }
        };

        return Task.FromResult(skillCategories);
    }

    private Task<Contact> CreateContactAsync()
    {
        var contact = new Contact
        {
            Id = 1,
            Email = "edonbytyqi@outlook.com",
            Phone = "076 567 85 58",
            Location = "6020 Emmenbrücke, Switzerland",
            LinkedInUrl = "https://linkedin.com/in/edon-bytyqi",
            GitHubUrl = "https://github.com/EdonBytyqi",
            WebsiteUrl = "https://edonbytyqi.com"
        };

        return Task.FromResult(contact);
    }

    private Task<SidebarViewModel> CreateSidebarAsync()
    {
        var sidebar = new SidebarViewModel
        {
            Name = "EDON BYTYQI",
            ProfileImageUrl = "/assets/images/profile.jpg",
            NavigationItems =
            [
                new NavigationItem { Label = "HOME", Href = "#home", IsActive = true },
                new NavigationItem { Label = "ABOUT", Href = "#about", IsActive = false },
                new NavigationItem { Label = "EXPERIENCE", Href = "#experience", IsActive = false },
                new NavigationItem { Label = "EDUCATION", Href = "#education", IsActive = false },
                new NavigationItem { Label = "SKILLS", Href = "#skills", IsActive = false },
                new NavigationItem { Label = "CONTACT", Href = "#contact", IsActive = false }
            ],
            TechStackItems =
            [
                new TechStackItem { Label = ".NET", Url = "http://localhost:5000", IsActive = true },
                new TechStackItem { Label = "Angular", Url = "http://localhost:4202", IsActive = false }
            ],
            SocialLinks =
            [
                new SocialLink { Label = "LinkedIn", Url = "https://linkedin.com/in/edon-bytyqi", Icon = "in" },
                new SocialLink { Label = "GitHub", Url = "https://github.com/EdonBytyqi", Icon = "gh" }
            ]
        };

        return Task.FromResult(sidebar);
    }

    private async Task<CvViewModel> CreateCompleteCvAsync()
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
