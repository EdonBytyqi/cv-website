namespace DotNetCV.Models;

/// <summary>
/// A degree, certification or course.
/// </summary>
public sealed class Education
{
    public required string Degree { get; init; }

    public required string Institution { get; init; }

    public required string Location { get; init; }

    public required DateOnly StartDate { get; init; }

    public DateOnly? EndDate { get; init; }

    public string? Grade { get; init; }

    public string? Description { get; init; }
}
