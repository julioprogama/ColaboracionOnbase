using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace api.datecsa.modelo
{
    public class DatosUsuarioSK
    {
        //Campos o Atributos
        #region Campos o Atributos
        private string usuarioOnbase;
        private string passwordUsuario;
        private string urlAppServer;
        private string dataSourceDB;
        #endregion

        //Propiedades
        #region Propiedades
        public string UsuarioOnbase 
        {
            get { return usuarioOnbase; }
            set { usuarioOnbase = value; }
        }

        public string PasswordUsuario 
        {
            get { return passwordUsuario; }
            set { passwordUsuario = value; }
        }

        public string UrlAppServer
        {
            get { return urlAppServer; }
            set { urlAppServer = value; }
        }

        public string DataSourceDB
        {
            get { return dataSourceDB; }
            set { dataSourceDB = value; }
        }
        #endregion

        //Constructores
        #region Constructores

        //Constructor por defecto
        public DatosUsuarioSK() 
        {
            usuarioOnbase = string.Empty;
            passwordUsuario = string.Empty;
            urlAppServer = string.Empty;
            dataSourceDB = string.Empty;
        }

        //Constructor por campos
        public DatosUsuarioSK(string usuarioOnbase, string passwordUsuario, string urlAppServer, string dataSourceDB)
        {
            this.usuarioOnbase = usuarioOnbase;
            this.passwordUsuario = passwordUsuario;
            this.urlAppServer = urlAppServer;
            this.dataSourceDB = dataSourceDB;
        }
        #endregion
    }
}
