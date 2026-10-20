using System.Text.RegularExpressions;

namespace DotNetCV.Tests.Specs;

/// <summary>
/// Enforces the spec-driven workflow: every acceptance criterion in an Approved or
/// Implemented spec has a test whose display name starts with its ID, and tests only
/// reference IDs that a spec defines. See specs/README.md.
/// </summary>
public sealed partial class SpecCoverageTests
{
    private static readonly string Root = FindDotNetCvRoot();

    [Fact]
    public void EveryTrackedCriterionHasATest()
    {
        var tested = TestReferences().Select(reference => reference.Id).ToHashSet();

        var untested = TrackedCriteria().Where(criterion => !tested.Contains(criterion.Id))
            .Select(criterion => $"{criterion.Id} ({criterion.Spec})");

        Assert.Empty(untested);
    }

    [Fact]
    public void TestsOnlyReferenceKnownCriteria()
    {
        var known = AllCriteria().Select(criterion => criterion.Id).ToHashSet();

        var unknown = TestReferences().Where(reference => !known.Contains(reference.Id))
            .Select(reference => $"{reference.Id} in {reference.File}");

        Assert.Empty(unknown);
    }

    private static IEnumerable<(string Id, string Spec, bool IsTracked)> ReadCriteria() =>
        from file in Directory.EnumerateFiles(Path.Combine(Root, "specs"), "spec.md", SearchOption.AllDirectories)
        let text = File.ReadAllText(file)
        let status = StatusPattern().Match(text) is { Success: true } match ? match.Groups[1].Value : "Draft"
        from Match criterion in CriterionPattern().Matches(text)
        select (criterion.Groups[1].Value, Path.GetRelativePath(Root, file), status is "Approved" or "Implemented");

    private static IEnumerable<(string Id, string Spec)> AllCriteria() =>
        ReadCriteria().Select(criterion => (criterion.Id, criterion.Spec));

    private static IEnumerable<(string Id, string Spec)> TrackedCriteria() =>
        ReadCriteria().Where(criterion => criterion.IsTracked).Select(criterion => (criterion.Id, criterion.Spec));

    private static IEnumerable<(string Id, string File)> TestReferences() =>
        from file in Directory.EnumerateFiles(Path.Combine(Root, "DotNetCV.Tests"), "*.cs", SearchOption.AllDirectories)
        where !file.Contains($"{Path.DirectorySeparatorChar}obj{Path.DirectorySeparatorChar}")
            && !file.Contains($"{Path.DirectorySeparatorChar}bin{Path.DirectorySeparatorChar}")
        from Match reference in TestReferencePattern().Matches(File.ReadAllText(file))
        select (reference.Groups[1].Value, Path.GetRelativePath(Root, file));

    private static string FindDotNetCvRoot()
    {
        for (var directory = new DirectoryInfo(AppContext.BaseDirectory); directory is not null; directory = directory.Parent)
        {
            if (Directory.Exists(Path.Combine(directory.FullName, "specs"))
                && Directory.Exists(Path.Combine(directory.FullName, "DotNetCV.Tests")))
            {
                return directory.FullName;
            }
        }

        throw new DirectoryNotFoundException("Could not find the dotnet-cv folder containing specs/.");
    }

    [GeneratedRegex(@"^Status:\s*(\w+)", RegexOptions.Multiline)]
    private static partial Regex StatusPattern();

    [GeneratedRegex(@"\*\*(AC-\d{3}-\d{2})\*\*")]
    private static partial Regex CriterionPattern();

    [GeneratedRegex(@"DisplayName = ""\[(AC-\d{3}-\d{2})\]")]
    private static partial Regex TestReferencePattern();
}
