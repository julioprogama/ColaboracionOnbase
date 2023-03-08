using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace api.datecsa.modelo
{
    /// <summary>
    /// Arreglo con espacio para un adjuntos
    /// </summary>
    public class RespuestaEstadoDoc1
    {
        public string nombre				{get;set;}	//Acto superior",
        public string numero				{get;set;}	//45897",
        public DateTime fechaExpedicion		{get;set;}  //30/04/2021",
        public string competente 			{get;set;}  //Consejo academico",
        public string estadoActo 			{get;set;}  //Derrogado, sustituido",
        public string normasRelacionadas 	{get;set;}  //Norma #  1,2,3",
        public string linkDocPopNormasAfecta{get;set;}  //http://srvonbasedesws.udea.local/AppNet/docpop/docpop.aspx?KT144_0_0_0=2021-2180-E&clienttype=html&doctypeid=633"

        public RespuestaEstadoDoc1() { }

        public RespuestaEstadoDoc1(string prmnombre, string prmnumero, DateTime prmfechaExpedicion, string prmcompetente, string prmestadoActo, string prmnormasRelacionadas, string prmlinkDocPopNormasAfecta)
        {
            this.nombre                = prmnombre;
            this.numero                = prmnumero;
            this.fechaExpedicion       = prmfechaExpedicion;
            this.competente            = prmcompetente;
            this.estadoActo            = prmestadoActo;
            this.normasRelacionadas    = prmnormasRelacionadas;
            this.linkDocPopNormasAfecta= prmlinkDocPopNormasAfecta;
        }
    }

    /// <summary>
    /// Arreglo con espacio para dos adjuntos
    /// </summary>
    public class RespuestaEstadoDoc2
    {
        public string destinatario { get; set; } //Edwin Gaviria Zapata",
        public string NumRadi { get; set; } //2021-2180-E ",
        public DateTime fechaFijacion { get; set; } //28/04/2021",
        public DateTime fechaDesFijacion { get; set; } //06/05/2021",
        public string Competente { get; set; } //División de tecnología",
        public string linkDocPopDoc { get; set; } //http://srvonbasedesws.udea.local/AppNet/docpop/docpop.aspx?KT144_0_0_0=2021-2180-E&clienttype=html&doctypeid=653",
        public string linkDocPopAdjuntos { get; set; } //http://srvonbasedesws.udea.local/AppNet/docpop/docpop.aspx?KT144_0_0_0=2021-2180-E&clienttype=html&doctypeid=612",

        public RespuestaEstadoDoc2() { }

        /// <summary>
        /// Asignacion de los detalles por documento
        /// </summary>
        /// <param name="prmdestinatario"></param>
        /// <param name="prmNumRadi"></param>
        /// <param name="prmfechaFijacion"></param>
        /// <param name="prmfechaDesFijacion"></param>
        /// <param name="prmCompetente"></param>
        /// <param name="prmlinkDocPopDoc"></param>
        /// <param name="prmlinkDocPopAdjuntos"></param>
        public RespuestaEstadoDoc2(string prmdestinatario, string prmNumRadi, DateTime prmfechaFijacion, DateTime prmfechaDesFijacion, string prmCompetente, string prmlinkDocPopDoc, string prmlinkDocPopAdjuntos)
        {
            this.destinatario = prmdestinatario;
            this.NumRadi = prmNumRadi;
            this.fechaFijacion = prmfechaFijacion;
            this.fechaDesFijacion = prmfechaDesFijacion;
            this.Competente = prmCompetente;
            this.linkDocPopDoc = prmlinkDocPopDoc;
            this.linkDocPopAdjuntos = prmlinkDocPopAdjuntos;
        }
    }

    /// <summary>
    /// Arreglo con espacio para tres adjuntos
    /// </summary>
    public class RespuestaEstadoDoc3
    {
        public string destinatario          {get;set;}///": "Edwin Gaviria Zapata",
        public string NunRadicadoPQRS       {get;set;}///": "2021-2180-E ",
        public DateTime fechaFijacion       {get;set;}///": "28/04/2021",
        public DateTime fechaDesFijacion    {get;set;}///": "06/05/2021",
        public string nombreDependencia     {get;set;}///": "División de tecnología",
        public string Descripción           {get;set;}///": "PQRS de prueba",
        public string tipoRespuesta         {get;set;}///": "Respuesta total",
        public string linkDocPopNotificacion{get;set;}///": http://srvonbasedesws.udea.local/AppNet/docpop/docpop.aspx?KT144_0_0_0=2021-2180-E&clienttype=html&doctypeid=638,
        public string linkDocPopRespuesta   {get;set;}///": http://srvonbasedesws.udea.local/AppNet/docpop/docpop.aspx?KT144_0_0_0=2021-2180-E&clienttype=html&doctypeid=653",
        public string linkDocPopAdjuntos    {get;set;}///": http://srvonbasedesws.udea.local/AppNet/docpop/docpop.aspx?KT144_0_0_0=2021-2180-E&clienttype=html&doctypeid=612",

        public RespuestaEstadoDoc3() { }

        public RespuestaEstadoDoc3 (string prmdestinatario, string prmNunRadicadoPQRS, DateTime prmfechaFijacion, DateTime prmfechaDesFijacion, string prmnombreDependencia, string prmDescripción, string prmtipoRespuesta, string prmlinkDocPopNotificacion, string prmlinkDocPopRespuesta, string prmlinkDocPopAdjuntos)
        {
            this.destinatario          = prmdestinatario;
            this.NunRadicadoPQRS       = prmNunRadicadoPQRS;
            this.fechaFijacion         = prmfechaFijacion;
            this.fechaDesFijacion      = prmfechaDesFijacion;
            this.nombreDependencia     = prmnombreDependencia;
            this.Descripción           = prmDescripción;
            this.tipoRespuesta         = prmtipoRespuesta;
            this.linkDocPopNotificacion= prmlinkDocPopNotificacion;
            this.linkDocPopRespuesta   = prmlinkDocPopRespuesta;
            this.linkDocPopAdjuntos    = prmlinkDocPopAdjuntos;
        }

    }

    /// <summary>
    /// Arreglo con espacio para tres adjuntos
    /// </summary>
    public class RespuestaEstadoDoc4
    {
        public string destinatario { get; set; }///": "Edwin Gaviria Zapata",
        public string NumRadi { get; set; }///": "2021-2180-E ",
        public DateTime fechaFijacion { get; set; }///": "28/04/2021",
        public DateTime fechaDesFijacion { get; set; }///": "06/05/2021",
        public string Competente { get; set; }///": "División de tecnología",
        public string Descripción { get; set; }///": "PQRS de prueba",
        public string tipoRespuesta { get; set; }///": "Respuesta total",
        public string linkDocPopAdjutno1 { get; set; }///": http://srvonbasedesws.udea.local/AppNet/docpop/docpop.aspx?KT144_0_0_0=2021-2180-E&clienttype=html&doctypeid=638,
        public string linkDocPopAnexo1 { get; set; }///": http://srvonbasedesws.udea.local/AppNet/docpop/docpop.aspx?KT144_0_0_0=2021-2180-E&clienttype=html&doctypeid=653",
        public string linkDocPopAnexo2 { get; set; }///": http://srvonbasedesws.udea.local/AppNet/docpop/docpop.aspx?KT144_0_0_0=2021-2180-E&clienttype=html&doctypeid=612",
        public string linkDocPopAnexo3 { get; set; }///": http://srvonbasedesws.udea.local/AppNet/docpop/docpop.aspx?KT144_0_0_0=2021-2180-E&clienttype=html&doctypeid=612",

        public RespuestaEstadoDoc4() { }

        public RespuestaEstadoDoc4(string prmdestinatario, string prmNunRadicadoPQRS, DateTime prmfechaFijacion, DateTime prmfechaDesFijacion, string prmnombreDependencia, string prmDescripción, string prmtipoRespuesta, string prmlinkDocPopAdjutno1, string prmlinkDocPopAdjutno2, string prmlinkDocPopAdjutno3, string prmlinkDocPopAdjutno4)
        {
            this.destinatario = prmdestinatario;
            this.NumRadi = prmNunRadicadoPQRS;
            this.fechaFijacion = prmfechaFijacion;
            this.fechaDesFijacion = prmfechaDesFijacion;
            this.Competente = prmnombreDependencia;
            this.Descripción = prmDescripción;
            this.tipoRespuesta = prmtipoRespuesta;
            this.linkDocPopAdjutno1 = prmlinkDocPopAdjutno1;
            this.linkDocPopAnexo1 = prmlinkDocPopAdjutno2;
            this.linkDocPopAnexo2 = prmlinkDocPopAdjutno3;
            this.linkDocPopAnexo3 = prmlinkDocPopAdjutno4;
        }

    }

    public class Lista
    {
        public List<string> Datos { get; set; }
        public Lista() { }
        public Lista(List<string> Dts) {
            this.Datos = Dts;
        }
    }

    /// <summary>
    /// Arreglo de Para lista
    /// </summary>
    public class ResquestLista
    {
        public string NomLista { get; set; }
        public Lista DatosLista { get; set; }

        public ResquestLista() { }
    }
}
