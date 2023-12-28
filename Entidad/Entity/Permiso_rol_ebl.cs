using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidad
{
    public partial class Permiso_rol_ebl
    {
        public Int32 id_permiso_rol { get; set; }
        public Int32 id_permiso_recurso { get; set; }
        public Int32 id_rol { get; set; }
        public bool estado_id { get; set; }
        public DateTime fecha_reg { get; set; }
        public Int32 id_usuario_r { get; set; }
        public DateTime fecha_mod { get; set; }
        public Int32 id_usuario_m { get; set; }
    }
}
