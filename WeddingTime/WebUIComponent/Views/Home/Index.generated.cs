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
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/Home/Index.cshtml")]
    public partial class _Views_Home_Index_cshtml : System.Web.Mvc.WebViewPage<dynamic>
    {
        public _Views_Home_Index_cshtml()
        {
        }
        public override void Execute()
        {
            
            #line 1 "..\..\Views\Home\Index.cshtml"
  
    ViewBag.Title = ".pl";

            
            #line default
            #line hidden
WriteLiteral("\r\n<div");

WriteLiteral(" class=\"main-content\"");

WriteLiteral(">\r\n    <div");

WriteLiteral(" class=\"home-content\"");

WriteLiteral(">\r\n        <div");

WriteLiteral(" class=\"container\"");

WriteLiteral(">\r\n            <div");

WriteLiteral(" class=\"col-sm-offset-1 col-sm-3\"");

WriteLiteral(">\r\n                <div");

WriteLiteral(" class=\"guests-container\"");

WriteLiteral(">\r\n                    <div>\r\n                        <div");

WriteLiteral(" class=\"text-heart text-heart-gu-top\"");

WriteLiteral(">\r\n");

WriteLiteral("                            ");

            
            #line 11 "..\..\Views\Home\Index.cshtml"
                       Write(Html.ActionLink("LISTA", "Index", "Guests"));

            
            #line default
            #line hidden
WriteLiteral("\r\n                        </div>\r\n                        <div");

WriteLiteral(" class=\"text-heart text-heart-gu-btm\"");

WriteLiteral(">\r\n");

WriteLiteral("                            ");

            
            #line 14 "..\..\Views\Home\Index.cshtml"
                       Write(Html.ActionLink("GOŚCI", "Index", "Guests"));

            
            #line default
            #line hidden
WriteLiteral("\r\n                        </div>\r\n                    </div>\r\n                   " +
" <div");

WriteLiteral(" class=\"visible-xs\"");

WriteLiteral(">\r\n                        <p");

WriteLiteral(" class=\"text-welcome text-guests\"");

WriteLiteral(@">
                            Uporządkowana i przejrzysta lista gości z możliwością bieżącej aktualizacji i wygodnym eksportem do pliku PDF jest łatwo i szybko dostępna.
                        </p>
                    </div>
                </div>
            </div>
            <div");

WriteLiteral(" class=\"col-sm-2\"");

WriteLiteral(">\r\n                <div");

WriteLiteral(" class=\"expenses-container\"");

WriteLiteral(">\r\n                    <div>\r\n                        <div");

WriteLiteral(" class=\"text-heart text-heart-ex\"");

WriteLiteral(">\r\n");

WriteLiteral("                            ");

            
            #line 28 "..\..\Views\Home\Index.cshtml"
                       Write(Html.ActionLink("WYDATKI", "Index", "Expenses"));

            
            #line default
            #line hidden
WriteLiteral("\r\n                        </div>\r\n                    </div>\r\n                   " +
" <div");

WriteLiteral(" class=\"visible-xs\"");

WriteLiteral(">\r\n                        <p");

WriteLiteral(" class=\"text-welcome text-expenses\"");

WriteLiteral(@">
                            Planowanie niezbędnych wydatków oraz bieżąca kontrola poniesionych kosztów i założonego budżetu nigdy nie była tak prosta.
                        </p>
                    </div>
                </div>
            </div>
            <div");

WriteLiteral(" class=\"col-sm-2\"");

WriteLiteral(">\r\n                <div");

WriteLiteral(" class=\"tasks-container\"");

WriteLiteral(">\r\n                    <div>\r\n                        <div");

WriteLiteral(" class=\"text-heart text-heart-ts\"");

WriteLiteral(">\r\n");

WriteLiteral("                            ");

            
            #line 42 "..\..\Views\Home\Index.cshtml"
                       Write(Html.ActionLink("ZADANIA", "Index", "Tasks"));

            
            #line default
            #line hidden
WriteLiteral("\r\n                        </div>\r\n                    </div>\r\n                   " +
" <div");

WriteLiteral(" class=\"visible-xs\"");

WriteLiteral(">\r\n                        <p");

WriteLiteral(" class=\"text-welcome text-tasks\"");

WriteLiteral(@">
                            Utworzenie listy spraw i zadań, przypisanie im terminu wykonania oraz nadanie statusu realizacji nie jest już skomplikowane.
                        </p>
                    </div>
                </div>
            </div>
            <div");

WriteLiteral(" class=\"col-sm-2\"");

WriteLiteral(">\r\n                <div");

WriteLiteral(" class=\"ballroom-container\"");

WriteLiteral(">\r\n                    <div>\r\n                        <div");

WriteLiteral(" class=\"text-heart text-heart-bl text-heart-bl-top\"");

WriteLiteral(">\r\n");

WriteLiteral("                            ");

            
            #line 56 "..\..\Views\Home\Index.cshtml"
                       Write(Html.ActionLink("SALA", "Index", "Ballroom"));

            
            #line default
            #line hidden
WriteLiteral("\r\n                        </div>\r\n                        <div");

WriteLiteral(" class=\"text-heart text-heart-bl text-heart-bl-btm\"");

WriteLiteral(">\r\n");

WriteLiteral("                            ");

            
            #line 59 "..\..\Views\Home\Index.cshtml"
                       Write(Html.ActionLink("WESELNA", "Index", "Ballroom"));

            
            #line default
            #line hidden
WriteLiteral("\r\n                        </div>\r\n                    </div>\r\n                   " +
" <div");

WriteLiteral(" class=\"visible-xs\"");

WriteLiteral(">\r\n                        <p");

WriteLiteral(" class=\"text-welcome text-ballroom\"");

WriteLiteral(@">
                            Odpowiednie ułożenie stołów na sali weselnej i wybranie idealnych miejsc dla poszczególnych gości nie jest już problemem.
                        </p>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div");

WriteLiteral(" class=\"container\"");

WriteLiteral(">\r\n        <div");

WriteLiteral(" class=\"welcome-container\"");

WriteLiteral(">\r\n            <div");

WriteLiteral(" class=\"welcome-header\"");

WriteLiteral(">\r\n                WITAMY SERDECZNIE!\r\n            </div>\r\n            <div");

WriteLiteral(" class=\"welcome-body\"");

WriteLiteral(@">
                Organizując własne wesele, poszukiwaliśmy nowoczesnego narzędzia, które pozwoliłoby nam zapanować nad listą gości, zadaniami do wykonania, wydatkami czy salą weselną. Bezskutecznie. Postanowiliśmy więc stworzyć własne – tak powstał serwis Zamiłowani.pl.
            </div>
        </div>
    </div>
</div>
");

        }
    }
}
#pragma warning restore 1591
