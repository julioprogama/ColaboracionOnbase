using System;
using System.Collections.Generic;
using Hyland.Unity.Extensions;
using Hyland.Unity;
using Hyland.Unity.UnityForm;
using System.IO;
using api.datecsa.modelo;
using Hyland.Unity.WorkView;

namespace api.datecsa.UnityAPI
{
    public class Archival
    {
        private Hyland.Unity.Application app = null;

        //La creacióndel documento en Onbase Se realiza con el tipo documental Generico
        //"DOCUMENTO ADJUNTO FORMULARIO IMPORTACION HVE
        //En el flujo de indexación CARGAR DOCUMENTOS HVE se realizan tareas adicionales para el expediente.
        public object StoreNewDocument64(IList<Metadato> listaMetadatos, string base64, string nombreTipologiaDocumental, string IdAplicacionOrigen, string RutaArchivo)
        {
            Document newDocument = null;
            try
            {


                KeywordType codigoSistemaExternoKT = app.Core.KeywordTypes.Find("CÓDIGO SISTEMA EXTERNO");
                Keyword codigoSistemaExternoKW = codigoSistemaExternoKT.CreateKeyword(IdAplicacionOrigen);
                KeywordType TipoDocumentalSistemaExternoKT = app.Core.KeywordTypes.Find("TIPO DOCUMENTAL SISTEMA EXTERNO");
                Keyword TipoDocumentalSistemaExternoKW = TipoDocumentalSistemaExternoKT.CreateKeyword(nombreTipologiaDocumental);

                Keyset keysetHomologacion = app.Core.AutoFillKeywordSets.Find("Consultar Homologaciones de Tipos Documentales");

                List<Keyword> primaryKeywordsHomolog = new List<Keyword>();
                //Keyword primaryKeywordHomolog = keysetHomologacion.PrimaryKeywordType.CreateKeyword(codigoSistemaExternoKW.AlphaNumericValue);

                primaryKeywordsHomolog.Add(codigoSistemaExternoKW);
                //primaryKeywordsHomolog.Add(TipoDocumentalSistemaExternoKW);

                // keysetDatas will contain an item for each matching keyword record
                KeysetDataList keysetDatasHomolog = keysetHomologacion.GetKeysetData(primaryKeywordsHomolog);

                string tipoDocumentalOnbase = "";
                foreach (KeysetData keysetX in keysetDatasHomolog) {


                    Keyword kw = keysetX.Keywords.Find("TIPO DOCUMENTAL SISTEMA EXTERNO");

                    if (kw.AlphaNumericValue.ToUpper().Equals(nombreTipologiaDocumental))
                    {

                        tipoDocumentalOnbase = keysetX.Keywords.Find("TIPOLOGIA_DOCUMENTAL").AlphaNumericValue;


                    }


                }



                //Se crea el tipo documental generico para realizar la indexación y que el flujo lo reindexe al tipo enviado. 
                string tipologiaDocumentalGenerica = "DOCUMENTO ADJUNTO FORMULARIO IMPORTACION HVE";
                DocumentType documentTypeGenerico = app.Core.DocumentTypes.Find(tipologiaDocumentalGenerica);

                List<EditableKeywordRecord> newRecList = new List<EditableKeywordRecord>();

                Random rnd = new Random();
               // int month = rnd.Next(1, 13);  // creates a number between 1 and 12
                int dice = rnd.Next(1, 99);   // creates a number between 1 and 6
                //int card = rnd.Next(52);     // creates a number between 0 and 51

               


                //Se valida que en la lista de Metadatos vengan los metadatos obligatorios.
                //La validación se realiza con el tipo documental enviado.
                //Los metadatos obligatorios se configuran en las opciones de los tipos documentales.


                DocumentType documentType = app.Core.DocumentTypes.Find(tipoDocumentalOnbase);


                if (documentType != null)
                {
                    // Find the DocumentType for "Loan Application". Check for null.                  
                    KeywordTypeList listObligatorios = documentType.KeywordTypesRequiredForArchival;
                    //Valida
                    string validarObligatoriedad3 = validarObligatoriedad(listObligatorios, listaMetadatos);
                    if (validarObligatoriedad3 != null)
                    {
                        throw new Exception(validarObligatoriedad3 + " es obligatorio");
                    }
                }
                else
                {

                    throw new Exception("No Existe la Tipología Documental");

                }

                //Obtener Keywords Requeridas
                // Keyword nombreProgramaKeyword = obtenerNombreProgramaKeyword(listaMetadatos);
                Keyword codigoProgramaKeyword = obtenerCodigoProgramaKeyword(listaMetadatos);
                Keyword codigoGeograficoKeyword = obtenerCodigoGeografico(listaMetadatos);
                Keyword idButKeyword = obtenerIdButKeyword(listaMetadatos);


                //Validar Id De la BUT no venga vacío ni 


                if(idButKeyword != null)
                {

                    if (idButKeyword.AlphaNumericValue.Equals("0") || idButKeyword.AlphaNumericValue.Length == 0)
                    {

                        throw new Excepcion("S02", "El ID_BUT no puede ser vacío o cero");



                    }



                }

                //Consultar Documento Versionable Existent           

                GetAccountDocuments consultarDocument = new GetAccountDocuments(app);

                if (esVersionable(tipoDocumentalOnbase))
                {

                    int cantidadDocumentos = consultarDocument.GetDocumentoVersionable(idButKeyword.AlphaNumericValue, tipoDocumentalOnbase, IdAplicacionOrigen);


                    if (cantidadDocumentos > 0)
                    {

                        throw new ExcepcionVersionExiste("S12","Ya existe una versión del tipo documental " + nombreTipologiaDocumental + "para el id_But: " + idButKeyword.AlphaNumericValue);


                    }
                }

                
                //Se arma el asunto con los valores de las palabras claves enviadas.
                string valorAsunto = armarAsunto(listaMetadatos);

                //Se crea el Keyword Asunto
                KeywordType asuntoKeyType = app.Core.KeywordTypes.Find("ASUNTO");

                // Create a keyword of this type. Use the TryCreateKeyword extension method. The AccountNum is passed into the method.
                Keyword asuntoKeyword = null;
                if (!asuntoKeyType.TryCreateKeyword(valorAsunto, out asuntoKeyword))
                {
                    throw new Exception(asuntoKeyType.Name + "keyword could not be created.");
                }


                //Llamar autofill de código de Referencio con nombre de programa y código de Ciudad.

                Keyset keyset = app.Core.AutoFillKeywordSets.Find("Consultar Codigo de Referencia por Codigo Programa");
                Keyword codigoReferenciaKeyword = null;

                if (keyset != null)
                {
                    // Specify which primary keywords you’re interested in; you can specify more than one.
                    List<Keyword> primaryKeywords = new List<Keyword>();
                    Keyword primaryKeyword = keyset.PrimaryKeywordType.CreateKeyword(codigoProgramaKeyword.AlphaNumericValue);

                    primaryKeywords.Add(primaryKeyword);

                    // keysetDatas will contain an item for each matching keyword record
                    KeysetDataList keysetDatas = keyset.GetKeysetData(primaryKeywords);



                    if (keysetDatas.Count > 0)
                    {

                        foreach (KeysetData keyData in keysetDatas)
                        {
                            KeywordList keywords = keyData.Keywords;
                            Keyword codCiudad = keywords.Find("CODIGO_GEOGRAFICO");

                            if (codCiudad.AlphaNumericValue.Equals(codigoGeograficoKeyword.AlphaNumericValue))
                            {
                                //Crear el keyword de codigo de referencia.

                                codigoReferenciaKeyword = keywords.Find("CODIGO_REFERENCIA");
                                break;

                            }

                        }

                    }
                    else
                    {

                        throw new Exception("No existen códigos de referencia para el programa");

                    }


                }
                else
                {



                    throw new Exception("Error en autofill Consultar Codigo de Referencia del Programa BOG");
                }
                //llamar el autofill  de codigo de referencia
                Keyset keysetCodigoReferencia = app.Core.AutoFillKeywordSets.Find("Consultar TRD HVE del Código de Referencia");

                // keysetDatas will contain an item for each matching keyword record
                KeysetDataList keysetDatasCodigoReferencia;
                if (codigoReferenciaKeyword != null)
                    keysetDatasCodigoReferencia = keysetCodigoReferencia.GetKeysetData(codigoReferenciaKeyword);
                else
                    throw new Exception("Código de Referencia Geografica No Válido");

                Keyword codigo_serie = null;
                Keyword nombre_serie = null;
                Keyword codigo_subserie = null;
                Keyword nombre_subserie = null;
                Keyword codigo_subfondo = null;
                Keyword nombre_subfondo = null;
                Keyword codigo_seccion = null;
                Keyword nombre_seccion = null;
                Keyword nombre_subseccion = null;
                Keyword codigo_subseccion = null;
                Keyword wv_id_objeto_TRD = null;

                if (keysetDatasCodigoReferencia.Count > 0) {

                    foreach (KeysetData keyData in keysetDatasCodigoReferencia)
                    {
                        KeywordList keywords = keyData.Keywords;


                        codigo_serie = keywords.Find("CODIGO_SERIE");
                        nombre_serie = keywords.Find("NOMBRE_SERIE");
                        codigo_subserie = keywords.Find("CODIGO_SUBSERIE");
                        nombre_subserie = keywords.Find("NOMBRE_SUBSERIE");
                        codigo_subfondo = keywords.Find("CODIGO_SUBFONDO");
                        nombre_subfondo = keywords.Find("NOMBRE_SUBFONDO");
                        codigo_seccion = keywords.Find("CODIGO_SECCION");
                        nombre_seccion = keywords.Find("NOMBRE_SECCION");
                        codigo_subseccion = keywords.Find("CODIGO_SUBSECCION");
                        nombre_subseccion = keywords.Find("NOMBRE_SUBSECCION");
                        wv_id_objeto_TRD = keywords.Find("wv_id_objeto_TRD");

                    }
                }
                else
                {

                    throw new Exception("No existe TRD Configurada para el código de Referencia");
                }


                //Llamar autofill Expediente. 

                Keyset keysetExpediente = app.Core.AutoFillKeywordSets.Find("Consultar expediente HVE");
                KeysetDataList keysetDatasExpediente = keysetExpediente.GetKeysetData(idButKeyword);
                Keyword estado_expediente = null;
                Keyword fechaInicial = null;
                Keyword numero_documentos_expediente = null;
                Keyword numero_carpetas_fisica_expediente = null;
                Keyword numero_folios_fisicos_expediente = null;
                Keyword ubicacion_fisica_gestion = null;
                Keyword ubicacion_carpeta_fisica_expediente = null;
                Keyword observaciones = null;
                Keyword numeroFoliosDigitalesExpediente = null;
                Keyword numeroFoliosTotalesExpediente = null;




                foreach (KeysetData keyData in keysetDatasExpediente)
                {
                    KeywordList keywords = keyData.Keywords;
                    estado_expediente = keywords.Find("ESTADO_EXPEDIENTE");
                    fechaInicial = keywords.Find("FECHA_INICIAL");
                    numero_documentos_expediente = keywords.Find("NUMERO_CARPETAS_FISICA_EXPEDIENTE");
                    numero_carpetas_fisica_expediente = keywords.Find("NUMERO_DOCUMENTOS_EXPEDIENTE");
                    numero_folios_fisicos_expediente = keywords.Find("NUMERO_FOLIOS_FISICO_EXPEDIENTE");
                    ubicacion_fisica_gestion = keywords.Find("UBICACION_FISICA_GESTION");
                    ubicacion_carpeta_fisica_expediente = keywords.Find("UBICACION_CARPETA_FISICA_EXPEDIENTE");
                    observaciones = keywords.Find("OBSERVACIONES");
                    numeroFoliosDigitalesExpediente = keywords.Find("NUMERO_FOLIOS_DIGITALES_EXPEDIENTE");
                    numeroFoliosTotalesExpediente = keywords.Find("NUMERO_FOLIOS_TOTALES_EXPEDIENTE");
                }

                Keyword tipologiaDocumental = null;
                Keyword capitulo = null;
                //Keyword numero_documentos_expediente = null;


                //Llamar Autofill Capitulo
                Keyset keysetCapitulo = app.Core.AutoFillKeywordSets.Find("Consultar Tipos Documentales de la TRD");
                KeysetDataList keysetDatasCapitulo = keysetCapitulo.GetKeysetData(wv_id_objeto_TRD);
                bool encuentraTipologia = false;
                if (keysetDatasCapitulo.Count > 0) {

                    foreach (KeysetData keyData in keysetDatasCapitulo)
                    {
                        KeywordList keywords = keyData.Keywords;

                        tipologiaDocumental = keywords.Find("TIPOLOGIA_DOCUMENTAL");

                        if (tipologiaDocumental.AlphaNumericValue.Equals(tipoDocumentalOnbase))
                        {
                            //Crear el keyword de codigo de referencia.
                            capitulo = keywords.Find("CAPITULO");
                            if (capitulo != null)
                            {
                                encuentraTipologia = true;
                            }
                            break;
                        }
                    }
                }
                else
                {
                    throw new Exception("No Existe Capitulo Configurado para el tipo Documental");
                }

                if (!encuentraTipologia)
                {

                    throw new Exception("La tabla de referencia no contiene el tipo documental");

                }


                string filePath = RutaArchivo + "file" + DateTime.Today.Year + DateTime.Now.Month + DateTime.Now.Day + DateTime.Now.Hour + DateTime.Now.Second + DateTime.Now.Millisecond + dice + ".pdf";
                byte[] bytes = Convert.FromBase64String(base64);
                System.IO.FileStream stream = new FileStream(filePath, FileMode.CreateNew);
                System.IO.BinaryWriter writer = new BinaryWriter(stream);
                writer.Write(bytes, 0, bytes.Length);
                writer.Close();


                // Create the PageData from the file path. (Use a using statement).
                using (PageData pageData = app.Core.Storage.CreatePageData(filePath))
                {
                    if (nombreTipologiaDocumental != null)
                    {
                        // Find the DocumentType for "Loan Application". Check for null.
                        // DocumentType documentType = app.Core.DocumentTypes.Find(nombreTipologiaDocumental);

                        // KeywordTypeList listObligatorios = documentType.KeywordTypesRequiredForArchival;
                        KeywordRecordTypeList keyTypeRecList = documentType.KeywordRecordTypes;
                        foreach (KeywordRecordType rec in keyTypeRecList)
                        {
                            string InstanceType = rec.RecordType.ToString().ToUpper();
                            if (InstanceType.Equals("MULTIINSTANCE"))
                            {
                                newRecList.Add(rec.CreateEditableKeywordRecord());
                            }
                        }
                        // Find the FileType for "Image File format". Check for null.
                        FileType imageFileType = app.Core.FileTypes.Find("PDF"); //or search for 2
                        if (imageFileType == null)
                        {
                            throw new Exception("Could not find file format: PDF");
                        }
                        // Create the StoreNewDocumentProperties object.
                        StoreNewDocumentProperties newDocProps = app.Core.Storage.CreateStoreNewDocumentProperties(documentTypeGenerico, imageFileType);

                        foreach (Metadato met in listaMetadatos)
                        {

                            var valorMetadatoX = met.valor;
                            // Find the keyword type for "Loan Account #". Check for null.
                            KeywordType nombreCompletoKeyType = app.Core.KeywordTypes.Find(met.nombre);

                            // Create a keyword of this type. Use the TryCreateKeyword extension method. The AccountNum is passed into the method.
                            Keyword valorMetadatoKeyword = null;
                            if (!nombreCompletoKeyType.TryCreateKeyword(valorMetadatoX, out valorMetadatoKeyword))
                            {
                                throw new Exception(met.nombre + "keyword could not be created.");
                            }
                            // Add the new keyword to our properties.


                            bool kwInKwRec = false;

                            foreach (KeywordRecordType kwRecType in keyTypeRecList)
                            {
                                string recordType = kwRecType.RecordType.ToString().ToUpper();

                                if (recordType.Equals("MULTIINSTANCE"))
                                {

                                    if (kwRecType.KeywordTypes.Contains(nombreCompletoKeyType))
                                    {

                                        foreach (EditableKeywordRecord rep in newRecList)
                                        {

                                            if (rep.KeywordRecordType.Name.Equals(kwRecType.Name))
                                            {

                                                if (!rep.Keywords.Contains(valorMetadatoKeyword))
                                                {

                                                    rep.AddKeyword(valorMetadatoKeyword);
                                                    kwInKwRec = true;
                                                }
                                                else
                                                {


                                                }



                                            }

                                        }
                                    }
                                }


                            }




                            if (kwInKwRec != true)
                            {
                                newDocProps.AddKeyword(valorMetadatoKeyword);

                            }
                        }//


                        foreach (EditableKeywordRecord repKW in newRecList)
                        {
                            newDocProps.AddKeywordRecord(repKW);
                        }


                        newDocProps.AddKeyword(codigoReferenciaKeyword);
                        newDocProps.AddKeyword(tipologiaDocumental);
                        newDocProps.AddKeyword(asuntoKeyword);
                        newDocProps.AddKeyword(codigo_serie);
                        newDocProps.AddKeyword(nombre_serie);
                        newDocProps.AddKeyword(codigo_subserie);
                        newDocProps.AddKeyword(nombre_subserie);
                        newDocProps.AddKeyword(codigo_subfondo);
                        newDocProps.AddKeyword(codigo_seccion);
                        newDocProps.AddKeyword(nombre_seccion);
                        newDocProps.AddKeyword(codigo_subseccion);
                        newDocProps.AddKeyword(nombre_subseccion);
                        newDocProps.AddKeyword(wv_id_objeto_TRD);
                        newDocProps.AddKeyword(capitulo);



                        if (estado_expediente != null) {
                            newDocProps.AddKeyword(estado_expediente);

                        }


                        if (fechaInicial != null) {
                            newDocProps.AddKeyword(fechaInicial);
                        }
                        if (numero_documentos_expediente != null)
                            newDocProps.AddKeyword(numero_documentos_expediente);
                        if (numero_carpetas_fisica_expediente != null)
                            newDocProps.AddKeyword(numero_carpetas_fisica_expediente);
                        if (numero_folios_fisicos_expediente != null)
                            newDocProps.AddKeyword(numero_folios_fisicos_expediente);
                        if (ubicacion_fisica_gestion != null)
                            newDocProps.AddKeyword(ubicacion_fisica_gestion);
                        if (ubicacion_carpeta_fisica_expediente != null)
                            newDocProps.AddKeyword(ubicacion_carpeta_fisica_expediente);
                        if (observaciones != null)
                            newDocProps.AddKeyword(observaciones);
                        if (numeroFoliosDigitalesExpediente != null)
                            newDocProps.AddKeyword(numeroFoliosDigitalesExpediente);
                        if (numeroFoliosTotalesExpediente != null)
                       newDocProps.AddKeyword(numeroFoliosTotalesExpediente);



                        newDocument = app.Core.Storage.StoreNewDocument(pageData, newDocProps);





                    }
                    else
                    {
                        throw new Excepcion("S", "La tipología documental " + nombreTipologiaDocumental + " No Existe");
                    }
                }
                File.Delete(filePath);
                return newDocument;

            }
            catch (SessionNotFoundException ex)
            {

                if (app != null)
                    app.Diagnostics.Write(ex);
                throw new Exception("The Unity API session could not be found, please reconnect.", ex);
            }
            catch (UnityAPIException ex)
            {
                if (app != null)
                    app.Diagnostics.Write(ex);
                throw new Exception(ex.Message, ex);
            }
            catch (ExcepcionVersionExiste exc)
            {
                if (app != null)
                    app.Diagnostics.Write(exc);
                //  connection.Disconnect(application);

                throw exc;

            }
            catch (Exception ex)
            {
                if (app != null)
                    app.Diagnostics.Write(ex);
                throw new Exception(ex.Message, ex);

            }
        }

