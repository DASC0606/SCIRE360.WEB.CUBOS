using Entidad.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidad.EntityApi
{
    public class InboxMail
    {
        public InboxPeriodoEA entity { get; set; }
        public virtual ICollection<AwsS3LegajosBE> Archivos { get; set; }
    }
}
