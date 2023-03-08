using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hyland.Unity;
using System.IO;

namespace api.datecsa.unityAPI
{
    public class SecurityKeywordUtilities
    {
        public SecurityKeywordUtilities() { }
        public void agregarSK(UserAdministration userAdmin, UserGroup grupo, Keyword kw)
        {
            try
            {
                userAdmin.AddSecurityKeyword(grupo, kw, Hyland.Unity.SecurityKeywordOperator.Equal);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void eliminarSK(UserAdministration userAdmin, UserGroup grupo, Keyword kw)
        {
            try
            {
                Hyland.Unity.SecurityKeywordList sKList = userAdmin.GetSecurityKeywords(grupo); ;

                foreach (SecurityKeyword sK in sKList)
                {
                    if (sK.Keyword.Equals(kw))
                    {
                        userAdmin.RemoveSecurityKeyword(sK);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
