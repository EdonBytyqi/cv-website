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
}
