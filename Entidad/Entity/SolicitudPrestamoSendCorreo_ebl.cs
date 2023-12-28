using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidad.Entity
{
    public class parametrosSoliditudPrestamo
    {
        public string ruc { get; set; }
        public string compania_id { get; set; }
        public string planilla_id { get; set; }
        public int id_usuario { get; set; }
        public string personal_id { get; set; }
        public string motivo_id { get; set; }
        public string motivo { get; set; }
        public int nro_cuota { get; set; }
        public decimal monto { get; set; }
        public string comentario { get; set; }
        public string nivel_id { get; set; }
        public string cadenaAprobacion { get; set; }
        public string Tipo_Suspension_RL_Id { get; set; }
        public string FechIn { get; set; }
        public string fechFin { get; set; }
        public string idMoneda { get; set; }
        public string moneda { get; set; }
    }

    public partial class SolicitudPrestamoSendCorreo_ebl
    {
        public string RUC { get; set; }
        public string PERSONAL_ID { get; set; }
        public string JEFATURA_ID { get; set; }
        public string Personal { get; set; }
        public string DNI { get; set; }
        public string NombreJefe { get; set; }
        public string CorreoPersonal { get; set; }
        public string CorreoJefe { get; set; }
        public Boolean flg_trabajador { get; set; }
    }
}
