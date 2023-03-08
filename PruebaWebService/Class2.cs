using System;
using Newtonsoft.Json;
using System.Net;
using System.IO;
using System.Text;
using System.Collections.Generic;
using Newtonsoft.Json.Converters;
using System.Globalization;
using System.Linq;
  
namespace UnityAPI
{
   public class ProgramWS
    {
        public void  WS()
        {
            int contantes, contdes;
            
            string url = "http://asone.udea.edu.co/WsSipe/servicios/consultarDependencias"; // "http: //asone.udea.edu.co/WsSipe/servicios/consultaEmpleados";
            WebClient json = new WebClient();
            json.Headers["OAuth_Token"] = "C0A207DDF3F5BE74A3BD1876A833197ADAC955EC";
            json.Headers["Tipo_Conexion"] = "Desarrollo";
            json.QueryString["interesaCentroCosto"] = "S";
            json.QueryString["dependencia"] = "";
            var stringjson = json.DownloadString(url);
            List<string> resultados = new List<string>();
            
            Rootobject root = JsonConvert.DeserializeObject<Rootobject>(stringjson);
            // string nombredep    =root.Object[0].Nombre.ToString();
            
            foreach (var i in root.Object)
            {
                string strnombre = i.nombreFacultad.ToString();
                               
                resultados.Add(strnombre);
                
                string nombrecompleto = strnombre;
                //Console.WriteLine(i.nombres.ToString());
                //Console.WriteLine(nombrecompleto);

            }
            contantes = resultados.Count();
            var distinctResultados = resultados.Distinct();
            contdes = distinctResultados.Count();
            // Console.WriteLine(nombredep);
            /* foreach (string valor in distinctResultados)
               {​​
                   Console.WriteLine(valor);
               }*/

        }

        public class Rootobject
        {

            [JsonProperty("object")]
            public Object[] Object { get; set; }
        }

        public partial class Object
        {
            [JsonProperty("centroCosto")]
            public string CentroCosto { get; set; }

            [JsonProperty("tipoIdentificacion")]
            public string tipoIdentificacion { get; set; }

            [JsonProperty("numeroIdentificacion")]
            public string numeroIdentificacion { get; set; }

            [JsonProperty("nombres")]
            public string nombres { get; set; }

            [JsonProperty("primerApellido")]
            public string primerApellido { get; set; }

            [JsonProperty("segundoApellido")]
            public string segundoApellido { get; set; }

            [JsonProperty("nombre")]
            public string nombre { get; set; }

            [JsonProperty("nombreFacultad")]
            public string nombreFacultad { get; set; }

            [JsonProperty("cargo")]
            public string cargo { get; set; }

            [JsonProperty("email")]
            public string email { get; set; }
        }

        public class dependendiasudea
        {

            public string centroCosto { get; set; }
            public string codigo { get; set; }
            public string codigoUnidad { get; set; }
            public bool manejaProyecto { get; set; }
            public string nombre { get; set; }
            public string nombreCentroCosto { get; set; }
            public string nombreUnidad { get; set; }
        }
    }

    public class ProgamRadicado
    {
        public string Extrae(string cadena) {
            string retorno ="";
            int posicion;
            posicion = cadena.IndexOf("-") -4 ;

            retorno = posicion.ToString();

            return retorno;
        }
    }
}

