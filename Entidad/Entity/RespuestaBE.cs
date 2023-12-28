using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidad.Entity
{
    public class RespuestaBE
    {
        public int result { get; set; }
        public string status { get; set; }
        public string message { get; set; }
        public object data { get; set; }
    }
}
