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
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/Expenses/EditorTemplates/ExpenseItemModel.cshtml")]
    public partial class _Views_Expenses_EditorTemplates_ExpenseItemModel_cshtml_ : System.Web.Mvc.WebViewPage<AIT.WebUIComponent.Models.Expenses.ExpenseItemModel>
    {
        public _Views_Expenses_EditorTemplates_ExpenseItemModel_cshtml_()
        {
        }
        public override void Execute()
        {
            
            #line 3 "..\..\Views\Expenses\EditorTemplates\ExpenseItemModel.cshtml"
  
    var id = Model.Id;

            
            #line default
            #line hidden
WriteLiteral("\r\n\r\n<div");

WriteLiteral(" class=\"modal-slice hidden\"");

WriteAttribute("id", Tuple.Create(" id=\"", 126), Tuple.Create("\"", 161)
            
            #line 7 "..\..\Views\Expenses\EditorTemplates\ExpenseItemModel.cshtml"
, Tuple.Create(Tuple.Create("", 131), Tuple.Create<System.Object, System.Int32>(ViewData["Index"]
            
            #line default
            #line hidden
, 131), false)
, Tuple.Create(Tuple.Create("", 149), Tuple.Create("-modal-slice", 149), true)
);

WriteLiteral(">\r\n    <div");

WriteLiteral(" class=\"form-ctrl-desc\"");

WriteLiteral(">\r\n");

WriteLiteral("        ");

            
            #line 9 "..\..\Views\Expenses\EditorTemplates\ExpenseItemModel.cshtml"
   Write(Html.LabelFor(model => model.Description));

            
            #line default
            #line hidden
WriteLiteral("\r\n    </div>\r\n    <div");

WriteLiteral(" class=\"form-ctrl-area\"");

WriteLiteral(">\r\n");

WriteLiteral("        ");

            
            #line 12 "..\..\Views\Expenses\EditorTemplates\ExpenseItemModel.cshtml"
   Write(Html.TextBoxFor(model => model.Description, new { id = id + "-Description", @class = "form-ctrl" }));

            
            #line default
            #line hidden
WriteLiteral("\r\n    </div>\r\n    <div");

WriteLiteral(" class=\"form-ctrl-desc\"");

WriteLiteral(">\r\n");

WriteLiteral("        ");

            
            #line 15 "..\..\Views\Expenses\EditorTemplates\ExpenseItemModel.cshtml"
   Write(Html.LabelFor(model => model.Quantity));

            
            #line default
            #line hidden
WriteLiteral("\r\n    </div>\r\n    <div");

WriteLiteral(" class=\"form-ctrl-area\"");

WriteLiteral(">\r\n");

WriteLiteral("        ");

            
            #line 18 "..\..\Views\Expenses\EditorTemplates\ExpenseItemModel.cshtml"
   Write(Html.TextBoxFor(model => model.Quantity, new { id = id + "-Quantity", @class = "form-ctrl int-input calcField-" + id }));

            
            #line default
            #line hidden
WriteLiteral("\r\n    </div>\r\n    <div");

WriteLiteral(" class=\"form-ctrl-desc\"");

WriteLiteral(">\r\n");

WriteLiteral("        ");

            
            #line 21 "..\..\Views\Expenses\EditorTemplates\ExpenseItemModel.cshtml"
   Write(Html.LabelFor(model => model.UnitPrice));

            
            #line default
            #line hidden
WriteLiteral("\r\n    </div>\r\n    <div");

WriteLiteral(" class=\"form-ctrl-area\"");

WriteLiteral(">\r\n");

WriteLiteral("        ");

            
            #line 24 "..\..\Views\Expenses\EditorTemplates\ExpenseItemModel.cshtml"
   Write(Html.TextBoxFor(model => model.UnitPrice, new { id = id + "-UnitPrice", @class = "form-ctrl float-input calcField-" + id }));

            
            #line default
            #line hidden
WriteLiteral("\r\n    </div>\r\n    <div");

WriteLiteral(" class=\"form-ctrl-desc\"");

WriteLiteral(">\r\n");

WriteLiteral("        ");

            
            #line 27 "..\..\Views\Expenses\EditorTemplates\ExpenseItemModel.cshtml"
   Write(Html.LabelFor(model => model.Price));

            
            #line default
            #line hidden
WriteLiteral("\r\n    </div>\r\n    <div");

WriteLiteral(" class=\"form-ctrl-area\"");

WriteLiteral(">\r\n");

WriteLiteral("        ");

            
            #line 30 "..\..\Views\Expenses\EditorTemplates\ExpenseItemModel.cshtml"
   Write(Html.TextBoxFor(model => model.Price, new { id = id + "-Price", @class = "form-ctrl float-input" }));

            
            #line default
            #line hidden
WriteLiteral("\r\n    </div>\r\n\r\n");

WriteLiteral("    ");

            
            #line 33 "..\..\Views\Expenses\EditorTemplates\ExpenseItemModel.cshtml"
Write(Html.Hidden("Id", id));

            
            #line default
            #line hidden
WriteLiteral("\r\n\r\n    <script");

WriteLiteral(" type=\"text/javascript\"");

WriteLiteral(">\r\n        $(function () {\r\n            expensesManager.manageEditFields(");

            
            #line 37 "..\..\Views\Expenses\EditorTemplates\ExpenseItemModel.cshtml"
                                        Write(id);

            
            #line default
            #line hidden
WriteLiteral(");\r\n        });\r\n    </script>\r\n</div>");

        }
    }
}
#pragma warning restore 1591
