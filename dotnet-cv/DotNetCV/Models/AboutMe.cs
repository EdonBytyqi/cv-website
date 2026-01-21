using System.ComponentModel.DataAnnotations;

namespace DotNetCV.Models;

/// <summary>
/// Represents personal information about the CV owner
/// </summary>
public sealed class AboutMe
{
    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(100, MinimumLength = 2)]
    public string FullName { get; set; } = string.Empty;

    [Required]
    [StringLength(100)]
    public string Title { get; set; } = string.Empty;

    [Required]
    [StringLength(1000)]
    public string Bio { get; set; } = string.Empty;

    [Required]
    [Url]
    [StringLength(500)]
    public string ProfileImageUrl { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
