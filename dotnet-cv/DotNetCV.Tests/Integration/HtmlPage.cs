using AngleSharp;
using AngleSharp.Dom;
using Microsoft.AspNetCore.Mvc.Testing;

namespace DotNetCV.Tests.Integration;

/// <summary>
/// Fetches a page from the in-memory app and parses it into a DOM.
/// </summary>
internal static class HtmlPage
{
    public static async Task<IDocument> LoadAsync(WebApplicationFactory<Program> factory, string path = "/")
    {
        using var client = factory.CreateClient();
        var html = await client.GetStringAsync(path);
        var context = BrowsingContext.New(AngleSharp.Configuration.Default);
        return await context.OpenAsync(request => request.Content(html));
    }

    /// <summary>
    /// Text content with runs of whitespace collapsed, as a reader would see it.
    /// </summary>
    public static string Text(this IElement? element) =>
        string.Join(' ', (element?.TextContent ?? string.Empty).Split((char[]?)null, StringSplitOptions.RemoveEmptyEntries));
}
