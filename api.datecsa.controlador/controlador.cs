using Hyland.Unity;
using api.datecsa.modelo;
using System;
using System.Collections.Generic;

using System.Text;


using api.datecsa.UnityAPI;
using System.Security.Cryptography;
using System.IO;

namespace api.datecsa.controlador
{
    public class Controlador
    {



        public static Application conectarSession(string session_id, string url, Connection connection)
        {


            Application app = connection.ConnectWithSessionID(session_id, url);
            return app;

        }

        public static Application conectar(string username, string url, string password, string datasource, Connection connection)
        {
            try
            {

                Application app = connection.Connect(username, password, datasource, url,0);
                return app;
            }
            catch (Exception exc)
            {

                throw exc;

            }

        }


        public AlmacenarDocumentoResponse AlmacenarDocumento(IList<Metadato> ListaMetadatos, string base64, string NombreTipoDocumental, string idAplicacionOrigen, string datasource, string url, string username, string password, string rutaArchivo, string linkDocPop)
        {
            Connection connection = new Connection("");
            Application application = null;
            AlmacenarDocumentoResponse response;
            try
            {


                application = connection.Connect(username, password, datasource, url,0);

               
                List<object> listaDocumentos = new List<object>();

                string g_datasource = datasource;
                string g_url = url;
                List<RespuestaAlmacenar> listaRespuesta = new List<RespuestaAlmacenar>();

                if (ListaMetadatos != null)
                {
                    Documento doc = new Documento(base64, "", "", linkDocPop, 0, "", "", ListaMetadatos, NombreTipoDocumental, idAplicacionOrigen);

                    try
                    {
                        UnityAPI.Archival almacenarDocumento = new Archival(application);


                        IList<Metadato> listaMetadatos = doc.Metadatos;

                        object objeto = almacenarDocumento.StoreNewDocument64(doc.Metadatos, doc.ArchivoBase64, doc.NombreTipoDocumental, doc.IdAplicacionOrigen, rutaArchivo);

                        string fechaCreacion = ((Document)objeto).DocumentDate.ToString();

                        RespuestaAlmacenar respuestaOk = new RespuestaAlmacenar(((Document)objeto).Name, fechaCreacion, linkDocPop + ((Document)objeto).ID, ((Document)objeto).ID, "201", "Documento Almacenado Exitosamente");

                        respuestaOk.codigoRespuesta = "S05";
                        respuestaOk.descripcionRespuesta = "Documento Almacenado Exitosamente";
                        respuestaOk.documentHandle = ((Document)objeto).ID;
                        //respuestaOk.NombreArchivo = ((Document)objeto).Name;

                        respuestaOk.linkDocPop = linkDocPop + ((Document)objeto).ID + "&chksum=" + UnityAPI.SecurityUtilities.checksum("docid=" + ((Document)objeto).ID); ;

                        RespuestaAlmacenar respuesta = respuestaOk;


                        if (application != null)
                        {
                            Desconectar(application);

                        }

                        response = new AlmacenarDocumentoResponse(respuestaOk);

                    }
                    catch (ExcepcionVersionExiste exc)
                    {
                        if (application != null)
                            connection.Disconnect(application);
                        RespuestaAlmacenar respuestaNoOk = new RespuestaAlmacenar("", "", "", 0, "401", exc.Message);
                        response = new AlmacenarDocumentoResponse(respuestaNoOk, exc.codigo, exc.Message);
                        response.respuesta.codigoRespuesta = exc.codigo;
                        response.respuesta.descripcionRespuesta = exc.Message + " " + exc.InnerException;

                        return response;
                    }
                    catch (Exception exc)
                    {
                        RespuestaAlmacenar respuestaNoOk = new RespuestaAlmacenar("", "", "", 0, "401", exc.Message);

                        respuestaNoOk.codigoRespuesta = "S02";
                        respuestaNoOk.descripcionRespuesta = exc.Message;
                        //listaRespuesta.Add(respuestaNoOk);
                        response = new AlmacenarDocumentoResponse(respuestaNoOk, "S02", exc.Message);
                        if (application != null)
                            connection.Disconnect(application);
                        return response;
                    }


                    if (application != null)
                        connection.Disconnect(application);


                    return response;
                }
                else
                {
                    if (application != null)
                        connection.Disconnect(application);

                    response = new AlmacenarDocumentoResponse();

                    response.respuesta.codigoRespuesta = "S02";
                    response.respuesta.descripcionRespuesta = "La lista de Metadatos no fue enviada";

                    return response;

                }


            }
            catch (Excepcion exc)
            {
                if (application != null)
                    Controlador.Desconectar(application);

                AlmacenarDocumentoResponse resp = new AlmacenarDocumentoResponse(null, "S02", exc.Message + " " + exc.InnerException);
                resp.respuesta.codigoRespuesta = "S02";
                resp.respuesta.descripcionRespuesta = exc.Message + " " + exc.InnerException;
                return resp;
            }
            catch (UnityAPIException exc)
            {
                if (application != null)
                    Controlador.Desconectar(application);


                string messageCode = "S02";
                string message = exc.Message + " " + exc.InnerException;
                bool exceededQueryMeter = exc.Message.Contains("has exceeded the query meter length");

                if (exceededQueryMeter)
                {
                    messageCode = "S11";
                    message = "Cantidad Máxima de licencias Query API alcanzadas";
                }
                AlmacenarDocumentoResponse resp = new AlmacenarDocumentoResponse(null, messageCode, message);
                resp.respuesta.codigoRespuesta = messageCode;
                resp.respuesta.descripcionRespuesta = message;
                return resp;
            }
            catch (ExcepcionVersionExiste exc)
            {
                if (application != null)
                    connection.Disconnect(application);

                AlmacenarDocumentoResponse resp = new AlmacenarDocumentoResponse(null, exc.codigo, exc.Message + " " + exc.InnerException);
                resp.respuesta.codigoRespuesta = exc.codigo;
                resp.respuesta.descripcionRespuesta = exc.Message + " " + exc.InnerException;

                return resp;
            }
            catch (Exception exc)
            {
                if (application != null)
                    connection.Disconnect(application);

                AlmacenarDocumentoResponse resp = new AlmacenarDocumentoResponse(null, "S02", exc.Message + " " + exc.InnerException);
                resp.respuesta.codigoRespuesta = "S02";
                resp.respuesta.descripcionRespuesta = exc.Message + " " + exc.InnerException;

                return resp;
            }


        }


