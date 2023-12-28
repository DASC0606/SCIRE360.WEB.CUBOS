using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidad.Entity
{
    public class CorreoBE
    {
        public string servidorcorreo { get; set; }
        public string puerto { get; set; }
        public string nombreusuario { get; set; }
        public string contrasena { get; set; }
        public string asuento { get; set; }
        public string nombrecorreo { get; set; }
        public string correocopia { get; set; }
        public string cuerpo { get; set; }
        public byte[] adjunto { get; set; }
        public string nombreadjunto { get; set; }
        public string tipoadjunto { get; set; }
        public Int32 bExchange { get; set; }
        public Boolean bValCertificado { get; set; }
        public string correodestino { get; set; }
        public string nombredestino { get; set; }
    }
}
