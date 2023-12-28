using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidad.Entity
{
    public class Tbl_Periodos_DocsDigitalBE
    {
        public Int64 id { get; set; }
        public string ruc { get; set; }
        public string compania_id { get; set; }
        public string planilla_id { get; set; }
        public string periodo_id { get; set; }
        public Boolean flg_migrado { get; set; }
        public Boolean flg_reenvio { get; set; }
        public DateTime? fechainsert { get; set; }
        public string usuScire3 { get; set; }
        public DateTime? fechamodif { get; set; }
        public string usuScire3Mod { get; set; }
    }
}
