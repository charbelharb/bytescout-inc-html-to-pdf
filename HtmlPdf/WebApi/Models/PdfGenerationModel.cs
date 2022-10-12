namespace WebApi.Models;

public class PdfGenerationModel
{
    // We can add an extra field for initial file naming
    // For example we send: "test" and the filename will be "test-XXX-YYY.pdf) 
    // Where XXX-YYY will be a unique identifier
    public string Html { get; set; }
}