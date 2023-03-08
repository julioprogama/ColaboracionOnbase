using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hyland.Applications.Web.Security;
 

namespace api.datecsa.UnityAPI
{
    public class SecurityUtilities
    {
        public static string checksum(string queryString)
        {
             
            string dpChecksum = "dg37vjnm57djnby3";
            string checksumValue = ""; 
            ChecksumCreator checksum = new ChecksumCreator(queryString, dpChecksum);
            checksumValue = checksum.CreateChecksum();
            return checksumValue;

        }
    }
}
