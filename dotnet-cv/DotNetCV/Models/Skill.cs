namespace DotNetCV.Models;

public class Skill
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public int ProficiencyLevel { get; set; }
    public int SortOrder { get; set; }
}

public class SkillCategory
{
    public string CategoryName { get; set; } = string.Empty;
    public List<Skill> Skills { get; set; } = new();
}
