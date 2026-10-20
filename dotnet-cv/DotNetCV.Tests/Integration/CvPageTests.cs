using System.Globalization;
using AngleSharp.Dom;
using DotNetCV.Services;
using Microsoft.AspNetCore.Mvc.Testing;

namespace DotNetCV.Tests.Integration;

/// <summary>
/// Spec 001: the rendered CV page.
/// </summary>
public sealed class CvPageTests(WebApplicationFactory<Program> factory) : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly ViewModels.CvViewModel _cv = new CvService().GetCv();

    [Fact(DisplayName = "[AC-001-01] Has one h1 with the first name and a separately wrapped last name")]
    public async Task HasOneHeadingWithTheName()
    {
        var page = await HtmlPage.LoadAsync(factory);

        var heading = Assert.Single(page.QuerySelectorAll("h1"));
        Assert.Contains(_cv.AboutMe.FirstName, heading.Text());
        Assert.Contains(heading.Children, child => child.Text() == _cv.AboutMe.LastName);
    }

    [Theory(DisplayName = "[AC-001-02] Has a section with an h2 for every CV area")]
    [InlineData("about")]
    [InlineData("experience")]
    [InlineData("education")]
    [InlineData("skills")]
    [InlineData("contact")]
    public async Task HasSectionWithHeading(string id)
    {
        var page = await HtmlPage.LoadAsync(factory);

        var section = page.QuerySelector($"section#{id}");
        Assert.NotNull(section);
        Assert.NotNull(section.QuerySelector("h2"));
    }

    [Fact(DisplayName = "[AC-001-03] Only links the main navigation to elements that exist")]
    public async Task NavigationTargetsExist()
    {
        var page = await HtmlPage.LoadAsync(factory);

        var links = page.QuerySelectorAll("nav a[href^='#']");
        Assert.NotEmpty(links);
        Assert.All(links, link => Assert.NotNull(page.GetElementById(link.GetAttribute("href")![1..])));
    }

    [Fact(DisplayName = "[AC-001-04] Lists experience newest first with an English 'Present' range")]
    public async Task ListsExperienceNewestFirst()
    {
        var originalCulture = CultureInfo.DefaultThreadCurrentCulture;
        CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("de-CH");
        try
        {
            var page = await HtmlPage.LoadAsync(factory);

            var titles = page.QuerySelectorAll("#experience h3").Select(title => title.Text());
            Assert.Equal(_cv.Experiences.Select(experience => experience.JobTitle), titles);

            var current = _cv.Experiences[0];
            var start = current.StartDate.ToString("MMM yyyy", CultureInfo.InvariantCulture);
            Assert.Contains($"{start} - Present", page.QuerySelector("#experience").Text());
        }
        finally
        {
            CultureInfo.DefaultThreadCurrentCulture = originalCulture;
        }
    }

    [Fact(DisplayName = "[AC-001-05] Shows a year range only when the end year differs")]
    public async Task ShowsEducationYears()
    {
        var page = await HtmlPage.LoadAsync(factory);
        var educationText = page.QuerySelector("#education").Text();

        foreach (var education in _cv.Education)
        {
            var expected = education.EndDate is { } end && end.Year != education.StartDate.Year
                ? $"{education.StartDate.Year} - {end.Year}"
                : $"{education.StartDate.Year} {education.Degree}";
            Assert.Contains(expected, educationText);
        }
    }

    [Fact(DisplayName = "[AC-001-06] Shows every skill under its category heading")]
    public async Task GroupsSkillsByCategory()
    {
        var page = await HtmlPage.LoadAsync(factory);
        var headings = page.QuerySelectorAll("#skills h3");

        foreach (var category in _cv.SkillCategories)
        {
            var heading = Assert.Single(headings, h => h.Text() == category.CategoryName);
            var groupText = heading.ParentElement.Text();
            Assert.All(category.Skills, skill => Assert.Contains(skill, groupText));
        }
    }

    [Fact(DisplayName = "[AC-001-07] Links the email address with mailto:")]
    public async Task LinksEmail()
    {
        var page = await HtmlPage.LoadAsync(factory);

        Assert.NotNull(page.QuerySelector($"a[href='mailto:{_cv.Contact.Email}']"));
    }
}
