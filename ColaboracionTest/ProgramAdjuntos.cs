using api.datecsa.UnityAPI;

using Hyland;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColaboracionTest
{
    class ProgramAdjuntos
    {
        static void Main12(string[] args)
        {
            Autenticacion autenticacion = new Autenticacion("");

            Utilidades utilidades = new Utilidades();
            Hyland.Unity.Application application = autenticacion.autenticacion("JLANDAZURY", "Datecsa12");

            GetDocument getDocument = new GetDocument(application);
        }

    }
}
