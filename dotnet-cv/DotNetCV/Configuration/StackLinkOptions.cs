using System.ComponentModel.DataAnnotations;

namespace DotNetCV.Configuration;

/// <summary>
/// Where the stack switch sends visitors for the other implementation of this CV.
/// </summary>
public sealed class StackLinkOptions
{
    public const string SectionName = "StackLinks";

    /// <summary>
    /// The Angular version: the live GitHub Pages site by default, the local dev server in Development.
    /// </summary>
    [Required]
    [Url]
    public string AngularUrl { get; init; } = "https://edonbytyqi.github.io/cv-website/";
}