        private void ActualizarFolios(Hyland.Unity.Application unityApplication, Document documentoActualizar, long valor)
        {

            using (DocumentLock documentLock = documentoActualizar.LockDocument())
            {
                // check whether document was locked
                if (documentLock.Status == DocumentLockStatus.LockObtained)
                {

                    Rendition ren = documentoActualizar.DefaultRenditionOfLatestRevision;
                    long numero = ren.NumberOfPages;
                    KeywordModifier keyModifier = documentoActualizar.CreateKeywordModifier();
                    //int valorEntero = Convert.ToInt32(numero);
                    // get keyword type object(used here to find keyword to modify)
                    KeywordType keywordType = unityApplication.Core.KeywordTypes.Find("FOLIO_ELECTRONICO_HVE");

                    // retrieve the keyword record that contains the keyword to be modified
                    KeywordRecord keyRecord = documentoActualizar.KeywordRecords.Find(keywordType);

                    // Create new keyword for keyword type, to hold the updated value
                    Keyword newKeyword = keywordType.CreateKeyword(numero);

                    // Retrieve keyword to update (could be multiple instances of the keyword)
                    foreach (Keyword keyword in keyRecord.Keywords.FindAll(keywordType))
                    {
                        // Update the keyword in the keyword modifier object
                        keyModifier.UpdateKeyword(keyword, newKeyword);
                    }

                    keyModifier.ApplyChanges();

                }
                else
                {
                    // document was already locked
                }
            }


        }

