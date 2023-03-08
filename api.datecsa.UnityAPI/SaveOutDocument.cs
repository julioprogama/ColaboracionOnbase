using System;
using Hyland.Unity;
using System.IO;

namespace api.datecsa.UnityAPI
{
    public class SaveOutDocument
    {
        private Hyland.Unity.Application app = null;
        private PageData PageData;

        public PageData GetPageData()
        {
            return PageData;
        }

        public void SetPageData(PageData value)
        {
            PageData = value;
        }

        public String RutaArchivo { get; set; }
        public String NombreArchivo { get; set; }


        public void SaveDocument(Document document, string providerName, string ruta)
        {
            try
            {            
                switch (providerName)
                {
                    case "Default":
                        // Get the default data provider. This will come from the Retrieval class. 
                        DefaultDataProvider defaultDataProvider = app.Core.Retrieval.Default;
                        // Use a using statement to get the PageData (for cleanup), and then save the document to disk. To Save, use the Save method above.
                        using (PageData pageData = defaultDataProvider.GetDocument(document.DefaultRenditionOfLatestRevision))
                        {
                            //Call the Save Method
                                                    
                            setRuta(pageData, document, providerName,"");
                            Utility.WriteStreamToFile(pageData.Stream, RutaArchivo);
                        }
                        break;
                    case "Native":
                        NativeDataProvider nativeProvider = app.Core.Retrieval.Native;
                        using (PageData pageData = nativeProvider.GetDocument(document.DefaultRenditionOfLatestRevision))
                        {

                            setRuta(pageData, document, providerName, "");
                            Utility.WriteStreamToFile(pageData.Stream, RutaArchivo);
                           
                        }
                        break;
                    case "Image":
                        ImageDataProvider imageProvider = app.Core.Retrieval.Image;
                        using (PageData pageData = imageProvider.GetDocument(document.DefaultRenditionOfLatestRevision))
                        {

                            setRuta(pageData, document, providerName, ruta);
                            Utility.WriteStreamToFile(pageData.Stream, RutaArchivo);
                           
                        }
                        break;
                    case "PDF":
                        PDFDataProvider pdfProvider = app.Core.Retrieval.PDF;
                        using (PageData pageData = pdfProvider.GetDocument(document.DefaultRenditionOfLatestRevision))
                        {



                            setRuta(pageData, document, providerName, ruta);
                            MemoryStream mem = new MemoryStream();
                            ///mem.
                            ///
                            pageData.Stream.CopyTo(mem);
                            string str = Convert.ToBase64String(mem.ToArray());
                            Utility.WriteStreamToFile(pageData.Stream, RutaArchivo);
                        }
                        break;
                    case "Text":
                        TextDataProvider textProvider = app.Core.Retrieval.Text;
                        using (PageData pageData = textProvider.GetDocument(document.DefaultRenditionOfLatestRevision))
                        {
                            this.SetPageData(pageData);
                            setRuta(pageData, document, providerName,ruta);
                        }
                        break;

                        
                }                 
            }
            catch (Exception ex)
            {
                app.Diagnostics.Write(ex);
                throw new Exception(ex.Message);
            }
        }


        public string SaveDocumentV2(Document document, string providerName, string ruta)
        {

            string b64 = "";
            try
            {
                switch (providerName)
                {
                    case "Default":
                        // Get the default data provider. This will come from the Retrieval class. 
                        DefaultDataProvider defaultDataProvider = app.Core.Retrieval.Default;
                        // Use a using statement to get the PageData (for cleanup), and then save the document to disk. To Save, use the Save method above.
                        using (PageData pageData = defaultDataProvider.GetDocument(document.DefaultRenditionOfLatestRevision))
                        {
                            //Call the Save Method

                            setRuta(pageData, document, providerName, "");
                            Utility.WriteStreamToFile(pageData.Stream, RutaArchivo);
                        }
                        break;
                    case "Native":
                        NativeDataProvider nativeProvider = app.Core.Retrieval.Native;
                        using (PageData pageData = nativeProvider.GetDocument(document.DefaultRenditionOfLatestRevision))
                        {

                            setRuta(pageData, document, providerName, "");
                            Utility.WriteStreamToFile(pageData.Stream, RutaArchivo);

                        }
                        break;
                    case "Image":
                        ImageDataProvider imageProvider = app.Core.Retrieval.Image;
                        using (PageData pageData = imageProvider.GetDocument(document.DefaultRenditionOfLatestRevision))
                        {

                            setRuta(pageData, document, providerName, ruta);
                            Utility.WriteStreamToFile(pageData.Stream, RutaArchivo);

                        }
                        break;
                    case "PDF":
                        PDFDataProvider pdfProvider = app.Core.Retrieval.PDF;
                        using (PageData pageData = pdfProvider.GetDocument(document.DefaultRenditionOfLatestRevision))
                        {
                            // setRuta(pageData, document, providerName, ruta);
                            MemoryStream mem = new MemoryStream();
                            ///mem.
                            ///
                            pageData.Stream.CopyTo(mem);
                            b64 = Convert.ToBase64String(mem.ToArray());
                            //Utility.WriteStreamToFile(pageData.Stream, RutaArchivo);
                        }
                        break;
                    case "Text":
                        TextDataProvider textProvider = app.Core.Retrieval.Text;
                        using (PageData pageData = textProvider.GetDocument(document.DefaultRenditionOfLatestRevision))
                        {
                            this.SetPageData(pageData);
                            setRuta(pageData, document, providerName, ruta);
                        }
                        break;


                }


                return b64;
            }
            catch (Exception ex)
            {
                app.Diagnostics.Write(ex);
                throw new Exception(ex.Message);
            }
        }

        private void setRuta(PageData pageData, Document document, string providerName, string ruta)
        {
            // Create the file path of where we want to save the document. This is completed for you.
            //string filePath = @"\\192.168.51.124\Users\Admin\Documents\" + document.ID.ToString() + "-" + providerName + "." + pageData.Extension;
            

            string filePath =  ruta + document.ID.ToString() + "-" + providerName + "." + pageData.Extension;
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
            NombreArchivo = document.Name;
            RutaArchivo = filePath;             
        }


        public String SaveDocument()
        {


            // Save the file to disk. We will want to use the Utility class. 
            //Utility.WriteStreamToFile(GetPageData().Stream, RutaArchivo);
            Byte[] bytes = File.ReadAllBytes(RutaArchivo);
            String file = Convert.ToBase64String(bytes);

            File.Delete(RutaArchivo);

            return file;

        }





        public SaveOutDocument(Hyland.Unity.Application app)
        {
            if (app == null)
            {
                throw new ArgumentNullException("app", "The Unity application object is null, make sure to connect first.");
            }
            this.app = app;
        }

    }
}
