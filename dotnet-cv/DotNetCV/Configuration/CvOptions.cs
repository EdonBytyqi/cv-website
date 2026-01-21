using System.ComponentModel.DataAnnotations;

namespace DotNetCV.Configuration;

/// <summary>
/// Configuration options for CV application settings
/// </summary>
public sealed class CvOptions
{
    public const string SectionName = "CvSettings";

    /// <summary>
    /// Cache duration in minutes for CV data
    /// </summary>
    [Range(1, 1440)]
    public int CacheDurationMinutes { get; init; } = 60;

    /// <summary>
    /// Enable or disable response caching
    /// </summary>
    public bool EnableCaching { get; init; } = true;

    /// <summary>
    /// Maximum age in seconds for static assets
    /// </summary>
    [Range(0, 31536000)]
    public int StaticAssetMaxAgeSeconds { get; init; } = 86400; // 1 day
}