        private Keyword obtenerCodigoGeografico(IList<Metadato> listaMetadatos)
        {
            foreach (Metadato met in listaMetadatos)
            {

                if (met.nombre.Equals("CODIGO_GEOGRAFICO"))
                {

                    KeywordType codigoGeograficoKeyType = app.Core.KeywordTypes.Find("CODIGO_GEOGRAFICO");

                    // Create a keyword of this type. Use the TryCreateKeyword extension method. The AccountNum is passed into the method.
                    Keyword codigoGeograficoKeyword = null;
                    if (!codigoGeograficoKeyType.TryCreateKeyword(met.valor, out codigoGeograficoKeyword))
                    {
                        throw new Exception(codigoGeograficoKeyType.Name + " keyword could not be created.");
                    }
                    else
                    {

                        return codigoGeograficoKeyword;

                    }



                }

            }
            return null;
        }

        private Keyword obtenerNombreProgramaKeyword(IList<Metadato> listaMetadatos)
        {
            foreach (Metadato met in listaMetadatos)
            {

                if (met.nombre.Equals("NOMBRE_PROGRAMA"))
                {

                    KeywordType nombreProgramaKeyType = app.Core.KeywordTypes.Find("NOMBRE_PROGRAMA");

                    // Create a keyword of this type. Use the TryCreateKeyword extension method. The AccountNum is passed into the method.
                    Keyword codigoProgramaKeyword = null;
                    if (!nombreProgramaKeyType.TryCreateKeyword(met.valor, out codigoProgramaKeyword))
                    {
                        throw new Exception(nombreProgramaKeyType.Name + " keyword could not be created.");
                    }
                    else
                    {

                        return codigoProgramaKeyword;

                    }



                }

            }
            return null;
        }

