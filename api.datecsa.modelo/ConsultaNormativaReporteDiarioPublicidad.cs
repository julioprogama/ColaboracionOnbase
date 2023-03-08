using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace api.datecsa.modelo
{

    /// <summary>
    /// Webservice para la cosulta del Reporte Diario y Consulta Normativa
    /// </summary>
    public class consultaNormativaPorPalabraClave
    {
        public string fechaConsultaReporte { get; set; } //fechaConsultaReporte para Reporte Diario
        public string temaActo { get; set; } //KW para la Consulta Normativa
        public string subtemaActo { get; set; } //KW para la Consulta Normativa
        public string numeroActo { get; set; } //KW para la Consulta Normativa
        public string clasificacion { get; set; } //KW para la Consulta Normativa
        public string nombreActo { get; set; } //KW para la Consulta Normativa
        public string tipoActo { get; set; } //KW para la Consulta Normativa
        public string tipoCompetente { get; set; } //KW para la Consulta Normativa
        public string competente { get; set; } //KW para la Consulta Normativa
        public string estadoActo { get; set; } //KW para la Consulta Normativa
        public string fechaExpedicionDesde { get; set; } //KW para la Consulta Normativa
        public string fechaExpedicionHasta { get; set; } //KW para la Consulta Normativa
        public string palabrasClaves { get; set; } //KW para la Consulta Normativa



        public consultaNormativaPorPalabraClave(string fechaConsultaReporte, string temaActo, string subtemaActo, string numeroActo,
            string clasificacion, string nombreActo, string tipoActo, string tipoCompetente, string competente, string estadoActo, 
            string fechaExpedicionDesde, string fechaExpedicionHasta, string palabrasClaves)
    
        {
            this.fechaConsultaReporte = fechaConsultaReporte;
            this.temaActo = temaActo;
            this.subtemaActo = subtemaActo;
            this.numeroActo = numeroActo;
            this.clasificacion = clasificacion;
            this.nombreActo = nombreActo;
            this.tipoActo = tipoActo;
            this.tipoCompetente = tipoCompetente;
            this.competente = competente;
            this.estadoActo = estadoActo;
            this.fechaExpedicionDesde = fechaExpedicionDesde;
            this.fechaExpedicionHasta = fechaExpedicionHasta;
            this.palabrasClaves = palabrasClaves;
        }     

        }

    /// <summary>
    /// Datos retornados segun palabra clave
    /// </summary>
    public class RespuestaConsultaNormativaPorPalabraClave
    {
        public string nombreActo { get; set; } //Retorna nombre del acto para Reporte Diario y Consulta Normativa
        public string consecutivoActo { get; set; } //Retorna Consecutivo del acto para Reporte Diario y Consulta Normativa
        public DateTime fechaExpedicion { get; set; } //Retorna fecha de expedición para Reporte Diario y Consulta Normativa
        public string competente { get; set; } //Retorna competente para Reporte Diario y Consulta Normativa
        public string estadoActo { get; set; } //Retorna estado del acto para Reporte Diario y Consulta Normativa
        public string normasQueAfecta { get; set; } ///Retorna documentos para Reporte Diario y Consulta Normativa
        public string normasQueLasAfecta { get; set; } //Retorna documentos para Reporte Diario y Consulta Normativa


        public RespuestaConsultaNormativaPorPalabraClave(string nombreActo, string consecutivoActo, DateTime fechaExpedicion,
            string competente, string estadoActo, string normasQueAfecta, string normasQueLasAfecta)
        {
            this.nombreActo = nombreActo;
            this.consecutivoActo = consecutivoActo;
            this.fechaExpedicion = fechaExpedicion;
            this.competente = competente;
            this.estadoActo = estadoActo;
            this.normasQueAfecta = normasQueAfecta;
            this.normasQueLasAfecta = normasQueLasAfecta;
        }


    }




    public class ConsultaNormativaPorPalabraClaveUnity
    {
        public string nombreActo;
        public string consecutivoActo;
        public string fechaExpedicion;
        public string competente;
        public string estadoActo;
        public string normasQueAfecta;
        public string normasQueLasAfecta;


        public ConsultaNormativaPorPalabraClaveUnity(string nombreActo, string consecuvoActo, string fechaExpedicion,
            string competente, string estadoActo, string normasQueAfecta, string normasQueLasAfecta)
        {
            this.nombreActo = nombreActo;
            this.consecutivoActo = consecuvoActo;
            this.fechaExpedicion = fechaExpedicion;
            this.competente = competente;
            this.estadoActo = estadoActo;
            this.normasQueAfecta = normasQueAfecta;
            this.normasQueLasAfecta = normasQueLasAfecta;
        }

     
        public string NombreActo { get => nombreActo; set => nombreActo = value; }

        public string ConsecutivoActo { get => consecutivoActo; set => consecutivoActo = value; }

        public string FechaExpedicion { get => fechaExpedicion; set => fechaExpedicion = value; }

        public string Competente { get => competente; set => competente = value; }

        public string EstadoActo { get => estadoActo; set => estadoActo = value; }

        public string NormasQueAfecta { get => normasQueAfecta; set => normasQueAfecta = value; }

        public string NormasQueLasAfecta { get => normasQueLasAfecta; set => normasQueLasAfecta = value; }
    }






}
