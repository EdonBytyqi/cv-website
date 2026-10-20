using System.Text.RegularExpressions;

namespace DotNetCV.Tests.Specs;

/// <summary>
/// Enforces Spec Kit traceability (constitution II): every functional requirement in an
/// Approved or Implemented feature under the repository's <c>specs/</c> folder that applies
/// to the .NET app has a test whose display name starts with its key, e.g.
/// <c>[001:FR-004]</c>, and tests only reference requirements that exist.
/// </summary>
public sealed partial class SpecCoverageTests
{
    private const string OtherAppOnly = "*(Angular only)*";

    private static readonly string RepoRoot = FindRepoRoot();
    private static readonly string TestsRoot = Path.Combine(RepoRoot, "dotnet-cv", "DotNetCV.Tests");

    [Fact]
    public void EveryTrackedRequirementHasATest()
    {
        var tested = TestReferences().Select(reference => reference.Key).ToHashSet();

        var untested = Requirements()
            .Where(requirement => requirement.IsTracked && requirement.AppliesToDotNet && !tested.Contains(requirement.Key))
            .Select(requirement => $"{requirement.Key} ({requirement.Spec})");

        Assert.Empty(untested);
    }

    [Fact]
    public void TestsOnlyReferenceKnownRequirements()
    {
        var known = Requirements().Select(requirement => requirement.Key).ToHashSet();

        var unknown = TestReferences().Where(reference => !known.Contains(reference.Key))
            .Select(reference => $"{reference.Key} in {reference.File}");

        Assert.Empty(unknown);
    }

    private static IEnumerable<(string Key, string Spec, bool IsTracked, bool AppliesToDotNet)> Requirements() =>
        from directory in Directory.EnumerateDirectories(Path.Combine(RepoRoot, "specs"))
        let number = FeatureNumberPattern().Match(Path.GetFileName(directory))
        where number.Success && File.Exists(Path.Combine(directory, "spec.md"))
        let file = Path.Combine(directory, "spec.md")
        let text = File.ReadAllText(file)
        let status = StatusPattern().Match(text) is { Success: true } match ? match.Groups[1].Value : "Draft"
        from Match requirement in RequirementPattern().Matches(text)
        select (
            $"{number.Groups[1].Value}:{requirement.Groups[1].Value}",
            Path.GetRelativePath(RepoRoot, file),
            status is "Approved" or "Implemented",
            !requirement.Groups[2].Value.Contains(OtherAppOnly));

    private static IEnumerable<(string Key, string File)> TestReferences() =>
        from file in Directory.EnumerateFiles(TestsRoot, "*.cs", SearchOption.AllDirectories)
        where !file.Contains($"{Path.DirectorySeparatorChar}obj{Path.DirectorySeparatorChar}")
            && !file.Contains($"{Path.DirectorySeparatorChar}bin{Path.DirectorySeparatorChar}")
        from Match reference in TestReferencePattern().Matches(File.ReadAllText(file))
        select (reference.Groups[1].Value, Path.GetRelativePath(RepoRoot, file));

    private static string FindRepoRoot()
    {
        for (var directory = new DirectoryInfo(AppContext.BaseDirectory); directory is not null; directory = directory.Parent)
        {
            if (Directory.Exists(Path.Combine(directory.FullName, "specs"))
                && Directory.Exists(Path.Combine(directory.FullName, "dotnet-cv")))
            {
                return directory.FullName;
            }
        }

        throw new DirectoryNotFoundException("Could not find the repository root containing specs/ and dotnet-cv/.");
    }

    [GeneratedRegex(@"^(\d{3})-")]
    private static partial Regex FeatureNumberPattern();

    [GeneratedRegex(@"^\*\*Status\*\*:\s*(\w+)", RegexOptions.Multiline)]
    private static partial Regex StatusPattern();

    [GeneratedRegex(@"^- \*\*(FR-\d{3})\*\*:(.*)$", RegexOptions.Multiline)]
    private static partial Regex RequirementPattern();

    [GeneratedRegex(@"DisplayName = ""\[(\d{3}:FR-\d{3})\]")]
    private static partial Regex TestReferencePattern();
}
