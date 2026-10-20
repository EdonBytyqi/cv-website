namespace DotNetCV.Models;

/// <summary>
/// A role in the work history.
/// </summary>
public sealed class Experience
{
    public required string JobTitle { get; init; }

    public required string Company { get; init; }

    public required string Location { get; init; }

    public required DateOnly StartDate { get; init; }

    /// <summary>
    /// The end of the role, or <see langword="null"/> for the current role.
    /// </summary>
    public DateOnly? EndDate { get; init; }

    public bool IsCurrentJob => EndDate is null;

    public IReadOnlyList<string> Responsibilities { get; init; } = [];
}
