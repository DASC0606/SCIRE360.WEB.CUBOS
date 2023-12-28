using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidad.EntityApi
{
    public class TblPeriodosDocsDigitalEA
    {
        public long Id { get; set; }
        public string Ruc { get; set; }
        public string CompaniaId { get; set; }
        public string PlanillaId { get; set; }
        public string PeriodoId { get; set; }
        public bool FlgMigrado { get; set; }
        public bool FlgReenvio { get; set; }
        public DateTime Fechainsert { get; set; }
        public string UsuScire3 { get; set; }
        public DateTime? Fechamodif { get; set; }
        public string UsuScire3Mod { get; set; }
    }
}
