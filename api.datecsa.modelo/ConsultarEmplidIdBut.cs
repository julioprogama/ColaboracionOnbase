using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace api.datecsa.modelo
{
    public class ConsultarEmplidIdBut
    {
        
        public string emplid { get; set; }
 

        public ConsultarEmplidIdBut(string emplid)
        {
            this.emplid = emplid;
           
        }

    }

    public class ConsultarEmplidIdButResponse
    {

        public string  idBut { get; set; }
        public string codigoRespuesta { get; set; }
        public string descripcionRespuesta { get; set; }
  

        public ConsultarEmplidIdButResponse(string idBut, string codigoRespuesta, string descripcionRespuesta)
        {
            this.idBut = idBut;
            this.codigoRespuesta = codigoRespuesta;
            this.descripcionRespuesta = descripcionRespuesta;
        }
        public ConsultarEmplidIdButResponse() { }

    }

    public class ParametrosConsultarEmplidIdBut
    {
        public ConsultarEmplidIdBut parametros { get; set; }

        public ParametrosConsultarEmplidIdBut(){ }

    }

    public class RespuestaConsultarEmplidIdBut
    {

        public ConsultarEmplidIdButResponse respuesta { get; set; }

        public RespuestaConsultarEmplidIdBut() { }


    }
}
