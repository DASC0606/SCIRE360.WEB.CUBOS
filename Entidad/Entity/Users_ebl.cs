using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidad
{
    public partial class Users_ebl
    {
        public Int32 id_permiso_usuario_recurso { get; set; }
        public Int32 id_permiso_recurso { get; set; }
        public Int32 id_usuario { get; set; }
        public string id_personal { get; set; }
        public string ruc { get; set; }
        public int bit_visible { get; set; }
        public DateTime fech_registra { get; set; }
        public string id_rol_a { get; set; }
        public string planilla_id { get; set; }
        public string num_users { get; set; }
        public bool FLAG_USERS { get; set; }

        public bool DivInfoV { get; set; }
        public string DivInfoCss { get; set; }
        public string DivMessage { get; set; }

    }
}
