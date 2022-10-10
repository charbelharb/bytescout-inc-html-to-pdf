using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using WebApi.Models;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PdfController : ControllerBase
{
    private readonly IConverterService _converterService;
    
    public PdfController(IConverterService converterService)
    {
        _converterService = converterService;
    }
    
    [HttpPost("convert")]
    public async Task<IActionResult> GeneratePdf([FromBody]PdfGenerationModel model)
    {
        var fileUrl = await _converterService.GeneratePdfAsync(model.Html);
        return new JsonResult(fileUrl);
    }

    [HttpGet("{fileName}")]
    public async Task<IActionResult> GetSavedFile(string fileName)
    {
        var stream = await _converterService.GetSavedPdfFileAsync(fileName);
        return new FileStreamResult(stream, "application/pdf");
    }
}