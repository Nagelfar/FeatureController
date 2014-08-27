using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc.Ajax;

namespace System.Web.Mvc.Ajax
{
    public static class DefaultOptions
    {
        public static AjaxOptions Form()
        {
            return Form(new AjaxOptions());
        }
        public static AjaxOptions Form(AjaxOptions ajaxOptions)
        {
            var options = ajaxOptions ?? new AjaxOptions();

            if (string.IsNullOrEmpty(options.OnSuccess))
                options.OnSuccess = "formSuccess";
            if (string.IsNullOrEmpty(options.OnFailure))
                options.OnFailure = "formFailure";

            return options;
        }
    }
}