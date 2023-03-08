using System.Text;
using System.Security.Cryptography;
using Hyland.Unity;
using api.datecsa.modelo;
using System;
using System.Collections.Generic;
using workview = Hyland.Unity.WorkView;
using System.IO;
using System.Data.Odbc;
using System.Data;

namespace api.datecsa.UnityAPI
{
    

    public class Utilidades
    {
        public static void Main()
        {
            using (StreamWriter w = File.AppendText("c:/log.txt"))
            {
                Log("Test1", w);
                Log("Test2", w);
            }

            using (StreamReader r = File.OpenText("/log.txt"))
            {
                DumpLog(r);
            }
        }


		/// <summary>
		/// 
		/// </summary>
		/// <param name="Opcion"></param>
		/// <returns></returns>
		public string Asunto(string Opcion, string radicado)
		{
			string result = "";
			switch (Opcion)
			{
				case "ACLARACION":
					result = @"Solicitud de aclaración PQRS con radicado " + radicado.ToUpper();
					break;
				case "TRASLADO":
					result = @"Devolución del caso con radicado " + radicado.ToUpper();
					break;
				case "AMPLIACION":
					result = @"Ampliación de términos radicado " + radicado.ToUpper();
					break;
				case "RESPUESTA":
					result = @"Respuesta cierre (total/parcial)";
					break;
				case "VENCIMIENTO":
					result = @"Alerta de Vencimiento  %U(K00523.1) %U(%K00554.1) %U(K00555.1) y %U(K00144.1)";
					break;
			}
			return result;
		}

		/// <summary>
		/// Notificaciones envidas por comunicaciones
		/// </summary>
		/// <param name="Opcion">Tipo de notificacion</param>
		/// <returns></returns>
		public string Notificacion(string Opcion)
		{
			string result = "";

			switch (Opcion)
			{
				case "GENERAL":
					result = @"	{0}
								Usted ha recibido una comunicación de la Universidad de Antioquia con número No. {1}.
								
								La comunicación la encontrará adjunta en el presente mensaje.
								Cordialmente, 
								Gestión de Correspondencia y Mensajería
								División de Servicios Logísticos ";
					break;
				case "RESPUESTA":
					result = @"	{0} 
								Usted ha recibido una comunicación de la Universidad de Antioquia con número No. {1}.
								
								Link al formulario de Interponer Recurso 
								Link al formulario de Encuesta de Percepción 
								
								La comunicación la encontrará adjunta en el presente mensaje.
								Cordialmente,
								Gestión de Correspondencia y Mensajerí
								División de Servicios Logísticos";
					break;
				case "":
					result = @"";
					break;
				case "VENCIMIENTO":
					result = @"Gestor (a)
							  %U(K00475.1)
							  %U(K00473.1)
							  
							  Cordial saludo.
							  
							  Frente a la PQRS con radicado %U(K00144.1) presentada por %U(K00523.1) %U(K00554.1) %U(K00555.1), el servidor público competente emitió una respuesta que debe ser notificada al usuario, conforme lo establece el Manual de Políticas y Procedimientos de Atención a la Ciudadanía de la Universidad de Antioquia.
							  
							  Atentamente,
							  
							  Secretaría General
							  (+57)(4) 219 5028
							  Correo: atencionciudadano@udea.edu.co";
					break;
			}

			return result;
		}

