using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hyland.Unity;
using Hyland.Unity.WorkView;

using static System.Net.Mime.MediaTypeNames;

namespace api.datecsa.UnityAPI
{
    public class GetDocument
    {
        private Hyland.Unity.Application app = null;

        public GetDocument(Hyland.Unity.Application app)
        {
            if (app == null)
            {
                throw new ArgumentNullException("app", "The Unity application object is null, make sure to connect first.");
            }
            this.app = app;
        }
        
        public Document GetDocumentForSave(long identificacion)
        {
            try
            {
                // Find the DocumentType for DocumentTypeName (passed into method). Check for null.
                /* DocumentType docType = app.Core.DocumentTypes.Find("ABONOS");
                 //DocumentType docTypeOtroSi = app.Core.DocumentTypes.Find("Otro SI");
                 if (docType == null)
                 {
                     throw new Exception("Document Type '" + "Contrato" + "' not found.");
                 } */

                // Create the DocumentQuery object.
                Document document = app.Core.GetDocumentByID(identificacion);
                return document;
            }
            catch (SessionNotFoundException ex)
            {
                app.Diagnostics.Write(ex);
                throw new Exception(ex.Message);
            }
            catch (UnityAPIException ex)
            {
                app.Diagnostics.Write(ex);
                throw new Exception(ex.Message);
            }
            catch (Exception ex)
            {
                app.Diagnostics.Write(ex);
                throw new Exception(ex.Message);
            }
        }

        public DocumentList GetDocumentForSaveIDAplicacion(string idDocumentoAplicacion, string idAplicacionOrigen)
        {
            try
            {
                // Find the DocumentType for DocumentTypeName (passed into method). Check for null.
                /* DocumentType docType = app.Core.DocumentTypes.Find("ABONOS");
                 //DocumentType docTypeOtroSi = app.Core.DocumentTypes.Find("Otro SI");
                 if (docType == null)
                 {
                     throw new Exception("Document Type '" + "Contrato" + "' not found.");
                 } */

                DocumentQuery docQuery = app.Core.CreateDocumentQuery();

                // Add the DocumentType to the document query.               

                // Add the "Loan Account #" keyword to search for. The Loan Account # value is passed into method. Be sure to use the equal operator and 'or' relation.
                docQuery.AddKeyword("idDocumentoAplicacion", idDocumentoAplicacion, KeywordOperator.Equal, KeywordRelation.And);
                docQuery.AddKeyword("idAplicacionOrigen", idAplicacionOrigen, KeywordOperator.Equal, KeywordRelation.And);
                // Create the DocumentQuery object.


                DocumentList documents = docQuery.Execute(1);

                return documents;
            }
            catch (SessionNotFoundException ex)
            {
                app.Diagnostics.Write(ex);
                throw ex;
            }
            catch (UnityAPIException ex)
            {
                app.Diagnostics.Write(ex);
                throw ex;
            }
            catch (Exception ex)
            {
                app.Diagnostics.Write(ex);
                throw ex;
            }
        }

