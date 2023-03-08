using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace api.datecsa.modelo
{
    [DataContract]
    [KnownType(typeof(Metadato))]
    public class Documento
    {
        [DataMember]
        public string ArchivoBase64 { get; set; }
        [DataMember]
        public string NombreArchivo { get; set; }
        [DataMember]
        public string FechaCreacion { get; set; }
        [DataMember]
        public string LinkDocPop { get; set; }
        [DataMember]
        public long DocHandle { get; set; }
        [DataMember]
        public string CodigoError { get; set; }
        [DataMember]
        public string DescripcionError { get; set; }

        [DataMember]
        public IList<Metadato> Metadatos { get; set; }

        public string NombreTipoDocumental { get; set; }
       

        public string IdAplicacionOrigen { get; set; }

        public Documento(string ArchivoBase64,string NombreArchivo, string FechaCreacion, string LinkDocPop, long DocHandle, string CodigoError, string DescripcionError, IList<Metadato> Metadatos, string NombreTipoDocumental, string IdAplicacionOrigen)
        {
            this.ArchivoBase64 = ArchivoBase64;
            this.NombreArchivo = NombreArchivo;
            this.FechaCreacion = FechaCreacion;
            this.LinkDocPop = LinkDocPop;
            this.DocHandle = DocHandle;
            this.DescripcionError = DescripcionError;
            this.Metadatos = Metadatos;
            this.NombreTipoDocumental = NombreTipoDocumental;
            this.IdAplicacionOrigen = IdAplicacionOrigen;

        }

        public Documento()
        {


        }


    }

    public class Respuesta 
    {
        [DataMember]
        public string nombreDocumento { get; set; }        
        [DataMember]
        public string linkDocPop { get; set; }
        [DataMember]
        public long documentHandle { get; set; }

        [DataMember]
        public string nombreTipoDocumental { get; set; }
       
        public Respuesta(string NombreArchivo,  string LinkDocPop, long DocHandle, string nombreTipoDocumental)
        {
            this.nombreDocumento = NombreArchivo;
            this.linkDocPop = LinkDocPop;
            this.documentHandle = DocHandle;
            this.nombreTipoDocumental = nombreTipoDocumental;

        }

        public Respuesta(){ }

    }

    public class RespuestaAlmacenar
    {


        
        [DataMember]
        public string linkDocPop { get; set; }
        [DataMember]
        public long documentHandle { get; set; }
        [DataMember]
        public string codigoRespuesta{ get; set; }
        [DataMember]
        public string descripcionRespuesta{ get; set; }



        public RespuestaAlmacenar(string NombreArchivo, string FechaCreacion, string LinkDocPop, long DocHandle, string CodigoError, string DescripcionError)
        {

           
            this.linkDocPop = LinkDocPop;
            this.documentHandle = DocHandle;
            this.descripcionRespuesta = DescripcionError;


        }


        public RespuestaAlmacenar( string LinkDocPop, long DocHandle, string CodigoError, string DescripcionError)
        {

            this.codigoRespuesta = CodigoError;
            this.linkDocPop = LinkDocPop;
            this.documentHandle = DocHandle;
            this.descripcionRespuesta = DescripcionError;


        }

        public RespuestaAlmacenar()
        {


        }

    }



}
