namespace DotNetCV.Models;

/// <summary>
/// Personal information about the CV owner.
/// </summary>
public sealed class AboutMe
{
    public required string FullName { get; init; }

    public required string Title { get; init; }

    public required string Bio { get; init; }

    public required string ProfileImageUrl { get; init; }

    /// <summary>
    /// The first word of <see cref="FullName"/>.
    /// </summary>
    public string FirstName => FullName.Split(' ', 2)[0];

    /// <summary>
    /// Everything after the first word of <see cref="FullName"/>, or an empty string.
    /// </summary>
    public string LastName => FullName.Split(' ', 2) is [_, var rest] ? rest : string.Empty;
}
