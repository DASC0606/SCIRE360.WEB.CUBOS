using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidad.Entity
{
    public class ListRespuestaBE
    {
        public int result { get; set; }
        public string status { get; set; }
        public string message { get; set; }
        public int currentPage { get; set; }
        public int pages { get; set; }
        public object data { get; set; }
    }
}
