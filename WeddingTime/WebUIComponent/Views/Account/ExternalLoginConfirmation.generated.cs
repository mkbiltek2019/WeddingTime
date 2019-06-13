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
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/Account/ExternalLoginConfirmation.cshtml")]
    public partial class _Views_Account_ExternalLoginConfirmation_cshtml : System.Web.Mvc.WebViewPage<AIT.WebUIComponent.Models.Account.ExternalLoginConfirmationViewModel>
    {
        public _Views_Account_ExternalLoginConfirmation_cshtml()
        {
        }
        public override void Execute()
        {
            
            #line 3 "..\..\Views\Account\ExternalLoginConfirmation.cshtml"
  
    ViewBag.Title = " - dodanie konta";
    Layout = "~/Views/Shared/_LayoutSubPage.cshtml";

            
            #line default
            #line hidden
WriteLiteral("\r\n\r\n<div");

WriteLiteral(" class=\"row\"");

WriteLiteral(">\r\n    <p");

WriteLiteral(" class=\"form-text\"");

WriteLiteral(">\r\n        POWIĄŻ SWOJE KONTO\r\n    </p>\r\n    <hr");

WriteLiteral(" class=\"separator\"");

WriteLiteral(" />\r\n</div>\r\n<div");

WriteLiteral(" class=\"row\"");

WriteLiteral(">\r\n    <div");

WriteLiteral(" class=\"text-center page-desc\"");

WriteLiteral(">\r\n        <p>\r\n            Zostałeś pomyślnie uwierzytelniony z konta <strong>");

            
            #line 17 "..\..\Views\Account\ExternalLoginConfirmation.cshtml"
                                                          Write(Model.LoginProvider);

            
            #line default
            #line hidden
WriteLiteral("</strong>.            \r\n        </p>\r\n        <p>\r\n            Witamy wsród zamił" +
"owanych użytkowników!\r\n        </p>\r\n    </div>\r\n    <div");

WriteLiteral(" class=\"form-panel\"");

WriteLiteral(">\r\n        <div");

WriteLiteral(" class=\"form-header form-header-blue\"");

WriteLiteral(">\r\n            <div");

WriteLiteral(" class=\"form-title\"");

WriteLiteral(">\r\n                Wprowadź nazwę użytkownika\r\n            </div>\r\n        </div>" +
"\r\n        <div");

WriteLiteral(" class=\"form-body form-body-blue\"");

WriteLiteral(">\r\n");

            
            #line 30 "..\..\Views\Account\ExternalLoginConfirmation.cshtml"
            
            
            #line default
            #line hidden
            
            #line 30 "..\..\Views\Account\ExternalLoginConfirmation.cshtml"
             using (Html.BeginForm("ExternalLoginConfirmation", "Account", new { ReturnUrl = ViewBag.ReturnUrl }, FormMethod.Post, new { id = "exLoginForm", role = "form" }))
            {
                
            
            #line default
            #line hidden
            
            #line 32 "..\..\Views\Account\ExternalLoginConfirmation.cshtml"
           Write(Html.AntiForgeryToken());

            
            #line default
            #line hidden
            
            #line 32 "..\..\Views\Account\ExternalLoginConfirmation.cshtml"
                                           
                
            
            #line default
            #line hidden
            
            #line 33 "..\..\Views\Account\ExternalLoginConfirmation.cshtml"
           Write(Html.HiddenFor(n => n.LoginProvider));

            
            #line default
            #line hidden
            
            #line 33 "..\..\Views\Account\ExternalLoginConfirmation.cshtml"
                                                     


            
            #line default
            #line hidden
WriteLiteral("                <div");

WriteLiteral(" class=\"form-inputs\"");

WriteLiteral(">\r\n                    <div");

WriteLiteral(" class=\"form-ctrl-area\"");

WriteLiteral(">\r\n");

WriteLiteral("                        ");

            
            #line 37 "..\..\Views\Account\ExternalLoginConfirmation.cshtml"
                   Write(Html.TextBoxFor(m => m.Username, new { @class = "form-ctrl", placeholder = "Nazwa użytkownika" }));

            
            #line default
            #line hidden
WriteLiteral("\r\n                    </div> \r\n");

WriteLiteral("                    ");

            
            #line 39 "..\..\Views\Account\ExternalLoginConfirmation.cshtml"
               Write(Html.ValidationSummary(true));

            
            #line default
            #line hidden
WriteLiteral("                   \r\n                </div>\r\n");

WriteLiteral("                <div");

WriteLiteral(" class=\"form-body-btn\"");

WriteLiteral(">\r\n                    <button");

WriteLiteral(" type=\"submit\"");

WriteLiteral(" class=\"btn-rounded btn-turquoise\"");

WriteLiteral(">\r\n                        <span");

WriteLiteral(" class=\"icon icon-lg icon-register\"");

WriteLiteral("></span>\r\n                    </button>\r\n                </div>                \r\n" +
"");

            
            #line 46 "..\..\Views\Account\ExternalLoginConfirmation.cshtml"
            }

            
            #line default
            #line hidden
WriteLiteral("        </div>\r\n    </div>\r\n</div>\r\n\r\n");

DefineSection("scripts", () => {

WriteLiteral("\r\n");

WriteLiteral("    ");

            
            #line 52 "..\..\Views\Account\ExternalLoginConfirmation.cshtml"
Write(Scripts.Render("~/bundles/enhance"));

            
            #line default
            #line hidden
WriteLiteral("\r\n");

});

        }
    }
}
#pragma warning restore 1591