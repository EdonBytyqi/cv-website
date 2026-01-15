namespace DotNetCV.ViewModels;

public class SidebarViewModel
{
    public string Name { get; set; } = string.Empty;
    public string ProfileImageUrl { get; set; } = string.Empty;
    public List<NavigationItem> NavigationItems { get; set; } = new();
    public List<TechStackItem> TechStackItems { get; set; } = new();
    public List<SocialLink> SocialLinks { get; set; } = new();
}

public class NavigationItem
{
    public string Label { get; set; } = string.Empty;
    public string Href { get; set; } = string.Empty;
    public bool IsActive { get; set; }
}

public class TechStackItem
{
    public string Label { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;
    public bool IsActive { get; set; }
}

public class SocialLink
{
    public string Label { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;
    public string Icon { get; set; } = string.Empty;
}
