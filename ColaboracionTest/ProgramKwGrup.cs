using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Net;
using System.IO;
using System.Globalization;
using api.datecsa.UnityAPI;
using Hyland.Unity;
using Hyland.Unity.UnityForm;

namespace ColaboracionTest
{
    class ProgramKwGrup
    {
        static void Main1(string[] args)
        {

            ///ejemplo de como agregar un valor de un key group que tiene muchos datos y se le pasa un solo valor a otro 
            ///keyword grup que esta en otro ducumento

            Autenticacion autenticacion = new Autenticacion("http://172.19.0.29/AppServer/Service.asmx");
            Hyland.Unity.Application application = autenticacion.autenticacion("JLANDAZURY", "Datecsa12");


            Utilidades utilidades = new Utilidades();
            GetDocument getDocument = new GetDocument(application);

            string Docu = "Adjunto PQRS";
            string Nomkey = "# formulario adjunto";

            string DocRela = "Acto expedido";
            string NomkeyRela = "Acto afectado";

            string DocConfig = "Datos de vigencia normativa - Publicidad del acto";
            string valkey = "122-2021";
            string nombre = "";
            string dato = "";

            //Identificar padre -- se ejecuta en el doc padre
            DocumentList documentlist = getDocument.GetListDocuments(Docu, Nomkey, valkey, 1, application);

            List<Dictionary<string, Keyword>> listaGrupo = new List<Dictionary<string, Keyword>>();

            KeywordRecordType keywordRecordType = application.Core.KeywordRecordTypes.Find(DocConfig);

            foreach (Document documents in documentlist)
            {
                // paso 1 idificar el keyworGrup
                foreach (KeywordRecord keyRecord in documents.KeywordRecords)
                {
                    Dictionary<string, Keyword> dicsionario = new Dictionary<string, Keyword>();

                    nombre = keyRecord.KeywordRecordType.Name;
                    //2 verifico que type grup es 
                    if (nombre == DocConfig)
                    {

                        KeywordList lista = keyRecord.Keywords;
                        //recorrer typegrup para sacar kw
                        for (int i = 0; i < keyRecord.KeywordRecordType.KeywordTypes.Count; i++)
                        {
                            string namef = keyRecord.KeywordRecordType.KeywordTypes[i].Name;
                            Keyword keyword = keyRecord.Keywords[i];
                            dicsionario.Add(namef, keyword);
                        }
                        //Listamos 
                        listaGrupo.Add(dicsionario);
                    }
                }


                foreach (Dictionary<string, Keyword> dickwvl in listaGrupo)
                {
                    ///Asignar valores al objeto de respuesta de salirda
                    DocumentList documentlistMod = getDocument.GetListDocuments(DocRela, Nomkey, dickwvl[NomkeyRela].ToString(), 1, application);
                    foreach (Document documents2 in documentlistMod)
                    {

                        KeywordModifier keyModifier = documents2.CreateKeywordModifier();

                        EditableKeywordRecord editableKeywordRecord = keywordRecordType.CreateEditableKeywordRecord();

                        foreach (var item in dickwvl.Keys)
                        {
                            KeywordType keywordType = keywordRecordType.KeywordTypes.Find(item);
                            editableKeywordRecord.AddKeyword(utilidades.Conversor(dickwvl[item].ToString(), keywordType));
                        }

                        keyModifier.AddKeywordRecord(editableKeywordRecord);
                        keyModifier.ApplyChanges();
                    }
                }
            }

        }
    }
}
