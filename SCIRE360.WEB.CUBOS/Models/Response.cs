using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SCIRE360.WEB.CUBOS.Models
{
    public class Response
    {
        public bool IsSuccess { get; set; }

        public string Message { get; set; }

        public object Result { get; set; }
    }
}