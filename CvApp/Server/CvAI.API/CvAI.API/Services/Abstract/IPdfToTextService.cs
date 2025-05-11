using iText.Kernel.Pdf.Canvas.Parser;
using iText.Kernel.Pdf;
using System.Text;

namespace CvAI.API.Services.Abstract
{
    public interface IPdfToTextService
    {
        string ExtractTextFromPdf(byte[] pdfBytes);

    }
}
