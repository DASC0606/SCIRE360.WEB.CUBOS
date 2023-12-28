using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidad
{
    public partial class Rol_ebl
    {

        public Int32 id_rol { get; set; }
        public string nomb_rol { get; set; }
        public Int32 stat_rol { get; set; }
        public DateTime? DATEADD { get; set; }
        public DateTime? DATEMOD { get; set; }
    }
}