        public string getIdBut(string emplid)
        {
            try
            {
                // Find the DocumentType for DocumentTypeName (passed into method). Check for null.
                DocumentType docType = app.Core.DocumentTypes.Find("FORMULARIO EXPEDIENTE HVE");
                //DocumentType docTypeOtroSi = app.Core.DocumentTypes.Find("Otro SI");
                if (docType == null)
                {
                    throw new Exception("Document Type '" + "FORMULARIO EXPEDIENTE HVE" + "' not found.");
                }

                DocumentQuery docQuery = app.Core.CreateDocumentQuery();

                // Add the DocumentType to the document query.               

                // Add the "Loan Account #" keyword to search for. The Loan Account # value is passed into method. Be sure to use the equal operator and 'or' relation.
                docQuery.AddKeyword("empl_id", emplid, KeywordOperator.Equal, KeywordRelation.And);

                // Create the DocumentQuery object.


                DocumentList documents = docQuery.Execute(1);
                Document doc;

                string valorIdBut = null;
                if (documents.Count > 0)
                {
                    doc = documents[0];

                    KeywordType keyIdBut = app.Core.KeywordTypes.Find("id_But");

                    foreach (KeywordRecord keyRecord in doc.KeywordRecords.FindAll(keyIdBut))
                    {
                        Keyword keywordIdBut = keyRecord.Keywords.Find(keyIdBut);
                        if (!keywordIdBut.IsBlank)
                            valorIdBut = keywordIdBut.AlphaNumericValue;

                        /*  foreach (Keyword keywordIdBut in keyRecord.Keywords.FindAll(keyIdBut))
                         {
                             if(!keywordIdBut.IsBlank)
                             valorIdBut = keywordIdBut.AlphaNumericValue;
                         } */
                    }
                }
                return valorIdBut;
            }
            catch (SessionNotFoundException ex)
            {
                app.Diagnostics.Write(ex);
                throw new Exception(ex.Message);
            }
            catch (UnityAPIException ex)
            {
                app.Diagnostics.Write(ex);
                throw new Exception(ex.Message);
            }
            catch (Exception ex)
            {
                app.Diagnostics.Write(ex);
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Consulta document en onbase
        /// </summary>
        /// <param name="NomDoc"></param>
        /// <param name="NomKwkey"></param>
        /// <param name="valuekey"></param>
        /// <param name="CntDoct"></param>
        /// <returns>Retorna lista de documentos</returns>
        public DocumentList GetListDocuments(string NomDoc, List<string> NomKwkey, List<string> valuekey, long CntDoct)
        {
            try
            {
                // Create the DocumentQuery object.
                DocumentQuery docQuery = app.Core.CreateDocumentQuery();

                // Find the DocumentType for DocumentTypeName (passed into method). Check for null.
                DocumentType docType = app.Core.DocumentTypes.Find(NomDoc);

                if (docType == null)
                {
                    throw new Exception("Document Type '" + NomDoc + "' not found.");
                }

                // Add the DocumentType to the document query.               
                docQuery.AddDocumentType(docType);

                // Add the "Loan Account #" keyword to search for. The Loan Account # value is passed into method. Be sure to use the equal operator and 'or' relation.
                for (int i = 0; i < NomKwkey.Count; i++)
                {
                    docQuery.AddKeyword(NomKwkey[i], valuekey[i], KeywordOperator.Equal, KeywordRelation.And);
                }

                DocumentList documents = docQuery.Execute(CntDoct);

                return documents;
            }
            catch (SessionNotFoundException ex)
            {
                app.Diagnostics.Write(ex);
                throw new Exception(ex.Message);
            }
            catch (UnityAPIException ex)
            {
                app.Diagnostics.Write(ex);
                throw new Exception(ex.Message);
            }
            catch (Exception ex)
            {
                app.Diagnostics.Write(ex);
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Consulta document en onbase
        /// <param name="keyword"></param>
        /// <param name="valuekey"></param>
        /// <param name="CntDoct"></param>
        /// <returns>Retorna lista de documentos</returns>
        public DocumentList GetListDocuments(Keyword keyword, List<string> valuekey, string NomDoc, long CntDoct)
        {
            try
            {
                // Create the DocumentQuery object.
                DocumentQuery docQuery = app.Core.CreateDocumentQuery();

                // Find the DocumentType for DocumentTypeName (passed into method). Check for null.
                DocumentType docType = app.Core.DocumentTypes.Find(NomDoc);

                if (docType == null)
                {
                    throw new Exception("Document Type '" + NomDoc + "' not found.");
                }

                // Add the DocumentType to the document query.               
                docQuery.AddDocumentType(docType);

                // Add the "Loan Account #" keyword to search for. The Loan Account # value is passed into method. Be sure to use the equal operator and 'or' relation.
                //for (int i = 0; i < NomKwkey.Count; i++)
                //{
                //    docQuery.AddKeyword(keyword, KeywordOperator.Equal, KeywordRelation.And);
                //}

                DocumentList documents = docQuery.Execute(CntDoct);

                return documents;
            }
            catch (SessionNotFoundException ex)
            {
                app.Diagnostics.Write(ex);
                throw new Exception(ex.Message);
            }
            catch (UnityAPIException ex)
            {
                app.Diagnostics.Write(ex);
                throw new Exception(ex.Message);
            }
            catch (Exception ex)
            {
                app.Diagnostics.Write(ex);
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Consulta document en onbase
        /// </summary>
        /// <param name="NomDoc"></param>
        /// <param name="NomKwkey"></param>
        /// <param name="valuekey"></param>
        /// <param name="CntDoct"></param>
        /// <returns>Retorna lista de documentos</returns>
        public DocumentList GetListDocuments(string NomDoc, string NomKwkey, string valuekey, long CntDoct, Hyland.Unity.Application appp)
        {
            try
            {
                // Create the DocumentQuery object.
                DocumentQuery docQuery = appp.Core.CreateDocumentQuery();

                // Find the DocumentType for DocumentTypeName (passed into method). Check for null.
                DocumentType docType = appp.Core.DocumentTypes.Find(NomDoc);

                if (docType == null)
                {
                    throw new Exception("Document Type '" + NomDoc + "' not found.");
                }

                // Add the DocumentType to the document query.               
                docQuery.AddDocumentType(docType);

                // Add the "Loan Account #" keyword to search for. The Loan Account # value is passed into method. Be sure to use the equal operator and 'or' relation.
                docQuery.AddKeyword(NomKwkey, valuekey, KeywordOperator.Equal, KeywordRelation.And);


                DocumentList documents = docQuery.Execute(CntDoct);

                return documents;
            }
            catch (SessionNotFoundException ex)
            {
                appp.Diagnostics.Write(ex);
                throw new Exception(ex.Message);
            }
            catch (UnityAPIException ex)
            {
                appp.Diagnostics.Write(ex);
                throw new Exception(ex.Message);
            }
            catch (Exception ex)
            {
                appp.Diagnostics.Write(ex);
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Consulta el valor de las kw de un documento
        /// </summary>
        /// <param name="keywordTypesList">Lista con los keyword type</param>
        /// <param name="documents">Documento del que se desean los datos</param>
        /// <returns>Retortna un diccionario de datos donde el primer campo corresponde al nombre de la kw y el segundo corresponde al valor que almacena</returns>
        public List<Dictionary<string, string>> ListDocKewordValue(List<KeywordType> keywordTypesList, Document document)
        {
            List<Dictionary<string, string>> datos = new List<Dictionary<string, string>>();

            Dictionary<string, string> keywordValue = new Dictionary<string, string>();

            foreach (KeywordType itemtype in keywordTypesList)
            {
                foreach (KeywordRecord keyRecord in document.KeywordRecords.FindAll(itemtype))
                {
                    foreach (Keyword keyword in keyRecord.Keywords.FindAll(itemtype))
                    {
                        keywordValue.Add(itemtype.Name, keyword.ToString());
                    }
                }
            }
            datos.Add(keywordValue);
            return datos;
        }
    }
}

