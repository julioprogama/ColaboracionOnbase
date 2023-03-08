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
    public class empleado
    {
        public string tipoIdentificacion { get; set; }
        public string numeroIdentificacion { get; set; }
        public string nombres { get; set; }
        public string primerApellido { get; set; }
        public string segundoApellido { get; set; }
        public string nombre { get; set; }
        public string cargo { get; set; }
        public string email { get; set; }
        public string emailInstitucional { get; set; }
        public string telefono { get; set; }
        public string centroCosto { get; set; }
        public string mserror { get; set; }

        public empleado() { }

        public void setempleado(string tipoIden, string numeroIdent, string noms,
                string primerApe, string segundoApe, string nom, string carg,
                string correo, string emailInst, string tel, string centroCos,
                string error) {


            tipoIdentificacion   = tipoIden;
            numeroIdentificacion = numeroIdent ;
            nombres              = noms ;
            primerApellido       = primerApe ;
            segundoApellido      = segundoApe;
            nombre               = nom ;
            cargo                = carg ;
            email                = correo ;
            emailInstitucional   = emailInst ;
            telefono             = tel ;
            centroCosto          = centroCos ;
            mserror              = error;
        }
    }

    public class WsSipe
    {
        empleado empl = new empleado();
        // consultaempleadossipe=http://asone.udea.edu.co/WsSipe/servicios/consultaEmpleados

        #region IExternalAutofillKeysetScript
        /// <summary>
        /// Implementation of <see cref="IExternalAutofillKeysetScript.OnExternalAutofillKeysetScriptExecute" />.
        /// <seealso cref="IExternalAutofillKeysetScript" />
        /// </summary>
        /// <param name="app"></param>
        /// <param name="args"></param>
        public List<string> consultaempleadossipe(string NroIdentificacion)
        {
            List<string> InfoEmpleado = new List<string>();
            try
            {
                string para = "?cedula=" + NroIdentificacion;
                string url = "http://asone.udea.edu.co/WsSipe/servicios/consultaEmpleados";
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
                    //Console.WriteLine(i.Nombre.ToString());

                    //string strcedula = i.numeroIdentificacion.ToString();
                    
                    //byte[] utf8Bytes = Encoding.Default.GetBytes(i.nombres.ToString());
                    //string name= Encoding.UTF8.GetString(utf8Bytes);

                    byte[] utf8Bytesnombre = Encoding.Default.GetBytes(i.nombre.ToString());
                    //string strnombre = Encoding.UTF8.GetString(utf8Bytesnombre);

                    byte[] utf8Bytesprimerapellido = Encoding.Default.GetBytes(i.primerApellido.ToString());
                    //string strprimerapellido = Encoding.UTF8.GetString(utf8Bytesprimerapellido);

                    byte[] utf8Bytessegundoapellido = Encoding.Default.GetBytes(i.segundoApellido.ToString());
                    //string strsegundoapellido = Encoding.UTF8.GetString(utf8Bytessegundoapellido);

                    //string nombrecompleto = strnombre + " " + strprimerapellido + " " + strsegundoapellido;

                    //string empleofuncionario = i.cargo.ToString();
                    
                    //string telefonofuncionario=i.telefono.ToString();
                    
                    //string correofuncionario = i.emailInstitucional.ToString();
                    
                    empl.setempleado(null, i.numeroIdentificacion.ToString(), null, Encoding.UTF8.GetString(utf8Bytesprimerapellido), Encoding.UTF8.GetString(utf8Bytessegundoapellido), Encoding.UTF8.GetString(utf8Bytesnombre), i.cargo.ToString(), null, i.emailInstitucional.ToString(), i.telefono.ToString(),null,null);
                    
                    InfoEmpleado.Add(i.numeroIdentificacion.ToString());
                    InfoEmpleado.Add(Encoding.UTF8.GetString(utf8Bytesnombre));
                    InfoEmpleado.Add(Encoding.UTF8.GetString(utf8Bytesprimerapellido));
                    InfoEmpleado.Add(Encoding.UTF8.GetString(utf8Bytessegundoapellido));
                    InfoEmpleado.Add(i.cargo.ToString());
                    InfoEmpleado.Add(i.emailInstitucional.ToString());
                    InfoEmpleado.Add(i.telefono.ToString());


                }
            }
            catch (Exception ex)
            {
                empl.mserror = ex.ToString();
            }

            return InfoEmpleado;
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

            [JsonProperty("cargo")]
            public string cargo { get; set; }

            [JsonProperty("email")]
            public string email { get; set; }

            [JsonProperty("emailInstitucional")]
            public string emailInstitucional { get; set; }

            [JsonProperty("telefono")]
            public string telefono { get; set; }

            [JsonProperty("centroCosto")]
            public string centroCosto { get; set; }
        }

    }
}