        private Keyword obtenerCodigoProgramaKeyword(IList<Metadato> listaMetadatos)
        {
            foreach (Metadato met in listaMetadatos)
            {

                if (met.nombre.Equals("CODIGO_PROGRAMA"))
                {

                    KeywordType nombreProgramaKeyType = app.Core.KeywordTypes.Find("CODIGO_PROGRAMA");

                    // Create a keyword of this type. Use the TryCreateKeyword extension method. The AccountNum is passed into the method.
                    Keyword codigoProgramaKeyword = null;
                    if (!nombreProgramaKeyType.TryCreateKeyword(met.valor, out codigoProgramaKeyword))
                    {
                        throw new Exception(nombreProgramaKeyType.Name + " keyword could not be created.");
                    }
                    else
                    {

                        return codigoProgramaKeyword;

                    }



                }

            }
            return null;
        }
        private Keyword obtenerIdButKeyword(IList<Metadato> listaMetadatos)
        {
            foreach (Metadato met in listaMetadatos)
            {

                if (met.nombre.Equals("ID_BUT"))
                {

                    KeywordType codigoGeograficoKeyType = app.Core.KeywordTypes.Find("ID_BUT");

                    // Create a keyword of this type. Use the TryCreateKeyword extension method. The AccountNum is passed into the method.
                    Keyword codigoGeograficoKeyword = null;
                    if (!codigoGeograficoKeyType.TryCreateKeyword(met.valor, out codigoGeograficoKeyword))
                    {
                        throw new Exception(codigoGeograficoKeyType.Name + " keyword could not be created.");
                    }
                    else
                    {

                        return codigoGeograficoKeyword;

                    }



                }

            }
            return null;
        }

