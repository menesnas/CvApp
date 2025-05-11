using System.Net.Http.Headers;
using System.Net.Http;
using System.Text.Json;
using System.Text;
using CvAI.API.Services.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.PortableExecutable;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Parser;

namespace CvAI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CvController : ControllerBase
    {
        private readonly ILangflowService _langflowService;
        private readonly IPdfToTextService _pdfToTextService;

        public CvController(IPdfToTextService pdfToTextService, ILangflowService langflowService)
        {
            _pdfToTextService = pdfToTextService;
            _langflowService = langflowService;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadCv([FromForm] IFormFile cv)
        {
            if (cv == null || cv.Length == 0 || Path.GetExtension(cv.FileName)?.ToLower() != ".pdf")
            {
                return BadRequest("Geçerli bir PDF dosyası yükleyin.");
            }
            string extractedText;
            using (var memoryStream = new MemoryStream())
            {
                await cv.CopyToAsync(memoryStream);
                extractedText = _pdfToTextService.ExtractTextFromPdf(memoryStream.ToArray());
            }

            var langflowResult = await _langflowService.SendCvAsync(extractedText);
            if(langflowResult == "error")
            {
                return StatusCode(500, "An unexpected error occurred.");
            }
            return Ok(langflowResult);
        }
    }
}