﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FeatureController.Infrastructure
{
    public class ValidatorActionFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var filters=filterContext.ActionDescriptor.GetFilterAttributes(true);
            if (filters.OfType<NormalValidationAttribute>().Any()) return;
            // Continue normally if the model is valid.
            if (filterContext.Controller.ViewData.ModelState.IsValid) return;

            var serializationSettings = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };

            var serializedModelState = JsonConvert.SerializeObject(
              filterContext.Controller.ViewData.ModelState,
              serializationSettings);

            var result = new ContentResult
            {
                Content = serializedModelState,
                ContentType = "application/json"
            };
            
            filterContext.HttpContext.Response.StatusCode = 400;
            filterContext.Result = result;
        }

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
         
        }
    }    

}