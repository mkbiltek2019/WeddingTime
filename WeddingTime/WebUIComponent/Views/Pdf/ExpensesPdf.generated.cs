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
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/Pdf/ExpensesPdf.cshtml")]
    public partial class _Views_Pdf_ExpensesPdf_cshtml : System.Web.Mvc.WebViewPage<AIT.WebUIComponent.Models.Pdf.ExpensesModel>
    {
        public _Views_Pdf_ExpensesPdf_cshtml()
        {
        }
        public override void Execute()
        {
            
            #line 3 "..\..\Views\Pdf\ExpensesPdf.cshtml"
  
    ViewBag.Title = "Expenses pdf";
    Layout = "~/Views/Shared/_PdfLayout.cshtml";

            
            #line default
            #line hidden
WriteLiteral("\r\n\r\n<table");

WriteLiteral(" style=\"margin-top: 20px\"");

WriteLiteral(@">
    <thead>
        <tr>
            <th>
                Tytuł/Opis
            </th>
            <th>
                Ilość
            </th>
            <th>
                Cena jednostkowa
            </th>
            <th>
                Wartość
            </th>
        </tr>
    </thead>
    <tbody>
");

            
            #line 26 "..\..\Views\Pdf\ExpensesPdf.cshtml"
        
            
            #line default
            #line hidden
            
            #line 26 "..\..\Views\Pdf\ExpensesPdf.cshtml"
         foreach (var expense in Model.Expenses)
        {

            
            #line default
            #line hidden
WriteLiteral("            <tr>\r\n                <td>\r\n");

WriteLiteral("                    ");

            
            #line 30 "..\..\Views\Pdf\ExpensesPdf.cshtml"
               Write(expense.Description);

            
            #line default
            #line hidden
WriteLiteral("\r\n                </td>\r\n                <td>\r\n");

            
            #line 33 "..\..\Views\Pdf\ExpensesPdf.cshtml"
                    
            
            #line default
            #line hidden
            
            #line 33 "..\..\Views\Pdf\ExpensesPdf.cshtml"
                     if (expense.Quantity != 0)
                    {
                        
            
            #line default
            #line hidden
            
            #line 35 "..\..\Views\Pdf\ExpensesPdf.cshtml"
                   Write(expense.Quantity);

            
            #line default
            #line hidden
            
            #line 35 "..\..\Views\Pdf\ExpensesPdf.cshtml"
                                         
                    }

            
            #line default
            #line hidden
WriteLiteral("                </td>\r\n                <td>\r\n");

            
            #line 39 "..\..\Views\Pdf\ExpensesPdf.cshtml"
                    
            
            #line default
            #line hidden
            
            #line 39 "..\..\Views\Pdf\ExpensesPdf.cshtml"
                     if (expense.UnitPrice != 0)
                    {
                        
            
            #line default
            #line hidden
            
            #line 41 "..\..\Views\Pdf\ExpensesPdf.cshtml"
                   Write(expense.UnitPrice);

            
            #line default
            #line hidden
            
            #line 41 "..\..\Views\Pdf\ExpensesPdf.cshtml"
                                          
                    }                    

            
            #line default
            #line hidden
WriteLiteral("                </td>\r\n                <td>\r\n");

WriteLiteral("                    ");

            
            #line 45 "..\..\Views\Pdf\ExpensesPdf.cshtml"
               Write(expense.Price);

            
            #line default
            #line hidden
WriteLiteral("\r\n                </td>\r\n            </tr>\r\n");

            
            #line 48 "..\..\Views\Pdf\ExpensesPdf.cshtml"
        }

            
            #line default
            #line hidden
WriteLiteral("        <tr>\r\n            <td");

WriteLiteral(" colspan=\"3\"");

WriteLiteral(" align=\"right\"");

WriteLiteral(" class=\"clear-border\"");

WriteLiteral(">\r\n                <h3>Suma:</h3>\r\n            </td>\r\n            <td");

WriteLiteral(" class=\"clear-border\"");

WriteLiteral(">\r\n                <h3>");

            
            #line 54 "..\..\Views\Pdf\ExpensesPdf.cshtml"
               Write(Model.Sum);

            
            #line default
            #line hidden
WriteLiteral("</h3>\r\n            </td>\r\n        </tr>\r\n    </tbody>\r\n</table>\r\n");

        }
    }
}
#pragma warning restore 1591
