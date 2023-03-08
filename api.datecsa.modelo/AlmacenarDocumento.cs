using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace api.datecsa.modelo
{
    [ServiceKnownType(typeof(Respuesta))]
    [ServiceKnownType(typeof(Documento))]
    [ServiceKnownType(typeof(Metadato))]
    public class AlmacenarDocumento
    {
        public AlmacenarDocumento(List<Metadato> arregloMetadatos, string idAplicacionOrigen, string nombreTipoDocumental, string archivoBase64)
        {
            this.arregloMetadatos = arregloMetadatos;
            this.archivoBase64 = archivoBase64;
            this.idAplicacionOrigen = idAplicacionOrigen;
            this.nombreTipoDocumental = nombreTipoDocumental;
        }



        public AlmacenarDocumento()
        {


        }

        public List<Metadato> arregloMetadatos { get; set; }
        public string archivoBase64  { get; set; }
        public string idAplicacionOrigen { get; set; }
        public string nombreTipoDocumental { get; set; }
 
       // public List<Metadato> ArregloMetadatos { get => arregloMetadatos; set => arregloMetadatos = value; }
       // public string ArchivoBase64 { get => archivoBase64; set => archivoBase64 = value; }
       // public string IdAplicacionOrigen { get => idAplicacionOrigen; set => idAplicacionOrigen = value; }
        // public string NombreTipoDocumental { get => nombreTipoDocumental; set => nombreTipoDocumental = value; }
    }

    public class AlmacenarDocumentoResponse
    {
        public AlmacenarDocumentoResponse(RespuestaAlmacenar Respuesta)
        {

            // this.CodigoError = CodigoError;
            this.respuesta = Respuesta;
            // this.DescripcionError = DescripcionError;

        }

        public AlmacenarDocumentoResponse(RespuestaAlmacenar Respuesta, string CodigoError, string DescripcionError)
        {

           // this.CodigoRespuesta = CodigoError;
            this.respuesta = Respuesta;
           // this.DescripcionRespuesta = DescripcionError;

        }

        public AlmacenarDocumentoResponse()
        {

        }


        public RespuestaAlmacenar respuesta { get; set; }

        //public string DocumentHandle { get; set; }

        //public string CodigoRespuesta { get; set; }

       // public string DescripcionRespuesta { get; set; }

        //public string LinkDocPop { get; set; }

        //public string NombreDocumento { get; set; }
    }


    public class AlmacenarDocumentoUnity
    {

        private string url;
        private string datasource;

        private string rutaArchivo, urlDocPop;
        private IList<object> listaDocumentos;
        private string user;
        private string password;
        //private string codigoSistemaOrigen;

        public AlmacenarDocumentoUnity(IList<object> listaDocumentos, string url, string datasource, string rutaArchivo, string urlDocPop, string user, string password)
        {
            this.url = url;
            this.datasource = datasource;
            this.rutaArchivo = rutaArchivo;
            this.urlDocPop = urlDocPop;
            this.listaDocumentos = listaDocumentos;
            this.user = user;
            this.password = password;
        }

        public string Url { get => url; set => url = value; }
        public string Datasource { get => datasource; set => datasource = value; }
        public string RutaArchivo { get => rutaArchivo; set => rutaArchivo = value; }
        public string UrlDocPop { get => urlDocPop; set => urlDocPop = value; }
        public IList<object> ListaDocumentos { get => listaDocumentos; set => listaDocumentos = value; }
        public string User { get => user; set => user = value; }
        public string Password { get => password; set => password = value; }
    }


    public class ParametrosAlmacenarDocumento {

        public AlmacenarDocumento parametros { get; set;  }


        public ParametrosAlmacenarDocumento()
        {



        }



        }


}
