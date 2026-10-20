using DotNetCV.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;

namespace DotNetCV.Tests.Integration;

/// <summary>
/// Rules that depend on the content: optional phone and ongoing education (feature 001), stack links (feature 004).
/// </summary>
public sealed class CvContentRulesTests(WebApplicationFactory<Program> factory) : IClassFixture<WebApplicationFactory<Program>>
{
    // Made-up details so these tests never depend on (or leak) real contact data
    private static readonly Contact ExampleContact = new()
    {
        Email = "someone@example.com",
        Location = "Zürich, Switzerland",
        LinkedInUrl = "https://linkedin.com/in/example",
        GitHubUrl = "https://github.com/example"
    };

    [Theory(DisplayName = "[001:FR-011] Shows a phone entry only when a number is set")]
    [InlineData(null, false)]
    [InlineData("044 123 45 67", true)]
    public async Task ShowsPhoneOnlyWhenSet(string? phone, bool expectPhone)
    {
        var cv = CvOverrides.WithParts(contact: new Contact
        {
            Email = ExampleContact.Email,
            Phone = phone,
            Location = ExampleContact.Location,
            LinkedInUrl = ExampleContact.LinkedInUrl,
            GitHubUrl = ExampleContact.GitHubUrl
        });

        var page = await HtmlPage.LoadAsync(factory.WithCv(cv));
        var contactText = page.QuerySelector("#contact").Text();

        Assert.Equal(expectPhone, contactText.Contains("Phone"));
        if (expectPhone)
        {
            Assert.Contains("044 123 45 67", contactText);
        }
    }

    [Fact(DisplayName = "[001:FR-006] Shows 'since <year>' for ongoing education and the description")]
    public async Task ShowsOngoingEducation()
    {
        var cv = CvOverrides.WithParts(education:
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

        var page = await HtmlPage.LoadAsync(factory.WithCv(cv));
        var educationText = page.QuerySelector("#education").Text();

        Assert.Contains("since 2022", educationText);
        Assert.Contains("Cloud and data courses", educationText);
    }

    [Fact(DisplayName = "[004:FR-011] Marks .NET as current and labels where each stack link goes")]
    public async Task StackLinksAreLabelled()
    {
        var page = await HtmlPage.LoadAsync(factory);

        var links = page.QuerySelectorAll(".stack-switch a");
        Assert.NotEmpty(links);
        Assert.All(links, link => Assert.False(string.IsNullOrWhiteSpace(link.GetAttribute("aria-label"))));

        var dotNet = Assert.Single(links, link => link.Text() == ".NET");
        Assert.Equal("page", dotNet.GetAttribute("aria-current"));

        Assert.All(links.Where(link => link.GetAttribute("href")!.StartsWith("http")), link =>
        {
            Assert.Equal("_blank", link.GetAttribute("target"));
            Assert.Equal("noopener noreferrer", link.GetAttribute("rel"));
            Assert.Contains("opens in a new tab", link.GetAttribute("aria-label"));
        });
    }

    [Theory(DisplayName = "[004:FR-012] Links Angular to the local dev server only in Development")]
    [InlineData("Development", "http://localhost:4200")]
    [InlineData("Production", "https://edonbytyqi.github.io/cv-website/")]
    public async Task AngularLinkFollowsEnvironment(string environment, string expectedUrl)
    {
        using var app = factory.WithWebHostBuilder(builder => builder.UseEnvironment(environment));

        var page = await HtmlPage.LoadAsync(app);
        var links = page.QuerySelectorAll(".stack-switch a");

        var angular = Assert.Single(links, link => link.Text() == "Angular");
        Assert.Equal(expectedUrl, angular.GetAttribute("href"));
        if (environment != "Development")
        {
            Assert.All(links, link => Assert.DoesNotContain("localhost", link.GetAttribute("href")));
        }
    }
}
