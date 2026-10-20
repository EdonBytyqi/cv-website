using AngleSharp.Dom;
using DotNetCV.Models;
using Microsoft.AspNetCore.Mvc.Testing;

namespace DotNetCV.Tests.Integration;

/// <summary>
/// Feature 004 (specs/004-notebook-redesign) and the skip link and photo checks from feature 002.
/// </summary>
public sealed class RedesignTests(WebApplicationFactory<Program> factory) : IClassFixture<WebApplicationFactory<Program>>
{
    private static readonly Experience ExampleJob = new()
    {
        JobTitle = "Developer",
        Company = "Example AG",
        Location = "Zürich, Switzerland",
        StartDate = new DateOnly(2024, 1, 1)
    };

    [Fact(DisplayName = "[002:FR-001] Starts with a skip link that targets main")]
    public async Task StartsWithSkipLink()
    {
        var page = await HtmlPage.LoadAsync(factory);

        var firstFocusable = page.QuerySelector("a[href], button, input, [tabindex]:not([tabindex='-1'])");
        Assert.Equal("Skip to content", firstFocusable.Text());
        var target = page.GetElementById(firstFocusable!.GetAttribute("href")![1..]);
        Assert.Equal("MAIN", target?.TagName);
    }

    [Fact(DisplayName = "[004:FR-001] Has a header landmark with the site name and main navigation")]
    public async Task HasHeaderWithNavigation()
    {
        var page = await HtmlPage.LoadAsync(factory);

        var header = page.QuerySelector("body > header");
        Assert.NotNull(header);
        Assert.Contains(CvOverrides.RealCv.Sidebar.Name, header.Text());
        Assert.NotNull(header.QuerySelector("nav[aria-label='Main navigation']"));
    }

    [Fact(DisplayName = "[004:FR-002] Renders a collapsed Menu button that controls the navigation list")]
    public async Task RendersMenuButton()
    {
        var page = await HtmlPage.LoadAsync(factory);

        var button = page.QuerySelector("header button");
        Assert.Equal("Menu", button.Text());
        Assert.Equal("false", button!.GetAttribute("aria-expanded"));
        var list = page.GetElementById(button.GetAttribute("aria-controls")!);
        Assert.NotNull(list?.Closest("nav"));
    }

    [Theory(DisplayName = "[004:FR-006] Shows the job title and a note about the current role")]
    [InlineData("Zürich, Switzerland", "currently at Example AG, Zürich")]
    [InlineData("", "currently: Example AG")]
    public async Task ShowsCurrentRoleNote(string location, string expectedNote)
    {
        var job = new Experience
        {
            JobTitle = ExampleJob.JobTitle,
            Company = ExampleJob.Company,
            Location = location,
            StartDate = ExampleJob.StartDate
        };

        var page = await HtmlPage.LoadAsync(factory.WithCv(CvOverrides.WithParts(experiences: [job])));
        var heroText = page.GetElementById("home").Text();

        Assert.Contains(CvOverrides.RealCv.AboutMe.Title, heroText);
        Assert.Contains(expectedNote, heroText);
    }

    [Fact(DisplayName = "[002:FR-003] Shows the hero photo as an img with the full name in its alt text")]
    public async Task ShowsHeroPhoto()
    {
        var page = await HtmlPage.LoadAsync(factory);

        var photo = page.QuerySelector("#home img");
        Assert.Contains(CvOverrides.RealCv.AboutMe.FullName, photo?.GetAttribute("alt"));
    }

    [Fact(DisplayName = "[004:FR-005] Labels each experience entry with its start year")]
    public async Task LabelsExperienceYears()
    {
        var page = await HtmlPage.LoadAsync(factory);
        var entries = page.QuerySelectorAll("#experience article");

        Assert.Equal(CvOverrides.RealCv.Experiences.Count, entries.Length);
        for (var i = 0; i < entries.Length; i++)
        {
            var year = CvOverrides.RealCv.Experiences[i].StartDate.Year.ToString();
            var label = entries[i].QuerySelectorAll("*").FirstOrDefault(element => element.Text() == year);
            Assert.NotNull(label);
            Assert.Null(label.Closest("time"));
        }
    }

    [Fact(DisplayName = "[004:FR-007] Offers email and LinkedIn as the two ways to get in touch")]
    public async Task OffersTwoWaysIn()
    {
        var page = await HtmlPage.LoadAsync(factory);
        var contact = CvOverrides.RealCv.Contact;

        Assert.NotNull(page.QuerySelector($"#contact a.button[href='mailto:{contact.Email}']"));
        Assert.NotNull(page.QuerySelector($"#contact a.button[href='{contact.LinkedInUrl}']"));
    }

    [Fact(DisplayName = "[004:FR-009] Has a footer landmark linking to LinkedIn and GitHub")]
    public async Task HasFooterLinks()
    {
        var page = await HtmlPage.LoadAsync(factory);

        var footer = page.QuerySelector("body > footer");
        Assert.NotNull(footer);
        Assert.NotNull(footer.QuerySelector("a[href*='linkedin.com']"));
        Assert.NotNull(footer.QuerySelector("a[href*='github.com']"));
    }

    [Fact(DisplayName = "[004:FR-010] Loads every stylesheet, script and font from the site itself")]
    public async Task LoadsOnlyOwnAssets()
    {
        using var client = factory.CreateClient();
        var page = await HtmlPage.LoadAsync(factory);

        var assets = page.QuerySelectorAll("link[rel='stylesheet'][href], script[src]")
            .Select(element => element.GetAttribute("href") ?? element.GetAttribute("src"))
            .ToList();
        Assert.NotEmpty(assets);
        Assert.All(assets, asset => Assert.StartsWith("/", asset));

        var css = await client.GetStringAsync(assets.First(asset => asset!.Contains(".css")));
        Assert.DoesNotContain("http", css.Split("url(").Skip(1).Select(url => url.Split(')')[0]));
        Assert.Contains("/fonts/", css);
    }
}
