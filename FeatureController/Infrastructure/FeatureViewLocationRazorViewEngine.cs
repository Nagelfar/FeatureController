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
              "~/Features/{1}/{0}/{0}.cshtml",
              "~/Features/{1}/{0}/{0}.vbhtml",
              "~/Features/{1}/{0}/Index.cshtml",
              "~/Features/{1}/{0}/Index.vbhtml",
            "~/Features/{1}/{0}.cshtml",
            "~/Features/{1}/{0}.vbhtml",
            "~/Features/Shared/{0}.cshtml",
            "~/Features/Shared/{0}.vbhtml",
          };

            ViewLocationFormats = featureFolderViewLocationFormats;
            MasterLocationFormats = featureFolderViewLocationFormats;
            PartialViewLocationFormats = featureFolderViewLocationFormats;
        }
    }
}