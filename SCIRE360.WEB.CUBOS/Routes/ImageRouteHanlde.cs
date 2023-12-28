using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;


namespace SCIRE360.WEB.CUBOS.Routing
{
    public class ImageRouteHandler : IRouteHandler
    {
        public IHttpHandler GetHttpHandler(RequestContext requestContext)
        {
            string filename = requestContext.RouteData.Values["filename"] as string;

            return new  ImageHttpHandler(filename);

        }
    }
}