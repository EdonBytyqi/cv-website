using DotNetCV.Services;
using DotNetCV.Tests.Integration;

namespace DotNetCV.Tests.Services;

public sealed class CvServiceTests
{
    private readonly CvService _service = CvOverrides.CreateService();

    [Fact]
    public void GetCv_BuildsTheCvOnlyOnce()
    {
        Assert.Same(_service.GetCv(), _service.GetCv());
    }

    [Fact]
    public void GetCv_ListsExperienceNewestFirst()
    {
        var startDates = _service.GetCv().Experiences.Select(experience => experience.StartDate).ToList();

        Assert.NotEmpty(startDates);
        Assert.Equal(startDates.OrderByDescending(date => date), startDates);
    }

    [Fact]
    public void GetCv_HasOneCurrentJobAndItComesFirst()
    {
        var experiences = _service.GetCv().Experiences;

        Assert.Single(experiences, experience => experience.IsCurrentJob);
        Assert.True(experiences[0].IsCurrentJob);
    }

    [Fact]
    public void GetCv_GivesEveryEntryItsRequiredText()
    {
        var cv = _service.GetCv();

        Assert.All(cv.Experiences, experience => Assert.NotEmpty(experience.JobTitle));
        Assert.All(cv.Education, education => Assert.NotEmpty(education.Degree));
        Assert.All(cv.SkillCategories, category => Assert.NotEmpty(category.Skills));
        Assert.Contains("@", cv.Contact.Email);
    }

    [Fact]
    public void GetCv_PointsNavigationAtPageSections()
    {
        Assert.All(_service.GetCv().Sidebar.NavigationItems, item => Assert.StartsWith("#", item.Href));
    }

    [Fact]
    public void GetCv_UsesHttpsForSocialLinks()
    {
        Assert.All(_service.GetCv().Sidebar.SocialLinks, link => Assert.StartsWith("https://", link.Url));
    }
}
