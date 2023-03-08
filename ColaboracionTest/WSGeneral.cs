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
    public class WSGeneral
    {
        #region IExternalAutofillKeysetScript
        /// <summary>
        /// Implementation of <see cref="IExternalAutofillKeysetScript.OnExternalAutofillKeysetScriptExecute" />.
        /// <seealso cref="IExternalAutofillKeysetScript" />
        /// </summary>
        /// <param name="app"></param>
        /// <param name="args"></param>
        public List<string> consultadatospersona(string NroIdentificacion)
        {
            List<string> InfoEstudiante = new List<string>();
            try
            {
                string para = "?cedula=" + NroIdentificacion ;
                string url = "http://asone.udea.edu.co/wsGeneral/servicios/consultaPersonaNatural";
                WebClient json = new WebClient();
                json.Headers["OAuth_Token"] = "C0A207DDF3F5BE74A3BD1876A833197ADAC955EC";
                json.Headers["Tipo_Conexion"] = "Desarrollo";
                
                var stringjson = "";
                stringjson = json.DownloadString(url + para);
                

                Rootobject root = JsonConvert.DeserializeObject<Rootobject>(stringjson);
                
                foreach (var i in root.Object)
                {

                    //byte[] utf8Bytesnombre = Encoding.Default.GetBytes(i.nombre.ToString());
                    //byte[] utf8Bytesapellidos = Encoding.Default.GetBytes(i.apellidos.ToString());
                    
                    byte[] utf8Bytesnombre = Encoding.Default.GetBytes(i.nombrePila.ToString());
                    string strnombre = Encoding.UTF8.GetString(utf8Bytesnombre);

                    byte[] utf8Bytesapellido1 = Encoding.Default.GetBytes(i.apellido1.ToString());
                    string strapellido1 = Encoding.UTF8.GetString(utf8Bytesapellido1);

                    byte[] utf8Bytesapellido2 = Encoding.Default.GetBytes(i.apellido2.ToString());
                    string strapellido2 = Encoding.UTF8.GetString(utf8Bytesapellido2);

                    byte[] utf8Bytesdireccion = Encoding.Default.GetBytes(i.direccion.ToString());
                    string strdireccion = Encoding.UTF8.GetString(utf8Bytesdireccion);


                    InfoEstudiante.Add(strnombre);
                    InfoEstudiante.Add(strapellido1);
                    InfoEstudiante.Add(strapellido2);
                    InfoEstudiante.Add(i.telefono.ToString());
                    InfoEstudiante.Add(strdireccion);
                    InfoEstudiante.Add(i.correoElectronico.ToString());
                    Console.WriteLine(i.nombrePila.ToString());
                    Console.ReadLine();
                }
            }
            catch (Exception ex)
            {
                InfoEstudiante.Add("-1");
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
            [JsonProperty("tipoIdentificacion")] 
            public string tipoIdentificacion { get; set; }

            [JsonProperty("identificacion")]
            public string identificacion { get; set; }

            [JsonProperty("nombrePila")]
            public string nombrePila { get; set; }

            [JsonProperty("apellido1")]
            public string apellido1 { get; set; }

            [JsonProperty("apellido2")]
            public string apellido2 { get; set; }

            [JsonProperty("correoElectronico")]
            public string correoElectronico { get; set; }

            [JsonProperty("direccion")]
            public string direccion { get; set; }

            [JsonProperty("telefono")]
            public string telefono { get; set; }

            [JsonProperty("identificacionLdap")]
            public string identificacionLdap { get; set; }

            [JsonProperty("fax")]
            public string fax { get; set; }

            [JsonProperty("continente")]
            public int continente { get; set; }

            [JsonProperty("pais")]
            public int pais { get; set; }

            [JsonProperty("departamento")]
            public int departamento { get; set; }

            [JsonProperty("municipio")]
            public int municipio { get; set; }

            [JsonProperty("temporal")]
            public int temporal { get; set; }

            [JsonProperty("emailPorRol")]
            public string emailPorRol { get; set; }

            [JsonProperty("celular")]
            public string celular { get; set; }

            //[JsonProperty("CODIGO_SAP")]
            //public string CODIGO_SAP { get; set; }

            [JsonProperty("TIPO_IDENTIFICACION")]
            public string TIPO_IDENTIFICACION { get; set; }

            [JsonProperty("IDENTIFICACION")]
            public string IDENTIFICACION { get; set; }

            [JsonProperty("NOMBRE_PILA")]
            public string NOMBRE_PILA { get; set; }

            [JsonProperty("APELLIDO1")]
            public string APELLIDO1 { get; set; }

            [JsonProperty("APELLIDO2")]
            public string APELLIDO2 { get; set; }

            [JsonProperty("CORREO_ELECTRONICO ")]
            public string CORREO_ELECTRONICO { get; set; }

            [JsonProperty("DIRECCION")]
            public string DIRECCION { get; set; }

            [JsonProperty("TELEFONO")]
            public string TELEFONO { get; set; }

            [JsonProperty("IDENTIFICACION_LDAP")]
            public string IDENTIFICACION_LDAP { get; set; }

            [JsonProperty("FAX")]
            public string FAX { get; set; }

            [JsonProperty("CONTINENTE")]
            public string CONTINENTE { get; set; }

            [JsonProperty("PAIS")]
            public string PAIS { get; set; }

            [JsonProperty("DEPARTAMENTO")]
            public string DEPARTAMENTO { get; set; }

            [JsonProperty("MUNICIPIO")]
            public string MUNICIPIO { get; set; }

            [JsonProperty("CELULAR")]
            public string CELULAR { get; set; }

            [JsonProperty("EMAIL_POR_ROL")]
            public string EMAIL_POR_ROL { get; set; }

            [JsonProperty("TEMPORAL")]
            public string TEMPORAL { get; set; }

        }
    }
}