        public ConsultarDocumentoDocHandleResponse ConsultarDocumentoDocHandle(long docHandle, string datasource, string url, string username, string password, string rutaArchivo, string urlDocPop)
        {
            Connection connection = new Connection("");
            Application application = null;
            try
            {
                // application = Controlador.conectarSession(session_id, url, connection);

                application = connection.Connect(username, password, datasource, url, 2);

                IList<Object> lista = new List<Object>();

                SaveOutDocument save = new SaveOutDocument(application);

                GetDocument getDocument = new GetDocument(application);

                Document doc = getDocument.GetDocumentForSave(docHandle);

                //Documento documentoRequerido = new Documento();

                ConsultarDocumentoDocHandleResponse response = new ConsultarDocumentoDocHandleResponse();
                if (doc != null)
                {
                    string b64 = save.SaveDocumentV2(doc, "PDF", rutaArchivo);
                    response.codigoRespuesta = "S08";
                    response.descripcionRespuesta = "Consulta Exitosa";
                    response.archivoBase64 = b64;

                    // response.archivoBase64 = save.SaveDocument();

                   // if(doc.Versions  )

                    try
                    {
                        response.version = doc.Versions.Count.ToString();

                    }catch(Exception)
                    {
                        response.version = "0";


                    }

                    
                    //response.DocumentHandle = doc.ID.ToString();
                    //  response.nombreDocumento = doc.Name;
                    response.linkDocPop = urlDocPop + doc.ID + "&chksum=" + UnityAPI.SecurityUtilities.checksum("docid=" + doc.ID); ; ;
                    //  response.fechaCreacion = doc.DocumentDate.ToString();

                }
                else
                {
                    response.codigoRespuesta = "S02";
                    response.descripcionRespuesta = "No existe archivo";
                }


                if (application != null)
                    connection.Disconnect(application);

                return response;

            }
            catch (Excepcion exc)
            {
                if (connection != null)
                    connection.Disconnect(application);
                return new ConsultarDocumentoDocHandleResponse("", "S02", exc.Message, "", "", "", "");


            }
            catch (UnityAPIException exc)
            {
                if (connection != null)
                    connection.Disconnect(application);


                string messageCode = "S02";
                string message = exc.Message + " " + exc.InnerException;
                bool exceededQueryMeter = exc.Message.Contains("has exceeded the query meter length");

                if (exceededQueryMeter)
                {
                    messageCode = "S11";
                    message = "Cantidad Máxima de licencias Query API alcanzadas";
                }
                return new ConsultarDocumentoDocHandleResponse("", messageCode, message, "", "", "", "");

            }

            catch (Exception exc)
            {
                if (connection != null)
                    connection.Disconnect(application);

                //throw new Exception(exc.Message);
                return new ConsultarDocumentoDocHandleResponse("", "S02", exc.Message, "", "", "", "");
            }
        }


