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
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/Account/EmailAlreadyConfirmed.cshtml")]
    public partial class _Views_Account_EmailAlreadyConfirmed_cshtml : System.Web.Mvc.WebViewPage<dynamic>
    {
        public _Views_Account_EmailAlreadyConfirmed_cshtml()
        {
        }
        public override void Execute()
        {
            
            #line 1 "..\..\Views\Account\EmailAlreadyConfirmed.cshtml"
  
    ViewBag.Title = " - rejestracja";
    Layout = "~/Views/Shared/_LayoutSubPage.cshtml";

            
            #line default
            #line hidden
WriteLiteral("\r\n\r\n<div");

WriteLiteral(" class=\"row\"");

WriteLiteral(">\r\n    <p");

WriteLiteral(" class=\"form-text\"");

WriteLiteral(">\r\n        KONTO JEST JUŻ AKTYWNE\r\n    </p>\r\n    <hr");

WriteLiteral(" class=\"separator\"");

WriteLiteral(" />\r\n</div>\r\n<div");

WriteLiteral(" class=\"row\"");

WriteLiteral(">\r\n    <div");

WriteLiteral(" class=\"text-center page-desc\"");

WriteLiteral(">       \r\n        <p>\r\n            <strong>");

            
            #line 15 "..\..\Views\Account\EmailAlreadyConfirmed.cshtml"
               Write(Html.ActionLink("Zaloguj się", "Login", "Account", new { returnUrl = "/Home" }, null));

            
            #line default
            #line hidden
WriteLiteral("</strong> i zacznij korzystać z serwisu Zamiłowani.pl\r\n        </p>\r\n    </div>\r\n" +
"</div>\r\n");

        }
    }
}
#pragma warning restore 1591