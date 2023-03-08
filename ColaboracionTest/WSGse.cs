using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net;
using System.ComponentModel;
using System.Security.Policy;
using System.Net.Http;
using System.Net.Http.Headers;
using Hyland.Unity;
using System.IO;
using Hyland;
using api.datecsa.UnityAPI;

namespace ColaboracionTest
{
    public class WSGse : MetodosGSE
    {
        /// <summary>
        /// Diligencia el token para hacer consumo de los otros servicios
        /// </summary>
        public void ObtenerToken ()
        {
            var client = new HttpClient();
            // Headers
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            //Data
            var contenido = JsonConvert.SerializeObject(contex, Formatting.Indented);
            var buffer = System.Text.Encoding.UTF8.GetBytes(contenido);
            var byteBuffer = new ByteArrayContent(buffer);
            byteBuffer.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var result = client.PostAsync(metodoLogin, byteBuffer).Result;
            var stringjson = result.Content.ReadAsStringAsync().Result;

            ObjectLogin root = JsonConvert.DeserializeObject<ObjectLogin>(stringjson);
            token = root.result.token;
            
        }
        
        /// <summary>
        /// Consumo de metodos
        /// </summary>
        /// <param name="metodo">Metodo get</param>
        /// <returns></returns>
        private string jsonString(string metodo)
        {
            WebClient json = new WebClient();

            json.Headers["Authorization"] = "Token " + token;
            json.Headers["Content-Type"] = "application/json";

            return json.DownloadString(metodo);
        }

        public ObjetDocumento LoginGse()
        {
            setLogin();
            ObtenerToken();

            string Correo = "JULIOLANDAZURY@DATECSA.COM",
                   asunto = "Pruebas GSE", 
                   fecha  = "2021-12-16",
                   documentTypeName = "Certificado de notificación electrónica",
                   fileTypeName = "PDF";

            setGeneral(fecha);

            ObjetGeneral root = JsonConvert.DeserializeObject<ObjetGeneral>(jsonString(metodoConsultaGeneral));
            ObjetDocumento Documento = null;

            foreach (resultGeneral item in root.result)
            {
                foreach (string mail in item.to)
                {
                    if (mail.ToUpper().Equals(Correo) && item.subject.Equals(asunto))
                    {
                        emailID = item.emailID;
                        setEventos();
                        setDescarga();

                        ObjetEventos eventos = JsonConvert.DeserializeObject<ObjetEventos>(jsonString(metodoConsultaEventos));

                        foreach(resultEventos row in eventos.result)
                        {
                            if (row.eventType.Equals("Open"))
                            {
                                Connection con = new Connection("http://172.19.0.29/AppServer/Service.asmx");

                                Application app = con.Connect("JLANDAZURY", "Datecsa12");

                                Documento = JsonConvert.DeserializeObject<ObjetDocumento>(jsonString(metodoConsultaDescarga));
                                string[] dt = Documento.result.base64.Split(',');
                                byte[] bytes = Convert.FromBase64String(dt[1]);

                                var memory = new MemoryStream(bytes);
                                DocumentType documentType = app.Core.DocumentTypes.Find(documentTypeName);
                                FileType fileType = app.Core.FileTypes.Find(fileTypeName);

                                StoreNewDocumentProperties storeNewDocument = app.Core.Storage.CreateStoreNewDocumentProperties(documentType, fileType);
                                KeywordType radicadotype = app.Core.KeywordTypes.Find("Número de radicado");
                                Keyword NumRadicado = radicadotype.CreateKeyword("2022-12-E");
                                storeNewDocument.AddKeyword(NumRadicado);

                                PageData pageData = app.Core.Storage.CreatePageData(new MemoryStream(bytes), fileTypeName);
                                Document document = app.Core.Storage.StoreNewDocument(pageData,storeNewDocument);


                                //File.WriteAllBytes(@"D:\OneDrive - Datecsa S.A\Documentos\Proyectos\Desarrollos\ColaboracionOnbase\pdf\pdfFileName.pdf", bytes);
                                break;
                            }
                        }
                    }
                }
            }
            return Documento;
        }

        /// <summary>
        /// Consulta de documento
        /// </summary>
        public class ObjetDocumento : standar
        {
            [JsonProperty("result")]
            public resultDocumento result { get; set; }
        }

        /// <summary>
        /// Consulta de eventos
        /// </summary>
        public class ObjetEventos : standar
        {
            [JsonProperty("result")]
            public resultEventos[] result { get; set; }
        }

        /// <summary>
        /// Consulta de correos
        /// </summary>
        public class ObjetGeneral : standar
        {
            [JsonProperty("result")]
            public resultGeneral[] result { get; set; }
        }

