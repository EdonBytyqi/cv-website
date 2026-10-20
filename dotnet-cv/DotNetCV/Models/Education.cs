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

    /// <summary>
    /// Still in progress; shown as "since &lt;start year&gt;".
    /// </summary>
    public bool IsOngoing { get; init; }

    public string? Grade { get; init; }

    public string? Description { get; init; }

    /// <summary>
    /// "since 2022" when ongoing, "2014 - 2018" when the years differ, otherwise the start year.
    /// </summary>
    public string YearRange => this switch
    {
        { IsOngoing: true } => $"since {StartDate.Year}",
        { EndDate: { } end } when end.Year != StartDate.Year => $"{StartDate.Year} - {end.Year}",
        _ => StartDate.Year.ToString(System.Globalization.CultureInfo.InvariantCulture)
    };
}