        public ActualizarDocumentoResponse ActualizarDocumento(long docHandle, string base64, string datasource, string url, string username, string password, string urlDocPop, string rutaAchivo)
        {
            Connection connection = new Connection("");
            Application application = null;

            try
            {

                // application = Controlador.conectar(session_id, url, connection);

                application = connection.Connect(username, password, datasource, url,0);


                UnityAPI.Archival almacenarDocumento = new Archival(application);
                Document doc = almacenarDocumento.UpdateDocument(docHandle, base64, rutaAchivo);


                ActualizarDocumentoResponse response = new ActualizarDocumentoResponse();


                if (doc != null)
                {


                    response.codigoRespuesta = "S06";
                    response.descripcionRespuesta = "Documento Actualizado Correctamente!";
                    response.documentHandle = doc.ID.ToString();
                    response.version = doc.Versions.Count.ToString();

                    response.linkDocPop = urlDocPop + doc.ID + "&chksum=" + UnityAPI.SecurityUtilities.checksum("docid=" + doc.ID); ;

                    if (application != null)
                        connection.Disconnect(application);

                    return response;

                }

                else
                {

                    connection.Disconnect(application);
                    return new ActualizarDocumentoResponse("S02", "No se encontró el documento handle", null, null, null, null);

                }

            }

            catch (Excepcion exc)
            {

                if (application != null)
                    connection.Disconnect(application);

                return new ActualizarDocumentoResponse(exc.codigo, exc.Message + " " + exc.InnerException, "", "", "", null);
            }
            catch (UnityAPIException exc)
            {

                if (application != null)
                    connection.Disconnect(application);


                string messageCode = "S02";
                string message = exc.Message + " " + exc.InnerException;
                bool exceededQueryMeter = exc.Message.Contains("has exceeded the query meter length");

                if (exceededQueryMeter)
                {
                    messageCode = "S11";
                    message = "Cantidad Máxima de licencias Query API alcanzadas";
                }

                return new ActualizarDocumentoResponse(messageCode, message, "", "", "", null);
            }
            catch (Exception exc)
            {

                if (application != null)
                    connection.Disconnect(application);
                //throw new Exception(exc.Message);

                if (exc.InnerException != null)
                    return new ActualizarDocumentoResponse("S02", exc.InnerException.ToString(), "", "", "", "");
                else
                    return new ActualizarDocumentoResponse("S02", exc.Message.ToString(), "", "", "", "");
            }
        }