        private string validarObligatoriedad(KeywordTypeList listObligatorios, IList<Metadato> listaMetadatos)
        {


            //string kwobl = null;
            foreach (KeywordType kw in listObligatorios)
            {

                bool encontro = false;
                foreach (Metadato met in listaMetadatos)
                {
                    if (kw.Name.Equals(met.nombre)) {
                        encontro = true;
                        break;
                    }



                }

                if (!encontro)
                {


                    return kw.Name;

                }
            }

            return null;


        }

        private string armarAsunto(IList<Metadato> listaMetadatos)
        {

            string nombre1 = "", apellido1 = "", nombre2 = "", apellido2 = "", id_but = "", concatenado;
            //Apellidos y nombres - número BUT
            foreach (Metadato met in listaMetadatos)
            {


                switch (met.nombre) {
                    case "APELLIDO1":
                        apellido1 = met.valor;
                        break;
                    case "APELLIDO2":
                        apellido2 = met.valor;
                        break;
                    case "NOMBRE1":
                        nombre1 = met.valor;
                        break;
                    case "NOMBRE2":
                        nombre2 = met.valor;
                        break;
                    case "ID_BUT":
                        id_but = met.valor;
                        break;
                }



            }

            concatenado = apellido1.ToUpper() + " " + apellido2.ToUpper() + " " + nombre1.ToUpper() + " " + nombre2.ToUpper() + " - " + id_but.ToUpper();

            return concatenado;


        }

        public long StoreNewLoanDocument(string FilePath, string nombreCompleto )
        {
            try
            {
                long newDocID = -1;


                /* byte[] bytes = Convert.FromBase64String(base64BinaryStr);
                 System.IO.FileStream stream = new FileStream(FilePath, FileMode.CreateNew);
                 System.IO.BinaryWriter writer = new BinaryWriter(stream);
                 writer.Write(bytes, 0, bytes.Length);
                 writer.Close();*/


                // Create the PageData from the file path. (Use a using statement).
                using (PageData pageData = app.Core.Storage.CreatePageData(FilePath))
                {
                    // Find the DocumentType for "Loan Application". Check for null.
                    DocumentType documentType = app.Core.DocumentTypes.Find("ABONOS");
                    if (documentType == null)
                    {
                        throw new Exception("Could not find document type: ABONOS");
                    }
                    // Find the FileType for "Image File format". Check for null.
                    FileType imageFileType = app.Core.FileTypes.Find("PDF"); //or search for 2
                    if (imageFileType == null)
                    {
                        throw new Exception("Could not find file format: PDF");
                    }
                    // Create the StoreNewDocumentProperties object.
                    StoreNewDocumentProperties newDocProps = app.Core.Storage.CreateStoreNewDocumentProperties(documentType, imageFileType);
                    // Find the keyword type for "Loan Account #". Check for null.
                    KeywordType nombreCompletoKeyType = app.Core.KeywordTypes.Find("NOMBRE_COMPLETO");
                    if (nombreCompletoKeyType == null)
                    {
                        throw new Exception("Could not find keyword type: NOMBRE_COMPLETO");
                    }
                    // Create a keyword of this type. Use the TryCreateKeyword extension method. The AccountNum is passed into the method.
                    Keyword nombreCompletoKeyword = null;
                    if (!nombreCompletoKeyType.TryCreateKeyword(nombreCompleto, out nombreCompletoKeyword))
                    {
                        throw new Exception("nombreCompletoKeyword keyword could not be created.");
                    }

                    KeywordType nombreCompleto2KeyType = app.Core.KeywordTypes.Find("NOMBRE_COMPLETO2");
                    if (nombreCompletoKeyType == null)
                    {
                        throw new Exception("Could not find keyword type: NOMBRE_COMPLETO");
                    }
                    // Create a keyword of this type. Use the TryCreateKeyword extension method. The AccountNum is passed into the method.
                    Keyword nombreCompleto2Keyword = null;
                    if (!nombreCompleto2KeyType.TryCreateKeyword(nombreCompleto, out nombreCompleto2Keyword))
                    {
                        throw new Exception("nombreCompletoKeyword keyword could not be created.");
                    }


                    // Add the new keyword to our properties.
                    newDocProps.AddKeyword(nombreCompletoKeyword);
                    newDocProps.AddKeyword(nombreCompleto2Keyword);

                    Document newDocument = app.Core.Storage.StoreNewDocument(pageData, newDocProps);
                    // Set the newDocID to the ID of the newly imported document. 
                    newDocID = newDocument.ID;

                    return newDocID;
                }
            }

            catch (SessionNotFoundException ex)
            {
                if (app != null)
                    app.Diagnostics.Write(ex);
                throw new Exception("The Unity API session could not be found, please reconnect.", ex);
            }
            catch (UnityAPIException ex)
            {
                if (app != null)
                    app.Diagnostics.Write(ex);
                throw new Exception("There was a Unity API exception.", ex);
            }
            catch (Exception ex)
            {
                if (app != null)
                    app.Diagnostics.Write(ex);
                throw new Exception("There was an unknown exception.", ex);
            }
        }

