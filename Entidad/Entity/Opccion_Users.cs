using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidad
{
    public class Opccion_Users
    {
        public Int32 id_permiso_recurso { get; set; }
        public string codi_webform { get; set; }
        public string codi_recurso { get; set; }
        public string nomb_seccion_recurso { get; set; }
        public string desc_objetivos { get; set; }
        public string usua_mant { get; set; }
        public DateTime fech_reg { get; set; }
        public DateTime fech_mod { get; set; }
        public bool estado { get; set; }
    }
}
