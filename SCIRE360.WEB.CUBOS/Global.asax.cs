using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.Http;
using System.Web.Optimization;
using SCIRE360.WEB.CUBOS.Routing;

namespace SCIRE360.WEB.CUBOS
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
        protected void Application_BeginRequest()
        {
            // Allow requests from a specific origin
            HttpContext.Current.Response.AddHeader("Access-Control-Allow-Origin", "*");

            // Allow credentials (e.g., cookies) to be included in the request
            HttpContext.Current.Response.AddHeader("Access-Control-Allow-Credentials", "true");

            // Allow specified HTTP methods
            HttpContext.Current.Response.AddHeader("Access-Control-Allow-Methods", "GET, POST, PUT, DELETE, OPTIONS");

            // Allow specified headers in the request
            HttpContext.Current.Response.AddHeader("Access-Control-Allow-Headers", "*");

            if (HttpContext.Current.Request.HttpMethod == "OPTIONS")
            {
                // Handle preflight requests
                HttpContext.Current.Response.StatusCode = 200;
                HttpContext.Current.Response.End();
            }
            /*HttpContext.Current.Response.AddHeader("Access-Control-Allow-Origin", "http://localhost:4200");

            HttpContext.Current.Response.AddHeader("Access-Control-Allow-Credentials", "true");

            HttpContext.Current.Response.AddHeader("Access-Control-Allow-Methods", "GET, POST, PUT, DELETE, OPTIONS");

            HttpContext.Current.Response.AddHeader("Access-Control-Allow-Headers", "Content-Type, Authorization");
            if (Request.Headers.AllKeys.Contains("Origin") && Request.HttpMethod == "OPTIONS")
            {
                Response.Flush();
            }*/
        }
    }
}
