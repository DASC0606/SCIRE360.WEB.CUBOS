using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidad.EntityApi
{
    public class LegajoPersonalCategoriaDetalleEA
    {
        public int IIdItem { get; set; }
        public string PersonalId { get; set; }
        public int ITipoLegajo { get; set; }
        public DateTime DFecEntrega { get; set; }
        public string VObs { get; set; }
        public string VFile { get; set; }
        public DateTime DFecSubida { get; set; }
        public int IIdUsuario { get; set; }
        public string VDescripcion { get; set; }
        public bool BEstado { get; set; }
        public DateTime DFecVencimiento { get; set; }
        public int IIdCatLegajo { get; set; }
        public string NroDoc { get; set; }
    }
}
