using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidad 
{
    public partial class Password_ebl
    {
        public string txtusser { get; set; }
        public string txtPass1New { get; set; }
        public string txtPass2New { get; set; }
        public string txtPass3New { get; set; }

        public bool DivInfoV { get; set; }
        public string DivInfoCss { get; set; }
        public string DivMessage { get; set; }
    }
}
