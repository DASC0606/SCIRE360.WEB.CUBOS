using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidad
{
    public partial class Cubo_Vacation
    {
        public Int64 ID { get; set; }
        public string VACACIONES_ID { get; set; }
        public string VACACIONES_PAGADAS_ID { get; set; }
        public string PERSONAL_ID { get; set; }
        public string TRABAJADOR { get; set; }
        public string PERIODO_ID { get; set; }
        public string NOMBRE_DE_PERIODO { get; set; }
        public DateTime? F_INICIO_PER_ANUAL { get; set; }
        public DateTime? F_FIN_PER_ANUAL { get; set; }
        public DateTime? F_SALIDA_VACACIONES { get; set; }
        public DateTime? F_RETORNO_VACACIONES { get; set; }
        public Int32? DIAS { get; set; }
        public Int32? DIAS_PAGADOS { get; set; }
        public Int32? DIAS_SALDO { get; set; }
        public Int32? DIAS_SALIDA { get; set; }
        public Int32? DIAS_SALIDA_SALDO { get; set; }
        public Decimal? DIAS_GANADOS { get; set; }
        public Decimal? DIAS_GANADOS_SALDO { get; set; }
        public Int32? DIAS_TRUNCOS { get; set; }
        public Decimal? VALOR { get; set; }

    }
}
