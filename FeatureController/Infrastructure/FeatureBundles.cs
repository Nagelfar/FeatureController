using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace FeatureController.Infrastructure
{
    public static class FeatureBundles
    {
        private static string PathForFeatureBundle(RouteData routeData, string extension)
        {
            var path = string.Format(
                "~/Features/{0}/{1}/Index.{2}",
                routeData.GetRequiredString("controller"),
                routeData.GetRequiredString("action"),
                extension
                );

            return path;
        }

        public static IHtmlString Scripts(RouteData routeData)
        {
            var path = PathForFeatureBundle(routeData, "js");

            if (BundleTable.Bundles.GetBundleFor(path) != null)
            {
                return System.Web.Optimization.Scripts.Render(path);
            }
            return MvcHtmlString.Empty;
        }
        public static IHtmlString Styles(RouteData routeData)
        {
            var path = PathForFeatureBundle(routeData, "css");

            if (BundleTable.Bundles.GetBundleFor(path) != null)
            {
                return System.Web.Optimization.Styles.Render(path);
            }
            return MvcHtmlString.Empty;
        }
    }
}