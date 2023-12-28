using Microsoft.Owin;
using SCIRE360.WEB.CUBOS.Models;
using Owin;
using System.Linq;
using System.Web.Http;

[assembly: OwinStartupAttribute(typeof(SCIRE360.WEB.CUBOS.Startup))]
namespace SCIRE360.WEB.CUBOS
{
    public class Startup
    {
        private readonly HttpConfiguration config = GlobalConfiguration.Configuration;
        public void Configuration(IAppBuilder app)
        {
            //app.UseDataEngineProviders()
            //    .AddDataEngine("complex10", () => ProductData.GetData(100000))
            //    .AddDataSource("dataset10", () => ProductData.GetData(100000).ToList())
            //    .AddCube("cube", @"Data Source=http://ssrs.componentone.com/OLAP/msmdpump.dll;Provider=msolap;Initial 
            //    Catalog=AdventureWorksDW2012Multidimensional", "Adventure Works");
        }
    }
}
