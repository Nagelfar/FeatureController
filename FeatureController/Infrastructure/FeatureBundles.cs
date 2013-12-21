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
        private static IEnumerable<string> PathForFeatureBundle(RouteData routeData, string extension)
        {
            yield return string.Format(
                "~/Features/{0}/Views/Shared/Index.{1}",
                routeData.GetRequiredString("controller"),
                extension
                );
            yield return string.Format(
                "~/Features/{0}/Views/{1}/Index.{2}",
                routeData.GetRequiredString("controller"),
                routeData.GetRequiredString("action"),
                extension
                );
        }

        public static IHtmlString Scripts(RouteData routeData)
        {
            var path = PathForFeatureBundle(routeData, "js")
                .Where(x => BundleTable.Bundles.GetBundleFor(x) != null);

            return System.Web.Optimization.Scripts.Render(path.ToArray());
        }
        public static IHtmlString Styles(RouteData routeData)
        {
            var path = PathForFeatureBundle(routeData, "css")
                .Where(x => BundleTable.Bundles.GetBundleFor(x) != null);

            return System.Web.Optimization.Styles.Render(path.ToArray());
        }
    }
}