using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hyland.Unity;
using Hyland.Unity.CodeAnalysis;
using api.datecsa.modelo;
using api.datecsa.UnityAPI;


namespace ColaboracionTest
{
    class ProgramTrazabilidad
    {
		static void Main1(string[] args)
		{
			Autenticacion autenticacion = new Autenticacion("http://172.19.1.9/appserver64/service.asmx");
			string NomDocRadicadoPQRS = "PDF - SG - Radicación de PQRS",
						NomDocRecursoInsis = "PDF - SG - Interponer Recurso de Insistencia",
						NomDocTrazabilidad = "PDF - Soporte Trazabilidad",
						NomDocNotificacion = "PDF - Notificaciones",
						NomDocSoporteElabo = "Soporte elaboración",
						NomDocActo = "SG - Expedir acto",
						NomDocAsesoria = "DJ - Solicitudes a la Dirección Jurídica",
					   	keywordRadicado = "Número de radicado",
						keywordBandera = "Bandera pqrs",
						keywordRelaActo = "Relación de portafolio radica pqrs -  expedir acto",
						keywordRelaAses = "Relación de portafolio radica pqrs - direc juridic";

			Utilidades utilidades = new Utilidades();
			Hyland.Unity.Application app = autenticacion.autenticacion("JLANDAZURY", "Datecsa12");
			GetDocument getDocument = new GetDocument(app);
			List<string> keywordname = new List<string>();
			List<string> keyvalor = new List<string>();
			keywordname.Add(keywordRadicado);
			keyvalor.Add("2021-1949-E");
			DocumentList Documentoactual = getDocument.GetListDocuments(NomDocNotificacion, keywordname, keyvalor, 100);
			List<Document> documento = new List<Document>();
			DocumentList documentolist = null;

			for (int i = Documentoactual.Count - 1; i >= 0; i--)
			{
				Hyland.Unity.Document document = Documentoactual[i];
				documento.Add(document);
				
			}
			
			foreach (Document document1 in documento)
            {

            }

			#region Kw de relacion
			//Se carga los KW'S Type respectivos para su posterior busqueda del valor 
			KeywordType KTRadicado = app.Core.KeywordTypes.Find(keywordRadicado);
			KeywordType KTBandera = app.Core.KeywordTypes.Find(keywordBandera);

			//Recupera el valor de los KW'S correspondiente en el documento		
			//KeywordRecord KRRadicado = documento.KeywordRecords.Find(KTRadicado);
			//KeywordRecord KRBandera = documento.KeywordRecords.Find(KTBandera);

			//Asigna el valor encontrado en el documento
			//Keyword KWRadicado = (KRRadicado != null) ? KRRadicado.Keywords.Find(KTRadicado) : null;
			//Keyword KWBandera = (KRBandera != null) ? KRBandera.Keywords.Find(KTBandera) : null;

			#endregion

			//Hyland.Unity.DocumentList doctype = getDocument.GetListDocuments(NomDocNotificacion, KWRadicado.KeywordType.Name, KWRadicado.AlphaNumericValue, 100, app);

			//IEnumerable<DocumentList> query = from doctyp in doctype
			//								  orderby doctype
			//								  select doctype;

			//DocumentList doc = query;
			Console.WriteLine("hola");
		}
	}
}