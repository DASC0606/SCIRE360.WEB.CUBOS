using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidad
{
    public class UserInfoVacaSolicitud
    {

        public string IDSOLICITUD { get; set; }
        public string RUC { get; set; }
        public string COMPANIA_ID { get; set; }
        public string PLANILLA_ID { get; set; }
        public string ID_USUARIO { get; set; }
        public string PERSONAL_ID { get; set; }
        public string FECHA_SALIDA { get; set; }
        public string FECHA_RETORNO { get; set; }
        public string ESTADO_SOLICITUD { get; set; }
        public string ESTADO_APROBACION { get; set; }
        public string COMENTARIO { get; set; }
        public string FECHA_REG { get; set; }
        public string FECHA_MODIF { get; set; }
        public string NIVEL_ID { get; set; }
        public string CadenaAprobacion { get; set; }
        public string FlagPase { get; set; }
        public string Dias { get; set; }
        public string ESTADO_SOL { get; set; }
        public string ESTADO_APROB { get; set; }
        public string TOTALDIAS { get; set; }
        public string TRABAJADOR { get; set; }
        public string DNI { get; set; }
        public string JEFE { get; set; }
        public string IDJEFE { get; set; }
        public string NOMNIVELJEF { get; set; }
        public string NIVELIDJEF { get; set; }
        public string TipoSol { get; set; }

    }

    public class JefMasterCompany
    {
        public string value { get; set; }
        public string text { get; set; }
    }

    public class TipoSolicitud
    {
        public string value { get; set; }
        public string text { get; set; }
    }
}