		public void AddKw(Hyland.Unity.Application app, Document doc, string kwtype, string kwvalue)
		{
			DocumentLock lockInfo = null;
			try
			{

				lockInfo = doc.LockDocument();
				if (lockInfo.Status == DocumentLockStatus.LockObtained)
				{
					KeywordModifier keywordMod = doc.CreateKeywordModifier();

					KeywordType keywordType = app.Core.KeywordTypes.Find(kwtype);
					if (keywordType == null)
					{
						app.Diagnostics.Write(String.Format("Keyword Type '{0}' not found", keywordType));
					}

					Keyword keyword = null;
					switch (keywordType.DataType)
					{
						case KeywordDataType.AlphaNumeric:
							keyword = keywordType.CreateKeyword(kwvalue);
							break;
						case KeywordDataType.Currency:
							keyword = keywordType.CreateKeyword(Convert.ToDouble(kwvalue));
							break;
						case KeywordDataType.Date:
							keyword = keywordType.CreateKeyword(Convert.ToDateTime(kwvalue));
							break;
						case KeywordDataType.DateTime:
							keyword = keywordType.CreateKeyword(Convert.ToDateTime(kwvalue));
							break;
						case KeywordDataType.FloatingPoint:
							keyword = keywordType.CreateKeyword(Convert.ToDecimal(kwvalue));
							break;
						case KeywordDataType.Numeric20:
							keyword = keywordType.CreateKeyword(Convert.ToInt64(kwvalue));
							break;
						case KeywordDataType.Numeric9:
							keyword = keywordType.CreateKeyword(Convert.ToInt32(kwvalue));
							break;
						case KeywordDataType.SpecificCurrency:
							keyword = keywordType.CreateKeyword(Convert.ToDecimal(kwvalue));
							break;
						case KeywordDataType.Undefined:
							keyword = keywordType.CreateKeyword(kwvalue);
							break;
					}
					keywordMod.AddKeyword(keyword);
					keywordMod.ApplyChanges();
				}
				else
				{
					app.Diagnostics.Write("Document already locked by: " + lockInfo.UserHoldingLock.Name);
				}

			}
			catch (UnityAPIException ObjUnityException)
			{
				app.Diagnostics.WriteIf(Hyland.Unity.Diagnostics.DiagnosticsLevel.Error, ObjUnityException);
				throw ObjUnityException;
			}
			catch (Exception ObjException)
			{
				app.Diagnostics.WriteIf(Hyland.Unity.Diagnostics.DiagnosticsLevel.Error, ObjException);
				throw ObjException;
			}
			finally
			{
				// Use the finally block to make sure and release the DocumentLock if it exists
				if (lockInfo != null)
				{
					lockInfo.Release();
				}
			}
		}

		public string ExecuteSPConsecutivos(Hyland.Unity.Application app, string constring)
		{
			OdbcDataReader datareader = null;

			string retorno = string.Empty;

			app.Diagnostics.Write("Envio Correspondencia: Inicio de SP");
			try
			{
				using (OdbcConnection conn = new OdbcConnection(constring))
				{
					//Assign I/O Parameter
					using (OdbcCommand command = conn.CreateCommand())
					//using(OdbcCommand command = new OdbcCommand("pa_consecutivo", conn))
					{

						command.CommandType = System.Data.CommandType.StoredProcedure;

						command.CommandText = "Call pa_consecutivo(?,?)";

						// OdbcParameter  parameter ;//= command.Parameters.Add( "@Tipo", OdbcType.VarChar, 25);
						//  parameter.Value = tipo;
						// parameter.Direction = ParameterDirection.Input;
						OdbcParameter parameter = command.Parameters.Add("@ANIO", OdbcType.Int);
						parameter.Value = DateTime.Now.Year;
						parameter.Direction = ParameterDirection.Input;
						OdbcParameter parameter1 = command.Parameters.Add("@AUXI", OdbcType.Int);
						parameter1.Direction = ParameterDirection.Output;
						conn.Open();
						app.Diagnostics.Write("Envio Correspondencia Conexión Abierta");

						command.ExecuteNonQuery();

						// 5. Consume the out parameters.
						retorno = parameter1.Value.ToString();

						conn.Close();

						app.Diagnostics.Write("Envio Correspondencia : Conexión Cerrada");
					}
				}
			}
			catch (UnityAPIException UE)
			{
				app.Diagnostics.Write("A Unity Error occured while executing the SP ");
				app.Diagnostics.Write(UE.Message);
				app.Diagnostics.Write(UE.StackTrace);
			}
			catch (Exception Ex)
			{
				app.Diagnostics.Write("A General Exception occured while executing the SP");
				app.Diagnostics.Write(Ex.Message);
				app.Diagnostics.Write(Ex.StackTrace);
			}
			return retorno;
		}

