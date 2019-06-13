using AIT.ExpenseManagement.Services;
using AIT.GuestDomain.Services;
using AIT.WebUIComponent.Models.Pdf;
using AIT.WebUIComponent.Services.AutoMapper;
using AIT.WebUIComponent.Utilities;
using AIT.WebUtilities.ActionResults;
using AIT.WebUtilities.Helpers;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Web.Mvc;

namespace AIT.WebUIComponent.Controllers
{
    public class PdfController : BaseController
    {
        private readonly IGroupService _groupService;
        private readonly IExpenseService _expenseService;
        private readonly IPdfAutoMapperService _autoMapperService;

        public PdfController(IGroupService groupService, IExpenseService expenseService, IPdfAutoMapperService autoMapperService)
        {
            _groupService = groupService;
            _expenseService = expenseService;
            _autoMapperService = autoMapperService;
        }

        [HttpGet]
        public ActionResult GuestsPdf()
        {
            var entities = _groupService.Get(UserId);                             // should be getEager with persons!
            var model = _autoMapperService.MapGroupEntities(entities);
            return PdfActionResult("GuestsPdf", model);
        }

        [HttpGet]
        public ActionResult GuestsByStatusPdf()
        {
            var entities = _groupService.Get(UserId);                             // should be getEager with persons!
            var model = _autoMapperService.MapGroupEntitiesByStatus(entities);
            return PdfActionResult("GuestsByStatusPdf", model);
        }

        
        [HttpGet]
        public ActionResult ExpensesPdf()
        {
            var expenses = _expenseService.GetExpenses(UserId);
            var items = _autoMapperService.MapExpenseItems(expenses);
            var model = new ExpensesModel { Expenses = items };
            
            return PdfActionResult("ExpensesPdf", model);
        }
        
        [HttpGet]
        public ActionResult ExpensesByIdPdf(List<int> ids)
        {
            var expenses = _expenseService.GetExpenses(UserId, ids);
            var items = _autoMapperService.MapExpenseItems(expenses);
            var model = new ExpensesModel { Expenses = items };
            return PdfActionResult("ExpensesPdf", model);
        }


        private ActionResult PdfActionResult(string viewName, object model)
        {
            var renderedView = this.RenderView(viewName, model);
            var bytes = RenderPdf(renderedView);

            return new BinaryContentResult(bytes, "application/pdf");
        }

        private byte[] RenderPdf(string view)
        {
            using (var stream = new MemoryStream())
            {
                using (var document = new Document())
                {
                    using (var writer = PdfWriter.GetInstance(document, stream))
                    {     
                        document.Open();
                        AddImage(document);

                        using (var streamHtml = new MemoryStream(Encoding.UTF8.GetBytes(view)))
                        {
                            var fontPath = GetFontPath();
                            XMLWorkerHelper.GetInstance().ParseXHtml(writer, document, streamHtml, null, Encoding.UTF8, new UnicodeFontFactory(fontPath));
                        }

                        document.Close();
                    }
                }
                return stream.ToArray();
            }
        }

        private void AddImage(Document document)
        {
            Image png = Image.GetInstance(Server.MapPath("~/Images/logo.png"));
            png.Alignment = Element.ALIGN_CENTER;
            document.Add(png);
        }

        private string GetFontPath()
        {
            return Server.MapPath("~/Content/fonts/glyphicons-englebert-regular.ttf");
        }
    }
}
