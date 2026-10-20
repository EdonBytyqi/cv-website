namespace DotNetCV.Models;

/// <summary>
/// Public contact details.
/// </summary>
public sealed class Contact
{
    public required string Email { get; init; }

    public string? Phone { get; init; }

    public required string Location { get; init; }

    public required string LinkedInUrl { get; init; }

    public required string GitHubUrl { get; init; }

    public string? WebsiteUrl { get; init; }
}
