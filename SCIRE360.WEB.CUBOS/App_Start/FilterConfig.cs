using System.Web;
using System.Web.Mvc;

namespace SCIRE360.WEB.CUBOS
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