        public ActualizarExpedienteResponse ActualizarExpediente(string Id_But, IList<Metadato> listaMetadatos, string base64, string datasource, string url, string username, string password)
        {
            Connection connection = new Connection("");
            Application application = null;
            try
            {
                //application = Controlador.conectar(session_id, url, connection);


                application = connection.Connect(username, password, datasource, url,0);
                IList<Object> lista = new List<Object>();

                if (listaMetadatos != null)
                {
                    //Consulto si el expediente está creado para validar
                    GetAccountDocuments acc = new GetAccountDocuments(application);
                    List<Metadato> metadatosExpediente = new List<Metadato>();
                    Metadato met = new Metadato(Id_But, "ID_BUT", "");
                    metadatosExpediente.Add(met);
                    Document expediente = acc.GetExpediente("FORMULARIO EXPEDIENTE HVE", metadatosExpediente);
                    if (expediente != null)
                    {
                        UnityAPI.Archival almacenarDocumento = new Archival(application);

                        bool doc = almacenarDocumento.UpdateFolder(Id_But, listaMetadatos);
                        ActualizarExpedienteResponse response = new ActualizarExpedienteResponse();
                        if (doc)
                        {
                            response.codigoRespuesta = "S07";
                            response.descripcionRespuesta = "Expediente Actualizado Exitosamente";

                            if (application != null)
                                connection.Disconnect(application);
                            return response;
                        }
                        if (application != null)
                            connection.Disconnect(application);
                        return new ActualizarExpedienteResponse("S02", "No se encontraron documentos asociados");
                    }
                    else
                    {

                        if (application != null)
                            connection.Disconnect(application);
                        return new ActualizarExpedienteResponse("S02", "No se encontró Expediente asociado");
                    }
                }
                else
                {

                    if (application != null)
                        connection.Disconnect(application);
                    return new ActualizarExpedienteResponse("S02", "Lista de Metadatos Errada");

                }

            }
            catch (Excepcion exc)
            {

                if (application != null)
                    connection.Disconnect(application);

                //throw new Exception(exc.Message);
                return new ActualizarExpedienteResponse("S02", exc.Message);
            }
            catch (UnityAPIException exc)
            {

                if (application != null)
                    connection.Disconnect(application);

                string messageCode = "S02";
                string message = exc.Message + " " + exc.InnerException;
                bool exceededQueryMeter = exc.Message.Contains("has exceeded the query meter length");

                if (exceededQueryMeter)
                {
                    messageCode = "S11";
                    message = "Cantidad Máxima de licencias Query API alcanzadas";
                }

                //throw new Exception(exc.Message);
                return new ActualizarExpedienteResponse(messageCode, message);
            }
            catch (Exception exc)
            {

                if (application != null)
                    connection.Disconnect(application);
                //throw new Exception(exc.Message);
                return new ActualizarExpedienteResponse("401", exc.Message);
            }
        }


