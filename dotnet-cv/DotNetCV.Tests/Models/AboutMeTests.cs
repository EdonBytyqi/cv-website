using DotNetCV.Models;

namespace DotNetCV.Tests.Models;

public sealed class AboutMeTests
{
    [Theory]
    [InlineData("Alex Example", "Alex", "Example")]
    [InlineData("Alex van Example", "Alex", "van Example")]
    [InlineData("Alex", "Alex", "")]
    public void SplitsFullNameIntoFirstAndLastName(string fullName, string firstName, string lastName)
    {
        var aboutMe = new AboutMe { FullName = fullName, Title = "", Bio = "", ProfileImageUrl = "" };

        Assert.Equal(firstName, aboutMe.FirstName);
        Assert.Equal(lastName, aboutMe.LastName);
    }
}
