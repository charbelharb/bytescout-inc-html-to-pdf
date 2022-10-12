namespace HtmlConverter.Interfaces;

public interface IParser
{
    /// <summary>
    /// Save HTML to pdf
    /// </summary>
    /// <param name="html">Html string</param>
    /// <param name="filePath">File Path</param>
    /// <param name="chromePath">Local chrome Path</param>
    Task ParseHtmlAndSavePdfAsync(string html, string filePath, string chromePath);
}