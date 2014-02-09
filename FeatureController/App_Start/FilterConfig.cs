using FeatureController.Infrastructure;
using System.Web;
using System.Web.Mvc;

namespace FeatureController
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new ValidatorActionFilter());
        }
    }
}
