using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidad.Entity
{
    public class Tbl_CompaniaBE
    {
        public string ruc { get; set; }
        public string id_compania { get; set; }
        public string ciiu_descripcion { get; set; }
        public string razn_social { get; set; }
        public string desc_direccion { get; set; }
        public string dpto_ubigeo { get; set; }
        public string dist_ubigeo { get; set; }
        public string prov_ubigeo { get; set; }
        public DateTime? fech_registro { get; set; }
        public Int32 flag_estado { get; set; }
        public string sufijo_compania { get; set; }
        public int NoModify { get; set; }
        public Int32 nCaractNombre { get; set; }
        public Int32 nCaractApellidoP { get; set; }
        public Int32 nCaractApellidoM { get; set; }
        public Int32 chk_mail_corp { get; set; }
        public Int32 chk_chge_passw { get; set; }
        public string img_logo { get; set; }
        public string email_admin { get; set; }
        public string mail_server { get; set; }
        public string mail_server_port { get; set; }
        public string mail_sender { get; set; }
        public string mail_sender_pass { get; set; }
        public Int32 mail_server_ssl { get; set; }
        public string name_sender { get; set; }
        public string ldap_auth { get; set; }
        public string ldap_server { get; set; }
        public string login_page { get; set; }
        public string mail_recipient { get; set; }
    }
}
