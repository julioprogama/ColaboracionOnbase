using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hyland.Unity;
using Newtonsoft.Json;
using System.Net;
using System.IO;
using Newtonsoft.Json.Converters;
using System.Globalization;

namespace ColaboracionTest
{
    public class WsMares
    {
        #region IExternalAutofillKeysetScript
        /// <summary>
        /// Implementation of <see cref="IExternalAutofillKeysetScript.OnExternalAutofillKeysetScriptExecute" />.
        /// <seealso cref="IExternalAutofillKeysetScript" />
        /// </summary>
        /// <param name="app"></param>
        /// <param name="args"></param>
        public List<string> consultaestudiante(string NroIdentificacion)
        {
            List<string> InfoEstudiante = new List<string>();
            try
            {
                string para = "?cedula=" + NroIdentificacion;
                string url = "https://asone.udea.edu.co/wsGeneral/servicios/consultaPersonaNatural";
                WebClient json = new WebClient();
                json.Headers["OAuth_Token"] = "C0A207DDF3F5BE74A3BD1876A833197ADAC955EC";
                json.Headers["Tipo_Conexion"] = "Desarrollo";
                //json.QueryString["nombre"]="Edwin albey";

                var stringjson = "";
                stringjson = json.DownloadString(url + para);

                Rootobject root = JsonConvert.DeserializeObject<Rootobject>(stringjson);
                // string nombredep    =root.Object[0].Nombre.ToString();

                foreach (var i in root.Object)
                {

                    byte[] utf8Bytesnombre = Encoding.Default.GetBytes(i.nombre.ToString());
                    byte[] utf8Bytesapellidos = Encoding.Default.GetBytes(i.apellidos.ToString()); 

                    InfoEstudiante.Add(i.nombre.ToString());
                    InfoEstudiante.Add(i.apellidos.ToString());
                    InfoEstudiante.Add(i.email.ToString());
                    InfoEstudiante.Add(i.celular.ToString());
                    InfoEstudiante.Add(i.direccion.ToString()); 
                    InfoEstudiante.Add(i.telefono.ToString());
                    //Console.WriteLine(i.nombre.ToString());
                    //Console.ReadLine();
                }
            }
            catch (Exception ex)
            {
                InfoEstudiante.Add("Error");
                InfoEstudiante.Add(ex.Message.ToString());
            }

            return InfoEstudiante;
        }
        #endregion
        public class Rootobject
        {

            [JsonProperty("object")]
            public Object[] Object { get; set; }
        }

        public partial class Object
        {
            [JsonProperty("cedula ")]
            public string   cedula { get; set; } //VARCHAR2	11	
            
            [JsonProperty("nombre")]
            public string   nombre { get; set; } //VARCHAR2	22	
            
            [JsonProperty("apellidos")]
            public string   apellidos { get; set; } //VARCHAR2	40	
            
            [JsonProperty("email")]
            public string   email { get; set; } //VARCHAR2	60	
            
            [JsonProperty("celular")]
            public string   celular { get; set; } //VARCHAR2	15	
            
            [JsonProperty("telefono")]
            public string   telefono { get; set; } //VARCHAR2	15	
            
            [JsonProperty("direccion")]
            public string   direccion { get; set; } //VARCHAR2	66	
            
            [JsonProperty("codigoContinenteRes")]
            public int      codigoContinenteRes { get; set; } //NUMBER		1	
            
            [JsonProperty("nombreContinenteRes")]
            public string   nombreContinenteRes { get; set; } //VARCHAR2	100	
            
            [JsonProperty("codigoPaisRes")]
            public int      codigoPaisRes { get; set; } //NUMBER		4	
            
            [JsonProperty("nombrePaisRes")]
            public string   nombrePaisRes { get; set; } //VARCHAR2	100	
            
            [JsonProperty("codigoDeptoRes")]
            public int      codigoDeptoRes { get; set; } //NUMBER		2	
            
            [JsonProperty("nombreDeptoRes")]
            public string   nombreDeptoRes { get; set; } //VARCHAR2	100	
            
            [JsonProperty("codigoMpioRes")]
            public int      codigoMpioRes { get; set; } //NUMBER		3	
            
            [JsonProperty("nombreMpioRes")]
            public string   nombreMpioRes { get; set; } //VARCHAR2	100	
            
            [JsonProperty("codigoContinenteNace")]
            public int      codigoContinenteNace { get; set; } //NUMBER		1	
            
            [JsonProperty("nombreContinenteNace")]
            public string   nombreContinenteNace { get; set; } //VARCHAR2	100	
            
            [JsonProperty("codigoPaisNace")]
            public int      codigoPaisNace { get; set; } //NUMBER		4	
            
            [JsonProperty("nombrePaisNace")]
            public string   nombrePaisNace { get; set; } //VARCHAR2	100	
            
            [JsonProperty("codigoDeptoNace")]
            public int      codigoDeptoNace { get; set; } //NUMBER		2	
            
            [JsonProperty("nombreDeptoNace")]
            public string   nombreDeptoNace { get; set; } //VARCHAR2	100	
            
            [JsonProperty("codigoMpioNace")]
            public int      codigoMpioNace { get; set; } //NUMBER		3	
           
            [JsonProperty("nombreMpioNace")]
            public string   nombreMpioNace { get; set; } //VARCHAR2	100	
            
            [JsonProperty("codigoColegio")]
            public string   codigoColegio { get; set; } //VARCHAR2	6	
            
            [JsonProperty("nombreColegio")]
            public string   nombreColegio { get; set; } //VARCHAR2	74	
            
            [JsonProperty("estadoPersona")]
            public string   estadoPersona { get; set; } //VARCHAR2	40	
            
            [JsonProperty("sexo")]
            public string   sexo { get; set; } //VARCHAR2	4	
            
            [JsonProperty("tipoDocumento")]
            public string   tipoDocumento { get; set; } //VARCHAR2	2	
            
            [JsonProperty("estadoCivil")]
            public string   estadoCivil { get; set; } //VARCHAR2	6	
            
            [JsonProperty("fechaNacimiento")]
            public string fechaNacimiento		{get; set;} //DATE		8	
            
            [JsonProperty("trabajador")]
            public string   trabajador { get; set; } //VARCHAR2	1	
            
            [JsonProperty("anoTitulo")]
            public int      anoTitulo { get; set; } //NUMBER		4	
            
            [JsonProperty("email_institucional")]
            public string   email_institucional { get; set; } //VARCHAR2	60	
            
            [JsonProperty("fecha_ultima_modif")]
            public DateTime  fecha_ultima_modif	{get; set;} //DATE		8	*/
        }
    }
}
