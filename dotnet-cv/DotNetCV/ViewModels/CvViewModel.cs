using DotNetCV.Models;

namespace DotNetCV.ViewModels;

/// <summary>
/// Everything the CV page shows, in display order.
/// </summary>
public sealed class CvViewModel
{
    public required AboutMe AboutMe { get; init; }

    public required IReadOnlyList<Experience> Experiences { get; init; }

    public required IReadOnlyList<Education> Education { get; init; }

    public required IReadOnlyList<SkillCategory> SkillCategories { get; init; }

    public required Contact Contact { get; init; }

    public required SidebarViewModel Sidebar { get; init; }

    /// <summary>
    /// "currently at Example AG, Zürich", or "currently: AI integration" when the current role
    /// has no location; <see langword="null"/> when there is no current role.
    /// </summary>
    public string? CurrentRoleNote => Experiences.FirstOrDefault(experience => experience.IsCurrentJob) switch
    {
        null => null,
        { Location: "" } job => $"currently: {job.Company}",
        var job => $"currently at {job.Company}, {job.Location.Split(',')[0]}"
    };
}
