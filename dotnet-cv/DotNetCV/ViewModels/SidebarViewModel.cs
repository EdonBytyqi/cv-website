namespace DotNetCV.ViewModels;

/// <summary>
/// Site name, navigation and links shown in the layout.
/// </summary>
public sealed class SidebarViewModel
{
    public required string Name { get; init; }

    public required IReadOnlyList<NavigationItem> NavigationItems { get; init; }

    public required IReadOnlyList<TechStackItem> TechStackItems { get; init; }

    public required IReadOnlyList<SocialLink> SocialLinks { get; init; }
}

public sealed record NavigationItem(string Label, string Href);

public sealed record TechStackItem(string Label, string Url, bool IsActive);

public sealed record SocialLink(string Label, string Url, string Icon);
