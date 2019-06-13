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
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/Tasks/_CreateTask.cshtml")]
    public partial class _Views_Tasks__CreateTask_cshtml : System.Web.Mvc.WebViewPage<dynamic>
    {
        public _Views_Tasks__CreateTask_cshtml()
        {
        }
        public override void Execute()
        {
            
            #line 1 "..\..\Views\Tasks\_CreateTask.cshtml"
 using (Ajax.BeginForm("CreateTask", "Tasks", new AjaxOptions
{
    OnBegin = "tasksManager.beforeTaskSave",
    OnSuccess = "tasksManager.onTaskSaved",
    OnFailure = "tasksManager.onTaskSaveFailure"
}, new { id = "createTaskForm" }))
{

            
            #line default
            #line hidden
WriteLiteral("    <input");

WriteLiteral(" type=\"text\"");

WriteLiteral(" name=\"TaskTitle\"");

WriteLiteral(" id=\"txtBudgetValue\"");

WriteLiteral(" data-val=\"true\"");

WriteLiteral(" data-val-required");

WriteLiteral(" class=\"new-task-input\"");

WriteLiteral(" placeholder=\"Nazwa zadania\"");

WriteLiteral(" />\r\n");

WriteLiteral("    <button type =\"submit\" class=\"btn-rounded btn-turquoise\" title=\"Zapisz\">\r\n   " +
"     <span");

WriteLiteral(" class=\"icon icon-lg icon-save\"");

WriteLiteral("></span>\r\n    </button>\r\n");

WriteLiteral("    <button");

WriteLiteral(" type=\"button\"");

WriteLiteral(" id=\"btnCancelAdd\"");

WriteLiteral(" class=\"btn-rounded btn-dismiss\"");

WriteLiteral(" title=\"Anuluj\"");

WriteLiteral(">\r\n        <span");

WriteLiteral(" class=\"icon icon-lg icon-cancel\"");

WriteLiteral("></span>\r\n    </button>\r\n");

            
            #line 15 "..\..\Views\Tasks\_CreateTask.cshtml"
}

            
            #line default
            #line hidden
        }
    }
}
#pragma warning restore 1591