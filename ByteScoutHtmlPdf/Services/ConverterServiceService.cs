using Core;
using FooterAppender.Interfaces;
using HtmlConverter.Interfaces;
using Services.Interfaces;
using Microsoft.Extensions.Configuration;

namespace Services;

public class ConverterServiceService : IConverterService
{
    private readonly IParser _parser;
    private readonly IFooter _footer;
    private readonly IConfiguration _configuration;
    private const char Separator = ']';
    
    public ConverterServiceService(IParser parser, IFooter footer, IConfiguration configuration)
    {
        _parser = parser;
        _footer = footer;
        _configuration = configuration;
    }
    
    public async Task<string> GeneratePdfAsync(string html)
    {
        var fileName =
            FileHelper.GenerateRandomFileName();
        var path = $"{_configuration["PdfGenerationPath"] ?? Path.GetTempPath()}//{fileName}{Separator}.pdf";
        await _parser.ParseHtmlAndSavePdfAsync(html, path,_configuration["ChromePath"]
                                                          ?? "C:\\Program Files\\Google\\Chrome\\Application\\chrome.exe");
        var hash = (await FileHelper.GetChecksum(path)).ToLower();
        _footer.AppendSha256Async(path,hash,Separator);
        return $"{_configuration["SelfBaseUrl"]}pdf/{fileName}.pdf";
    }

    public async Task<Stream> GetSavedPdfFileAsync(string fileName)
    {
        var path = $"{_configuration["PdfGenerationPath"] ?? Path.GetTempPath()}//{fileName}";
        var bytes = await File.ReadAllBytesAsync(path);
        return new MemoryStream(bytes);
    }
}