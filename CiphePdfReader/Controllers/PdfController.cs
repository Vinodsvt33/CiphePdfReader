using System.IO;
using System.Text;
using System.Web;
using System.Web.Mvc;
using CiphePdfReader.Models;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;


namespace CiphePdfReader.Controllers
{
    public class PdfController : Controller
    {
        [HttpGet]
        public ActionResult Upload()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase file)
        {
            if (file != null && file.ContentLength > 0)
            {
                string content = ExtractTextFromPdf(file.InputStream);
                PdfData pdfData = new PdfData { Content = content };
                return View("DisplayPdfData", pdfData);
            }
            return View();
        }

        private string ExtractTextFromPdf(Stream inputStream)
        {
            using (PdfReader reader = new PdfReader(inputStream))
            {
                StringBuilder text = new StringBuilder();

                for (int i = 1; i <= reader.NumberOfPages; i++)
                {
                    text.Append(PdfTextExtractor.GetTextFromPage(reader, i));
                }
                return text.ToString();
            }
        }
    }
}