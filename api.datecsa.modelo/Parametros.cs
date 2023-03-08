using System;

namespace api.datecsa.modelo
{
    /// <summary>
    /// Parametros para autenticacion
    /// </summary>s
    public class Parametros
    {
        public string Usuario { get; set; }
        public string Contrasena { get; set; }
    }

    /// <summary>
    /// Parametros para consulta por estado la pqrs
    /// </summary>
    public class ParametrosEstadpPqrs
    {
        public string EstadoPublicación { get; set; }
    }

    /// <summary>
    /// Parametros para consulta por radicado la pqrs
    /// </summary>
    public class ParametrosConRadicadopPqrs
    {
        public string EstadoPublicación { get; set; }
        public string NumeroRadicado { get; set; }
    }

    /// <summary>
    /// Parametros para ingresar una nueva PQRS
    /// </summary>
    public class ParametrosRadicadoPqrs
    {
       public string RadicacionAnonima    { get; set; }
       public string Justificación        { get; set; }
       public string TipoSolicitud        { get; set; }
       public string Caracterizacion      { get; set; }
       public string CondicionEspecial    { get; set; }
       public string VinculoUniversidad   { get; set; }
       public string SedeDirige           { get; set; }
       public string Descripcion          { get; set; }
       public string TipoIdentificacion   { get; set; }
       public string NumeroIdentificacion { get; set; }
       public string Nombre               { get; set; }
       public string PrimerApellido       { get; set; }
       public string SegundoApellido      { get; set; }
       public string Correo               { get; set; }
       public string Direccion            { get; set; }
       public string AutorizaTrataDatos   { get; set; }
       public string AutorizaEnvioCorreo  { get; set; }
       public string Telefono             { get; set; }
    }

    /// <summary>
    /// Parametros para consulta por estado la publicidad del acto
    /// </summary>
    public class ParamEstadoPublicidadActo
    {
        public string EstadoPublicación { get; set; }
    }

    /// <summary>
    /// Parametros para consultar por estado y numero de identificacion documento Publicidad del acto 
    /// </summary>
    public class ParamNumIdentPublicidadActo
    {
        public string EstadoPublicación { get; set; }
        public string NumIdentificaciónDest { get; set; }
    }

    /// <summary>
    /// Parametros para consulta por Normativa
    /// </summary>
    public class ParamNormativa
    {
        public DateTime fechaNormativa { get; set; }
    }
    
    /// <summary>
    /// Parametros para consultar varios filtros
    /// </summary>
    public class ParamVariosFiltros
    {
       public string Tema                   { get; set; } //": "Nombramiento",
       public string Subtema                { get; set; } //": "Gerencias",
       public string NúmeroActo             { get; set; } //": "4533",
       public string Clasificacion          { get; set; } //": "Juridica",
       public string NombreActo             { get; set; } //": "Acto superior",
       public string TipoActo               { get; set; } //": "Juridico",
       public string TipoCompetente         { get; set; } //": "Juridica",
       public string Competente             { get; set; } //": "Asistencia juridica",
       public string EstadoActo             { get; set; } //": "Actvo",
       public DateTime FechaExpediciondesde { get; set; } //": "20/04/2021",
       public DateTime FechaExpedicionhasta { get; set; } //": "27/04/2021",
       public string PalabrasClaves         { get; set; } //": "Nomb",

}

}