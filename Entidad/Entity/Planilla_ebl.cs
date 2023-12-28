using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class Planilla_ebl
    {
        public string ID_PLANILLA { get; set; }
        public string DESC_PLANILLA { get; set; }
        public string STAT_PLANILLA { get; set; }
        public string ID_PARENT_PLANILLA { get; set; }
        public string ID_PERIODICIDAD { get; set; }
        public string RUC { get; set; }
    }
}
