using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Hyland.Unity;
using api.datecsa.modelo;
using api.datecsa.UnityAPI;
using api.datecsa.unityAPI;
using System.IO;

using api.datecsa.modelo;
using api.datecsa.UnityAPI;


namespace api.datecsa.controlador
{
    public class controladorAdminSK
    {
        private Hyland.Unity.Application app;


        public controladorAdminSK(string usuario, string password, string urlApsserver, string dataSourceDB)
        {
            Connection cn = new Connection();

            app = cn.Connect(usuario, password, dataSourceDB, urlApsserver, 0);
        }

        public bool Ingresar(bool proceso)
        {
            String line;
            try
            {
                string file = @"C:\Carga SKW\DocumentoDeCarga.csv";
                StreamReader sr = new StreamReader(file);

                //Read the first line of text
                line = sr.ReadLine();

                //Continue to read until you reach end of file
                while (line != null)
                {
                    //write the line to console window
                    string[] words = line.Split(';');

                    string grupoUsurio = words[0];
                    string palabraClave = words[1];
                    string valor = words[2].ToString();

                    UserGroup userGroup = app.Core.UserGroups.Find(grupoUsurio);
                    KeywordType keywordType = app.Core.KeywordTypes.Find(palabraClave);
                    Keyword securityKeyword = keywordType.CreateKeyword(valor);

                    Hyland.Unity.UserAdministration userAdmin = app.Core.UserAdministration;
                    unityAPI.SecurityKeywordUtilities sk = new unityAPI.SecurityKeywordUtilities();

                    if (proceso)
                    {
                        sk.agregarSK(userAdmin, userGroup, securityKeyword);
                    }
                    else
                    {
                        sk.eliminarSK(userAdmin, userGroup, securityKeyword);
                    }

                    //Read the next line
                    line = sr.ReadLine();
                    //app.Disconnect();
                }
                //close the file
                sr.Close();
                app.Disconnect();
            }
            catch (Exception ex)
            {
                app.Diagnostics.Write(ex);
                Console.WriteLine(ex);
                Console.ReadLine();
                app.Disconnect();
            }
        }

        public controladorAdminSK(string url, string datasource, datosUsuario usu)
        {
            Connection cn = new Connection();
            app = cn.Connect(usu, datasource, url,0);

        }

        public bool Ingresar()
        {

            return true;
        }
    }
}
