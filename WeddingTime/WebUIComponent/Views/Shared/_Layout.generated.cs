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
    using MvcSiteMapProvider.Web.Html;
    using MvcSiteMapProvider.Web.Html.Models;
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/Shared/_Layout.cshtml")]
    public partial class _Views_Shared__Layout_cshtml : System.Web.Mvc.WebViewPage<dynamic>
    {
        public _Views_Shared__Layout_cshtml()
        {
        }
        public override void Execute()
        {
WriteLiteral("<!DOCTYPE html>\r\n<html");

WriteLiteral(" lang=\"en\"");

WriteLiteral(">\r\n<head>\r\n    <meta");

WriteLiteral(" charset=\"utf-8\"");

WriteLiteral(" />\r\n    <title>");

            
            #line 5 "..\..\Views\Shared\_Layout.cshtml"
      Write(string.Format("Zamiłowani{0}", ViewBag.Title));

            
            #line default
            #line hidden
WriteLiteral("</title>\r\n    <link");

WriteAttribute("href", Tuple.Create(" href=\"", 149), Tuple.Create("\"", 169)
, Tuple.Create(Tuple.Create("", 156), Tuple.Create<System.Object, System.Int32>(Href("~/favicon.ico")
, 156), false)
);

WriteLiteral(" rel=\"shortcut icon\"");

WriteLiteral(" type=\"image/x-icon\"");

WriteLiteral(" />\r\n    <meta");

WriteLiteral(" name=\"viewport\"");

WriteLiteral(" content=\"width=device-width initial-scale=1.0\"");

WriteLiteral(" />\r\n\r\n");

WriteLiteral("    ");

            
            #line 9 "..\..\Views\Shared\_Layout.cshtml"
Write(Styles.Render("~/Content/site/css"));

            
            #line default
            #line hidden
WriteLiteral("\r\n\r\n</head>\r\n<body>    \r\n    <div");

WriteLiteral(" class=\"header\"");

WriteLiteral(">\r\n        <div");

WriteLiteral(" class=\"container\"");

WriteLiteral(">\r\n            <div");

WriteLiteral(" class=\"relative-position\"");

WriteLiteral(">\r\n                <div");

WriteLiteral(" class=\"logo-img\"");

WriteLiteral("></div>\r\n            </div>\r\n            <section>\r\n                <nav");

WriteLiteral(" class=\"navbar\"");

WriteLiteral(">\r\n                    <div");

WriteLiteral(" class=\"navbar-header\"");

WriteLiteral(">\r\n                        <button");

WriteLiteral(" type=\"button\"");

WriteLiteral(" id=\"btnNavMenu\"");

WriteLiteral(" class=\"navbar-toggle btn-nav\"");

WriteLiteral(" data-toggle=\"collapse\"");

WriteLiteral(" data-target=\"#navbarPageOptions\"");

WriteLiteral(">\r\n                            <span");

WriteLiteral(" class=\"sr-only\"");

WriteLiteral(">Toggle nav</span>\r\n                            <span");

WriteLiteral(" class=\"icon-nav icon-menu icon-menu-inactive\"");

WriteLiteral("></span>\r\n                        </button>\r\n                        <button");

WriteLiteral(" type=\"button\"");

WriteLiteral(" id=\"btnNavLogin\"");

WriteLiteral(" class=\"navbar-toggle btn-nav btn-rounded btn-turquoise\"");

WriteLiteral(" data-toggle=\"collapse\"");

WriteLiteral(" data-target=\"#navbarUserOptions\"");

WriteLiteral(">\r\n                            <span");

WriteLiteral(" class=\"sr-only\"");

WriteLiteral(">Toggle nav</span>\r\n                            <span");

WriteLiteral(" class=\"icon icon-lg icon-user\"");

WriteLiteral("></span>\r\n                        </button>\r\n                    </div>\r\n");

            
            #line 30 "..\..\Views\Shared\_Layout.cshtml"
                    
            
            #line default
            #line hidden
            
            #line 30 "..\..\Views\Shared\_Layout.cshtml"
                       Html.RenderPartial("_Login"); 
            
            #line default
            #line hidden
WriteLiteral("\r\n                    <div");

WriteLiteral(" class=\"hidden-xs menu-separator-wrapper\"");

WriteLiteral(">\r\n                        <hr");

WriteLiteral(" class=\"menu-separator\"");

WriteLiteral(" />\r\n                    </div>\r\n                    <div");

WriteLiteral(" class=\"navbar-collapse collapse\"");

WriteLiteral(" id=\"navbarPageOptions\"");

WriteLiteral(">\r\n");

WriteLiteral("                        ");

            
            #line 35 "..\..\Views\Shared\_Layout.cshtml"
                   Write(Html.MvcSiteMap().Menu());

            
            #line default
            #line hidden
WriteLiteral("\r\n                    </div>\r\n                </nav>\r\n            </section>\r\n   " +
"     </div>       \r\n    </div>\r\n\r\n");

WriteLiteral("    ");

            
            #line 42 "..\..\Views\Shared\_Layout.cshtml"
Write(RenderBody());

            
            #line default
            #line hidden
WriteLiteral("\r\n\r\n    <div>\r\n        <footer");

WriteLiteral(" class=\"container page-footer\"");

WriteLiteral(">\r\n            <div");

WriteLiteral(" class=\"row\"");

WriteLiteral(">\r\n                <div");

WriteLiteral(" class=\"hidden-xs\"");

WriteLiteral(">\r\n                    <hr");

WriteLiteral(" class=\"footer-line\"");

WriteLiteral(" />\r\n                </div>\r\n                <div");

WriteLiteral(" class=\"footer-body\"");

WriteLiteral(">\r\n                    <div");

WriteLiteral(" class=\"footer-copy-rights pull-left\"");

WriteLiteral(">\r\n                        <span>&copy; ");

            
            #line 52 "..\..\Views\Shared\_Layout.cshtml"
                                Write(DateTime.Now.Year);

            
            #line default
            #line hidden
WriteLiteral(" - Zamiłowani.pl</span>\r\n                    </div>\r\n                    <div");

WriteLiteral(" class=\"footer-social-icons\"");

WriteLiteral(">\r\n                        <a");

WriteLiteral(" href=\"https://plus.google.com/b/118188235749641783100/118188235749641783100/post" +
"s\"");

WriteLiteral(" target=\"_blank\"");

WriteLiteral(" class=\"btn-rounded btn-red btn-social\"");

WriteLiteral(">\r\n                            <span");

WriteLiteral(" class=\"icon icon-lg icon-gp\"");

WriteLiteral("></span>\r\n                        </a>\r\n                        <a");

WriteLiteral(" href=\"#\"");

WriteLiteral(" class=\"btn-rounded btn-blue btn-social\"");

WriteLiteral(">\r\n                            <span");

WriteLiteral(" class=\"icon icon-lg icon-tw\"");

WriteLiteral("></span>\r\n                        </a>\r\n                        <a");

WriteLiteral(" href=\"https://www.facebook.com/zamilowani\"");

WriteLiteral(" target=\"_blank\"");

WriteLiteral(" class=\"btn-rounded btn-navy-blue btn-social\"");

WriteLiteral(">\r\n                            <span");

WriteLiteral(" class=\"icon icon-lg icon-fb\"");

WriteLiteral("></span>\r\n                        </a>\r\n                    </div>               " +
"     \r\n                </div>\r\n            </div>\r\n            <div");

WriteLiteral(" class=\"row\"");

WriteLiteral(">\r\n                <div");

WriteLiteral(" class=\"fotter-additional-info\"");

WriteLiteral(">\r\n                    Korzystanie z serwisu oznacza akceptację ");

            
            #line 69 "..\..\Views\Shared\_Layout.cshtml"
                                                        Write(Html.ActionLink("regulaminu", "UserAgreement", "Documents"));

            
            #line default
            #line hidden
WriteLiteral(" <br />\r\n");

WriteLiteral("                    ");

            
            #line 70 "..\..\Views\Shared\_Layout.cshtml"
               Write(Html.ActionLink("Polityka prywatności i plików cookies", "PrivacyAndCookiesPolicy", "Documents"));

            
            #line default
            #line hidden
WriteLiteral("\r\n                </div>\r\n            </div>\r\n        </footer>\r\n    </div>\r\n\r\n");

WriteLiteral("    ");

            
            #line 76 "..\..\Views\Shared\_Layout.cshtml"
Write(Scripts.Render("~/bundles/libs"));

            
            #line default
            #line hidden
WriteLiteral("\r\n");

WriteLiteral("    ");

            
            #line 77 "..\..\Views\Shared\_Layout.cshtml"
Write(Scripts.Render("~/bundles/utils"));

            
            #line default
            #line hidden
WriteLiteral("\r\n");

WriteLiteral("    ");

            
            #line 78 "..\..\Views\Shared\_Layout.cshtml"
Write(Scripts.Render("~/bundles/shared"));

            
            #line default
            #line hidden
WriteLiteral("\r\n\r\n");

WriteLiteral("    ");

            
            #line 80 "..\..\Views\Shared\_Layout.cshtml"
Write(RenderSection("scripts", required: false));

            
            #line default
            #line hidden
WriteLiteral("\r\n</body>\r\n</html>\r\n");

        }
    }
}
#pragma warning restore 1591
