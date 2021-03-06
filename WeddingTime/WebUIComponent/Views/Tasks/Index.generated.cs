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
    
    #line 1 "..\..\Views\Tasks\Index.cshtml"
    using AIT.TaskDomain.Model.Enums;
    
    #line default
    #line hidden
    using AIT.WebUtilities.Helpers;
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/Tasks/Index.cshtml")]
    public partial class _Views_Tasks_Index_cshtml : System.Web.Mvc.WebViewPage<dynamic>
    {
        public _Views_Tasks_Index_cshtml()
        {
        }
        public override void Execute()
        {
WriteLiteral("\r\n");

            
            #line 3 "..\..\Views\Tasks\Index.cshtml"
  
    ViewBag.Title = " - zadania";
    Layout = "~/Views/Shared/_LayoutSubPage.cshtml";

            
            #line default
            #line hidden
WriteLiteral("\r\n\r\n<div");

WriteLiteral(" id=\"tasksContainer\"");

WriteLiteral(">\r\n    <div");

WriteLiteral(" class=\"row\"");

WriteLiteral(">\r\n        <div");

WriteLiteral(" class=\"add-task-area\"");

WriteLiteral(">\r\n            <div");

WriteLiteral(" class=\"add-task-action\"");

WriteLiteral(">\r\n                <button");

WriteLiteral(" type=\"button\"");

WriteLiteral(" class=\"btn-rounded btn-turquoise\"");

WriteLiteral(" id=\"btnAddTask\"");

WriteLiteral(" title=\"Dodaj nowe zadanie\"");

WriteLiteral(">\r\n                    <span");

WriteLiteral(" class=\"icon icon-lg icon-add\"");

WriteLiteral("></span>\r\n                </button>\r\n            </div>\r\n            <div");

WriteLiteral(" id=\"newTaskArea\"");

WriteLiteral(" class=\"new-task-actions\"");

WriteLiteral(">\r\n");

            
            #line 17 "..\..\Views\Tasks\Index.cshtml"
                
            
            #line default
            #line hidden
            
            #line 17 "..\..\Views\Tasks\Index.cshtml"
                   Html.RenderPartial("_CreateTask"); 
            
            #line default
            #line hidden
WriteLiteral("\r\n            </div>\r\n        </div>\r\n    </div>\r\n\r\n    <div");

WriteLiteral(" class=\"row\"");

WriteLiteral(">\r\n        <div");

WriteLiteral(" class=\"relative-position\"");

WriteLiteral(">\r\n            <div");

WriteLiteral(" class=\"switch-display task hidden-xs hidden-sm\"");

WriteLiteral(">\r\n                <button");

WriteLiteral(" type=\"button\"");

WriteLiteral(" class=\"btn-rounded btn-small btn-turquoise\"");

WriteLiteral(" id=\"btnToggleLayout\"");

WriteLiteral(">\r\n                    <span");

WriteLiteral(" class=\"icon icon-sm icon-left\"");

WriteLiteral("></span>\r\n                </button>\r\n            </div>\r\n            <div");

WriteLiteral(" id=\"tasksBoard\"");

WriteLiteral(" class=\"tasks-board\"");

WriteLiteral(">\r\n                <div");

WriteLiteral(" class=\"tasks-row\"");

WriteLiteral(">\r\n                    <div");

WriteLiteral(" class=\"tasks-cell outside narrow skin-green tasks-single-line\"");

WriteLiteral(">\r\n                        <div");

WriteLiteral(" class=\"tasks-header skin-green\"");

WriteLiteral(">\r\n                            Nowe\r\n                        </div>\r\n            " +
"            <div");

WriteLiteral(" id=\"newTasksArea\"");

WriteLiteral(" class=\"tasks-area\"");

WriteLiteral(" data-state=\"");

            
            #line 35 "..\..\Views\Tasks\Index.cshtml"
                                                                          Write((int)TaskState.NewTask);

            
            #line default
            #line hidden
WriteLiteral("\"");

WriteLiteral(">\r\n\r\n                        </div>\r\n                    </div>\r\n                " +
"    <div");

WriteLiteral(" class=\"tasks-cell skin-blue tasks-multiple-row\"");

WriteLiteral(">\r\n                        <div");

WriteLiteral(" class=\"tasks-header skin-blue\"");

WriteLiteral(">\r\n                            Trwające\r\n                        </div>\r\n        " +
"                <div");

WriteLiteral(" id=\"ongoingTasksArea\"");

WriteLiteral(" class=\"tasks-area\"");

WriteLiteral(" data-state=\"");

            
            #line 43 "..\..\Views\Tasks\Index.cshtml"
                                                                              Write((int)TaskState.OngoingTask);

            
            #line default
            #line hidden
WriteLiteral("\"");

WriteLiteral(">\r\n\r\n                        </div>\r\n                    </div>\r\n                " +
"    <div");

WriteLiteral(" class=\"tasks-cell outside skin-powder tasks-single-line\"");

WriteLiteral(">\r\n                        <div");

WriteLiteral(" class=\"tasks-header skin-powder\"");

WriteLiteral(">\r\n                            Zakończone\r\n                        </div>\r\n      " +
"                  <div");

WriteLiteral(" id=\"doneTasksArea\"");

WriteLiteral(" class=\"tasks-area\"");

WriteLiteral(" data-state=\"");

            
            #line 51 "..\..\Views\Tasks\Index.cshtml"
                                                                           Write((int)TaskState.DoneTask);

            
            #line default
            #line hidden
WriteLiteral("\"");

WriteLiteral(">\r\n\r\n                        </div>\r\n                    </div>\r\n                " +
"</div>\r\n            </div>\r\n        </div>\r\n    </div>\r\n\r\n");

            
            #line 60 "..\..\Views\Tasks\Index.cshtml"
    
            
            #line default
            #line hidden
            
            #line 60 "..\..\Views\Tasks\Index.cshtml"
      
        Html.RenderPartial("_UpdateTask");
        Html.RenderPartial("_TaskCard");
    
            
            #line default
            #line hidden
WriteLiteral("\r\n</div>\r\n<div");

WriteLiteral(" id=\"undoArea\"");

WriteLiteral(">\r\n</div>\r\n\r\n");

DefineSection("scripts", () => {

WriteLiteral("\r\n");

WriteLiteral("    ");

            
            #line 69 "..\..\Views\Tasks\Index.cshtml"
Write(Scripts.Render("~/bundles/tasks"));

            
            #line default
            #line hidden
WriteLiteral("\r\n");

});

        }
    }
}
#pragma warning restore 1591