        public long StoreNewLoanDocument(string FilePath, Keyword NumRadica, string documentType, string fileTyp)
        {
            try
            {
                long newDocID = -1;
                // Create the PageData from the file path. (Use a using statment).

                using (PageData page = app.Core.Storage.CreatePageData(FilePath))
                {

                    // Find the DocumentType for "Loan Application". Check for null.
                    DocumentType docType = app.Core.DocumentTypes.Find(documentType);

                    if (docType == null) { throw new Exception("Does not exist docType"); }

                    // Find the FileType for "Image File format". Check for null.
                    FileType fileType = app.Core.FileTypes.Find(fileTyp);
                    if (docType == null) { throw new Exception("Does not exist fileType"); }

                    // Create the StoreNewDocumentProperties object.
                    StoreNewDocumentProperties docProperties = app.Core.Storage.CreateStoreNewDocumentProperties(docType, fileType);

                    // Add the new keyword to our properties.
                    docProperties.AddKeyword(NumRadica);

                    // Create the new document.*/
                    Document docum = app.Core.Storage.StoreNewDocument(page, docProperties);
                    // Set the newDocID to the ID of the newly imported document. 

                    newDocID = docum.ID;

                    return newDocID;

                }
            }

            catch (SessionNotFoundException ex)
            {
                app.Diagnostics.Write(ex);
                throw new Exception("The Unity API session could not be found, please reconnect.", ex);
            }
            catch (UnityAPIException ex)
            {
                app.Diagnostics.Write(ex);
                throw new Exception("There was a Unity API exception.", ex);
            }
            catch (Exception ex)
            {
                app.Diagnostics.Write(ex);
                throw new Exception("There was an unknown exception.", ex);
            }
        }

        public Document UpdateDocument(long docID, string Arhivobase64, string RutaArchivo)
        {
            try
            {
                Storage storage = app.Core.Storage;

                // Get the document from the core using ID. The Document ID is passed in. Check for null.
                Document doc = app.Core.GetDocumentByID(docID);


                if (doc == null)
                {
                    throw new Exception("Could not find document by id: " + docID);
                }

                //Set the File Type
                FileType fileType = app.Core.FileTypes.Find("PDF");


                // Get the document type from the document object.
                DocumentType docType = doc.DocumentType;


                StoreRevisionProperties storeRevisionProperties = storage.CreateStoreRevisionProperties(doc, fileType);



                // StoreRenditionProperties storeRenditionProperties = storage.CreateStoreRenditionProperties(doc.LatestRevision,fileType);


                string filePath = RutaArchivo + "file" + DateTime.Today.Year + DateTime.Now.Month + DateTime.Now.Day + DateTime.Now.Hour + DateTime.Now.Second + DateTime.Now.Millisecond + ".pdf";
                byte[] bytes = Convert.FromBase64String(Arhivobase64);
                System.IO.FileStream stream = new FileStream(filePath, FileMode.CreateNew);
                System.IO.BinaryWriter writer = new BinaryWriter(stream);
                writer.Write(bytes, 0, bytes.Length);
                writer.Close();

                List<string> fileList = new List<string>();
                fileList.Add(filePath);

                if (docType.Revisable)
                {


                    ///storage.CreateStoreRevisionProperties(,
                    //Document newDocument = storage.StoreNewRendition(fileList, storeRenditionProperties);
                    Document newDocument = storage.StoreNewRevision(fileList, storeRevisionProperties);
                    EDMManagement edmManagement = app.Core.EDMManagement;


                    edmManagement.StampVersion(newDocument.DefaultRenditionOfLatestRevision, "");



                    return newDocument;

                }
                else
                {


                    throw new Excepcion("S10", "No se actualizó el documento porque no permite versionamiento");
                }


            }


            catch (SessionNotFoundException ex)
            {

                if (app != null)
                    app.Diagnostics.Write(ex);
                throw new Exception(ex.Message);
            }
            catch (UnityAPIException ex)
            {

                if (app != null)
                    app.Diagnostics.Write(ex);
                throw new Exception(ex.Message);
            }
            catch (Excepcion ex)
            {

                if (app != null)
                    app.Diagnostics.Write(ex);
                throw ex;
            }
            catch (Exception ex)
            {

                if (app != null)
                    app.Diagnostics.Write(ex);
                throw new Exception(ex.Message);
            }



        }

