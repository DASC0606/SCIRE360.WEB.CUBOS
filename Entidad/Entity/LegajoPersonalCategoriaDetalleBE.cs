using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCIRE360.WEB.CUBOS.API.Entity
{
    public class LegajoPersonalCategoriaDetalleBE
    {
        public Int32 i_idItem { get; set; }
        public string Personal_id { get; set; }
        public string Nro_Doc { get; set; }
        public Int32 i_TipoLegajo { get; set; }
        public DateTime d_fecEntrega { get; set; }
        public string v_obs { get; set; }
        public string v_file { get; set; }
        public DateTime d_fecSubida { get; set; }
        public Int32 i_idUsuario { get; set; }
        public string v_descripcion { get; set; }
        public bool b_estado { get; set; }
        public DateTime d_fecVencimiento { get; set; }
        public Int32 i_idCatLegajo { get; set; }
        public bool Archivo { get; set; }

    }
}
