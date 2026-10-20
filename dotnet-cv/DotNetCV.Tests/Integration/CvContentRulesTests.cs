using DotNetCV.Models;
using DotNetCV.Services;
using DotNetCV.ViewModels;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;

namespace DotNetCV.Tests.Integration;

/// <summary>
/// Spec 001: rules that depend on the content (optional phone, ongoing education, stack links).
/// </summary>
public sealed class CvContentRulesTests(WebApplicationFactory<Program> factory) : IClassFixture<WebApplicationFactory<Program>>
{
    private static readonly CvViewModel RealCv = new CvService().GetCv();

    // Made-up details so these tests never depend on (or leak) real contact data
    private static readonly Contact ExampleContact = new()
    {
        Email = "someone@example.com",
        Location = "Zürich, Switzerland",
        LinkedInUrl = "https://linkedin.com/in/example",
        GitHubUrl = "https://github.com/example"
    };

    [Theory(DisplayName = "[AC-001-08] Shows a phone entry only when a number is set")]
    [InlineData(null, false)]
    [InlineData("044 123 45 67", true)]
    public async Task ShowsPhoneOnlyWhenSet(string? phone, bool expectPhone)
    {
        var cv = WithParts(contact: new Contact
        {
            Email = ExampleContact.Email,
            Phone = phone,
            Location = ExampleContact.Location,
            LinkedInUrl = ExampleContact.LinkedInUrl,
            GitHubUrl = ExampleContact.GitHubUrl
        });

        var page = await HtmlPage.LoadAsync(WithCv(cv));
        var contactText = page.QuerySelector("#contact").Text();

        Assert.Equal(expectPhone, contactText.Contains("Phone"));
        if (expectPhone)
        {
            Assert.Contains("044 123 45 67", contactText);
        }
    }

    [Fact(DisplayName = "[AC-001-09] Shows 'since <year>' for ongoing education and the description")]
    public async Task ShowsOngoingEducation()
    {
        var cv = WithParts(education:
        [
            new Education
            {
                Degree = "Online courses",
                Institution = "Example Academy",
                Location = "online",
                StartDate = new DateOnly(2022, 1, 1),
                IsOngoing = true,
                Description = "Cloud and data courses"
            }
        ]);

        var page = await HtmlPage.LoadAsync(WithCv(cv));
        var educationText = page.QuerySelector("#education").Text();

        Assert.Contains("since 2022", educationText);
        Assert.Contains("Cloud and data courses", educationText);
    }

    [Fact(DisplayName = "[AC-001-10] Has no localhost stack links and labels where each one goes")]
    public async Task StackLinksAreSafe()
    {
        var page = await HtmlPage.LoadAsync(factory);

        var links = page.QuerySelectorAll(".tech-switch a");
        Assert.NotEmpty(links);
        Assert.All(links, link =>
        {
            Assert.DoesNotContain("localhost", link.GetAttribute("href"));
            Assert.False(string.IsNullOrWhiteSpace(link.GetAttribute("aria-label")));
        });

        var dotNet = Assert.Single(links, link => link.Text() == ".NET");
        Assert.Equal("page", dotNet.GetAttribute("aria-current"));

        Assert.All(links.Where(link => link.GetAttribute("href")!.StartsWith("http")), link =>
        {
            Assert.Equal("_blank", link.GetAttribute("target"));
            Assert.Equal("noopener noreferrer", link.GetAttribute("rel"));
            Assert.Contains("opens in a new tab", link.GetAttribute("aria-label"));
        });
    }

    private WebApplicationFactory<Program> WithCv(CvViewModel cv) =>
        factory.WithWebHostBuilder(builder =>
            builder.ConfigureServices(services => services.AddSingleton<ICvService>(new FixedCvService(cv))));

    private static CvViewModel WithParts(Contact? contact = null, IReadOnlyList<Education>? education = null) => new()
    {
        AboutMe = RealCv.AboutMe,
        Experiences = RealCv.Experiences,
        Education = education ?? RealCv.Education,
        SkillCategories = RealCv.SkillCategories,
        Contact = contact ?? RealCv.Contact,
        Sidebar = RealCv.Sidebar
    };

    private sealed class FixedCvService(CvViewModel cv) : ICvService
    {
        public CvViewModel GetCv() => cv;
    }
}
