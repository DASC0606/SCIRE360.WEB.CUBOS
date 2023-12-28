using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidad.Entity
{
    public class MotivoPrestamo_ebl
    {
        public string Ruc { get; set; }
        public string Motivo_CtaCte_Id { get; set; }
        public string Descripcion { get; set; }
        public decimal? Limite { get; set; }
    }
}
