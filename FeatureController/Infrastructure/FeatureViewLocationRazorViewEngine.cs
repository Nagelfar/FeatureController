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
                "~/Features/{1}/Views/{0}/{0}.cshtml",
                "~/Features/{1}/Views/{0}/Index.cshtml",
                "~/Features/{1}/Views/{0}.cshtml",
                "~/Features/{1}/Views/Shared/{0}.cshtml",
                "~/Features/Shared/{0}.cshtml",
          };

            ViewLocationFormats = featureFolderViewLocationFormats;
            MasterLocationFormats = featureFolderViewLocationFormats;
            PartialViewLocationFormats = featureFolderViewLocationFormats;
        }
    }
}