using System.Web;
using System.Web.Mvc;

namespace DESCUENTOS_ASOCIADOS
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
