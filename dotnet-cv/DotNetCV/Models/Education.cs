namespace DotNetCV.Models;

public class Education
{
    public int Id { get; set; }
    public string Degree { get; set; } = string.Empty;
    public string Institution { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string? Grade { get; set; }
    public string? Description { get; set; }
    public int SortOrder { get; set; }
}
