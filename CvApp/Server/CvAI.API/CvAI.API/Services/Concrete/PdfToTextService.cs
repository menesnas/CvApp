using System.Text;
using CvAI.API.Services.Abstract;
using iText.Kernel.Pdf.Canvas.Parser;
using iText.Kernel.Pdf;

namespace CvAI.API.Services.Concrete
{
    public class PdfToTextService : IPdfToTextService
    {
        public string ExtractTextFromPdf(byte[] pdfBytes)
        {
            using var reader = new PdfReader(new MemoryStream(pdfBytes));
            using var pdfDoc = new PdfDocument(reader);
            var sb = new StringBuilder();

            for (int i = 1; i <= pdfDoc.GetNumberOfPages(); i++)
            {
                var page = pdfDoc.GetPage(i);
                sb.AppendLine(PdfTextExtractor.GetTextFromPage(page));
            }

            return sb.ToString();
        }
    }
}