using FeatureSwitcher;
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

        public static void FindAndRegisterAllFeatureBundles(HttpContext context, BundleCollection bundles, IEnumerable<IController> features)
        {
            var websitePath = HttpRuntime.AppDomainAppPath;
            var basePath = new DirectoryInfo(Path.Combine(websitePath, "Features"));

            var featureFolder = "~/Features";

            var featureBundles = features.SelectMany(x => FindBundles(context, x, featureFolder));

            foreach (var featureBundle in featureBundles)
            {
                bundles.Add(featureBundle);
            }
        }

        private static IEnumerable<Bundle> FindBundles(HttpContext context, IController feature, string featureFolder)
        {
            var featureName = Feature.Configuration.Current.NamingConvention(feature.GetType()).Value;
            var folderName = featureFolder + "/" + featureName.Replace("Controller", "");
            var basePath = new DirectoryInfo(context.Server.MapPath(folderName));

            if (!basePath.Exists)
                return Enumerable.Empty<Bundle>();

            return FindStyleFiles(folderName, basePath)
                .Concat(FindJSFiles(folderName, basePath)
                );
        }

        private static IEnumerable<Bundle> FindJSFiles(string featureFolder, DirectoryInfo basePath)
        {
            var jsFiles = basePath.EnumerateFiles("*.js", SearchOption.AllDirectories);

            foreach (var jsFile in jsFiles)
            {
                var virtualPath = jsFile.FullName
                    .Replace(basePath.FullName, featureFolder)
                    .Replace(@"\", "/");

                yield return new ScriptBundle(virtualPath).Include(virtualPath);
            }
        }
        private static IEnumerable<Bundle> FindStyleFiles(string featureFolder, DirectoryInfo basePath)
        {
            var styleFiles = basePath.EnumerateFiles("*.css", SearchOption.AllDirectories);

            foreach (var styleFile in styleFiles)
            {
                var virtualPath = styleFile.FullName
                    .Replace(basePath.FullName, featureFolder)
                    .Replace(@"\", "/");

                yield return new StyleBundle(virtualPath).Include(virtualPath);
            }
        }
    }
}