using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCIRE360.WEB.CUBOS.API.Entity
{
    public class LegajoPersonalTipoBE
    {
        public Int32 i_TipoLegajo { get; set; }
        public Int32 i_idCatLegajo { get; set; }
        public string v_descripcion { get; set; }
        public bool b_estado { get; set; }
        public bool b_usuario { get; set; }
    }
}
