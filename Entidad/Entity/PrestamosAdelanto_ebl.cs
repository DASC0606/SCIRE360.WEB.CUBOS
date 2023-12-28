using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidad.Entity
{
    public class PrestamosAdelanto_ebl
    {
        public Int64 IDSOLICITUD { get; set; }
        public string RUC { get; set; }
        public string COMPANIA_ID { get; set; }
        public string RAZON_SOCIAL { get; set; }
        public string PLANILLA_ID { get; set; }
        public string DESC_PLANILLA { get; set; }
        public string ID_USUARIO { get; set; }
        public string PERSONAL_ID { get; set; }
        public string PERSONAL_NOMBRE { get; set; }
        public string OPERACION_ID { get; set; }
        public decimal MOTIVO_ID { get; set; }
        public string MOTIVO { get; set; }
        public decimal NRO_CUOTA { get; set; }
        public decimal MONTO { get; set; }
        public string FECHA_INICIO { get; set; }
        public string FECHA_FIN { get; set; }
        public int ESTADO_SOLICITUD { get; set; }
        public int ESTADO_APROBACION { get; set; }
        public string COMENTARIO { get; set; }
        public string FECHA_REG { get; set; }
        public string FECHA_MODIF { get; set; }
        public string NIVEL_ID { get; set; }
        public string CadenaAprobacion { get; set; }
        public string FlagPase { get; set; }
        public string Dias { get; set; }
        public string Tipo_Suspension_RL_Id { get; set; }
        public string ESTADO_SOL { get; set; }
        public string ESTADO_APROB { get; set; }
        public string TIPO_SOLICITUD { get; set; }
        public string JEFATURA_ID { get; set; }
        public string TRABAJADOR { get; set; }
        public string NIVEL_ID_JEFE { get; set; }
    }

    public class ObtSolicictudesP
    {
        public Int64 IDSOLICITUD { get; set; }
        public string RUC { get; set; }
        public Int64 IDUSUARIO { get; set; }
        public string IDPERSONAL { get; set; }
        public DateTime? FEC_SAL { get; set; }
        public DateTime? FEC_RET { get; set; }
        public decimal TOTDIAS { get; set; }
        public Int32? ESTADO_SOLICITUD { get; set; }
        public Int32? ESTADO_APROBACION { get; set; }
        public string COMENTARIO { get; set; }
        public string TRABAJADOR { get; set; }
        public string DNI { get; set; }
        public string JEFE { get; set; }
        public string IDJEFE { get; set; }
        public string CadenaAprobacion { get; set; }
        public string NIVEL_ID { get; set; }
        public decimal NRO_CUOTA { get; set; }
        public decimal MONTO { get; set; }

        public string MOTIVO { get; set; }
        public string NOMNIVELJEF { get; set; }
        public string NIVELIDJEF { get; set; }
    }
}
