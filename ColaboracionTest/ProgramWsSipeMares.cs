using System;
using Newtonsoft.Json;
using System.Net;
using System.IO;
using System.Text;
using System.Collections.Generic;
using Newtonsoft.Json.Converters;
using System.Globalization;
using System.Linq;
//using UnityAPI;

namespace ColaboracionTest
{
    class Program
    {
        static void Main3(string[] args) {

            ///ProgamRadicado condigo = new ProgamRadicado();

            //condigo.Extrae("");

            WSGeneral general = new WSGeneral();

            general.consultadatospersona("8231710");


            //ProgramWS pb = new ProgramWS();
            //WsMares Mar = new WsMares();

            //.consultaestudiante("8231710");
            //pb.WS();

            /*Pdf ppdf = new Pdf();

            ppdf.Main();
            

            //DateTime fecha;
            string dia, mes, anio, texto, deFecha = " de ";

            string dt = DateTime.Now.ToString("dd/MMMM/yyyy");
            dia = dt.Substring(0, dt.IndexOf("/"));
            mes = dt.Substring(dt.IndexOf("/") + 1, dt.LastIndexOf("/") -3);
            anio = dt.Substring(dt.LastIndexOf("/") + 1);
            texto = dia + deFecha + mes + deFecha + anio;
            
            ///dt = "1/3/2011";
            //DateTime myDateTime = DateTime.Parse(dt);
            //dia = Convert.ToString(myDateTime.Day);
            //mes = Convert.ToString(myDateTime.Month);
            //anio = Convert.ToString(myDateTime.Year);
            Console.WriteLine(dt);
            Console.WriteLine("*****");
            Console.WriteLine(texto);*/
            Console.WriteLine("*****");



        }
    }
}
