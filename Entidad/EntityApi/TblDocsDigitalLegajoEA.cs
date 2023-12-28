using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidad.EntityApi
{
    public class TblDocsDigitalLegajoEA
    {
        public long Id { get; set; }
        public int IIdItem { get; set; }
        public string Directory { get; set; }
        public string Filename { get; set; }
        public DateTime Fechainsert { get; set; }
        public string UsuScire3 { get; set; }
        public DateTime? Fechamodif { get; set; }
        public string UsuScire3Mod { get; set; }
    }
}
