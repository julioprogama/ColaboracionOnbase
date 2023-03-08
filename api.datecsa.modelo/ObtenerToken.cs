using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace api.datecsa.modelo
{
    public class ObtenerToken
    {

        private string usuario;
        private string contrasena;

        public ObtenerToken(string usuario, string contrasena)
        {
            this.usuario = usuario;
            this.contrasena = contrasena;
        }

        public string Usuario { get => usuario; set => usuario = value; }
        public string Contrasena { get => contrasena; set => contrasena = value; }
    }


    public class ObtenerTokenResponse
    {
        private string _codigoRespuesta;
        private string _descripcionRespuesta;
        private string _token;

        public ObtenerTokenResponse(string codigoRespuesta, string mensajeRespuesta,string token)
        {
            this._codigoRespuesta = codigoRespuesta;
            this._descripcionRespuesta = mensajeRespuesta;
            this._token = token;
        }

        public string codigoRespuesta { get => _codigoRespuesta; set => _codigoRespuesta = value; }
        public string descripcionRespuesta { get => _descripcionRespuesta; set => _descripcionRespuesta = value; }
        public string token { get => _token; set => _token = value; }
    }
}
