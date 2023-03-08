using System;
using System.Collections.Generic;

using api.datecsa.modelo;
using api.datecsa.UnityAPI;
using Hyland.Unity;
using workview = Hyland.Unity.WorkView;

namespace api.datecsa.unityAPI
{
    /// <summary>
    /// Consultar un autofill
    /// </summary>
    public class Autofill
    {
        private Hyland.Unity.Application application = null;

        public Autofill(Application app)
        {
            if (app == null)
            {
                throw new ArgumentNullException("app", "The Unity application object is null, make sure to connect first.");
            }
            this.application = app;
        }

        /// <summary>
        /// Trae las listas por medio de autofill
        /// </summary>
        /// <param name="application"> Aplicacion onbase </param>
        /// <param name="PalabraClave"> kw por la que se filtar en el auto fill</param>
        /// <param name="primary"> valor que se envia en la kw</param>
        /// <returns></returns>
        public List<string> Getlist(string PalabraClave, string Autofil, string primary) {
            List<string> datos = new List<string>();

            KeywordType KeywordPqrsType = application.Core.KeywordTypes.Find(PalabraClave);
            Keyword kwPrimary = KeywordPqrsType.CreateKeyword(primary);
            List<Keyword> primaryKeywordsHomolog = new List<Keyword>();
            primaryKeywordsHomolog.Add(kwPrimary);
            Keyset keysetHomologacion = application.Core.AutoFillKeywordSets.Find(Autofil);
            KeysetDataList keysetDatasHomolog = keysetHomologacion.GetKeysetData(primaryKeywordsHomolog);

            foreach(KeysetData keysetX in keysetDatasHomolog) {
                datos.Add(keysetX.PrimaryKeyword.ToString());
            }

            return datos;
        }

        /// <summary>
        /// Trae las listas por medio de autofill
        /// </summary>
        /// <param name="PalabraClave">Nombre de alabra clave del auto fill</param>
        /// <param name="Nomautofil">Nombre del auto fill</param>
        /// <returns></returns>
        public List<string> Getlist(string PalabraClave, string Nomautofil)
        {
            List<string> datos = new List<string>();

            KeywordType KeywordPqrsType = application.Core.KeywordTypes.Find(PalabraClave);
            Keyword keyword = KeywordPqrsType.CreateKeyword("1");
            List<Keyword> primaryKeywordsHomolog = new List<Keyword>();
            primaryKeywordsHomolog.Add(keyword);
            Keyset keysetHomologacion = application.Core.AutoFillKeywordSets.Find(Nomautofil);
            KeysetDataList keysetDatasHomolog = keysetHomologacion.GetKeysetData(primaryKeywordsHomolog);

            foreach (KeysetData keysetX in keysetDatasHomolog)
            {
                datos.Add(keysetX.PrimaryKeyword.ToString());
            }

            return datos;
        }

        /// <summary>
        /// Obtiene datos de una lista, basada en una aplicación creada en WV para cargar los datos de una clase
        /// </summary>
        /// <param name="aplicacion">Aplicación de la cual obtendremos la clase</param>
        /// <param name="clase"> Clase de la cual se tomaran los datos</param>
        /// <param name="campo"> Corresponde al atributo que se visualizara en la lista</param>
        /// <returns>Retorna una lista de tipo cadena</returns>
        public List<string> GetlistList(string aplicacion,string clase,string campo)
        {

            List<string> datos = new List<string>();

            if (application == null)
            {
                throw new Exception("Not Connected to OnBase!");
            }

            workview.Application wvApplication = application.WorkView.Applications.Find(aplicacion); //aplicacion
            if (wvApplication == null)
            {
                throw new Exception("Could not find workview application!");
            }

            workview.Class accountsClass = wvApplication.Classes.Find(clase);// clase 
            if (accountsClass == null)
            {
                throw new Exception("Could not find the class for Account in workview");
            }
           
            workview.DynamicFilterQuery dynamicFilterQuery = accountsClass.CreateDynamicFilterQuery();
            workview.FilterQueryResultItemList queryResults = dynamicFilterQuery.Execute(100);

            foreach (workview.FilterQueryResultItem queryResult in queryResults)
            {
                workview.Object resultObject = accountsClass.GetObjectByID(queryResult.ObjectID);
                if (resultObject == null)
                {
                    throw new Exception("Cannot find object in result set.");
                }

                string val = WorkviewHelperMethods.GetAttributeValueAsAlpha(resultObject,campo); // aqui debe ir el campo
                if (!string.IsNullOrEmpty(val))
                {
                    datos.Add(val);
                }
            }
            return datos;
        }

