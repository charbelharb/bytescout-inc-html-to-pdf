namespace Services.Interfaces;

public interface IConverterService
{
    /// <summary>
    /// Generate PDF from HTML string
    /// </summary>
    /// <param name="html">HTML string</param>
    /// <returns></returns>
    Task<string> GeneratePdfAsync(string html);

    /// <summary>
    /// Get already saved PDF
    /// </summary>
    /// <param name="fileName">Filename</param>
    /// <returns>Stream</returns>
    Task<Stream> GetSavedPdfFileAsync(string fileName);
}