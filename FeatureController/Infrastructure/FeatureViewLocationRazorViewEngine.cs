using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FeatureController.Infrastructure
{
    public class FeatureViewLocationRazorViewEngine : RazorViewEngine
    {
        public FeatureViewLocationRazorViewEngine()
        {
            var featureFolderViewLocationFormats = new[]
          {
              // Features/Account/Views/Login/Login.cshtml
                "~/Features/{1}/Views/{0}/{0}.cshtml",
                // Features/Account/Views/Login/Index.cshtml
                "~/Features/{1}/Views/{0}/Index.cshtml",
                // Features/Account/Views/Login.cshtml
                "~/Features/{1}/Views/{0}.cshtml",
                // Features/Account/Views/Shared/Login.cshtml
                "~/Features/{1}/Views/Shared/{0}.cshtml",
                // Features/Shared/Login.cshtml
                "~/Features/Shared/{0}.cshtml",
          };

            ViewLocationFormats = featureFolderViewLocationFormats;
            MasterLocationFormats = featureFolderViewLocationFormats;
            PartialViewLocationFormats = featureFolderViewLocationFormats;
        }
    }
}