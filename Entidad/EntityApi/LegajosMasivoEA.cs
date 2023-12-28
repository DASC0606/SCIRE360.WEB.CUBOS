using Entidad.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Entidad.EntityApi
{
    public class LegajosMasivoEA
    {
        public string Excelbase64 { get; set; }
        public virtual ICollection<AwsS3LegajosBE> ArchivosLegajos { get; set; }
    }
}
