using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidad.Entity
{
    public class AwsS3LegajosBE
    {
        public string base64 { get; set; }
        public int i_idItem { get; set; }
        public string filename { get; set; }
        public string directory { get; set; }
        public string extension { get; set; }
}
}
