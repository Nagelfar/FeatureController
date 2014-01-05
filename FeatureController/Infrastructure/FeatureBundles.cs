using System;
using System.Collections.Generic;
using System.IO;
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
            //  "~/Features/Account/Views/Shared/Account.js"
            yield return string.Format(
                "~/Features/{0}/Views/Shared/{0}.{1}",
                routeData.GetRequiredString("controller"),
                extension
                );

            //  "~/Features/Account/Views/Register/Register.js"
            yield return string.Format(
                "~/Features/{0}/Views/{1}/{1}.{2}",
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

        public static void FindAndRegisterAllFeatureBundles(BundleCollection bundles)
        {
            var websitePath = HttpRuntime.AppDomainAppPath;
            var basePath = new DirectoryInfo(Path.Combine(websitePath, "Features"));

            var scriptBundles = FindJSFiles(websitePath, basePath);
            var styleBundles = FindStyleFiles(websitePath, basePath);

            foreach (var featureBundles in scriptBundles.Concat(styleBundles))
            {
                bundles.Add(featureBundles);
            }
        }

        private static IEnumerable<Bundle> FindJSFiles(string websitePath, DirectoryInfo basePath)
        {
            var jsFiles = basePath.EnumerateFiles("*.js", SearchOption.AllDirectories);

            foreach (var jsFile in jsFiles)
            {
                var virtualPath = jsFile.FullName
                    .Replace(websitePath, "~/")
                    .Replace(@"\", "/");
                yield return new ScriptBundle(virtualPath).Include(virtualPath);
            }
        }
        private static IEnumerable<Bundle> FindStyleFiles(string websitePath, DirectoryInfo basePath)
        {
            var styleFiles = basePath.EnumerateFiles("*.css", SearchOption.AllDirectories);

            foreach (var styleFile in styleFiles)
            {
                var virtualPath = styleFile.FullName
                    .Replace(websitePath, "~/")
                    .Replace(@"\", "/");
                yield return new StyleBundle(virtualPath).Include(virtualPath);
            }
        }
    }
}