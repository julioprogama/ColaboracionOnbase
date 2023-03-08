using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iTextSharp.text.pdf;
using iTextSharp.text.xml;

namespace PruebaWebService
{
    class Pdf
    {
        public void Main() {
            // Right side of equation is location of YOUR pdf file 
            string ppath = "D:\\Usuarios\\juliolandazury\\OneDrive - Datecsa S.A\\Documentos\\Personal\\Julio Cesar Landazury (3).pdf";
            PdfReader pdfReader = new PdfReader(ppath);
            int numberOfPages = pdfReader.NumberOfPages;
            
            Console.WriteLine(numberOfPages);
            Console.ReadLine();


		}

    }
}
