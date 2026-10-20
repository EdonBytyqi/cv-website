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

/// <summary>A link to one implementation of this CV.</summary>
/// <param name="Label">Visible text, e.g. ".NET".</param>
/// <param name="Url">Where the link goes.</param>
/// <param name="IsActive">Whether this is the version being viewed.</param>
/// <param name="Description">Accessible name that says where the link goes.</param>
/// <param name="OpensInNewTab">Whether the link opens another site in a new tab.</param>
public sealed record TechStackItem(string Label, string Url, bool IsActive, string Description, bool OpensInNewTab);

public sealed record SocialLink(string Label, string Url, string Icon);
