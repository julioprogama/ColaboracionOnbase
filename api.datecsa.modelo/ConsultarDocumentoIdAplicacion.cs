using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace api.datecsa.modelo
{
    public class ConsultarDocumentoIdAplicacion
    {
        
        public string idDocumentoAplicacion { get; set; }
        public string idAplicacionOrigen { get; set;  }

        public ConsultarDocumentoIdAplicacion(string idDocumentoAplicacion, string idAplicacionOrigen)
        {
            this.idDocumentoAplicacion = idDocumentoAplicacion;
            this.idAplicacionOrigen = idAplicacionOrigen;
        }

}

    public class ConsultarDocumentoIdAplicacionResponse
    {

        public string archivoBase64 { get; set; }
        public string codigoRespuesta { get; set; }
        public string descripcionRespuesta { get; set; }
        public string linkDocPop { get; set; }
      
        public string documentHandle { get; set; }

        public ConsultarDocumentoIdAplicacionResponse(string archivoBase64, string codigoRespuesta, string descripcionRespuesta, string linkDocPop ,string documentHandle)
        {
            this.archivoBase64 = archivoBase64;
            this.codigoRespuesta = codigoRespuesta;
            this.descripcionRespuesta = descripcionRespuesta;
            this.linkDocPop = linkDocPop;
            this.documentHandle = documentHandle; 
            
        }
        public ConsultarDocumentoIdAplicacionResponse()
        {
 
        }

    }

    public class ConsultarDocumentoIdAplicacionUnity
    {

        private string url;
        private string datasource;

        private string rutaArchivo, urlDocPop;
        private string  idAplicacion;
        private string user;
        private string password;

        public ConsultarDocumentoIdAplicacionUnity(string idAplicacion, string url, string datasource, string rutaArchivo, string urlDocPop, string user, string password)
        {
            this.url = url;
            this.datasource = datasource;
            this.rutaArchivo = rutaArchivo;
            this.urlDocPop = urlDocPop;
            this.idAplicacion = idAplicacion;
            this.user = user;
            this.password = password;
        }

        public string Url { get => url; set => url = value; }
        public string Datasource { get => datasource; set => datasource = value; }
        public string RutaArchivo { get => rutaArchivo; set => rutaArchivo = value; }
        public string UrlDocPop { get => urlDocPop; set => urlDocPop = value; }
        public string User { get => user; set => user = value; }
        public string Password { get => password; set => password = value; }
        public string IdAplicacion { get => idAplicacion ; set => idAplicacion = value; }
    }


    public class ParametrosConsultarDocumentoIdAplicacion
    {

        public ConsultarDocumentoIdAplicacion parametros { get; set; }

        public ParametrosConsultarDocumentoIdAplicacion()
        {



        }


    }


    public class RespuestaConsultarDocumentoIdAplicacion
    {

        public ConsultarDocumentoIdAplicacionResponse respuesta { get; set; }

        public RespuestaConsultarDocumentoIdAplicacion(ConsultarDocumentoIdAplicacionResponse respuesta)
        {

            this.respuesta = respuesta;

        }


    }
}
