using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidad.EntityApi
{
    public class InboxCustom
    {
        public int IdInbox { get; set; }
        public string IdPersonal { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string NombPersonal { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public string Filepath { get; set; }
        public int State { get; set; }
        public DateTime SendDate { get; set; }
    }
}
