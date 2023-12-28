using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidad
{
    public partial class Autenticate
    {
        public string ID_USUARIO { get; set; }
        public string NOMB_USUARIO { get; set; }
        public string NOMB_PERSONAL { get; set; }
        public string DNI { get; set; }
        public string ID_ROL { get; set; }
        public string NOMB_ROL { get; set; }
        public string RAZN_SOCIAL { get; set; }
        public string RUC { get; set; }
        public string LOGO { get; set; }
        public string LDAP_AUTH { get; set; }
        public string IDPERSONAL { get; set; }
        public string ReturnURL { get; set; }
        public string txtusername { get; set; }
        public string txtpassword { get; set; }
        public string HASH { get; set; }
        public byte[] SALT { get; set; }
        public bool isRemember { get; set; }

    }
}
