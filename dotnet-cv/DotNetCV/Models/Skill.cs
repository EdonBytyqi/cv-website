namespace DotNetCV.Models;

/// <summary>
/// A named group of skills.
/// </summary>
public sealed class SkillCategory
{
    public required string CategoryName { get; init; }

    public required IReadOnlyList<string> Skills { get; init; }
}
