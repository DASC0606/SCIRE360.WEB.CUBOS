using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidad.EntityApi
{
    public class LegajoPersonalTipoEA
    {
        public int ITipoLegajo { get; set; }
        public int IIdCatLegajo { get; set; }
        public string VDescripcion { get; set; }
        public bool BEstado { get; set; }
        public bool BUsuario { get; set; }
    }
}
