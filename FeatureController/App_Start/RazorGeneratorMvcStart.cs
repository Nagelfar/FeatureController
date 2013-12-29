using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;
using RazorGenerator.Mvc;
using System.Linq;

[assembly: WebActivatorEx.PostApplicationStartMethod(typeof(FeatureController.App_Start.RazorGeneratorMvcStart), "Start")]

namespace FeatureController.App_Start {
    public static class RazorGeneratorMvcStart {
        public static void Start() {
            if (!ViewEngines.Engines.OfType<PrecompiledMvcEngine>().Any())
            {
                var engine = new PrecompiledMvcEngine(typeof(RazorGeneratorMvcStart).Assembly)
                {
                    UsePhysicalViewsIfNewer = HttpContext.Current.Request.IsLocal
                };

                var featureFolderViewLocationFormats = new[]
          {
              // Features/Account/Views/Login/Login.cshtml
                "~/Features/{1}/Views/{0}/{0}.cshtml",
                // Features/Account/Views/Login/Index.cshtml
                //"~/Features/{1}/Views/{0}/Index.cshtml",
                // Features/Account/Views/Login.cshtml
                "~/Features/{1}/Views/{0}.cshtml",
                // Features/Account/Views/Shared/Login.cshtml
                "~/Features/{1}/Views/Shared/{0}.cshtml",
                // Features/Shared/Login.cshtml
                "~/Features/Shared/{0}.cshtml",
          };

                engine.ViewLocationFormats = featureFolderViewLocationFormats;
                engine.MasterLocationFormats = featureFolderViewLocationFormats;
                engine.PartialViewLocationFormats = featureFolderViewLocationFormats;

                ViewEngines.Engines.Insert(0, engine);

                // StartPage lookups are done by WebPages. 
                //VirtualPathFactoryManager.RegisterVirtualPathFactory(engine);
            }
        }
    }
}
