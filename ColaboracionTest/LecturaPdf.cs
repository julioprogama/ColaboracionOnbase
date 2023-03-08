using iText.Kernel.Pdf;

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iText.Pdfocr;
using iText.Pdfocr.Tesseract4;

namespace ColaboracionTest
{
    public class LecturaPdf
    {
        private static readonly Tesseract4OcrEngineProperties tesseract4OcrEngineProperties = new Tesseract4OcrEngineProperties();
        private static string OUTPUT_PDF = @"D:\Test\hello1.pdf";
        private const string DEFAULT_RGB_COLOR_PROFILE_PATH = @"D:\Test\sRGB Color Space Profile.icm";
       

        public void Main()
        {
            var tesseractReader = new Tesseract4LibOcrEngine(tesseract4OcrEngineProperties);
            tesseract4OcrEngineProperties.SetPathToTessData(new FileInfo(@"D:\Test\tessdata_best-main\"));

            var properties = new OcrPdfCreatorProperties();
            properties.SetPdfLang("en"); //we need to define a language to make it PDF/A compliant

            var ocrPdfCreator = new OcrPdfCreator(tesseractReader, properties);
            FileInfo fileInfo = new FileInfo(@"D:\Test\invoice_front.JPG");

            IList<FileInfo> LIST_IMAGES_OCR = new List<FileInfo> { fileInfo };
            
            Stream @is = new FileStream(DEFAULT_RGB_COLOR_PROFILE_PATH, FileMode.Open, FileAccess.Read);
             
            using (var writer = new PdfWriter(OUTPUT_PDF))
            {
                ocrPdfCreator.CreatePdfA(LIST_IMAGES_OCR, writer, new PdfOutputIntent("", "", "", "sRGB IEC61966-2.1", @is)).Close();
            }
        }

        static PdfOutputIntent GetRgbPdfOutputIntent()
        {
            Stream @is = new FileStream(DEFAULT_RGB_COLOR_PROFILE_PATH, FileMode.Open, FileAccess.Read);
            return new PdfOutputIntent("", "", "", "sRGB IEC61966-2.1", @is);
        }
    }
}



