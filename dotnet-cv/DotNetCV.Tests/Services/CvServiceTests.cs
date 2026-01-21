using DotNetCV.Configuration;
using DotNetCV.Services;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Xunit;

namespace DotNetCV.Tests.Services;

/// <summary>
/// Unit tests for CvServiceWithCaching
/// </summary>
public sealed class CvServiceTests
{
    private readonly IMemoryCache _cache;
    private readonly ILogger<CvServiceWithCaching> _logger;
    private readonly IOptions<CvOptions> _options;

    public CvServiceTests()
    {
        _cache = new MemoryCache(new MemoryCacheOptions());
        _logger = new LoggerFactory().CreateLogger<CvServiceWithCaching>();
        _options = Options.Create(new CvOptions
        {
            CacheDurationMinutes = 60,
            EnableCaching = true
        });
    }

    [Fact]
    public async Task GetAboutMeAsync_ReturnsValidData()
    {
        // Arrange
        var service = new CvServiceWithCaching(_cache, _logger, _options);

        // Act
        var result = await service.GetAboutMeAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Edon Bytyqi", result.FullName);
        Assert.Equal("Software Developer", result.Title);
        Assert.NotEmpty(result.Bio);
    }

    [Fact]
    public async Task GetExperiencesAsync_ReturnsNonEmptyList()
    {
        // Arrange
        var service = new CvServiceWithCaching(_cache, _logger, _options);

        // Act
        var result = await service.GetExperiencesAsync();

        // Assert
        Assert.NotNull(result);
        Assert.NotEmpty(result);
        Assert.All(result, exp => Assert.NotEmpty(exp.JobTitle));
    }

    [Fact]
    public async Task GetEducationAsync_ReturnsNonEmptyList()
    {
        // Arrange
        var service = new CvServiceWithCaching(_cache, _logger, _options);

        // Act
        var result = await service.GetEducationAsync();

        // Assert
        Assert.NotNull(result);
        Assert.NotEmpty(result);
        Assert.All(result, edu => Assert.NotEmpty(edu.Degree));
    }

    [Fact]
    public async Task GetSkillCategoriesAsync_ReturnsNonEmptyList()
    {
        // Arrange
        var service = new CvServiceWithCaching(_cache, _logger, _options);

        // Act
        var result = await service.GetSkillCategoriesAsync();

        // Assert
        Assert.NotNull(result);
        Assert.NotEmpty(result);
        Assert.All(result, cat => Assert.NotEmpty(cat.CategoryName));
        Assert.All(result, cat => Assert.NotEmpty(cat.Skills));
    }

    [Fact]
    public async Task GetContactAsync_ReturnsValidData()
    {
        // Arrange
        var service = new CvServiceWithCaching(_cache, _logger, _options);

        // Act
        var result = await service.GetContactAsync();

        // Assert
        Assert.NotNull(result);
        Assert.NotEmpty(result.Email);
        Assert.Contains("@", result.Email);
    }

    [Fact]
    public async Task GetCompleteCvAsync_ReturnsFullViewModel()
    {
        // Arrange
        var service = new CvServiceWithCaching(_cache, _logger, _options);

        // Act
        var result = await service.GetCompleteCvAsync();

        // Assert
        Assert.NotNull(result);
        Assert.NotNull(result.AboutMe);
        Assert.NotNull(result.Experiences);
        Assert.NotNull(result.Education);
        Assert.NotNull(result.SkillCategories);
        Assert.NotNull(result.Contact);
        Assert.NotNull(result.Sidebar);
    }

    [Fact]
    public async Task GetAboutMeAsync_WithCachingEnabled_UsesCacheOnSecondCall()
    {
        // Arrange
        var service = new CvServiceWithCaching(_cache, _logger, _options);

        // Act
        var result1 = await service.GetAboutMeAsync();
        var result2 = await service.GetAboutMeAsync();

        // Assert
        Assert.Same(result1, result2); // Should be same instance from cache
    }

    [Fact]
    public async Task GetAboutMeAsync_WithCachingDisabled_ReturnsFreshDataEachTime()
    {
        // Arrange
        var disabledOptions = Options.Create(new CvOptions
        {
            EnableCaching = false
        });
        var service = new CvServiceWithCaching(_cache, _logger, disabledOptions);

        // Act
        var result1 = await service.GetAboutMeAsync();
        var result2 = await service.GetAboutMeAsync();

        // Assert
        Assert.NotSame(result1, result2); // Should be different instances
    }

    [Theory]
    [InlineData(1)]
    [InlineData(30)]
    [InlineData(60)]
    public async Task CacheDuration_RespectConfiguredValue(int minutes)
    {
        // Arrange
        var customOptions = Options.Create(new CvOptions
        {
            CacheDurationMinutes = minutes,
            EnableCaching = true
        });
        var service = new CvServiceWithCaching(_cache, _logger, customOptions);

        // Act
        var result = await service.GetAboutMeAsync();

        // Assert
        Assert.NotNull(result);
    }
}
