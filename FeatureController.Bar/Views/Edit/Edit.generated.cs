﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34003
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FeatureController.Features.Bar.Views.Edit
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Text;
    using System.Web;
    using System.Web.Helpers;
    using System.Web.Mvc;
    using System.Web.Mvc.Ajax;
    using System.Web.Mvc.Html;
    using System.Web.Optimization;
    using System.Web.Routing;
    using System.Web.Security;
    using System.Web.UI;
    using System.Web.WebPages;
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/Edit/Edit.cshtml")]
    public partial class Edit : System.Web.Mvc.WebViewPage<FeatureController.Features.Bar.ViewModels.BarEditViewModel>
    {
        public Edit()
        {
        }
        public override void Execute()
        {
            
            #line 3 "..\..\Views\Edit\Edit.cshtml"
 using (Html.BeginForm())
{
    
            
            #line default
            #line hidden
            
            #line 5 "..\..\Views\Edit\Edit.cshtml"
Write(Html.ValidationSummary(false));

            
            #line default
            #line hidden
            
            #line 5 "..\..\Views\Edit\Edit.cshtml"
                                  

    
            
            #line default
            #line hidden
            
            #line 7 "..\..\Views\Edit\Edit.cshtml"
Write(Html.HiddenFor(x => x.Id));

            
            #line default
            #line hidden
            
            #line 7 "..\..\Views\Edit\Edit.cshtml"
                              

            
            #line default
            #line hidden
WriteLiteral("    <div");

WriteLiteral(" class=\"control-group\"");

WriteLiteral(">\r\n");

WriteLiteral("        ");

            
            #line 9 "..\..\Views\Edit\Edit.cshtml"
   Write(Html.LabelFor(x => x.BarEingabe));

            
            #line default
            #line hidden
WriteLiteral("\r\n");

WriteLiteral("        ");

            
            #line 10 "..\..\Views\Edit\Edit.cshtml"
   Write(Html.EditorFor(x => x.BarEingabe));

            
            #line default
            #line hidden
WriteLiteral("\r\n    </div>\r\n");

            
            #line 12 "..\..\Views\Edit\Edit.cshtml"


            
            #line default
            #line hidden
WriteLiteral("    <button");

WriteLiteral(" type=\"submit\"");

WriteLiteral(" class=\"btn\"");

WriteLiteral(">Speichern</button>\r\n");

            
            #line 14 "..\..\Views\Edit\Edit.cshtml"
}
            
            #line default
            #line hidden
        }
    }
}
#pragma warning restore 1591
