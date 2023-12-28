using System.Web;
using System.Web.Routing;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.Compilation;
using System;

namespace SCIRE360.WEB.CUBOS.Routing
{
    public class RouteHandler : IRouteHandler
    {
        public string VirtualPath { get; private set; }
        
        public interface IRoutablePage
        {
            RequestContext RequestContext { set; }
        }

        public RouteHandler(string path)
        {
            this.VirtualPath = path;
        }

        public IHttpHandler GetHttpHandler(RequestContext requestContext)
        {
            var page = BuildManager.CreateInstanceFromVirtualPath(VirtualPath, typeof(Page)) as IHttpHandler;

            if (page != null)
            {
                var routablePage = page as IRoutablePage;

                if (routablePage != null) routablePage.RequestContext = requestContext;
            }

            return page;
        }
    }
}
