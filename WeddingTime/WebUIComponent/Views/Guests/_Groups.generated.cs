﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ASP
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
    using AIT.WebUtilities.Helpers;
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/Guests/_Groups.cshtml")]
    public partial class _Views_Guests__Groups_cshtml : System.Web.Mvc.WebViewPage<IEnumerable<AIT.WebUIComponent.Models.Guests.GroupModel>>
    {
        public _Views_Guests__Groups_cshtml()
        {
        }
        public override void Execute()
        {
            
            #line 3 "..\..\Views\Guests\_Groups.cshtml"
 foreach (var item in Model)
{
    Html.RenderPartial("_Group", item);
}
            
            #line default
            #line hidden
        }
    }
}
#pragma warning restore 1591