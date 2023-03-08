using System;
using System.Collections.Generic;
using Hyland.Unity;
using api.datecsa.modelo;
using Hyland.Unity.WorkView;

namespace api.datecsa.UnityAPI
{
	public class GetAccountDocuments
	{
		private Hyland.Unity.Application app = null;

		public DocumentList GetDocumentsForContract(IList<Metadato> PalabrasClave, string FechaInicial, string FechaFinal, string tipoDocumental,string IdAplicacionOrigen)
		{
			try
			{
                // Find the DocumentType for DocumentTypeName (passed into method). Check for null.
                //DocumentType docType = app.Core.DocumentTypes.Find("CON Contrato Firmado");
                DocumentQuery docQuery = app.Core.CreateDocumentQuery();

                DocumentType docType = null;

                if (tipoDocumental != null) {

                    Hyland.Unity.WorkView.WorkView wvModule = app.WorkView;

                    //Get the 'Unity Example Application - Help Desk' Application object.
                    Hyland.Unity.WorkView.Application wvApplication = wvModule.Applications.Find("Parámetros");

                    //Get the 'All Customers' Filter created that is used in Unity.
                    Hyland.Unity.WorkView.Filter filter = wvApplication.Filters.Find("Todos Las Homologaciones de Tipos Documentales");

                    //Create a FilterQuery object using the 'All Customers' Filter as the source.
                    Hyland.Unity.WorkView.FilterQuery filterQuery = filter.CreateFilterQuery();

                    filterQuery.AddConstraint("CódigoSistemaExterno", Operator.Equal, IdAplicacionOrigen);
                    filterQuery.AddConstraint("TipoDocumentalSistemaExterno", Operator.Equal, tipoDocumental );


                    FilterQueryResultItemList results = filterQuery.Execute(1);

                    string tipoDocumentalOnbase = "";

                    foreach (FilterQueryResultItem result in results)
                    {
                        FilterColumnValue columnValue = result.GetFilterColumnValue("TiposDocumentalesOnBase");

                        //Determine if the ColumnValue has a valid value set.
                        if (columnValue.HasValue == true)
                        {
                            //Get the name of the 'Customer'.
                            tipoDocumentalOnbase = columnValue.AlphanumericValue;

                            //Determine if the customer name is John Smith

                        }
                    }

                docType = app.Core.DocumentTypes.Find(tipoDocumentalOnbase);
                if (docType == null)
                {
                    throw new Exception("Document Type '" + tipoDocumental + "' not found.");
                 
                }
    
                docQuery.AddDocumentType(docType);


                }

                // Add the DocumentType to the document query.

                KeywordType keyType = null;
                Keyword key = null;
                string dataType = null;
                DateTime date = new DateTime();
                foreach (Metadato met in PalabrasClave)
                {
                    keyType = app.Core.KeywordTypes.Find(met.nombre);
                     dataType = keyType.DataType.ToString();
                    switch (dataType.ToUpper())
                    {
                        case "DATE":
                            
                            DateTime.TryParse(met.valor, out date);
                            key = keyType.CreateKeyword(date);
                            break;

                        case "DATETIME":                   
                            DateTime.TryParse(met.valor, out date);
                            key = keyType.CreateKeyword(date);
                            break;

                        case "ALPHANUMERIC":
                            key = keyType.CreateKeyword(met.valor);
                            break;

                        case "INTEGER":
                            int valor;
                            int.TryParse(met.valor, out valor);
                            key = keyType.CreateKeyword(met.valor);
                            break;
                    }
                    docQuery.AddKeyword(key, KeywordOperator.Equal, KeywordRelation.And);
                }

                DateTime fechaInicio = DateTime.ParseExact(FechaInicial, "d/M/yyyy",
                                        System.Globalization.CultureInfo.InvariantCulture);

                DateTime fechaFin = DateTime.ParseExact(FechaFinal, "d/M/yyyy",
                                       System.Globalization.CultureInfo.InvariantCulture);

                docQuery.AddDateRange(fechaInicio, fechaFin);

                // Execute the Query.
                DocumentList documents = docQuery.Execute(1000);
              
                return documents;
			}
			catch (SessionNotFoundException ex)
			{
				app.Diagnostics.Write(ex);
                throw ex;
            }
            catch (MaxLicensesException ex)
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


        public DocumentList GetDocumentsForContractV2(IList<Metadato> PalabrasClave, string FechaInicial, string FechaFinal, string tipoDocumental, string IdAplicacionOrigen)
        {
            try
            {
                // Find the DocumentType for DocumentTypeName (passed into method). Check for null.
                //DocumentType docType = app.Core.DocumentTypes.Find("CON Contrato Firmado");
                DocumentQuery docQuery = app.Core.CreateDocumentQuery();

                DocumentType docType = null;

                if (tipoDocumental != null)
                {

                    KeywordType codigoSistemaExternoKT = app.Core.KeywordTypes.Find("CÓDIGO SISTEMA EXTERNO");
                    Keyword codigoSistemaExternoKW = codigoSistemaExternoKT.CreateKeyword(IdAplicacionOrigen);
                    KeywordType TipoDocumentalSistemaExternoKT = app.Core.KeywordTypes.Find("TIPO DOCUMENTAL SISTEMA EXTERNO");
                    Keyword TipoDocumentalSistemaExternoKW = TipoDocumentalSistemaExternoKT.CreateKeyword(tipoDocumental);

                    Keyset keysetHomologacion = app.Core.AutoFillKeywordSets.Find("Consultar Homologaciones de Tipos Documentales");

                    List<Keyword> primaryKeywordsHomolog = new List<Keyword>();
                    //Keyword primaryKeywordHomolog = keysetHomologacion.PrimaryKeywordType.CreateKeyword(codigoSistemaExternoKW.AlphaNumericValue);

                    primaryKeywordsHomolog.Add(codigoSistemaExternoKW);
                    //primaryKeywordsHomolog.Add(TipoDocumentalSistemaExternoKW);

                    // keysetDatas will contain an item for each matching keyword record
                    KeysetDataList keysetDatasHomolog = keysetHomologacion.GetKeysetData(primaryKeywordsHomolog);

                    string tipoDocumentalOnbase = "";
                    foreach (KeysetData keysetX in keysetDatasHomolog)
                    {


                        Keyword kw = keysetX.Keywords.Find("TIPO DOCUMENTAL SISTEMA EXTERNO");

                        if (kw.AlphaNumericValue.ToUpper().Equals(tipoDocumental))
                        {

                            tipoDocumentalOnbase = keysetX.Keywords.Find("TIPOLOGIA_DOCUMENTAL").AlphaNumericValue;


                        }


                    }

                    docType = app.Core.DocumentTypes.Find(tipoDocumentalOnbase);
                    if (docType == null)
                    {
                        throw new Exception("Document Type '" + tipoDocumental + "' not found.");

                    }

                    docQuery.AddDocumentType(docType);


                }

                // Add the DocumentType to the document query.

                KeywordType keyType = null;
                Keyword key = null;
                string dataType = null;
                DateTime date = new DateTime();
                foreach (Metadato met in PalabrasClave)
                {
                    keyType = app.Core.KeywordTypes.Find(met.nombre);
                    dataType = keyType.DataType.ToString();
                    switch (dataType.ToUpper())
                    {
                        case "DATE":

                            DateTime.TryParse(met.valor, out date);
                            key = keyType.CreateKeyword(date);
                            break;

                        case "DATETIME":
                            DateTime.TryParse(met.valor, out date);
                            key = keyType.CreateKeyword(date);
                            break;

                        case "ALPHANUMERIC":
                            key = keyType.CreateKeyword(met.valor);
                            break;

                        case "INTEGER":
                            int valor;
                            int.TryParse(met.valor, out valor);
                            key = keyType.CreateKeyword(met.valor);
                            break;
                    }
                    docQuery.AddKeyword(key, KeywordOperator.Equal, KeywordRelation.And);
                }

                DateTime fechaInicio = DateTime.ParseExact(FechaInicial, "d/M/yyyy",
                                        System.Globalization.CultureInfo.InvariantCulture);

                DateTime fechaFin = DateTime.ParseExact(FechaFinal, "d/M/yyyy",
                                       System.Globalization.CultureInfo.InvariantCulture);

                docQuery.AddDateRange(fechaInicio, fechaFin);

                // Execute the Query.
                DocumentList documents = docQuery.Execute(1000);

                return documents;
            }
            catch (SessionNotFoundException ex)
            {
                app.Diagnostics.Write(ex);
                throw ex;
            }
            catch (MaxLicensesException ex)
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

        public GetAccountDocuments(Hyland.Unity.Application app)
		{
			if (app == null)
			{
				throw new ArgumentNullException("app", "The Unity application object is null, make sure to connect first.");
			}
			this.app = app;
		}


        public Document GetExpediente(string nombreTipologiaExpediente, IList<Metadato> PalabrasClave)
        {
            try
            {
                // Find the DocumentType for DocumentTypeName (passed into method). Check for null.
                //DocumentType docType = app.Core.DocumentTypes.Find("CON Contrato Firmado");
                DocumentType docType = app.Core.DocumentTypes.Find(nombreTipologiaExpediente);
                if (docType == null)
                {
                    throw new Exception("Document Type '" + nombreTipologiaExpediente + "' not found.");
                }



                // Create the DocumentQuery object.
                DocumentQuery docQuery = app.Core.CreateDocumentQuery();



                // Add the DocumentType to the document query.
                docQuery.AddDocumentType(docType);


                foreach (Metadato met in PalabrasClave)
                {

                
                    docQuery.AddKeyword(met.nombre, met.valor, KeywordOperator.Equal, KeywordRelation.And);


                }

            

                // Execute the Query.
                DocumentList documents = docQuery.Execute(1);
                if (documents.Count > 0)
                    return documents[0];
                else
                    return null;

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
   
        public int GetDocumentoVersionable(string id_but, string tipoDocumental, string IdAplicacionOrigen)
        {
             
            DocumentQuery docQuery = app.Core.CreateDocumentQuery();
            DocumentType docType = null;


            docType = app.Core.DocumentTypes.Find(tipoDocumental);
            if (docType == null)
            {
                throw new Exception("Document Type '" + tipoDocumental + "' not found.");

            }

            docQuery.AddDocumentType(docType);

            KeywordType keyType = null;

            keyType = app.Core.KeywordTypes.Find("ID_BUT");

            Keyword key = null;

            key = keyType.CreateKeyword(id_but);
           
            docQuery.AddKeyword(key, KeywordOperator.Equal, KeywordRelation.And);
            DocumentList documents = docQuery.Execute(1);

            if(documents.Count>0)
            return 1;


            return 0;

        }
    
 
    }
}
