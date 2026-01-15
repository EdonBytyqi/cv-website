using DotNetCV.Models;

namespace DotNetCV.ViewModels;

public class CvViewModel
{
    public AboutMe AboutMe { get; set; } = new();
    public List<Experience> Experiences { get; set; } = new();
    public List<Education> Education { get; set; } = new();
    public List<SkillCategory> SkillCategories { get; set; } = new();
    public Contact Contact { get; set; } = new();
    public SidebarViewModel Sidebar { get; set; } = new();
}
