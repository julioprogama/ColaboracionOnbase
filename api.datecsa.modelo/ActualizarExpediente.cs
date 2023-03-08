using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace api.datecsa.modelo
{
    public class ActualizarExpediente
    {

        public IList<Metadato> arregloMetadatos { get; set; }
      
          public string idBut { get; set; }


        public ActualizarExpediente(IList<Metadato> ListaMetadatos, string IdBut)
        {
            this.arregloMetadatos = ListaMetadatos;
            this.idBut = IdBut;
           
        }

    }


    public class ActualizarExpedienteResponse
    {

        

        public string codigoRespuesta { get; set; }

        public string descripcionRespuesta { get; set; }
 

        public ActualizarExpedienteResponse(string CodigoRespuesta, string DescripcionRespuesta)
        {
            this.codigoRespuesta = CodigoRespuesta;
            this.descripcionRespuesta = DescripcionRespuesta;
            
        }


        public ActualizarExpedienteResponse()
        {

        }




    }


    public class ParametrosActualizarExpediente
    {

        public ActualizarExpediente parametros { get; set; }


        public ParametrosActualizarExpediente()
        {



        }

    }

    public class RespuestaActualizarExpediente
    {

        public ActualizarExpedienteResponse respuesta { get; set; }


        public RespuestaActualizarExpediente()
        {




        }


    }
     
}
