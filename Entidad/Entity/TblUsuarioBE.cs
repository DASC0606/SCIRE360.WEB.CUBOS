using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidad.Entity
{
    public class TblUsuarioBE
    {
        public int IdUsuario { get; set; }
        public string NombUsuario { get; set; }
        public string PassUsua { get; set; }
        public int? StatUsua { get; set; }
        public int? IdRol { get; set; }
        public DateTime? FechRegUsuario { get; set; }
        public DateTime? FechUpdUsuario { get; set; }
        public string OrigRegistro { get; set; }
        public string MailUsuario { get; set; }
        public int? FlagUsuarioPrueba { get; set; }
        public DateTime? FechIni { get; set; }
        public DateTime? FechFin { get; set; }
        public string ImgnFotoUsua { get; set; }
        public string IdenTokenUsua { get; set; }
        public DateTime? FechUltimaSession { get; set; }
        public int? IsAdmin { get; set; }
        public string TipoReg { get; set; }
        public int? PassPrim { get; set; }
        public int? SendEmail { get; set; }
        public string IdHash { get; set; }
    }
}
