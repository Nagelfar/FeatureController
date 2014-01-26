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
            if (!routeData.DataTokens.ContainsKey("area"))
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
            else
            {
                 yield return string.Format(
                    "~/Areas/{1}/Features/{0}/Views/Shared/{0}.{2}",
                    routeData.GetRequiredString("controller"),
                    routeData.DataTokens["area"],
                    extension
                    );

                //  "~/Features/Account/Views/Register/Register.js"
                yield return string.Format(
                    "~/Areas/{2}/Features/{0}/Views/{1}/{1}.{3}",
                    routeData.GetRequiredString("controller"),
                    routeData.GetRequiredString("action"),
                    routeData.DataTokens["area"],   
                    extension
                    );
            }
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
            var basePath = FindFolder(context, feature, featureFolder);

            if (!basePath.Item2.Exists)
            {

                return Enumerable.Empty<Bundle>();

            }
            return FindStyleFiles(basePath.Item1, basePath.Item2)
                .Concat(FindJSFiles(basePath.Item1, basePath.Item2)
                );
        }



        private static Tuple<string,DirectoryInfo> FindFolder(HttpContext context, IController feature, string featureFolder)
        {
            var featureName = Feature.Configuration.Current.NamingConvention(feature.GetType())
                .Value
                .Replace("Controller", "");

            var folderName = featureFolder + "/" + featureName;
            var basePath = new DirectoryInfo(context.Server.MapPath(folderName));

            if (!basePath.Exists)
            {
                var area = GetAreaNameAndNamespace(RouteTable.Routes)
                    .SingleOrDefault(x => feature.GetType().Namespace.StartsWith(x.Item2));


                if (area != null)
                {
                    folderName = "~/Areas/" + area.Item1 + "/Features/" + featureName;
                    basePath = new DirectoryInfo(context.Server.MapPath(folderName));
                }


            }

            return Tuple.Create(folderName,basePath);
        }
        private static string ExtractSingleValue(object potentialList)
        {
            var list = potentialList as IEnumerable<string>;
            if (list != null)
                return list.FirstOrDefault();

            return potentialList as string;
        }

        private static IEnumerable<Tuple<string, string>> GetAreaNameAndNamespace(RouteCollection routeEntries)
        {
            var areas = routeEntries
                .OfType<Route>()
                .Where(x => x.DataTokens != null)
                .Where(x => x.DataTokens.Any(token => token.Key == "area"))
                .Select(x => Tuple.Create(ExtractSingleValue(x.DataTokens["area"]), ExtractSingleValue(x.DataTokens["Namespaces"]).Replace("*", "").TrimEnd('.')))
                ;
            return areas.ToArray();

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