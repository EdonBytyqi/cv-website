using System.ComponentModel.DataAnnotations;

namespace DotNetCV.Configuration;

/// <summary>
/// Configuration options for CORS policy
/// </summary>
public sealed class CorsOptions
{
    public const string SectionName = "Cors";

    /// <summary>
    /// Allowed origins for CORS
    /// </summary>
    [Required]
    [MinLength(1)]
    public string[] AllowedOrigins { get; init; } = ["http://localhost:4202"];

    /// <summary>
    /// Enable credentials for CORS requests
    /// </summary>
    public bool AllowCredentials { get; init; }

    /// <summary>
    /// Allowed HTTP methods
    /// </summary>
    public string[] AllowedMethods { get; init; } = ["GET"];

    /// <summary>
    /// Allowed headers
    /// </summary>
    public string[] AllowedHeaders { get; init; } = ["*"];
}
