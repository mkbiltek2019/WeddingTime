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
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/Tasks/_UpdateTask.cshtml")]
    public partial class _Views_Tasks__UpdateTask_cshtml : System.Web.Mvc.WebViewPage<dynamic>
    {
        public _Views_Tasks__UpdateTask_cshtml()
        {
        }
        public override void Execute()
        {
WriteLiteral("<div");

WriteLiteral(" class=\"modal fade\"");

WriteLiteral(" id=\"updateTaskModal\"");

WriteLiteral(" tabindex=\"-1\"");

WriteLiteral(" role=\"dialog\"");

WriteLiteral(" aria-labelledby=\"lblUpdateTask\"");

WriteLiteral(" aria-hidden=\"true\"");

WriteLiteral(" data-backdrop=\"static\"");

WriteLiteral(" data-keyboard=\"false\"");

WriteLiteral(">\r\n    <div");

WriteLiteral(" class=\"modal-dialog modal-sm\"");

WriteLiteral(">\r\n        <div");

WriteLiteral(" class=\"modal-content\"");

WriteLiteral(">\r\n");

            
            #line 4 "..\..\Views\Tasks\_UpdateTask.cshtml"
            
            
            #line default
            #line hidden
            
            #line 4 "..\..\Views\Tasks\_UpdateTask.cshtml"
             using (Ajax.BeginForm("UpdateTask", "Tasks", new AjaxOptions
            {
                OnBegin = "tasksManager.beforeTaskUpdate",
                OnSuccess = "tasksManager.onTaskUpdated",
                OnFailure = "tasksManager.onTaskUpdateFailure"
            }, new { id = "updateTaskFrom" }))
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

WriteLiteral(" id=\"lblUpdateTask\"");

WriteLiteral(">AKTUALIZACJA ZADANIA</h4>\r\n                </div>\r\n");

WriteLiteral("                <div");

WriteLiteral(" class=\"modal-body\"");

WriteLiteral(">\r\n                    <div");

WriteLiteral(" id=\"updateTaskContent\"");

WriteLiteral(">\r\n                        ");

WriteLiteral("\r\n                    </div>\r\n                </div>\r\n");

WriteLiteral("                <div");

WriteLiteral(" class=\"modal-footer\"");

WriteLiteral(">\r\n                    <button");

WriteLiteral(" type=\"submit\"");

WriteLiteral(" class=\"btn-rounded btn-turquoise disabled\"");

WriteLiteral(" title=\"Zapisz\"");

WriteLiteral(">\r\n                        <span");

WriteLiteral(" class=\"icon icon-lg icon-save\"");

WriteLiteral("></span>\r\n                    </button>\r\n                </div>               \r\n");

            
            #line 27 "..\..\Views\Tasks\_UpdateTask.cshtml"
            }

            
            #line default
            #line hidden
WriteLiteral("        </div>\r\n    </div>\r\n</div>");

        }
    }
}
#pragma warning restore 1591