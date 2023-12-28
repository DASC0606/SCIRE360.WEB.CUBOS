using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidad.Entity
{
    public class Tbl_DocsDigital_LegajosBE
    {
        public Int64 id { get; set; }
        public Int32 i_idItem { get; set; }
        public string directory { get; set; }
        public string filename { get; set; }
        public DateTime fechainsert { get; set; }
        public string usuScire3 { get; set; }
        public DateTime? fechamodif { get; set; }
        public string usuScire3Mod { get; set; }
    }
}
