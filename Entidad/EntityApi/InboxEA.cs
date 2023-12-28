using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidad.EntityApi
{
    public class InboxEA
    {
        public int IdInbox { get; set; }
        public string Ruc { get; set; }
        public string SenderIdPersonal { get; set; }
        public string ReceiverIdPersonal { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public string Filepath { get; set; }
        public int State { get; set; }
        public DateTime SendDate { get; set; }
    }
}