        /// <summary>
        /// conversor de datos
        /// </summary>
        /// <param name="valor"></param>
        /// <param name="keywordDataType"></param>
        /// <returns></returns>
        public Keyword conversor(string valor, KeywordType keywordDataType)
        {
            Keyword keyValue = null;

            switch (keywordDataType.DataType)
            {
                case KeywordDataType.AlphaNumeric:
                    keyValue = keywordDataType.CreateKeyword(valor);
                    break;
                case KeywordDataType.Currency:
                case KeywordDataType.SpecificCurrency:
                    keyValue = keywordDataType.CreateKeyword(Convert.ToDecimal(valor));
                    break;
                case KeywordDataType.Date:
                    keyValue = keywordDataType.CreateKeyword(Convert.ToDateTime(valor));
                    break;
                case KeywordDataType.DateTime:
                    keyValue = keywordDataType.CreateKeyword(Convert.ToDateTime(valor));
                    break;
                case KeywordDataType.FloatingPoint:
                    keyValue = keywordDataType.CreateKeyword(Convert.ToDouble(valor));
                    break;
                case KeywordDataType.Numeric20:
                    keyValue = keywordDataType.CreateKeyword(Convert.ToDecimal(valor));
                    break;
                case KeywordDataType.Numeric9:
                    keyValue = keywordDataType.CreateKeyword(Convert.ToInt64(valor));
                    break;
            }

            return keyValue;
        }

        public bool UpdateFolder(string id_but, IList<Metadato> ListaMetadatos)
        {
            try
            {

                FolderManagement folderManager = app.Core.FolderManagement;
                FolderQuery folderQuery = folderManager.CreateFolderQuery();
                FolderType foldertype = app.Core.FolderManagement.FolderTypes.Find(141);
                FolderType foldertype2 = app.Core.FolderManagement.FolderTypes.Find(138);

                // Add a Date Restriction to the Folder Query
                DateTime time1 = new DateTime(1900, 1, 1);
                DateTime time2 = DateTime.Now;

                folderQuery.AddDateRange(time1, time2);

                // Restrict Folder Query to Specific Folder Type
                folderQuery.AddFolderType(foldertype);
                folderQuery.AddFolderType(foldertype2);

                // Restrict Folder Query to Specific Folder Type
                // Add a keyword restriction to the Folder Query
                folderQuery.AddKeyword("ID_BUT", id_but);

                // Execute Folder Query
                FolderList folderList = folderQuery.Execute(100);
                Folder folderResultado = null;
                DocumentList docList = null;

                if (folderList.Count > 0)
                {
                    folderResultado = folderList[0];
                    docList = folderResultado.GetDocuments();
                }



                //Actualizar Documentos

                if (docList != null) {
                    return ActualizarDocumentosExpediente(docList, ListaMetadatos);
                }
                else
                {

                    throw new Exception("No Se encontraron Documentos");
                }







            }


            catch (SessionNotFoundException ex)
            {

                if (app != null)
                    app.Diagnostics.Write(ex);
                throw new Exception(ex.Message);
            }
            catch (UnityAPIException ex)
            {

                if (app != null)
                    app.Diagnostics.Write(ex);
                throw new Exception(ex.Message);
            }
            catch (Exception ex)
            {

                if (app != null)
                    app.Diagnostics.Write(ex);
                throw new Exception(ex.Message);
            }



        }

        private bool ActualizarDocumentosExpediente(DocumentList docList, IList<Metadato> listaMetadatos)
        {
            foreach (Document doc in docList)
            {

                using (DocumentLock documentLock = doc.LockDocument())
                {
                    // check whether document was locked
                    if (documentLock.Status == DocumentLockStatus.LockObtained)
                    {

                        KeywordModifier keyModifier = doc.CreateKeywordModifier();

                        foreach (Metadato met in listaMetadatos)
                        {

                            // get keyword type object(used here to find keyword to modify)
                            KeywordType keywordType = app.Core.KeywordTypes.Find(met.nombre);

                            // retrieve the keyword record that contains the keyword to be modified
                            KeywordRecord keyRecord = doc.KeywordRecords.Find(keywordType);

                            // Create new keyword for keyword type, to hold the updated value
                            Keyword newKeyword = keywordType.CreateKeyword(met.valor);

                            // Retrieve keyword to update (could be multiple instances of the keyword)
                            foreach (Keyword keyword in keyRecord.Keywords.FindAll(keywordType))
                            {
                                // Update the keyword in the keyword modifier object
                                keyModifier.UpdateKeyword(keyword, newKeyword);
                            }

                        }

                        keyModifier.ApplyChanges();

                        return true;

                    }
                    else
                    {


                        throw new Exception(doc.Name + ": No pudo ser actualizado porque se encuentra bloqueado");

                    }
                }
            }

            return true;
        }

