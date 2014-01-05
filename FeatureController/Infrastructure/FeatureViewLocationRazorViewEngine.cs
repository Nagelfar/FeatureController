using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FeatureController.Infrastructure
{
    public class FeatureViewLocationRazorViewEngine : RazorViewEngine
    {
        private static readonly string[] DefaultFeatureFolderViewLocationFormats = new[]
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

        public FeatureViewLocationRazorViewEngine()
        {
            ViewLocationFormats = DefaultFeatureFolderViewLocationFormats;

            var shellViewLocation = new[]{
                 "~/Views/Shell/{0}.cshtml",
            };

            MasterLocationFormats = DefaultFeatureFolderViewLocationFormats
                .Concat(shellViewLocation)
                .ToArray();
            PartialViewLocationFormats = DefaultFeatureFolderViewLocationFormats
                .Concat(shellViewLocation)
                .ToArray();
        }
    }
}