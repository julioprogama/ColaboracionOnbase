using api.datecsa.UnityAPI;
using Hyland.Unity;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

using static ColaboracionTest.WSGse;
using ColaboracionTest;

namespace ColaboracionTest
{
    class ProgramaEjemplo
    {
        static void Main(string[] args)
        {
            WsAutenticacion wsAutenticacion = new WsAutenticacion();

            wsAutenticacion.autentica1();
            //WSGeneral persona = new WSGeneral();

            //LecturaPdf  lecturaPdf = new LecturaPdf();

            //lecturaPdf.Main();

            //WSGse gse = new WSGse();

            //ObjetDocumento d = gse.LoginGse();

            //persona.consultadatospersona("71769825");

            //string url = "https://link.udea.edu.co/listadows&serviceid=" + "consultapersonanatural";
            //WebClient json = new WebClient();
            //var stringjson = json.DownloadString(url);
            //Console.WriteLine("URL Servicio: " + stringjson);


            /////ejemplo de como agregar un valor de un key group que tiene muchos datos y se le pasa un solo valor a otro 
            /////keyword grup que esta en otro ducumento
            //Autenticacion autenticacion = new Autenticacion("http://172.19.0.29/AppServer/Service.asmx");

            //Utilidades utilidades = new Utilidades();

            //Hyland.Unity.Application application = autenticacion.autenticacion("JLANDAZURY", "Datecsa12");

            //GetDocument getDocument = new GetDocument(application);


            //string Docu = "asbas";

            //int fecha;
            //if (int.TryParse(Docu, out fecha))
            //{
            //    Docu = "hola mundo";
            //}

            //    Type t = Docu.GetType();
            //if (t.Equals(typeof(int))) { }

            //    string Nomkey = "# formulario adjunto";

            ////Identificar padre -- se ejecuta en el doc padre
            ////DocumentList documentlist = getDocument.GetListDocuments(Docu, Nomkey, valkey, 1, application);
        }
    }
}
