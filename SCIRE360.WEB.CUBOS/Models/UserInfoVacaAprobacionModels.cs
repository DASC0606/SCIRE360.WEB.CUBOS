using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SCIRE360.WEB.CUBOS.Models
{
    public class UserInfoVacaAprobacionModels
    {
        public string IDSOLICITUD { get; set; }
        public string RUC { get; set; }
        public string ID_USUARIO { get; set; }
        public string PERSONAL_ID { get; set; }
        public string FECHA_SALIDA { get; set; }
        public string FECHA_RETORNO { get; set; }
        public string TOTALDIAS { get; set; }
        public string ESTADO_SOLICITUD { get; set; }
        public string ESTADO_APROBACION { get; set; }
        public string COMENTARIO { get; set; }
        public string TRABAJADOR { get; set; }
        public string DNI { get; set; }
        //public string JEFE { get; set; }
        //public string IDJEFE { get; set; }
        public string CadenaAprobacion { get; set; }
        public string NIVEL_ID { get; set; }
        public string NOMNIVELJEF { get; set; }
        public string NIVELIDJEF { get; set; }
        public string TIPOSOL { get; set; }
    }
}