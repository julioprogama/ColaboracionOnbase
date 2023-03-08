//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//using iTextSharp.text.pdf;

//namespace ColaboracionTest
//{
//    class Prueba
//    {
//        /// 
//        ///		Combina una serie de archivos PDF
//        /// 
//        internal static bool Merge(string strFileTarget, string[] arrStrFilesSource)
//        {
//            bool blnMerged = false;

//            // Crea el PDF de salida
//            try
//            {
//                using (System.IO.FileStream stmFile = new System.IO.FileStream(strFileTarget, System.IO.FileMode.Create))
//                {
//                     //Document objDocument = null;
//                    PdfWriter objWriter = null;

//                    // Recorre los archivos
//                    for (int intIndexFile = 0; intIndexFile < arrStrFilesSource.Length; intIndexFile++)
//                    {
//                        PdfReader objReader = new PdfReader(arrStrFilesSource[intIndexFile]);
//                        int intNumberOfPages = objReader.NumberOfPages;

//                        // La primera vez, inicializa el documento y el escritor
//                        if (intIndexFile == 0)
//                        { // Asigna el documento y el generador
//                            objDocument = new Document(objReader.GetPageSizeWithRotation(1));
//                            objWriter = PdfWriter.GetInstance(objDocument, stmFile);
//                            // Abre el documento
//                            objDocument.Open();
//                        }
//                        // Añade las páginas
//                        for (int intPage = 0; intPage < intNumberOfPages; intPage++)
//                        {
//                            int intRotation = objReader.GetPageRotation(intPage + 1);
//                            PdfImportedPage objPage = objWriter.GetImportedPage(objReader, intPage + 1);

//                            // Asigna el tamaño de la página
//                            objDocument.SetPageSize(objReader.GetPageSizeWithRotation(intPage + 1));
//                            // Crea una nueva página
//                            objDocument.NewPage();
//                            // Añade la página leída
//                            if (intRotation == 90 || intRotation == 270)
//                                objWriter.DirectContent.AddTemplate(objPage, 0, -1f, 1f, 0, 0,
//                                                                    objReader.GetPageSizeWithRotation(intPage + 1).Height);
//                            else
//                                objWriter.DirectContent.AddTemplate(objPage, 1f, 0, 0, 1f, 0, 0);
//                        }
//                    }
//                    // Cierra el documento
//                    if (objDocument != null)
//                        objDocument.Close();
//                    // Cierra el stream del archivo
//                    stmFile.Close();
//                }
//                // Indica que se ha creado el documento
//                blnMerged = true;
//            }
//            catch (Exception objException)
//            {
//                System.Diagnostics.Debug.WriteLine(objException.Message);
//            }
//            // Devuelve el valor que indica si se han mezclado los archivos
//            return blnMerged;
//        }
//    }
//}
