using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class Periodo_ebl
    {

        public string RUC { get; set; }
        public string ID_PERIODO { get; set; }
        public string DESC_PERIODO { get; set; }
        public DateTime FECH_INI { get; set; }
        public DateTime FECH_FIN { get; set; }
        public string IDEN_SEMANA { get; set; }
        public Int32 IDEN_SEMANA_MES { get; set; }
        public string STAT_PERIODO { get; set; }
        public string ID_MES { get; set; }
        public string ID_PLANILLA { get; set; }
    }
}
