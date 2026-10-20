using System.Net;
using System.Text.Json;
using DotNetCV.Services;
using DotNetCV.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;

namespace DotNetCV.Tests.Integration;

/// <summary>
/// Spec 002: JSON API, health endpoint, headers and error handling.
/// </summary>
public sealed class ApiAndSecurityTests(WebApplicationFactory<Program> factory) : IClassFixture<WebApplicationFactory<Program>>
{
    [Fact(DisplayName = "[AC-002-01] Returns every CV section as JSON")]
    public async Task ReturnsCompleteCv()
    {
        using var client = factory.CreateClient();

        using var json = JsonDocument.Parse(await client.GetStringAsync("/api/cvapi"));

        foreach (var section in new[] { "aboutMe", "experiences", "education", "skillCategories", "contact", "sidebar" })
        {
            Assert.True(json.RootElement.TryGetProperty(section, out _), $"missing {section}");
        }
    }

    [Fact(DisplayName = "[AC-002-02] Rejects writes to the read-only API")]
    public async Task RejectsPost()
    {
        using var client = factory.CreateClient();

        using var response = await client.PostAsync("/api/cvapi", content: null);

        Assert.Equal(HttpStatusCode.MethodNotAllowed, response.StatusCode);
    }

    [Fact(DisplayName = "[AC-002-03] Sends security headers on pages")]
    public async Task SendsSecurityHeaders()
    {
        using var client = factory.CreateClient();

        using var response = await client.GetAsync("/");

        Assert.Contains("frame-ancestors 'none'", string.Join(';', response.Headers.GetValues("Content-Security-Policy")));
        Assert.Equal("nosniff", Assert.Single(response.Headers.GetValues("X-Content-Type-Options")));
        Assert.NotEmpty(response.Headers.GetValues("Referrer-Policy"));
    }

    [Fact(DisplayName = "[AC-002-04] Reports healthy")]
    public async Task ReportsHealthy()
    {
        using var client = factory.CreateClient();

        using var response = await client.GetAsync("/health");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Theory(DisplayName = "[AC-002-05] Hides exception details when the CV service fails")]
    [InlineData("/")]
    [InlineData("/api/cvapi")]
    public async Task HidesExceptionDetails(string path)
    {
        const string secret = "connection string with a password";
        using var failingApp = factory.WithWebHostBuilder(builder =>
        {
            builder.UseEnvironment("Production");
            builder.ConfigureServices(services => services.AddSingleton<ICvService>(new FailingCvService(secret)));
        });
        using var client = failingApp.CreateClient();

        using var response = await client.GetAsync(path);

        Assert.Equal(HttpStatusCode.InternalServerError, response.StatusCode);
        Assert.DoesNotContain(secret, await response.Content.ReadAsStringAsync());
    }

    private sealed class FailingCvService(string message) : ICvService
    {
        public CvViewModel GetCv() => throw new InvalidOperationException(message);
    }
}