        /// <summary>
        /// Objeto del Login 
        /// </summary>
        public class ObjectLogin : standar
        {
            [JsonProperty("result")]
            public resultLogin result { get; set; }
        }

    }

    /// <summary>
    /// Parametros estandar de la respuesta
    /// </summary>
    public class standar
    {
        [JsonProperty("statusCode")]
        public int statusCode { get; set; }

        [JsonProperty("statusMessage")]
        public string statusMessage { get; set; }

        [JsonProperty("statusDescription")]
        public string statusDescription { get; set; }
    }

    public partial class resultDocumento
    {
        [JsonProperty("totalPages")]
        public int totalPages { get; set; }

        [JsonProperty("base64")]
        public string base64 { get; set; }

        [JsonProperty("filename")]
        public string filename { get; set; }

        [JsonProperty("firm")]
        public bool firm { get; set; }
    }


    public partial class resultEventos
    {
        [JsonProperty("eventType")]
        public string eventType { get; set; }
        
        [JsonProperty("timestamp")]
        public string timestamp { get; set; }
        
        [JsonProperty("result")]
        public string result { get; set; }
    }

    /// <summary>
    /// Retorno de consulta general
    /// </summary>
    public partial class resultGeneral
    {
        [JsonProperty("_id")]
        public string _id { get; set; }

        [JsonProperty("companyID")]
        public companyIDGeneral companyID { get; set; }

        [JsonProperty("to")]
        public List<string> to { get; set; }

        [JsonProperty("emailID")]
        public string emailID { get; set; }

        [JsonProperty("subject")]
        public string subject { get; set; }

        [JsonProperty("type")]
        public string type { get; set; }

        [JsonProperty("createdAt")]
        public string createdAt { get; set; }
    }

    /// <summary>
    /// Result Login
    /// </summary>
    public partial class resultLogin : companyIDLogin
    {
        //[JsonProperty("_id")]
        //public string _id { get; set; }

        [JsonProperty("email")]
        public string email { get; set; }

        [JsonProperty("firstName")]
        public string firstName { get; set; }

        [JsonProperty("lastName")]
        public string lastName { get; set; }

        [JsonProperty("documentType")]
        public string documentType { get; set; }

        [JsonProperty("documentNumber")]
        public string documentNumber { get; set; }

        [JsonProperty("changePassword")]
        public bool changePassword { get; set; }

        [JsonProperty("companyID")]
        public companyIDLogin companyID { get; set; }

        [JsonProperty("attachment")]
        public string attachment { get; set; }

        [JsonProperty("token")]
        public string token { get; set; }

    }
    
    /// <summary>
    /// Arreglo del compani id general
    /// </summary>
    public partial class companyIDGeneral
    {
        [JsonProperty("_id")]
        public string _id { get; set; }

        [JsonProperty("name")]
        public string name { get; set; }

        [JsonProperty("nit")]
        public string nit { get; set; }
    }

    /// <summary>
    /// Arreglo del compani id
    /// </summary>
    public partial class companyIDLogin : companyIDGeneral
    {
        [JsonProperty("deleteAt")]
        public bool deleteAt { get; set; }
    }

    /// <summary>
    /// Objeto con parametros de
    /// </summary>
    public class conexionContex
    {
        public string email { get; set; }
        public string password { get; set; }
        public conexionContex ()
        {
            this.email = "aplicaciongestiondoc@udea.edu.co";
            this.password = "Correo2021*";
        }
    }

    /// <summary>
    /// Metodos usados
    /// </summary>
    public class MetodosGSE : conexionContex
    {
        public conexionContex contex = new conexionContex();
        public string metodoLogin { get; set; }
        public string token { get; set; }
        public string emailID { get; set; }
        public string metodoConsultaGeneral { get; set; }
        public string metodoConsultaEventos { get; set; }
        public string metodoConsultaDescarga { get; set; }

        public MetodosGSE () {}
        public void setLogin()
        {
            this.metodoLogin = "https://microservice.vinkel.co/v1/auth/login";
        }
        public void setEventos ()
        {
            this.metodoConsultaEventos = "https://microservice.vinkel.co/v1/emailAPI/";
            this.metodoConsultaEventos = this.metodoConsultaEventos + this.emailID;
        }
        public void setGeneral(string date)
        {
            this.metodoConsultaGeneral = "https://microservice.vinkel.co/v1/emailAPI/?date=";
            this.metodoConsultaGeneral = this.metodoConsultaGeneral + date;
        }
        public void setDescarga()
        {
            this.metodoConsultaDescarga = "https://microservice.vinkel.co/v1/emailAPI/" + emailID + "/record/";
        }
    }

}
