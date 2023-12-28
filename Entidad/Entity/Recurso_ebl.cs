using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidad
{
    public class Recurso_ebl
    {

        public Int64 id_permiso_recurso { get; set; }
        public string codi_webform { get; set; }
        public string codi_recurso { get; set; }
        public string nomb_seccion_recurso { get; set; }
        public string desc_objetivos { get; set; }
        public string usua_mant { get; set; }
        public DateTime fech_reg { get; set; }
        public DateTime fech_mod { get; set; }
        public bool estado { get; set; }
        public Int64 id_permiso_re_pa { get; set; }
        public bool flag_padre { get; set; }
        public decimal i_orden { get; set; }
    }
}
