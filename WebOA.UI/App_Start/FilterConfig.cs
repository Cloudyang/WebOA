using System.Web;
using System.Web.Mvc;
using WebOA.UI.Models;

namespace WebOA.UI
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new MyHandleErrorAttribute());
        }
    }
}
