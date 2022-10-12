using HtmlConverter.Interfaces;
using PuppeteerSharp;

namespace HtmlConverter;

public class Parser : IParser
{
    public async Task ParseHtmlAndSavePdfAsync(string html, string filePath, string chromePath)
    {
        /*
            var browserFetcher = new BrowserFetcher();
            await browserFetcher.DownloadAsync();
            
            Using exe path to avoid download of chromium
         */
        await using var browser = await Puppeteer.LaunchAsync(new LaunchOptions { Headless = true, ExecutablePath = chromePath});
        await using var page = await browser.NewPageAsync();
        await page.SetContentAsync(html);
        await page.PdfAsync(filePath);
    }
}