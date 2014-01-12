using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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

                // Features/Account/Views/Login.cshtml
                "~/Features/{1}/Views/{0}.cshtml",

                // Features/Account/Views/Shared/Login.cshtml
                "~/Features/{1}/Views/Shared/{0}.cshtml",

                // Features/Shared/Login.cshtml
                "~/Features/Shared/{0}.cshtml",
          };

        private static readonly string[] DefaultAreaFeatureFolderViewLocationFormats = new[]{
                // Features/Account/Views/Login/Login.cshtml
                "~/Areas/{2}/Features/{1}/Views/{0}/{0}.cshtml",

                // Features/Account/Views/Login.cshtml
                "~/Areas/{2}/Features/{1}/Views/{0}.cshtml",

                // Features/Account/Views/Shared/Login.cshtml
                "~/Areas/{2}/Features/{1}/Views/Shared/{0}.cshtml",

                // Features/Shared/Login.cshtml
                "~/Areas/{2}/Features/Shared/{0}.cshtml",
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

            AreaViewLocationFormats = DefaultAreaFeatureFolderViewLocationFormats;
            AreaMasterLocationFormats = DefaultAreaFeatureFolderViewLocationFormats
                .Concat(shellViewLocation)
                .ToArray();
            AreaPartialViewLocationFormats = DefaultAreaFeatureFolderViewLocationFormats
                .Concat(shellViewLocation)
                .ToArray();
        }

        private const string FeaturePartialViewFormat = "~/Features/{1}/Views/{0}/{2}.cshtml";
        private const string AreaFeaturePartialViewFormat = "Areas/{3}/Features/{1}/Views/{0}/{2}.cshtml";


        private string FindFeaturePartialView(ControllerContext controllerContext, string partialViewName, bool useCache)
        {
            var controller = controllerContext.RouteData.GetRequiredString("controller");
            var action = controllerContext.RouteData.GetRequiredString("action");

            var cacheKey = string.Format("FeaturePartialView:{0}:{1}:{2}", controller, action, partialViewName);

            if (useCache)
            {
                var cached = base.ViewLocationCache.GetViewLocation(controllerContext.HttpContext, cacheKey);
                return cached;
            }
            else
            {
                var potentialPath = string.Format(FeaturePartialViewFormat, action, controller, partialViewName);

                if (controllerContext.RouteData.DataTokens.ContainsKey("area"))
                {
                    var area = controllerContext.RouteData.DataTokens["area"];
                    potentialPath = string.Format(FeaturePartialViewFormat, action, controller, partialViewName, area);
                }

                if (FileExists(controllerContext, potentialPath))
                {
                    base.ViewLocationCache.InsertViewLocation(controllerContext.HttpContext, cacheKey, potentialPath);
                    return potentialPath;
                }
            }
            return null;
        }

        private MethodInfo GetPathMethod = typeof(VirtualPathProviderViewEngine).GetMethod("GetPath", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
        public override ViewEngineResult FindPartialView(ControllerContext controllerContext, string partialViewName, bool useCache)
        {
            if (controllerContext == null)
            {
                throw new ArgumentNullException("controllerContext");
            }
            if (String.IsNullOrEmpty(partialViewName))
            {
                throw new ArgumentException("Must not be null or empty", "partialViewName");
            }

            var partialPath = FindFeaturePartialView(controllerContext, partialViewName, useCache);

            if (String.IsNullOrEmpty(partialPath))
            {
                return base.FindPartialView(controllerContext, partialViewName, useCache);
            }

            return new ViewEngineResult(CreatePartialView(controllerContext, partialPath), this);
        }

        //private string GetPath(ControllerContext controllerContext, string[] locations, string[] areaLocations, string locationsPropertyName, string name, string controllerName, string cacheKeyPrefix, bool useCache, out string[] searchedLocations)
        //{
        //    searchedLocations = _emptyLocations;

        //    if (String.IsNullOrEmpty(name))
        //    {
        //        return String.Empty;
        //    }

        //    string areaName = AreaHelpers.GetAreaName(controllerContext.RouteData);
        //    bool usingAreas = !String.IsNullOrEmpty(areaName);
        //    List<ViewLocation> viewLocations = GetViewLocations(locations, (usingAreas) ? areaLocations : null);

        //    if (viewLocations.Count == 0)
        //    {
        //        throw new InvalidOperationException(String.Format(CultureInfo.CurrentCulture,
        //                                                          MvcResources.Common_PropertyCannotBeNullOrEmpty, locationsPropertyName));
        //    }

        //    bool nameRepresentsPath = IsSpecificPath(name);
        //    string cacheKey = CreateCacheKey(cacheKeyPrefix, name, (nameRepresentsPath) ? String.Empty : controllerName, areaName);

        //    if (useCache)
        //    {
        //        // Only look at cached display modes that can handle the context.
        //        IEnumerable<IDisplayMode> possibleDisplayModes = DisplayModeProvider.GetAvailableDisplayModesForContext(controllerContext.HttpContext, controllerContext.DisplayMode);
        //        foreach (IDisplayMode displayMode in possibleDisplayModes)
        //        {
        //            string cachedLocation = ViewLocationCache.GetViewLocation(controllerContext.HttpContext, AppendDisplayModeToCacheKey(cacheKey, displayMode.DisplayModeId));

        //            if (cachedLocation == null)
        //            {
        //                // If any matching display mode location is not in the cache, fall back to the uncached behavior, which will repopulate all of our caches.
        //                return null;
        //            }

        //            // A non-empty cachedLocation indicates that we have a matching file on disk. Return that result.
        //            if (cachedLocation.Length > 0)
        //            {
        //                if (controllerContext.DisplayMode == null)
        //                {
        //                    controllerContext.DisplayMode = displayMode;
        //                }

        //                return cachedLocation;
        //            }
        //            // An empty cachedLocation value indicates that we don't have a matching file on disk. Keep going down the list of possible display modes.
        //        }

        //        // GetPath is called again without using the cache.
        //        return null;
        //    }
        //    else
        //    {
        //        return nameRepresentsPath
        //            ? GetPathFromSpecificName(controllerContext, name, cacheKey, ref searchedLocations)
        //            : GetPathFromGeneralName(controllerContext, viewLocations, name, controllerName, areaName, cacheKey, ref searchedLocations);
        //    }
        //}

        //private string GetPathFromGeneralName(ControllerContext controllerContext, List<ViewLocation> locations, string name, string controllerName, string areaName, string cacheKey, ref string[] searchedLocations)
        //{
        //    string result = String.Empty;
        //    searchedLocations = new string[locations.Count];

        //    for (int i = 0; i < locations.Count; i++)
        //    {
        //        ViewLocation location = locations[i];
        //        string virtualPath = location.Format(name, controllerName, areaName);
        //        DisplayInfo virtualPathDisplayInfo = DisplayModeProvider.GetDisplayInfoForVirtualPath(virtualPath, controllerContext.HttpContext, path => FileExists(controllerContext, path), controllerContext.DisplayMode);

        //        if (virtualPathDisplayInfo != null)
        //        {
        //            string resolvedVirtualPath = virtualPathDisplayInfo.FilePath;

        //            searchedLocations = _emptyLocations;
        //            result = resolvedVirtualPath;
        //            ViewLocationCache.InsertViewLocation(controllerContext.HttpContext, AppendDisplayModeToCacheKey(cacheKey, virtualPathDisplayInfo.DisplayMode.DisplayModeId), result);

        //            if (controllerContext.DisplayMode == null)
        //            {
        //                controllerContext.DisplayMode = virtualPathDisplayInfo.DisplayMode;
        //            }

        //            // Populate the cache for all other display modes. We want to cache both file system hits and misses so that we can distinguish
        //            // in future requests whether a file's status was evicted from the cache (null value) or if the file doesn't exist (empty string).
        //            IEnumerable<IDisplayMode> allDisplayModes = DisplayModeProvider.Modes;
        //            foreach (IDisplayMode displayMode in allDisplayModes)
        //            {
        //                if (displayMode.DisplayModeId != virtualPathDisplayInfo.DisplayMode.DisplayModeId)
        //                {
        //                    DisplayInfo displayInfoToCache = displayMode.GetDisplayInfo(controllerContext.HttpContext, virtualPath, virtualPathExists: path => FileExists(controllerContext, path));

        //                    string cacheValue = String.Empty;
        //                    if (displayInfoToCache != null && displayInfoToCache.FilePath != null)
        //                    {
        //                        cacheValue = displayInfoToCache.FilePath;
        //                    }
        //                    ViewLocationCache.InsertViewLocation(controllerContext.HttpContext, AppendDisplayModeToCacheKey(cacheKey, displayMode.DisplayModeId), cacheValue);
        //                }
        //            }
        //            break;
        //        }

        //        searchedLocations[i] = virtualPath;
        //    }

        //    return result;
        //}
    }
}