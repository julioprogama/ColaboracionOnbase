using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace api.datecsa.modelo
{
    public class ConsultarDocumentoFechaPalabrasClave
    {

        public string nombreTipoDocumental { get; set; }
        public string fechaInicial { get; set; }
        public string fechaFinal { get; set; }
        public string idAplicacionOrigen { get; set; }
        public IList<Respuesta> parametros { get; set; }
        public ConsultarDocumentoFechaPalabrasClave() { }
        public ConsultarDocumentoFechaPalabrasClave(IList<Respuesta> arregloMetadatos, string fechaInicial, string fechaFinal, string nombreTipoDocumental, string idAplicacionOrigen)
        {
            this.parametros = arregloMetadatos;
            this.fechaInicial = fechaInicial;
            this.fechaFinal = fechaFinal;
            this.nombreTipoDocumental = nombreTipoDocumental;
            this.idAplicacionOrigen = idAplicacionOrigen;
        }
    }

    public class ConsultarDocumentoFechaPalabrasClaveResponse
    {
 
        public string codigoRespuesta { get; set; }
        public string descripcionRespuesta { get; set; }
 

        public IList<Respuesta> arregloDocumentos { get; set; }

        public ConsultarDocumentoFechaPalabrasClaveResponse(string CodigoRespuesta, string DescripcionRespuesta, IList<Respuesta> ResultadosDocumentos)
       {
            this.codigoRespuesta = CodigoRespuesta;
            this.descripcionRespuesta= DescripcionRespuesta;
            this.arregloDocumentos = ResultadosDocumentos; 
          
       }

        public ConsultarDocumentoFechaPalabrasClaveResponse() { }

    }


    public class ConsultarDocumentoFechaPalabrasClaveUnity
    {

        private string url;
        private string datasource;

        private string rutaArchivo, urlDocPop;

        private string fechaInicial;
        private string fechaFinal;
        private string nombreTipoDocumental;
        private IList<object> palabrasClave;
        private string user;
        private string password;

        public ConsultarDocumentoFechaPalabrasClaveUnity(string fechaInicial, string fechaFinal, string nombreTipoDocumental, IList<object> palabrasClave,string url, string datasource, string rutaArchivo, string urlDocPop, string user, string password )
        {
            this.url = url;
            this.datasource = datasource;
            this.rutaArchivo = rutaArchivo;
            this.urlDocPop = urlDocPop;
            this.user = user;
            this.password = password;
            this.fechaInicial = fechaInicial;
            this.fechaFinal = fechaFinal;
            this.nombreTipoDocumental = nombreTipoDocumental;
            this.palabrasClave = palabrasClave;
        }

        public string Url { get => url; set => url = value; }
        public string Datasource { get => datasource; set => datasource = value; }
        public string RutaArchivo { get => rutaArchivo; set => rutaArchivo = value; }
        public string UrlDocPop { get => urlDocPop; set => urlDocPop = value; }
 
        public string User { get => user; set => user = value; }
        public string Password { get => password; set => password = value; }
        public string FechaInicial { get => fechaInicial; set => fechaInicial = value; }
        public string FechaFinal { get => fechaFinal; set => fechaFinal = value; }
        public string NombreTipoDocumental { get => nombreTipoDocumental; set => nombreTipoDocumental = value; }
        public IList<object> PalabrasClave { get => palabrasClave; set => palabrasClave = value; }
    }

    
}
