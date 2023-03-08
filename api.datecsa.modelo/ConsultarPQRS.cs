using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace api.datecsa.modelo
{

    /// <summary>
    ///  Parametros de respuesta de consultas de documentos listo para publicar
    /// </summary>
    public class ResponseEstadoPQRS
    {
        public string codigoRespuesta { get; set; }
        public string descripcionRespuesta { get; set; }
        public IList<RespuestaEstadoDoc3> arregloDocumentos { get; set; }

        /// <summary>
        /// Metodo vacio
        /// </summary>
        public ResponseEstadoPQRS() { }

        /// <summary>
        /// cargar Listado de documentos 
        /// </summary>
        /// <param name="CodigoRespuesta">Codigo de la respuesta</param>
        /// <param name="DescripcionRespuesta">Descripcion de la respuetas</param>
        /// <param name="ResultadosDocumentos">Listado de documentos</param>
        public ResponseEstadoPQRS(string CodigoRespuesta, string DescripcionRespuesta, IList<RespuestaEstadoDoc3> ResultadosDocumentos)
        {
            this.codigoRespuesta = CodigoRespuesta;
            this.descripcionRespuesta = DescripcionRespuesta;
            this.arregloDocumentos = ResultadosDocumentos;
        }
    }

    /// <summary>
    ///  Parametros de respuesta de consultas de documentos listo para publicar
    /// </summary>
    public class ResponseRadicadoPQRS
    {
        public string codigoRespuesta { get; set; }
        public string descripcionRespuesta { get; set; }
        public string radicado { get; set; }

        /// <summary>
        /// Metodo vacio
        /// </summary>
        public ResponseRadicadoPQRS() { }

        /// <summary>
        /// cargar Listado de documentos 
        /// </summary>
        /// <param name="CodigoRespuesta">Codigo de la respuesta</param>
        /// <param name="DescripcionRespuesta">Descripcion de la respuetas</param>
        /// <param name="ResultadosDocumentos">Listado de documentos</param>
        public ResponseRadicadoPQRS(string CodigoRespuesta, string DescripcionRespuesta, string Radicado)
        {
            this.codigoRespuesta = CodigoRespuesta;
            this.descripcionRespuesta = DescripcionRespuesta;
            this.radicado = Radicado;
        }
    }
    
    /// <summary>
    ///  Parametros de respuesta de consultas de documentos listo para publicar
    /// </summary>
    public class ResponseListaoPQRS
    {
        public string codigoRespuesta { get; set; }
        public string descripcionRespuesta { get; set; }
        public List<ResquestLista> Listas { get; set; }
     
        /// <summary>
        /// Metodo vacio
        /// </summary>
        public ResponseListaoPQRS() { }

        /// <summary>
        /// cargar Listado de documentos 
        /// </summary>
        /// <param name="CodigoRespuesta">Codigo de la respuesta</param>
        /// <param name="DescripcionRespuesta">Descripcion de la respuetas</param>
        /// <param name="ResultadosDocumentos">Listado de documentos</param>
        public ResponseListaoPQRS(string CodigoRespuesta, string DescripcionRespuesta, List<ResquestLista> Lista)
        {
            this.codigoRespuesta = CodigoRespuesta;
            this.descripcionRespuesta = DescripcionRespuesta;
            this.Listas = Lista;
        }
    }

}
