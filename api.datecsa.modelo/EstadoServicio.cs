using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace api.datecsa.modelo
{
    public class EstadoServicio
    {

      
        public EstadoServicio( )
        {
           
        }

       
    }


    public class EstadoServicioResponse
    {
        private string codigoRespuesta;
        private string descripcionRespuesta;
     

        public EstadoServicioResponse(string codigoRespuesta, string mensajeRespuesta)
        {
            this.codigoRespuesta = codigoRespuesta;
            this.descripcionRespuesta = mensajeRespuesta;
          
        }

        public string CodigoRespuesta { get => codigoRespuesta; set => codigoRespuesta = value; }
        public string DescripcionRespuesta { get => descripcionRespuesta; set => descripcionRespuesta = value; }
   
    }
}