        public ConsultarDocumentoFechaPalabrasClaveResponse ConsultarDocumentoFechaPalabrasClave(IList<Metadato> PalabrasClave, string FechaInicial, string FechaFinal, string NombreTipoDocumental, string idAplicacionOrigen, string datasource, string url, string username, string password, string session_id, string urlDocPop)
        {
            UnityAPI.Connection connection = new UnityAPI.Connection("");

          
            //SessionIDAuthenticationProperties authProps = Application.CreateSessionIDAuthenticationProperties(url, session_id, false);
                             
            Hyland.Unity.Application application = null;

            try
            {

                application = connection.Connect(username, password, datasource, url,2);
                IList<Respuesta> lista = new List<Respuesta>();

                UnityAPI.GetAccountDocuments c = new UnityAPI.GetAccountDocuments(application);
                DocumentList listaDocumentos = c.GetDocumentsForContract(PalabrasClave, FechaInicial, FechaFinal, NombreTipoDocumental, idAplicacionOrigen);

                ConsultarDocumentoFechaPalabrasClaveResponse response = new ConsultarDocumentoFechaPalabrasClaveResponse();

                Respuesta DocRespuesta = null;
                if (listaDocumentos.Count > 0)
                {

                    foreach (Document doc in listaDocumentos)
                    {

                        DocRespuesta = new Respuesta();
                        DocRespuesta.documentHandle = doc.ID;

                        DocRespuesta.linkDocPop = urlDocPop + doc.ID + "&chksum=" + UnityAPI.SecurityUtilities.checksum("docid=" + doc.ID);
                        DocRespuesta.nombreDocumento = doc.Name;
                        DocRespuesta.nombreTipoDocumental = doc.DocumentType.Name;
                        lista.Add(DocRespuesta);
                    }



                    response.codigoRespuesta = "S08";
                    response.descripcionRespuesta = "Consulta Exitosa";
                    response.arregloDocumentos = lista;


                }
                else
                {
                    response.codigoRespuesta = "V11";
                    response.descripcionRespuesta = "No se encontraron documentos con esos criterios de búsqueda";

                }
                if (application != null)
                    application.Disconnect();
                return response;
            }
            catch (Excepcion exc)
            {
                if (application != null)
                    application.Disconnect();

                //throw new Exception(exc.Message);
                return new ConsultarDocumentoFechaPalabrasClaveResponse("S02", exc.Message, null);
                // return new ConsultarDocumentoFechaPalabrasClaveResponse("", "401", exc.Message, "", "");
            }
            catch (MaxLicensesException exc)
            {
                if (application != null)
                    application.Disconnect();

                //throw new Exception(exc.Message);
                return new ConsultarDocumentoFechaPalabrasClaveResponse("S11", exc.Message, null);
                // return new ConsultarDocumentoFechaPalabrasClaveResponse("", "401", exc.Message, "", "");
            }
            catch (UnityAPIException exc)
            {
                if (application != null)
                    application.Disconnect();
                string messageCode = "S02";
                string message = exc.Message + " " + exc.InnerException;
                bool exceededQueryMeter = exc.Message.Contains("has exceeded the query meter length");

                if (exceededQueryMeter)
                {
                    messageCode = "S11";
                    message = "Cantidad Máxima de licencias Query API alcanzadas";
                }
                return new ConsultarDocumentoFechaPalabrasClaveResponse(messageCode, message, null);
                // return new ConsultarDocumentoFechaPalabrasClaveResponse("", "401", exc.Message, "", "");
            }
            catch (Exception exc)
            {
                if (application != null)
                    application.Disconnect();      
                return new ConsultarDocumentoFechaPalabrasClaveResponse("S02", exc.Message, null);
            }
        }

        public ConsultarDocumentoIdAplicacionResponse ConsultarDocumentoIdAplicacion(string idDocumentoAplicacion, string idAplicacionOrigen, string datasource, string url, string username, string password, string session_id, string urlDocPop, string rutaArchivo)
        {

            UnityAPI.Connection connection = new UnityAPI.Connection("");
            
            //SessionIDAuthenticationProperties authProps = Application.CreateSessionIDAuthenticationProperties(url, session_id, false);
            Hyland.Unity.Application application = null;

            try
            {


                application = connection.Connect(username, password, datasource, url,2);
                IList<Object> lista = new List<Object>();
                SaveOutDocument save = new SaveOutDocument(application);
                GetDocument getDocument = new GetDocument(application);
                DocumentList docList = getDocument.GetDocumentForSaveIDAplicacion(idDocumentoAplicacion, idAplicacionOrigen);

                // Documento documentoRequerido = new Documento();

                ConsultarDocumentoIdAplicacionResponse response = new ConsultarDocumentoIdAplicacionResponse();
                if (docList != null && docList.Count > 0)
                {



                    save.SaveDocument(docList[0], "PDF", rutaArchivo);

                    response.codigoRespuesta = "S08";
                    response.descripcionRespuesta = "Consulta Exitosa!";
                    response.archivoBase64 = save.SaveDocument();
                    response.documentHandle = docList[0].ID.ToString();
                    response.linkDocPop = urlDocPop + docList[0].ID + "&chksum=" + UnityAPI.SecurityUtilities.checksum("docid=" + docList[0].ID);



                }
                else
                {

                    response.codigoRespuesta = "V11";
                    response.descripcionRespuesta = "No se encontraron documentos con esos criterios de búsqueda";

                }
                if (application != null)
                    connection.Disconnect(application);

                return response;
            }

            catch (UnityAPIException exc)
            {

                if (connection != null)
                    connection.Disconnect(application);

                string messageCode = "S02";
                string message = exc.Message + " " + exc.InnerException;
                bool exceededQueryMeter = exc.Message.Contains("has exceeded the query meter length");

                if (exceededQueryMeter)
                {
                    messageCode = "S11";
                    message = "Cantidad Máxima de licencias Query API alcanzadas";
                }

                //throw new Exception(exc.Message);
                return new ConsultarDocumentoIdAplicacionResponse("", messageCode, message, "", "");
            }
            catch (Exception exc)
            {

                if (connection != null)
                    connection.Disconnect(application);

                //throw new Exception(exc.Message);
                return new ConsultarDocumentoIdAplicacionResponse("", "S02", exc.Message, "", "");
            }
        }


