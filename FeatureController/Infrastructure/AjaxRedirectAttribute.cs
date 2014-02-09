using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace FeatureController.Infrastructure
{
    public class AjaxRedirectAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            var result = filterContext.Result as RedirectResult;
            if (result != null && filterContext.HttpContext.Request.IsAjaxRequest())
            {
                string destinationUrl = UrlHelper.GenerateContentUrl(result.Url, filterContext.HttpContext);
                filterContext.Result = new JavaScriptResult()
                {
                    Script = "window.location = '" + destinationUrl + "';"
                };
            }

            var redirectRoute = filterContext.Result as RedirectToRouteResult;
            if(redirectRoute != null  && filterContext.HttpContext.Request.IsAjaxRequest()){
                string destinationUrl = UrlHelper.GenerateUrl(redirectRoute.RouteName,null,null,redirectRoute.RouteValues,
                    RouteTable.Routes,
                    filterContext.RequestContext,false);
                filterContext.Result = new JavaScriptResult()
                {
                    Script = "window.location = '" + destinationUrl + "';"
                };
            }
        }
    }
}