        /// <summary>
        /// Obtiene datos de una lista, basada en una aplicación creada en WV para cargar los datos de una clase
        /// </summary>
        /// <param name="aplicacion">Aplicación de la cual obtendremos la clase</param>
        /// <param name="clase"> Clase de la cual se tomaran los datos</param>
        /// <param name="campo"> Corresponde al atributo que se visualizara en la lista</param>
        /// <returns>Retorna una lista de tipo cadena</returns>
        public List<string> AutofillGet(string aplicacion, string clase, string campo)
        {

            List<string> datos = new List<string>();

            if (application == null)
            {
                throw new Exception("Not Connected to OnBase!");
            }

            workview.Application wvApplication = application.WorkView.Applications.Find(aplicacion); //aplicacion
            if (wvApplication == null)
            {
                throw new Exception("Could not find workview application!");
            }

            workview.Class accountsClass = wvApplication.Classes.Find(clase);// clase 
            if (accountsClass == null)
            {
                throw new Exception("Could not find the class for Account in workview");
            }

            workview.DynamicFilterQuery dynamicFilterQuery = accountsClass.CreateDynamicFilterQuery();
            workview.FilterQueryResultItemList queryResults = dynamicFilterQuery.Execute(100);

            foreach (workview.FilterQueryResultItem queryResult in queryResults)
            {
                workview.Object resultObject = accountsClass.GetObjectByID(queryResult.ObjectID);
                if (resultObject == null)
                {
                    throw new Exception("Cannot find object in result set.");
                }

                string val = WorkviewHelperMethods.GetAttributeValueAsAlpha(resultObject, campo); // aqui debe ir el campo
                if (!string.IsNullOrEmpty(val))
                {
                    datos.Add(val);
                }
            }
            
            return datos;
        }

        /// <summary>
        /// Trae las listas por medio de autofill para retornar datos sin filtro
        /// </summary>
        /// <param name="PalabraClave"></param>
        /// <param name="Lista"></param>
        /// <returns></returns>
        public List<string> AutofillGet(string PalabraClave, string Lista)
        {
            List<string> datos = new List<string>();

            KeywordType KeywordPqrsType = application.Core.KeywordTypes.Find(PalabraClave);
            Keyword TipoSolicitudPqrs = KeywordPqrsType.CreateKeyword("1");
            List<Keyword> primaryKeywordsHomolog = new List<Keyword>();
            primaryKeywordsHomolog.Add(TipoSolicitudPqrs);
            Keyset keysetHomologacion = application.Core.AutoFillKeywordSets.Find(Lista);
            KeysetDataList keysetDatasHomolog = keysetHomologacion.GetKeysetData(primaryKeywordsHomolog);

            foreach (KeysetData keysetX in keysetDatasHomolog)
            {
                datos.Add(keysetX.PrimaryKeyword.ToString());
            }

            return datos;
        }

        /// <summary>
        /// Trae las listas por medio de autofill para una columna
        /// </summary>
        /// <param name="Lista">Nombre del autofill</param>
        /// <param name="PalabraClave">Palabra clave del filtro</param>
        /// <param name="PalabraValor">Valor contenido de la palabra clave</param>
        /// <param name="kwResultado">Palabra clave en la que retorna el resultado</param>
        /// <returns></returns>
        public List<string> AutofillGet(string Lista, string PalabraClave, string PalabraValor, string kwResultado)
        {
            List<string> datos = new List<string>();
            
            ///Definicion de keyword
            KeywordType KeywordType = application.Core.KeywordTypes.Find(PalabraClave);
            Keyword keyword = KeywordType.CreateKeyword(PalabraValor);
            //List<Keyword> primaryKeywordsHomolog = new List<Keyword>();
            //primaryKeywordsHomolog.Add(keyword);

            //Definir Autofil
            Keyset Autofill = application.Core.AutoFillKeywordSets.Find(Lista);

            //Ejecutar autofil
            KeysetDataList keysetDataSet = Autofill.GetKeysetData(keyword);

            //Recorrer resultados del autofill
            foreach (KeysetData keysetX in keysetDataSet)
            {
                //recorremos el rusltado del autofil
                foreach (Keyword keyword1 in keysetX.Keywords)
                {
                    //guardamos el valor de las kw desultantes
                    if (keyword1.KeywordType.Name == kwResultado)
                    {
                        datos.Add(keyword1.Value.ToString());
                    }
                }
            }

            return datos;
        }

    }
}
