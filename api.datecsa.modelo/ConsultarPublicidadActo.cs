using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace api.datecsa.modelo { 

    /// <summary>
    /// Datos retornados por la consulta de publicidad del acto
    /// </summary>
    public class ResponseEstadoPublicidadActo
    {
        public string codigoRespuesta { get; set; }
        public string descripcionRespuesta { get; set; }
        public IList<RespuestaEstadoDoc4> arregloDocumentos { get; set; }

        /// <summary>
        /// Metodo vacio
        /// </summary>
        public ResponseEstadoPublicidadActo() { }
        
        /// <summary>
        /// cargar Listado de documentos 
        /// </summary>
        /// <param name="CodigoRespuesta">Codigo de la respuesta</param>
        /// <param name="DescripcionRespuesta">Descripcion de la respuetas</param>
        /// <param name="ResultadosDocumentos">Listado de documentos</param>
        public ResponseEstadoPublicidadActo(string CodigoRespuesta, string DescripcionRespuesta, IList<RespuestaEstadoDoc4> ResultadosDocumentos)
        {
            this.codigoRespuesta = CodigoRespuesta;
            this.descripcionRespuesta = DescripcionRespuesta;
            this.arregloDocumentos = ResultadosDocumentos;
        }
    }

    /// <summary>
    /// Datos retornados por la consulta de publicidad del acto
    /// </summary>
    public class ResponseNumActoPublicidadActo
    {
        public string codigoRespuesta { get; set; }
        public string descripcionRespuesta { get; set; }
        public IList<RespuestaEstadoDoc3> arregloDocumentos { get; set; }

        /// <summary>
        /// Metodo vacio
        /// </summary>
        public ResponseNumActoPublicidadActo() { }

        /// <summary>
        /// cargar Listado de documentos 
        /// </summary>
        /// <param name="CodigoRespuesta">Codigo de la respuesta</param>
        /// <param name="DescripcionRespuesta">Descripcion de la respuetas</param>
        /// <param name="ResultadosDocumentos">Listado de documentos</param>
        public ResponseNumActoPublicidadActo(string CodigoRespuesta, string DescripcionRespuesta, IList<RespuestaEstadoDoc3> ResultadosDocumentos)
        {
            this.codigoRespuesta = CodigoRespuesta;
            this.descripcionRespuesta = DescripcionRespuesta;
            this.arregloDocumentos = ResultadosDocumentos;
        }
    }

    /// <summary>
    /// Datos retornados por la consulta de publicidad del acto
    /// </summary>
    public class ResponseRadiPublicidadActo
    {
        public string codigoRespuesta { get; set; }
        public string descripcionRespuesta { get; set; }
        public IList<RespuestaEstadoDoc4> arregloDocumentos { get; set; }

        /// <summary>
        /// Metodo vacio
        /// </summary>
        public ResponseRadiPublicidadActo() { }

        /// <summary>
        /// cargar Listado de documentos 
        /// </summary>
        /// <param name="CodigoRespuesta">Codigo de la respuesta</param>
        /// <param name="DescripcionRespuesta">Descripcion de la respuetas</param>
        /// <param name="ResultadosDocumentos">Listado de documentos</param>
        public ResponseRadiPublicidadActo(string CodigoRespuesta, string DescripcionRespuesta, IList<RespuestaEstadoDoc4> ResultadosDocumentos)
        {
            this.codigoRespuesta = CodigoRespuesta;
            this.descripcionRespuesta = DescripcionRespuesta;
            this.arregloDocumentos = ResultadosDocumentos;
        }
    }


    /// <summary>
    /// Datos retornados por la consulta de Normativa
    /// </summary>
    public class ResponseNormativa
    {
        public string codigoRespuesta { get; set; }
        public string descripcionRespuesta { get; set; }
        public IList<RespuestaEstadoDoc1> arregloDocumentos { get; set; }

        /// <summary>
        /// Metodo vacio
        /// </summary>
        public ResponseNormativa() { }

        /// <summary>
        /// cargar Listado de documentos 
        /// </summary>
        /// <param name="CodigoRespuesta">Codigo de la respuesta</param>
        /// <param name="DescripcionRespuesta">Descripcion de la respuetas</param>
        /// <param name="ResultadosDocumentos">Listado de documentos</param>
        public ResponseNormativa(string CodigoRespuesta, string DescripcionRespuesta, IList<RespuestaEstadoDoc1> ResultadosDocumentos)
        {
            this.codigoRespuesta = CodigoRespuesta;
            this.descripcionRespuesta = DescripcionRespuesta;
            this.arregloDocumentos = ResultadosDocumentos;
        }
    }



}