        public RespuestaConsultarEmplidIdBut ConsultarEmplidIdBut(string emplid, string datasource, string url, string username, string password, string session_id, string urlDocPop, string rutaArchivo)
        {

            UnityAPI.Connection connection = new UnityAPI.Connection("");
         
            //SessionIDAuthenticationProperties authProps = Application.CreateSessionIDAuthenticationProperties(url, session_id, false);
            Hyland.Unity.Application application = null;

            try
            {


                application = connection.Connect(username, password, datasource, url,2);
                IList<Object> lista = new List<Object>();
                SaveOutDocument save = new SaveOutDocument(application);
                GetDocument getDocument = new GetDocument(application);
                string idBut = getDocument.getIdBut(emplid);

                // Documento documentoRequerido = new Documento();

                RespuestaConsultarEmplidIdBut response = new RespuestaConsultarEmplidIdBut();
                response.respuesta = new ConsultarEmplidIdButResponse();
                if (emplid != null)
                {

                    response.respuesta.idBut = idBut;
                    response.respuesta.codigoRespuesta = "S08";
                    response.respuesta.descripcionRespuesta = "Consulta Exitosa!";

                }
                else
                {

                    response.respuesta.codigoRespuesta = "V11";
                    response.respuesta.descripcionRespuesta = "No se encontró expediente con esos criterios de búsqueda";
                }
                if (application != null)
                    connection.Disconnect(application);

                return response;
            }
            catch (UnityAPIException exc)
            {

                if (connection != null)
                    connection.Disconnect(application);


                string messageCode = "S02";
                string message = exc.Message + " " + exc.InnerException;
                bool exceededQueryMeter = exc.Message.Contains("has exceeded the query meter length");

                if (exceededQueryMeter)
                {
                    messageCode = "S11";
                    message = "Cantidad Máxima de licencias Query API alcanzadas";
                }

                //throw new Exception(exc.Message);
                RespuestaConsultarEmplidIdBut resp = new RespuestaConsultarEmplidIdBut();
                resp.respuesta = new ConsultarEmplidIdButResponse("", messageCode, message);
                return resp;
            }

            catch (Exception exc)
            {

                if (connection != null)
                    connection.Disconnect(application);

                //throw new Exception(exc.Message);
                RespuestaConsultarEmplidIdBut resp = new RespuestaConsultarEmplidIdBut();
                resp.respuesta = new ConsultarEmplidIdButResponse("", "S02", exc.Message);
                return resp;
            }
        }
        public static void Desconectar(Application app)
        {

            if (app != null)
                app.Disconnect();
        }



        public string DecryptPassword(string encryptedText, string privateKey)
        {
            //we want to decrypt, therefore we need a csp and load our private key
            RSACryptoServiceProvider csp = new RSACryptoServiceProvider();

            string privKeyString;
            {
                privKeyString = File.ReadAllText(privateKey);
                //get a stream from the string
                var sr = new StringReader(privKeyString);
                //we need a deserializer
                var xs = new System.Xml.Serialization.XmlSerializer(typeof(RSAParameters));
                //get the object back from the stream
                RSAParameters privKey = (RSAParameters)xs.Deserialize(sr);
                csp.ImportParameters(privKey);
            }


            byte[] bytesCipherText = Convert.FromBase64String(encryptedText);

            //decrypt and strip pkcs#1.5 padding
            byte[] bytesPlainTextData = csp.Decrypt(bytesCipherText, false);

            //get our original plainText back...
            // File.WriteAllBytes(filePath, bytesPlainTextData);

            string str = Encoding.Default.GetString(bytesPlainTextData);
            Console.WriteLine("The String is: " + str);
            return str;
        }
    }


}

