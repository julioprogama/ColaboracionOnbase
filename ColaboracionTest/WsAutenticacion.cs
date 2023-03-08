using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using Hyland.Security;
using System.Net.Http.Headers;
using System.Globalization;


namespace ColaboracionTest
{
	public class WsAutenticacion
	{


        private string username = "yonatan.duran";
        private string password = "$%&thenigga*";
        //private string username = "EDWIN.GAVIRIA";
        //private string password = "Edwingz224%2A";
        private string cedula = "";
		public void autentica()
{
			Parametros Param = new Parametros();
			Param.setParametros(username, password);

			string url = "https://ayudame2.udea.edu.co/php_ws/validarUsuarioPortal";

			Token token = new Token();

			var client = new HttpClient();
//			var tipe = new HttpContent();
			// Headers
			client.DefaultRequestHeaders.Clear();
			client.DefaultRequestHeaders.Add("OAuth_Token", token.OAuth_Token);
			client.DefaultRequestHeaders.Add("Tipo_Conexion", token.Tipo_Conexion);
			client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue ( "application/x-www-form-urlencoded"));
			//client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

			//Data
			Dictionary<string, string> dic = new Dictionary<string, string>();
			dic.Add("usuario", username);
			dic.Add("clave", password);
			
			var result = client.PostAsync(url, new FormUrlEncodedContent( dic)).Result;
			var stringjson = result.Content.ReadAsStringAsync().Result;

			try
            {
				RootDatos root = JsonConvert.DeserializeObject<RootDatos>(stringjson);
				if (root.tipo_usuario == "oid")
				{
					this.cedula = "Credenciales correctas. ";
				}
            }catch			{
				this.cedula = stringjson;
			}
		}

		public void autentica1()
		{
			//appp.Diagnostics.Write("Inicio proceso");
			//WebService uri = new WebService();
			string url = "https://ayudame2.udea.edu.co/php_ws/validarUsuarioPortal"; //uri.getUrl("validarUsuarioPortal");//("validarusuariooidxcn");
			//appp.Diagnostics.Write("url = " + url);
			Token token = new Token();

			////			if(password.IndexOf('&') > 0)
			//string paswo = Uri.EscapeUriString(password);
			//string passwo = Uri.EscapeDataString(password);
			//string pasw = System.Net.WebUtility.HtmlEncode (password);
			//string passw = System.Net.WebUtility.UrlEncode(password);
			//string pas = System.Web.HttpUtility.HtmlEncode(password);
			//string pass = System.Web.HttpUtility.UrlEncodeUnicode(password);
			// password = pass;

			string para = "?usuario="+ username+"&clave=" + password;
			WebClient json = new WebClient();

			json.Headers["OAuth_Token"] = token.OAuth_Token;
			json.Headers["Tipo_Conexion"] = token.Tipo_Conexion;

//			appp.Diagnostics.Write("url = " + url + para);

			try
			{
				var stringjson = "";
				stringjson = json.DownloadString(url + para);
				Console.WriteLine(stringjson);
				Console.ReadLine();
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				Console.ReadLine();
				//appp.Diagnostics.Write("Exception: " + e);
			}
			//			RootDatos root = JsonConvert.DeserializeObject<RootDatos>(stringjson);
			//			appp.Diagnostics.Write(root);

		}

	}

	public class Token
	{
		//["OAuth_Token"]
		public string OAuth_Token { get; set; }
		//["Tipo_Conexion"]
		public string Tipo_Conexion { get; set; }
		public Token()
		{
			this.OAuth_Token = "C0A207DDF3F5BE74A3BD1876A833197ADAC955EC";
			this.Tipo_Conexion = "Desarrollo";
		}
	}

	public class Parametros
	{
		[JsonProperty("usuario")]
		public string usuario { get; set; }
		public string clave { get; set; }
		public Parametros() { }
		public void setParametros(string usuario, string clave)
		{
			this.usuario = usuario;
			this.clave = clave;
		}
	}

	public class RootDatos 
	{
		public string cedula { get; set; }
		public string tipo_usuario { get; set; }
		public RootDatos() { }
	}

}
