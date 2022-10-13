# Byescout Inc Html-Pdf

### Prerequisites
- [.Net 6](https://dotnet.microsoft.com/en-us/download/dotnet/6.0)
- [Google Chrome](https://www.google.com/chrome/)
- [MSSQL](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)

### Configurable settings
- `PdfGenerationPath` The local path where PDF will be saved - App should have R/W permission.
- `SelfBaseUrl` The App base Url.
- `ChromePath` Local Chrome Executable Path.
- `ConnectionStrings::WebApiContext` SQL Server connection string.
- `Jwt::Key` JWT Key
- `Jwt::Issuer` JWT Issuer

**Database Instructions**
1. Create a database using any name, suggested is `HtmlToPdf`
2. Update Connection string with the name.
3. Run application (migration will automatically run)

**Note**
1. By configuring the app download chromium, we can get rid of Chrome dependency
Check `HtmlConverter.Parser` for more info.
2. Ensure to create the database with same name as in the connection string.

