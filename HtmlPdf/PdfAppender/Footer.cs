using FooterAppender.Interfaces;
using iText.IO.Font.Constants;
using iText.Kernel.Colors;
using iText.Kernel.Font;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;

namespace FooterAppender;

public class Footer : IFooter
{
    public void AppendSha256Async(string path, string hash, char separator)
    {
        var fileName = Path.GetFileName(path).Split('.')[0].Split(separator)[0];
        var p = Path.GetDirectoryName(path);
        using var pdfDoc = new PdfDocument(new PdfReader(path), new PdfWriter($"{p}//{fileName}.pdf"));
        var doc = new Document(pdfDoc);
        var header = new Paragraph(hash)
            .SetFont(PdfFontFactory.CreateFont(StandardFonts.HELVETICA))
            .SetFontSize(8)
            .SetFontColor(ColorConstants.GRAY);
        
        for (var i = 1; i <= pdfDoc.GetNumberOfPages(); i++) 
        {
            var pageSize = pdfDoc.GetPage(i).GetPageSize();
            var x = pageSize.GetWidth() / 2;
            var y = pageSize.GetBottom() + 20;
            doc.ShowTextAligned(header, x, y, i, TextAlignment.CENTER, VerticalAlignment.BOTTOM, 0);
        }
        doc.Close();
        pdfDoc.Close();
        File.Delete(path);
    }
}