		public string ExecuteSPConsecutivosTipos(Hyland.Unity.Application app, string constring, int type)
		{
			OdbcDataReader datareader = null;

			string retorno = string.Empty;

			app.Diagnostics.Write("Envio Correspondencia: Inicio de SP");
			try
			{
				using (OdbcConnection conn = new OdbcConnection(constring))
				{
					//Assign I/O Parameter
					using (OdbcCommand command = conn.CreateCommand())
					{

						command.CommandType = System.Data.CommandType.StoredProcedure;

						command.CommandText = "Call PA_CONSECUTIVO_TIPOS(?,?,?)";

						OdbcParameter parameter = command.Parameters.Add("@ANIO", OdbcType.Int);
						parameter.Value = DateTime.Now.Year;
						OdbcParameter parameter1 = command.Parameters.Add("@TIPO", OdbcType.Int);
						parameter1.Value = type;
						parameter.Direction = ParameterDirection.Input;
						OdbcParameter parameter2 = command.Parameters.Add("@AUXI", OdbcType.Int);
						parameter2.Direction = ParameterDirection.Output;
						conn.Open();
						app.Diagnostics.Write("Envio Correspondencia Conexión Abierta");

						command.ExecuteNonQuery();

						// 5. Consume the out parameters.
						retorno = parameter2.Value.ToString();

						conn.Close();

						app.Diagnostics.Write("Envio Correspondencia : Conexión Cerrada");
					}
				}
			}
			catch (UnityAPIException UE)
			{
				app.Diagnostics.Write("A Unity Error occured while executing the SP ");
				app.Diagnostics.Write(UE.Message);
				app.Diagnostics.Write(UE.StackTrace);
			}
			catch (Exception Ex)
			{
				app.Diagnostics.Write("A General Exception occured while executing the SP");
				app.Diagnostics.Write(Ex.Message);
				app.Diagnostics.Write(Ex.StackTrace);
			}
			return retorno;
		}

		public string ExecuteSPMensajes(Hyland.Unity.Application app, string constring,
			string parmNumRadi, string parmRadiComu,
			int parmfrmRadicadp, int parmfrmRecursod, int parmfrmComunica,
			string parmDe, string parmPara, string parmMensaje, string parmMensaje2,
			string parmDescrip, string parmDescrip2
			)
		{
			OdbcDataReader datareader = null;

			string retorno = string.Empty;

			app.Diagnostics.Write("Envio Correspondencia: Inicio de SP");
			try
			{
				using (OdbcConnection conn = new OdbcConnection(constring))
				{
					//Assign I/O Parameter
					using (OdbcCommand command = conn.CreateCommand())
					{

						command.CommandType = System.Data.CommandType.StoredProcedure;

						command.CommandText = "Call PA_MENSAJE_TRAZA(?,?,?,?,?,?,?,?,?,?,?)";

						OdbcParameter parameter1 = command.Parameters.Add("@numRadicado", OdbcType.NVarChar);
						parameter1.Value = parmNumRadi;

						OdbcParameter parameter2 = command.Parameters.Add("@RadicadoCom", OdbcType.NVarChar);
						parameter2.Value = parmRadiComu;

						OdbcParameter parameter3 = command.Parameters.Add("@frmRadicadp", OdbcType.Int);
						parameter3.Value = parmfrmRadicadp;

						OdbcParameter parameter4 = command.Parameters.Add("@frmRecursod", OdbcType.Int);
						parameter4.Value = parmfrmRecursod;

						OdbcParameter parameter5 = command.Parameters.Add("@frmComunica", OdbcType.Int);
						parameter5.Value = parmfrmComunica;

						OdbcParameter parameter6 = command.Parameters.Add("@De", OdbcType.NVarChar);
						parameter6.Value = parmDe;

						OdbcParameter parameter7 = command.Parameters.Add("@Para", OdbcType.NVarChar);
						parameter7.Value = parmPara;

						OdbcParameter parameter8 = command.Parameters.Add("@Mensaje", OdbcType.NVarChar);
						parameter8.Value = parmMensaje;

						OdbcParameter parameter9 = command.Parameters.Add("@Mensaje2", OdbcType.NVarChar);
						parameter9.Value = parmMensaje2;

						OdbcParameter parameter10 = command.Parameters.Add("@Descrip", OdbcType.NVarChar);
						parameter10.Value = parmDescrip;

						OdbcParameter parameter11 = command.Parameters.Add("@Descrip2", OdbcType.NVarChar);
						parameter11.Value = parmDescrip2;

						//parameter1.Direction = ParameterDirection.Input;

						conn.Open();
						app.Diagnostics.Write("Envio Correspondencia Conexión Abierta");

						command.ExecuteNonQuery();

						// 5. Consume the out parameters.
						retorno = "1";

						conn.Close();

						app.Diagnostics.Write("Envio Correspondencia : Conexión Cerrada");
					}
				}
			}
			catch (UnityAPIException UE)
			{
				app.Diagnostics.Write("A Unity Error occured while executing the SP ");
				app.Diagnostics.Write(UE.Message);
				app.Diagnostics.Write(UE.StackTrace);
			}
			catch (Exception Ex)
			{
				app.Diagnostics.Write("A General Exception occured while executing the SP");
				app.Diagnostics.Write(Ex.Message);
				app.Diagnostics.Write(Ex.StackTrace);
			}
			return retorno;
		}


