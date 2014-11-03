using System.Web;
using System.Web.Mvc;

namespace ZippyJobs.Ux
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
