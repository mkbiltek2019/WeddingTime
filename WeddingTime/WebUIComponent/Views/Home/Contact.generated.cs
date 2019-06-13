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
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/Home/Contact.cshtml")]
    public partial class _Views_Home_Contact_cshtml : System.Web.Mvc.WebViewPage<AIT.WebUIComponent.Models.Home.ContactModel>
    {
        public _Views_Home_Contact_cshtml()
        {
        }
        public override void Execute()
        {
            
            #line 3 "..\..\Views\Home\Contact.cshtml"
  
    ViewBag.Title = " - kontakt";
    Layout = "~/Views/Shared/_LayoutSubPage.cshtml";

            
            #line default
            #line hidden
WriteLiteral("\r\n\r\n<div");

WriteLiteral(" class=\"row\"");

WriteLiteral(">\r\n    <p");

WriteLiteral(" class=\"form-text\"");

WriteLiteral(">\r\n        KONTAKT\r\n    </p>\r\n    <hr");

WriteLiteral(" class=\"separator\"");

WriteLiteral(" />\r\n    <div");

WriteLiteral(" class=\"text-center page-desc\"");

WriteLiteral(">\r\n        <p>\r\n            Napisz swoją opinię, zgłoś problem lub zaproponuj zmi" +
"anę.\r\n        </p>\r\n    </div>\r\n    <div");

WriteLiteral(" class=\"text-center page-desc\"");

WriteLiteral(">\r\n        <p>\r\n");

WriteLiteral("            ");

            
            #line 20 "..\..\Views\Home\Contact.cshtml"
       Write(ViewBag.StatusMessage);

            
            #line default
            #line hidden
WriteLiteral("\r\n        </p>\r\n    </div>\r\n</div>\r\n<div");

WriteLiteral(" class=\"row\"");

WriteLiteral(">\r\n    <div");

WriteLiteral(" class=\"form-panel\"");

WriteLiteral(">\r\n        <div");

WriteLiteral(" class=\"form-header form-header-blue\"");

WriteLiteral(">\r\n            <div");

WriteLiteral(" class=\"form-title\"");

WriteLiteral(">\r\n                Wypełnij formularz kontaktowy\r\n            </div>\r\n        </d" +
"iv>\r\n        <div");

WriteLiteral(" class=\"form-body form-body-blue wide\"");

WriteLiteral(">\r\n");

            
            #line 32 "..\..\Views\Home\Contact.cshtml"
            
            
            #line default
            #line hidden
            
            #line 32 "..\..\Views\Home\Contact.cshtml"
             using (Html.BeginForm("Contact", "Home", FormMethod.Post, new { id = "contactForm", role = "form" }))
            {
                
            
            #line default
            #line hidden
            
            #line 34 "..\..\Views\Home\Contact.cshtml"
           Write(Html.AntiForgeryToken());

            
            #line default
            #line hidden
            
            #line 34 "..\..\Views\Home\Contact.cshtml"
                                        

            
            #line default
            #line hidden
WriteLiteral("                <div");

WriteLiteral(" class=\"form-inputs\"");

WriteLiteral(">\r\n                    <div");

WriteLiteral(" class=\"form-ctrl-area\"");

WriteLiteral(">\r\n");

WriteLiteral("                        ");

            
            #line 37 "..\..\Views\Home\Contact.cshtml"
                   Write(Html.TextBoxFor(m => m.Name, new { @class = "form-ctrl", placeholder = "Twoje imię" }));

            
            #line default
            #line hidden
WriteLiteral("\r\n                    </div>\r\n                    <div");

WriteLiteral(" class=\"form-ctrl-area\"");

WriteLiteral(">\r\n");

WriteLiteral("                        ");

            
            #line 40 "..\..\Views\Home\Contact.cshtml"
                   Write(Html.TextBoxFor(m => m.Email, new { @class = "form-ctrl", placeholder = "Twój e-mail" }));

            
            #line default
            #line hidden
WriteLiteral("\r\n");

WriteLiteral("                        ");

            
            #line 41 "..\..\Views\Home\Contact.cshtml"
                   Write(Html.ValidationMessageFor(m => m.Email));

            
            #line default
            #line hidden
WriteLiteral("\r\n                    </div>\r\n                    <div");

WriteLiteral(" class=\"form-ctrl-area\"");

WriteLiteral(">\r\n");

WriteLiteral("                        ");

            
            #line 44 "..\..\Views\Home\Contact.cshtml"
                   Write(Html.TextBoxFor(m => m.Subject, new { @class = "form-ctrl", placeholder = "Tytuł" }));

            
            #line default
            #line hidden
WriteLiteral("\r\n");

WriteLiteral("                        ");

            
            #line 45 "..\..\Views\Home\Contact.cshtml"
                   Write(Html.ValidationMessageFor(m => m.Subject));

            
            #line default
            #line hidden
WriteLiteral("\r\n                    </div>\r\n                    <div");

WriteLiteral(" class=\"form-ctrl-area\"");

WriteLiteral(">\r\n");

WriteLiteral("                        ");

            
            #line 48 "..\..\Views\Home\Contact.cshtml"
                   Write(Html.TextAreaFor(m => m.Body, new { @class = "form-ctrl", placeholder = "Treść", rows = 8 }));

            
            #line default
            #line hidden
WriteLiteral("\r\n");

WriteLiteral("                        ");

            
            #line 49 "..\..\Views\Home\Contact.cshtml"
                   Write(Html.ValidationMessageFor(m => m.Body));

            
            #line default
            #line hidden
WriteLiteral("\r\n                    </div>\r\n                </div>               \r\n");

WriteLiteral("                <div");

WriteLiteral(" class=\"form-body-btn\"");

WriteLiteral(">\r\n                    <button");

WriteLiteral(" type=\"submit\"");

WriteLiteral(" class=\"btn-rounded btn-turquoise\"");

WriteLiteral(" title=\"Wyślij formularz\"");

WriteLiteral(">\r\n                        <span");

WriteLiteral(" class=\"icon icon-lg icon-send\"");

WriteLiteral("></span>\r\n                    </button>\r\n                </div>\r\n");

            
            #line 57 "..\..\Views\Home\Contact.cshtml"
            }

            
            #line default
            #line hidden
WriteLiteral("        </div>\r\n    </div>\r\n</div>\r\n\r\n");

DefineSection("scripts", () => {

WriteLiteral("\r\n");

WriteLiteral("    ");

            
            #line 63 "..\..\Views\Home\Contact.cshtml"
Write(Scripts.Render("~/bundles/enhance"));

            
            #line default
            #line hidden
WriteLiteral("\r\n");

});

        }
    }
}
#pragma warning restore 1591