		public static void Log(string logMessage, TextWriter w)
        {
            w.WriteLine("{0} {1} {2}", DateTime.Now.ToShortDateString(),
                DateTime.Now.ToShortTimeString(), logMessage);        
        }

        public static void DumpLog(StreamReader r)
        {
            string line;
            while ((line = r.ReadLine()) != null)
            {
                Console.WriteLine(line);
            }
        }

        public static Application conectarSession(string session_id, string url, Connection connection) {
            Application app = connection.ConnectWithSessionID(session_id, url);
            return app;

        }

        /// <summary>
        /// Conecta ha onbase
        /// </summary>
        /// <param name="username"></param>
        /// <param name="url"></param>
        /// <param name="password"></param>
        /// <param name="datasource"></param>
        /// <param name="connection"></param>
        /// <returns></returns>
        public static Application conectar(string username, string url, string password, string datasource, Connection connection) {
            try {

                Application app = connection.Connect(username, password, datasource, url, 0);
                return app;
            }
            catch(Exception exc) {

                throw exc;

            }

        }

        /// <summary>
        /// Encripta
        /// </summary>
        /// <param name="plainText"></param>
        /// <param name="pubKeyPath"></param>
        /// <returns></returns>
        public static string EncryptPassword(string plainText, string pubKeyPath) {
            //converting the public key into a string representation
            string pubKeyString;
            {
                using(StreamReader reader = new StreamReader(pubKeyPath)) { pubKeyString = reader.ReadToEnd(); }
            }
            //get a stream from the string
            var sr = new StringReader(pubKeyString);

            //we need a deserializer
            var xs = new System.Xml.Serialization.XmlSerializer(typeof(RSAParameters));

            //get the object back from the stream
            RSACryptoServiceProvider csp = new RSACryptoServiceProvider();
            csp.ImportParameters((RSAParameters)xs.Deserialize(sr));
            byte[] bytesPlainTextData = Encoding.Default.GetBytes(plainText);

            //apply pkcs#1.5 padding and encrypt our data 
            var bytesCipherText = csp.Encrypt(bytesPlainTextData, false);
            //we might want a string representation of our cypher text... base64 will do
            string encryptedText = Convert.ToBase64String(bytesCipherText);
            return encryptedText;
        }

