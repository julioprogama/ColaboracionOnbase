using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace api.datecsa.modelo
{
    public class ActualizarDocumento
    {

        public long documentHandle { get; set; }

        public string archivoBase64 { get; set; }

        public ActualizarDocumento(long documentHandle, string ArchivoBase64)
        {
            this.documentHandle = documentHandle;

            this.archivoBase64 = ArchivoBase64;

        }

    }


    public class ActualizarDocumentoResponse
    {

        public string documentHandle { get; set; }

        public string codigoRespuesta { get; set; }

        public string descripcionRespuesta { get; set; }

        public string linkDocPop { get; set; }

        public string version { get; set; }




        public ActualizarDocumentoResponse(string CodigoRespuesta, string DescripcionRespuesta, string DocumentHandle, string LinkDocPop, string NombreDocumento, string version)
        {
            this.codigoRespuesta = CodigoRespuesta;
            this.descripcionRespuesta = DescripcionRespuesta;
            this.documentHandle = DocumentHandle;
            this.linkDocPop = LinkDocPop;
            this.version = version;

        }


        public ActualizarDocumentoResponse()
        {

        }




    }


    public class ActualizarDocumentoUnity
    {

        private string url;
        private string datasource;

        private string rutaArchivo, urlDocPop;
        private IList<object> listaMetadatos;
        private string user;
        private string password;
        private long documentHandle;

        public ActualizarDocumentoUnity(long documentHandle, IList<object> listaMetadatos, string url, string datasource, string rutaArchivo, string urlDocPop, string user, string password)
        {
            this.url = url;
            this.datasource = datasource;
            this.rutaArchivo = rutaArchivo;
            this.urlDocPop = urlDocPop;
            this.listaMetadatos = listaMetadatos;
            this.user = user;
            this.password = password;
            this.documentHandle = documentHandle;
        }

        public string Url { get => url; set => url = value; }
        public string Datasource { get => datasource; set => datasource = value; }
        public string RutaArchivo { get => rutaArchivo; set => rutaArchivo = value; }
        public string UrlDocPop { get => urlDocPop; set => urlDocPop = value; }
        public IList<object> ListaMetadatos { get => listaMetadatos; set => listaMetadatos = value; }
        public string User { get => user; set => user = value; }
        public string Password { get => password; set => password = value; }
        public long DocumentHandle { get => documentHandle; set => documentHandle = value; }
    }



    public class ParametrosActualizarDocumento
    {
        public ActualizarDocumento parametros { get; set; }
        public ParametrosActualizarDocumento()
        {



        }
    }


        public class RespuestaActualizarDocumento{

        public  ActualizarDocumentoResponse respuesta { get; set; }


        }
}
