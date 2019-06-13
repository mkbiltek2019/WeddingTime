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
    
    #line 2 "..\..\Views\Tasks\_TaskCard.cshtml"
    using AIT.TaskDomain.Model.Enums;
    
    #line default
    #line hidden
    using AIT.WebUtilities.Helpers;
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/Tasks/_TaskCard.cshtml")]
    public partial class _Views_Tasks__TaskCard_cshtml : System.Web.Mvc.WebViewPage<AIT.WebUIComponent.Models.Tasks.TaskCardModel>
    {
        public _Views_Tasks__TaskCard_cshtml()
        {
        }
        public override void Execute()
        {
WriteLiteral("\r\n<div");

WriteLiteral(" class=\"modal fade\"");

WriteLiteral(" id=\"taskCardModal\"");

WriteLiteral(" tabindex=\"-1\"");

WriteLiteral(" role=\"dialog\"");

WriteLiteral(" aria-labelledby=\"lblAddTaskCard\"");

WriteLiteral(" aria-hidden=\"true\"");

WriteLiteral(" data-backdrop=\"static\"");

WriteLiteral(" data-keyboard=\"false\"");

WriteLiteral(">\r\n    <div");

WriteLiteral(" class=\"modal-dialog modal-sm\"");

WriteLiteral(">\r\n        <div");

WriteLiteral(" class=\"modal-content\"");

WriteLiteral(">\r\n");

            
            #line 7 "..\..\Views\Tasks\_TaskCard.cshtml"
            
            
            #line default
            #line hidden
            
            #line 7 "..\..\Views\Tasks\_TaskCard.cshtml"
             using (Ajax.BeginForm("UpdateCard", "Tasks", new AjaxOptions
            {
                OnBegin = "cardManager.beforeCardUpdate",
                OnSuccess = "cardManager.onCardUpdated",
                OnFailure = "cardManager.onCardUpdateFailure"
            }, new { id = "updateCardForm" }))
            {

            
            #line default
            #line hidden
WriteLiteral("                <div");

WriteLiteral(" class=\"modal-header\"");

WriteLiteral(">\r\n                    <button");

WriteLiteral(" type=\"button\"");

WriteLiteral(" class=\"btn-rounded btn-small btn-dismiss right\"");

WriteLiteral(" data-dismiss=\"modal\"");

WriteLiteral(" aria-hidden=\"true\"");

WriteLiteral(">\r\n                        <span");

WriteLiteral(" class=\"icon icon-sm icon-cancel\"");

WriteLiteral("></span>\r\n                    </button>\r\n                    <h4");

WriteLiteral(" class=\"modal-title\"");

WriteLiteral(" id=\"lblAddTaskCard\"");

WriteLiteral(">KARTA DO ZADANIA</h4>\r\n                </div>\r\n");

WriteLiteral("                <div");

WriteLiteral(" class=\"modal-body\"");

WriteLiteral(">\r\n                    <div");

WriteLiteral(" id=\"taskCardContent\"");

WriteLiteral(">\r\n                        <div");

WriteLiteral(" class=\"hidden\"");

WriteLiteral(">\r\n                            <div");

WriteLiteral(" class=\"card-title-area\"");

WriteLiteral(">\r\n                                <div");

WriteLiteral(" class=\"form-ctrl-area\"");

WriteLiteral(">\r\n");

WriteLiteral("                                    ");

            
            #line 25 "..\..\Views\Tasks\_TaskCard.cshtml"
                               Write(Html.TextBoxFor(m => m.Title, new { @class = "form-ctrl", placeholder = "Tytuł karty", id = "cardTitle" }));

            
            #line default
            #line hidden
WriteLiteral("\r\n                                </div>\r\n                            </div>\r\n   " +
"                         <div");

WriteLiteral(" id=\"cardItemInputArea\"");

WriteLiteral(">\r\n                                <div");

WriteLiteral(" class=\"form-ctrl-area\"");

WriteLiteral(">\r\n                                    <input");

WriteLiteral(" id=\"cardItemInput\"");

WriteLiteral(" type=\"text\"");

WriteLiteral(" class=\"form-ctrl\"");

WriteLiteral(" fl-omit />\r\n                                </div>\r\n                            " +
"</div>\r\n                            <div");

WriteLiteral(" class=\"card-actions\"");

WriteLiteral(">\r\n                                <div");

WriteLiteral(" class=\"card-select-actions\"");

WriteLiteral(">\r\n                                    <button");

WriteLiteral(" type=\"button\"");

WriteLiteral(" class=\"btn-rounded btn-small btn-turquoise btn-card-item\"");

WriteLiteral("\r\n                                            data-item=\"");

            
            #line 36 "..\..\Views\Tasks\_TaskCard.cshtml"
                                                  Write(ItemType.Email);

            
            #line default
            #line hidden
WriteLiteral("\"");

WriteLiteral("\r\n                                            data-title=\"Adres email\"");

WriteLiteral(" title=\"Dodaj adres e-mail\"");

WriteLiteral(">\r\n                                        <span");

WriteLiteral(" class=\"icon icon-sm icon-mail\"");

WriteLiteral("></span>\r\n                                    </button>\r\n                        " +
"            <button");

WriteLiteral(" type=\"button\"");

WriteLiteral(" class=\"btn-rounded btn-small btn-turquoise btn-card-item\"");

WriteLiteral("\r\n                                            data-item=\"");

            
            #line 41 "..\..\Views\Tasks\_TaskCard.cshtml"
                                                  Write(ItemType.ContactPerson);

            
            #line default
            #line hidden
WriteLiteral("\"");

WriteLiteral("\r\n                                            data-title=\"Osoba kontaktowa\"");

WriteLiteral(" title=\"Dodaj osobę kontaktową\"");

WriteLiteral(">\r\n                                        <span");

WriteLiteral(" class=\"icon icon-sm icon-contact\"");

WriteLiteral("></span>\r\n                                    </button>\r\n                        " +
"            <button");

WriteLiteral(" type=\"button\"");

WriteLiteral(" class=\"btn-rounded btn-small btn-turquoise btn-card-item\"");

WriteLiteral("\r\n                                            data-item=\"");

            
            #line 46 "..\..\Views\Tasks\_TaskCard.cshtml"
                                                  Write(ItemType.Address);

            
            #line default
            #line hidden
WriteLiteral("\"");

WriteLiteral("\r\n                                            data-title=\"Adres\"");

WriteLiteral(" title=\"Dodaj adres\"");

WriteLiteral(">\r\n                                        <span");

WriteLiteral(" class=\"icon icon-sm icon-home\"");

WriteLiteral("></span>\r\n                                    </button>\r\n                        " +
"            <button");

WriteLiteral(" type=\"button\"");

WriteLiteral(" class=\"btn-rounded btn-small btn-turquoise btn-card-item\"");

WriteLiteral("\r\n                                            data-item=\"");

            
            #line 51 "..\..\Views\Tasks\_TaskCard.cshtml"
                                                  Write(ItemType.Phone);

            
            #line default
            #line hidden
WriteLiteral("\"");

WriteLiteral("\r\n                                            data-title=\"Telefon\"");

WriteLiteral(" title=\"Dodaj numer telefonu\"");

WriteLiteral(">\r\n                                        <span");

WriteLiteral(" class=\"icon icon-sm icon-phone\"");

WriteLiteral("></span>\r\n                                    </button>\r\n                        " +
"            <button");

WriteLiteral(" type=\"button\"");

WriteLiteral(" class=\"btn-rounded btn-small btn-turquoise btn-card-item\"");

WriteLiteral("\r\n                                            data-item=\"");

            
            #line 56 "..\..\Views\Tasks\_TaskCard.cshtml"
                                                  Write(ItemType.Link);

            
            #line default
            #line hidden
WriteLiteral("\"");

WriteLiteral("\r\n                                            data-title=\"Link\"");

WriteLiteral(" title=\"Dodaj link\"");

WriteLiteral(">\r\n                                        <span");

WriteLiteral(" class=\"icon icon-sm icon-link\"");

WriteLiteral("></span>\r\n                                    </button>\r\n                        " +
"        </div>\r\n                                <div");

WriteLiteral(" class=\"card-apply-actions\"");

WriteLiteral(">\r\n                                    <button");

WriteLiteral(" type=\"button\"");

WriteLiteral(" id=\"btnSaveCardItem\"");

WriteLiteral(" class=\"btn-rounded btn-small btn-turquoise\"");

WriteLiteral(" title=\"Zapisz\"");

WriteLiteral(">\r\n                                        <span");

WriteLiteral(" class=\"icon icon-sm icon-save\"");

WriteLiteral("></span>\r\n                                    </button>\r\n                        " +
"            <button");

WriteLiteral(" type=\"button\"");

WriteLiteral(" id=\"btnCancelCardItem\"");

WriteLiteral(" class=\"btn-rounded btn-small btn-dismiss\"");

WriteLiteral(" title=\"Anuluj\"");

WriteLiteral(">\r\n                                        <span");

WriteLiteral(" class=\"icon icon-sm icon-cancel\"");

WriteLiteral("></span>\r\n                                    </button>\r\n                        " +
"        </div>\r\n                            </div>                            \r\n" +
"                            <hr");

WriteLiteral(" class=\"separator\"");

WriteLiteral(" />\r\n                            <div");

WriteLiteral(" id=\"cardItems\"");

WriteLiteral(" data-fl-dynamic-area>\r\n                                ");

WriteLiteral("\r\n                            </div>\r\n                        </div>\r\n           " +
"         </div>\r\n                </div>\r\n");

WriteLiteral("                <div");

WriteLiteral(" class=\"modal-footer\"");

WriteLiteral(">\r\n                    <button");

WriteLiteral(" type=\"submit\"");

WriteLiteral(" class=\"btn-rounded btn-turquoise disabled\"");

WriteLiteral(" title=\"Zapisz\"");

WriteLiteral(">\r\n                        <span");

WriteLiteral(" class=\"icon icon-lg icon-save\"");

WriteLiteral("></span>\r\n                    </button>\r\n                </div>\r\n");

WriteLiteral("                <div>\r\n");

WriteLiteral("                    ");

            
            #line 83 "..\..\Views\Tasks\_TaskCard.cshtml"
               Write(Html.Hidden("Id"));

            
            #line default
            #line hidden
WriteLiteral("\r\n");

WriteLiteral("                    ");

            
            #line 84 "..\..\Views\Tasks\_TaskCard.cshtml"
               Write(Html.Hidden("TaskId"));

            
            #line default
            #line hidden
WriteLiteral("\r\n                </div>\r\n");

            
            #line 86 "..\..\Views\Tasks\_TaskCard.cshtml"
            }

            
            #line default
            #line hidden
WriteLiteral("        </div>\r\n    </div>\r\n</div>");

        }
    }
}
#pragma warning restore 1591
