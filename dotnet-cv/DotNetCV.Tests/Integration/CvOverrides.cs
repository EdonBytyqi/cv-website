using DotNetCV.Models;
using DotNetCV.Services;
using DotNetCV.ViewModels;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;

namespace DotNetCV.Tests.Integration;

/// <summary>
/// Runs the app with a modified CV, so tests can use made-up data instead of the real content.
/// </summary>
internal static class CvOverrides
{
    public static readonly CvViewModel RealCv = new CvService().GetCv();

    public static WebApplicationFactory<Program> WithCv(this WebApplicationFactory<Program> factory, CvViewModel cv) =>
        factory.WithWebHostBuilder(builder =>
            builder.ConfigureServices(services => services.AddSingleton<ICvService>(new FixedCvService(cv))));

    public static CvViewModel WithParts(
        Contact? contact = null,
        IReadOnlyList<Education>? education = null,
        IReadOnlyList<Experience>? experiences = null) => new()
    {
        AboutMe = RealCv.AboutMe,
        Experiences = experiences ?? RealCv.Experiences,
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
