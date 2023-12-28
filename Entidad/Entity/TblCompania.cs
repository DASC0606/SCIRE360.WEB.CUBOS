using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public partial class TblCompania
    {

        public string Ruc { get; set; }
        public string IdCompania { get; set; }
        public string CiiuDescripcion { get; set; }
        public string RaznSocial { get; set; }
        public string DescDireccion { get; set; }
        public string DptoUbigeo { get; set; }
        public string DistUbigeo { get; set; }
        public string ProvUbigeo { get; set; }
        public DateTime? FechRegistro { get; set; }
        public int? FlagEstado { get; set; }
        public string SufijoCompania { get; set; }
        public int? NoModify { get; set; }
        public int? NCaractNombre { get; set; }
        public int? NCaractApellidoP { get; set; }
        public int? NCaractApellidoM { get; set; }
        public int? ChkMailCorp { get; set; }
        public int? ChkChgePassw { get; set; }
        public string ImgLogo { get; set; }
        public string EmailAdmin { get; set; }
        public string MailServer { get; set; }
        public string MailServerPort { get; set; }
        public string MailSender { get; set; }
        public string MailSenderPass { get; set; }
        public int? MailServerSsl { get; set; }
        public string NameSender { get; set; }
        public string LdapAuth { get; set; }
        public string LdapServer { get; set; }
        public string LoginPage { get; set; }
        public string MailRecipient { get; set; }
        public string StripeColor { get; set; }
        public bool? ObservacionBoleta { get; set; }
    }
}
