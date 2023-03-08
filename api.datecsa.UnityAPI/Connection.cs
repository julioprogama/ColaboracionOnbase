using System;
using System.Security.Policy;

using api.datecsa.modelo;
using Hyland.Unity;

namespace api.datecsa.UnityAPI
{
    public class Connection
    {
        private string g_datasource = "OnBase";
        private string g_url { get; set; }// = "http://172.19.0.29/AppServer/Service.asmx";

        public Hyland.Unity.Application Connect(string username, string password)
        {
            try
            {
                OnBaseAuthenticationProperties authprops = Hyland.Unity.Application.CreateOnBaseAuthenticationProperties(g_url, username, password, g_datasource);
                Hyland.Unity.Application app = Hyland.Unity.Application.Connect(authprops);
                authprops.LicenseType = LicenseType.QueryMetering;

                return app;
            }
            catch (InvalidLoginException ex)
            {
                throw new Exception("The credentials entered are invalid.", ex);
            }
            catch (AuthenticationFailedException ex)
            {
                throw new Exception("Authentication failed.", ex);
            }
            catch (MaxConcurrentLicensesException ex)
            {
                throw new Exception("All licenses are currently in use, please try again later.", ex);
            }
            catch (NamedLicenseNotAvailableException ex)
            {
                throw new Exception("Your license is not availble, please insure you are logged out of other OnBase clients.", ex);
            }
            catch (SystemLockedOutException ex)
            {
                throw new Exception("The system is currently locked, please try back later.", ex);
            }
            catch (UnityAPIException ex)
            {
                throw new Exception("There was an unhandled exception with the Unity API.", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("There was an unhandled exception.", ex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="datasource"></param>
        /// <param name="url"></param>
        /// <param name="tipoLicencia">Tipo de licencia Default = 0 Enterprise Query = 1 QueryMetering = 2</param>
        /// <returns></returns>
        public Hyland.Unity.Application Connect(string username, string password, string datasource, string url, int tipoLicencia)
        {
            Hyland.Unity.Application app = null;

            try
            {
                OnBaseAuthenticationProperties authProps = Hyland.Unity.Application.CreateOnBaseAuthenticationProperties(url, username, password, datasource);

                switch (tipoLicencia) {
                    case 0: 
                        authProps.LicenseType = LicenseType.Default;
                        break;
                    case 1:
                        authProps.LicenseType = LicenseType.EnterpriseCoreAPI;
                        break;
                    case 2:
                        authProps.LicenseType = LicenseType.QueryMetering;
                        break;
                    default:
                        authProps.LicenseType = LicenseType.Default;
                        break;

                }


                app = Hyland.Unity.Application.Connect(authProps);          
                return app;
			}

            catch (MaxLicensesException ex)
            {
                throw new Exception("Error: All available licenses have been consumed.", ex);
            }
            catch (InvalidLoginException ex)
			{
              //  app.Diagnostics.Write(ex);
                throw new Exception("The credentials entered are invalid.", ex);
			}
			catch (AuthenticationFailedException ex)
			{
               // app.Diagnostics.Write(ex);
                throw new Exception("Authentication failed.", ex);
			}
			catch (MaxConcurrentLicensesException ex)
            {
                //app.Diagnostics.Write(ex);
                throw new Exception("All licenses are currently in use, please try again later.", ex);
			}
			catch (NamedLicenseNotAvailableException ex)
            {
               // app.Diagnostics.Write(ex);
                throw new Exception("Your license is not availble, please insure you are logged out of other OnBase clients.", ex);
			}
			catch (SystemLockedOutException ex)
			{
                //app.Diagnostics.Write(ex);
                throw new Exception("The system is currently locked, please try back later.", ex);
			}
			catch (UnityAPIException ex)
			{
                //app.Diagnostics.Write(ex);
                throw new Exception("There was an unhandled exception with the Unity API.", ex);
			}
			catch (Exception ex)
			{

                //app.Diagnostics.Write(ex);
                throw new Exception("There was an unhandled exception.", ex);
			}
		}
        public Hyland.Unity.Application Connect(datosUsuario usu, string datasource, string url, int tipoLicencia)
        {
            Hyland.Unity.Application app = null;

            try
            {
                OnBaseAuthenticationProperties authProps = Hyland.Unity.Application.CreateOnBaseAuthenticationProperties(url, usu.usuario, usu.pass, datasource);

                switch (tipoLicencia)
                {
                    case 0:
                        authProps.LicenseType = LicenseType.Default;
                        break;
                    case 1:
                        authProps.LicenseType = LicenseType.EnterpriseCoreAPI;
                        break;
                    case 2:
                        authProps.LicenseType = LicenseType.QueryMetering;
                        break;
                    default:
                        authProps.LicenseType = LicenseType.Default;
                        break;

                }


                app = Hyland.Unity.Application.Connect(authProps);
                return app;
            }

            catch (MaxLicensesException ex)
            {
                throw new Exception("Error: All available licenses have been consumed.", ex);
            }
            catch (InvalidLoginException ex)
            {
                //  app.Diagnostics.Write(ex);
                throw new Exception("The credentials entered are invalid.", ex);
            }
            catch (AuthenticationFailedException ex)
            {
                // app.Diagnostics.Write(ex);
                throw new Exception("Authentication failed.", ex);
            }
            catch (MaxConcurrentLicensesException ex)
            {
                //app.Diagnostics.Write(ex);
                throw new Exception("All licenses are currently in use, please try again later.", ex);
            }
            catch (NamedLicenseNotAvailableException ex)
            {
                // app.Diagnostics.Write(ex);
                throw new Exception("Your license is not availble, please insure you are logged out of other OnBase clients.", ex);
            }
            catch (SystemLockedOutException ex)
            {
                //app.Diagnostics.Write(ex);
                throw new Exception("The system is currently locked, please try back later.", ex);
            }
            catch (UnityAPIException ex)
            {
                //app.Diagnostics.Write(ex);
                throw new Exception("There was an unhandled exception with the Unity API.", ex);
            }
            catch (Exception ex)
            {

                //app.Diagnostics.Write(ex);
                throw new Exception("There was an unhandled exception.", ex);
            }
        }

        public Application ConnectWithSessionID(string SESSION_ID, string APP_SERVER_URL)
        {
            SessionIDAuthenticationProperties authProps = Application.CreateSessionIDAuthenticationProperties(APP_SERVER_URL, SESSION_ID, false);
             

            try
            {
                Hyland.Unity.Application app = Hyland.Unity.Application.Connect(authProps);

                return app;
            }
            
			catch (InvalidLoginException ex)
			{
              //  app.Diagnostics.Write(ex);
                throw new Exception("The credentials entered are invalid.", ex);
    }
			catch (AuthenticationFailedException ex)
			{
               // app.Diagnostics.Write(ex);
                throw new Exception("Authentication failed.", ex);
}
			catch (MaxConcurrentLicensesException ex)
            {
                //app.Diagnostics.Write(ex);
                throw new Exception("All licenses are currently in use, please try again later.", ex);
			}
			catch (NamedLicenseNotAvailableException ex)
            {
               // app.Diagnostics.Write(ex);
                throw new Exception("Your license is not availble, please insure you are logged out of other OnBase clients.", ex);
			}
			catch (SystemLockedOutException ex)
			{
                //app.Diagnostics.Write(ex);
                throw new Exception("The system is currently locked, please try back later.", ex);
			}
			catch (UnityAPIException ex)
			{
                //app.Diagnostics.Write(ex);
                throw new Exception("There was an unhandled exception with the Unity API.", ex);
			}
			catch (Exception ex)
			{

                //app.Diagnostics.Write(ex);
                throw new Exception("There was an unhandled exception.", ex);
			}
        }

        public bool GetSessionID(string username, string password, string datasource, string url, int tipoLicencia)
        {
            Hyland.Unity.Application app = null;

            try
            {
                OnBaseAuthenticationProperties authProps = Hyland.Unity.Application.CreateOnBaseAuthenticationProperties(url, username, password, datasource);
                switch (tipoLicencia)
                {
                    case 0:
                        authProps.LicenseType = LicenseType.Default;
                        break;
                    case 1:
                        authProps.LicenseType = LicenseType.EnterpriseCoreAPI;
                        break;
                    case 2:
                        authProps.LicenseType = LicenseType.QueryMetering;
                        break;
                    default:
                        authProps.LicenseType = LicenseType.Default;
                        break;

                }
                app = Hyland.Unity.Application.Connect(authProps);
                bool connected = app.IsConnected;
                app.Disconnect();
                return connected;
            }
            catch (InvalidLoginException ex)
            {
                //  app.Diagnostics.Write(ex);
                throw new Exception("The credentials entered are invalid.", ex);
            }
            catch (AuthenticationFailedException ex)
            {
                // app.Diagnostics.Write(ex);
                throw new Exception("Authentication failed.", ex);
            }
            catch (MaxConcurrentLicensesException ex)
            {
                //app.Diagnostics.Write(ex);
                throw new Exception("All licenses are currently in use, please try again later.", ex);
            }
            catch (NamedLicenseNotAvailableException ex)
            {
                // app.Diagnostics.Write(ex);
                throw new Exception("Your license is not availble, please insure you are logged out of other OnBase clients.", ex);
            }
            catch (SystemLockedOutException ex)
            {
                //app.Diagnostics.Write(ex);
                throw new Exception("The system is currently locked, please try back later.", ex);
            }
            catch (UnityAPIException ex)
            {
                //app.Diagnostics.Write(ex);
                throw new Exception("There was an unhandled exception with the Unity API.", ex);
            }
            catch (Exception ex)
            {

                //app.Diagnostics.Write(ex);
                throw new Exception("There was an unhandled exception.", ex);
            }
        }

        public void Disconnect(Hyland.Unity.Application app)
		{

            try {

                if (app != null && app.IsConnected)
                {
                    app.Diagnostics.Write("Disconnected");
                    app.Disconnect();
                }
            }
            catch(Exception exc)
            {
                app.Diagnostics.Write(exc);

                throw new Exception("There was an unhandled exception.", exc);

            }
            finally
            {
                app.Dispose();
}
}

        public Connection (string g_url)
        {
            this.g_url = g_url;
        }
        public Connection()
        {
        }


    }

    public class Autenticacion
    {
        private Application _application = null;
        private string g_url { get; set; }
        /// <summary>
        /// Se autentica en onbase
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public Application autenticacion(string txtUsername, string txtPassword)
        {
            if (string.IsNullOrWhiteSpace(txtUsername) || string.IsNullOrWhiteSpace(txtPassword))
            {
                throw new Exception("Please enter both a username and password.");
            }

            Connection connection = new Connection(g_url);
            _application = connection.Connect(txtUsername, txtPassword);
            if (_application == null)
            {
                throw new Exception("There was an error in attempting to sign in. Please contact your administrator.");
            }
            _application.Diagnostics.WriteIf(Diagnostics.DiagnosticsLevel.Verbose, string.Format("Connected to OnBase! Session ID: {0} for user {1}", _application.SessionID, txtUsername));

            return _application;
        }

        public Autenticacion( string g_url) {
            this.g_url = g_url;
        }

    }
}
