using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Optimization;
using System.Linq;
namespace FeatureController
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css",
                      "~/Content/bootstrap-responsive.css"));

            FindAndRegisterAllBundles(bundles);
        }

        private static void FindAndRegisterAllBundles(BundleCollection bundles)
        {
            var websitePath = HttpRuntime.AppDomainAppPath;
            var basePath = new DirectoryInfo(Path.Combine(websitePath, "Features"));

            var scriptBundles=FindJSFiles(websitePath, basePath);
            var styleBundles = FindStyleFiles(websitePath, basePath);

            foreach (var featureBundles in scriptBundles.Concat(styleBundles))
            {
                bundles.Add(featureBundles);
            }
        }

        private static IEnumerable<Bundle> FindJSFiles( string websitePath, DirectoryInfo basePath)
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
