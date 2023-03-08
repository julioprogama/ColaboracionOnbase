using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace api.datecsa.modelo
{
    public class ConsultarDocumentoDocHandle
    {
        
        public long documentHandle { get; set; } 

        public ConsultarDocumentoDocHandle(long DocumentHandle)
        {
            this.documentHandle = DocumentHandle;
        }
        public ConsultarDocumentoDocHandle()
        {


        }

    }

    public class ConsultarDocumentoDocHandleResponse
    {
        public ConsultarDocumentoDocHandleResponse(string archivoBase64, string codigoError, string descripcionError, string linkDocPop, string nombreDocumento, string fechaCreacion, string version)
        {
            this.archivoBase64 = archivoBase64;
            this.codigoRespuesta = codigoError;
            this.descripcionRespuesta = descripcionError;
            this.linkDocPop = linkDocPop;
          //  this.nombreDocumento = nombreDocumento;
           // this.fechaCreacion = fechaCreacion;
        }

        public ConsultarDocumentoDocHandleResponse()
        {

        }
        public string archivoBase64 { get; set; }
 
        public string codigoRespuesta { get; set; }

        public string descripcionRespuesta{ get; set; }

        public string linkDocPop { get; set; }

        public string version { get; set; }

        // public string nombreDocumento { get; set; }

        // public string fechaCreacion { get; set; }

        // public string DocumentHandle { get; set; }



    }


    public class ConsultarDocHandleUnity
    {

        private string url;
        private string datasource;

        private string rutaArchivo, urlDocPop;
        private long docHandle;
        private string user;
        private string password;

        public ConsultarDocHandleUnity(long docHandle, string url, string datasource, string rutaArchivo, string urlDocPop, string user, string password)
        {
            this.url = url;
            this.datasource = datasource;
            this.rutaArchivo = rutaArchivo;
            this.urlDocPop = urlDocPop;
            this.docHandle = docHandle;
            this.user = user;
            this.password = password;
        }

        public string Url { get => url; set => url = value; }
        public string Datasource { get => datasource; set => datasource = value; }
        public string RutaArchivo { get => rutaArchivo; set => rutaArchivo = value; }
        public string UrlDocPop { get => urlDocPop; set => urlDocPop = value; }
         
        public string User { get => user; set => user = value; }
        public string Password { get => password; set => password = value; }
        public long DocHandle { get => docHandle; set => docHandle = value; }
    }


    public class ParametrosConsultarDocHandle
    {

        public ConsultarDocumentoDocHandle parametros { get; set; }


        public ParametrosConsultarDocHandle()
        {



        }

    }


    public class RespuestaDocHandle
    {

        public ConsultarDocumentoDocHandleResponse respuesta;

        public RespuestaDocHandle(ConsultarDocumentoDocHandleResponse resp)
        {


            this.respuesta = resp;

        }




    }

}
