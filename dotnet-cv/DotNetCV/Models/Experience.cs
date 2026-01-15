namespace DotNetCV.Models;

public class Experience
{
    public int Id { get; set; }
    public string JobTitle { get; set; } = string.Empty;
    public string Company { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public bool IsCurrentJob { get; set; }
    public List<string> Responsibilities { get; set; } = new();
    public int SortOrder { get; set; }
}