        /// <summary>
        /// Des encripta
        /// </summary>
        /// <param name="encryptedText"></param>
        /// <param name="privateKey"></param>
        /// <returns></returns>
        public string DecryptPassword(string encryptedText, string privateKey) {
            //we want to decrypt, therefore we need a csp and load our private key
            RSACryptoServiceProvider csp = new RSACryptoServiceProvider();

            string privKeyString;
            {
                privKeyString = File.ReadAllText(privateKey);
                //get a stream from the string
                var sr = new StringReader(privKeyString);
                //we need a deserializer
                var xs = new System.Xml.Serialization.XmlSerializer(typeof(RSAParameters));
                //get the object back from the stream
                RSAParameters privKey = (RSAParameters)xs.Deserialize(sr);
                csp.ImportParameters(privKey);
            }


            byte[] bytesCipherText = Convert.FromBase64String(encryptedText);

            //decrypt and strip pkcs#1.5 padding
            byte[] bytesPlainTextData = csp.Decrypt(bytesCipherText, false);

            //get our original plainText back...
            // File.WriteAllBytes(filePath, bytesPlainTextData);

            string str = Encoding.Default.GetString(bytesPlainTextData);
            Console.WriteLine("The String is: " + str);
            return str;
        }

        /// <summary>
        /// Asignacion de valor a keywork
        /// </summary>
        /// <param name="namekw">Nombre de la keywork</param>
        /// <param name="value">Valor que se le asigna a la keywork</param>
        /// <param name="app"></param>
        /// <returns>Retorna una keywork</returns>
        public static Keyword Typekeyword (string namekw, string value, Application app)
        {
            KeywordType keyType = null;
            Keyword key = null;
            string dataType = null;

            DateTime date = new DateTime();
            
            keyType = app.Core.KeywordTypes.Find(namekw);
            dataType = keyType.DataType.ToString();

            switch (dataType.ToUpper())
            {
                case "DATE":
                    DateTime.TryParse(value, out date);
                    key = keyType.CreateKeyword(date);
                    break;

                case "DATETIME":
                    DateTime.TryParse(value, out date);
                    key = keyType.CreateKeyword(date);
                    break;

                case "ALPHANUMERIC":
                    key = keyType.CreateKeyword(value);
                    break;

                case "INTEGER":
                    int valor;
                    int.TryParse(value, out valor);
                    key = keyType.CreateKeyword(value);
                    break;
            }

            return key;

        }

        /// <summary>
        /// Convierte un valor cadena y se lo asigna a la keywordType que se le indique
        /// </summary>
        /// <param name="valor"></param>
        /// <param name="keywordDataType"></param>
        /// <returns>Retorna un valor de tipo keyword con su asignacion correspondiente</returns>
        public Keyword Conversor(string valor, KeywordType keywordDataType)
        {
            Keyword keyValue = null;

            switch (keywordDataType.DataType)
            {
                case KeywordDataType.AlphaNumeric:
                    keyValue = keywordDataType.CreateKeyword(valor);
                    break;
                case KeywordDataType.Currency:
                case KeywordDataType.SpecificCurrency:
                    keyValue = keywordDataType.CreateKeyword(Convert.ToDecimal(valor));
                    break;
                case KeywordDataType.Date:
                    keyValue = keywordDataType.CreateKeyword(Convert.ToDateTime(valor));
                    break;
                case KeywordDataType.DateTime:
                    keyValue = keywordDataType.CreateKeyword(Convert.ToDateTime(valor));
                    break;
                case KeywordDataType.FloatingPoint:
                    keyValue = keywordDataType.CreateKeyword(Convert.ToDouble(valor));
                    break;
                case KeywordDataType.Numeric20:
                    keyValue = keywordDataType.CreateKeyword(Convert.ToDecimal(valor));
                    break;
                case KeywordDataType.Numeric9:
                    keyValue = keywordDataType.CreateKeyword(Convert.ToInt64(valor));
                    break;
            }

            return keyValue;
        }


    }

    public class WorkviewHelperMethods
    {
        /// <summary>
        /// Retrieves any attribute as a string
        /// </summary>
        /// <param name="obj">Workview object</param>
        /// <param name="attrName">The name of the attribute to find</param>
        /// <returns>blank string if no value. If value it returns the attribute value as a string.</returns>
        public static string GetAttributeValueAsAlpha(workview.Object obj, string attrName) {
            string value = string.Empty;
            workview.AttributeValue wvAttribute = obj.AttributeValues.Find(attrName);
            if(wvAttribute != null) {
                if(wvAttribute.HasValue) {
                    value = wvAttribute.Value.ToString();
                }
            }

            return value;
        }
    }
}