        public long StoreNewUserUnityForm(string FullName, string ManagerName)
        {
            long newDocID = -1;
            try
            {
                // Find the Unity form Template for "New User Request Form". Check for null.
                FormTemplate newUserTemplate = app.Core.UnityFormTemplates.Find("Formulario Envío a Entes Externos");
                if (newUserTemplate == null)
                {
                    throw new Exception("Could not find the unity template with name: New User Request Form");
                }
                // Create the StoreNewUnityFormProperties object.
                StoreNewUnityFormProperties unityFormProps = app.Core.Storage.CreateStoreNewUnityFormProperties(newUserTemplate);
                // Find the KeywordType for "Full Name". Check for null.
                KeywordType tipoSolicitudKeywordType = app.Core.KeywordTypes.Find("TIPO_SOLICITUD");

                if (tipoSolicitudKeywordType == null)
                {
                    throw new Exception("Could not find the keyword type with name: TIPO_SOLICITUD");
                }
                // Create a keyword for this type. Use the TryCreateKeyword extension method. The FullName value is passed into this method.
                Keyword tipoSolicitudKeyword = null;
                if (!tipoSolicitudKeywordType.TryCreateKeyword(FullName, out tipoSolicitudKeyword))
                {
                    throw new Exception("Could not create the TIPO_SOLICITUD keyword.");
                }
                // Add the full name keyword to our properties.
                unityFormProps.AddKeyword(tipoSolicitudKeyword);
                // Find the KeywordType for "Manager Name". Check for null.
                KeywordType tipoCorrespondenciaKeyType = app.Core.KeywordTypes.Find("TIPO_CORRESPONDENCIA");
                if (tipoCorrespondenciaKeyType == null)
                {
                    throw new Exception("Could not find the keyword type with name: TIPO_CORRESPONDENCIA");
                }
                // Create a keyword for this type. Use the TryCreateKeyword extension method. The ManagerName value is passed into this method.
                Keyword tipoCorrespondenciaKey = null;
                if (!tipoCorrespondenciaKeyType.TryCreateKeyword(ManagerName, out tipoCorrespondenciaKey))
                {
                    throw new Exception("Could not create the TIPO_CORRESPONDENCIA keyword.");
                }
                // Add the manager name keyword to our properties. 
                unityFormProps.AddKeyword(tipoCorrespondenciaKey);
                // Archive the Unity Form.
                Document newUnityFormDocument = app.Core.Storage.StoreNewUnityForm(unityFormProps);
                // Set the newDocID. 
                newDocID = newUnityFormDocument.ID;
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
            return newDocID;
        }

        public Archival(Hyland.Unity.Application app)
        {
            if (app == null)
            {
                throw new ArgumentNullException("app", "The Unity application object is null, make sure to connect first.");
            }
            this.app = app;
        }

        public long StoreNewUserUnityForm(IList<object> listaMetadatos, string nombreTipoIdentificacionKW)
        {
            long newDocID = -1;
            try
            {

                Hyland.Unity.WorkView.Application application = app.WorkView.Applications.Find("Administracion");
                Hyland.Unity.WorkView.Class metadatos = application.Classes.Find("Metadatos");


                // Find the Unity form Template for "New User Request Form". Check for null.
                FormTemplate newUserTemplate = app.Core.UnityFormTemplates.Find(183);
                // Create the StoreNewUnityFormProperties object.
                StoreNewUnityFormProperties unityFormProps = app.Core.Storage.CreateStoreNewUnityFormProperties(newUserTemplate);
                // Find the KeywordType for "Full Name". Check for null.

                if (newUserTemplate == null)
                {
                    throw new Exception("Could not find the unity template with name: New User Request Form");
                }


                string nombreTipologiaDocumental = "Expediente Estudiante";
                if (nombreTipologiaDocumental != null)
                {
                    // Find the DocumentType for "Loan Application". Check for null.
                    DocumentType documentType = app.Core.DocumentTypes.Find(nombreTipologiaDocumental);
                    if (documentType != null)
                    {


                        foreach (Metadato met in listaMetadatos)
                        {
                            var valorMetadatoX = met.valor;

                            // Find the keyword type for "Loan Account #". Check for null.
                            KeywordType nombreCompletoKeyType = app.Core.KeywordTypes.Find(met.nombre);

                            // Create a keyword of this type. Use the TryCreateKeyword extension method. The AccountNum is passed into the method.
                            Keyword valorMetadatoKeyword = null;
                            if (!nombreCompletoKeyType.TryCreateKeyword(valorMetadatoX, out valorMetadatoKeyword))
                            {
                                throw new Exception(met.nombre + "keyword could not be created.");
                            }
                            unityFormProps.AddKeyword(valorMetadatoKeyword);
                        }

                    }

                    // Archive the Unity Form.
                    Document newUnityFormDocument = app.Core.Storage.StoreNewUnityForm(unityFormProps);
                    // Set the newDocID. 
                    newDocID = newUnityFormDocument.ID;
                }
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
            return newDocID;
        }

        private bool esVersionable(string tipoDocumental) {


            KeywordType tipologiaDocumentalExternoKT = app.Core.KeywordTypes.Find("TIPOLOGIA_DOCUMENTAL");
            Keyword tipoDocumentalKW = tipologiaDocumentalExternoKT.CreateKeyword(tipoDocumental);
            KeywordType origenKT = app.Core.KeywordTypes.Find("ORIGEN");
            Keyword origenKW = origenKT.CreateKeyword("A");

            Keyset keysetHomologacion = app.Core.AutoFillKeywordSets.Find("Consultar Documentos Versionables V2");

            List<Keyword> primaryKeywordsHomolog = new List<Keyword>();
            //Keyword primaryKeywordHomolog = keysetHomologacion.PrimaryKeywordType.CreateKeyword(codigoSistemaExternoKW.AlphaNumericValue);

            primaryKeywordsHomolog.Add(origenKW);
            //primaryKeywordsHomolog.Add(TipoDocumentalSistemaExternoKW);

            // keysetDatas will contain an item for each matching keyword record
            KeysetDataList keysetDatasHomolog = keysetHomologacion.GetKeysetData(primaryKeywordsHomolog);

            string tipoDocumentalOnbase = "";
            foreach (KeysetData keysetX in keysetDatasHomolog)
            {


                Keyword kw = keysetX.Keywords.Find("TIPOLOGIA_DOCUMENTAL");

                if (kw.AlphaNumericValue.ToUpper().Equals(tipoDocumental))
                {

                    tipoDocumentalOnbase = keysetX.Keywords.Find("TIPOLOGIA_DOCUMENTAL").AlphaNumericValue;

                    return true;
                } 

                


            }


            return false;


        }


    }
}
