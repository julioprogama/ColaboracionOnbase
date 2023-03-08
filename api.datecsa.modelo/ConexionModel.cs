using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace api.datecsa.modelo
{
    public class ConexionModel
    {

        public string UserName { get ; set ; }
        public string Password { get ; set ; }

        public ConexionModel(string user, string pass) {
            this.UserName = user;
            this.Password = pass;
        }

    }
}
