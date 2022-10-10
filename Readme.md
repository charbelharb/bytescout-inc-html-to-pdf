# Byescout Inc Html-Pdf

### Prerequisites
- [.Net 6](https://dotnet.microsoft.com/en-us/download/dotnet/6.0)
- [Google Chrome](https://www.google.com/chrome/)

### Configurable settings
- `PdfGenerationPath` The local path where PDF will be saved - App should have R/W permission.
- `SelfBaseUrl` The App base Url.
- `ChromePath` Local Chrome Executable Path.

**Note**: By configuring the app download chromium, we can get rid of Chrome dependency
Check `HtmlConverter.Parser` for more info.
