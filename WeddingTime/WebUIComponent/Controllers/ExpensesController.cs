using AIT.ExpenseManagement.Model.DTO;
using AIT.ExpenseManagement.Services;
using AIT.UndoManagement.Services;
using AIT.WebUIComponent.Models.Expenses;
using AIT.WebUIComponent.Models.Undo;
using AIT.WebUIComponent.Services.AutoMapper;
using AIT.WebUIComponent.UndoCommands;
using AIT.WebUtilities.Helpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web.Mvc;

namespace AIT.WebUIComponent.Controllers
{
    public class ExpensesController : BaseController
    {
        private readonly IExpenseService _expenseService;
        private readonly IExpenseAutoMapperService _autoMapperService;
        private readonly IUndoService _undoService;

        public ExpensesController(IExpenseService expenseService, IExpenseAutoMapperService autoMapperService, IUndoService undoService)
        {
            _expenseService = expenseService;
            _autoMapperService = autoMapperService;
            _undoService = undoService;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetExpensesInfo()
        {
            var userId = UserId;

            var budget = _expenseService.GetBudget(userId);
            var budgetModel = _autoMapperService.MapBudgetItem(budget);
            var totalPrice = _expenseService.GetTotalPrice(userId);

            var model = new ExpensesInfoModel { Budget = budgetModel, TotalPrice = totalPrice };
            var view = this.RenderPartialView("_ExpensesInfo", model);

            return Json(new { View = view, BudgetValue = budget.Value, TotalPrice = totalPrice }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public void UpdateBudget(BudgetModel model)
        {
            var budget = _autoMapperService.MapBudgetModel(model);

            if (budget.Id == 0)             // means that budget is not yet created
            {
                budget.UserId = UserId;
                _expenseService.CreateBudget(budget);
            }
            else
            {
                _expenseService.UpdateBudget(UserId, budget);
            }
        }

        [HttpGet]
        public JsonResult GetExpenses()
        {            
            List<Expense> items = _expenseService.GetExpenses(UserId);
            List<ExpenseItemModel> model = _autoMapperService.MapExpenseItems(items);

            var data = this.RenderPartialView("_Expenses", model);
            var expenses = items.ToDictionary(x => x.Id.ToString(), y => new { Price = y.Price, Desc = y.Description });

            return Json(new { View = data, Expenses = expenses }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public void UpdateExpenseOrderNo(UpdateExpenseOrderNoModel model)
        {
            var data = _autoMapperService.MapUpdateExpenseModel(model);
            data.UserId = UserId;

            _expenseService.UpdateExpensesOrderNo(data);
        }

        [HttpGet]
        public JsonResult CreateNewExpense(int nextIndex)
        {
            var model = new List<ExpenseItemModel> { new ExpenseItemModel { Id = nextIndex } };     // since new item has no id, pass next index value as id            
            var view = this.RenderPartialView("_CreateExpense", model).Replace("[0]", string.Format("[{0}]", nextIndex));

            return Json(view, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public void CreateExpenses(List<ExpenseItemModel> model)
        {
            //if (!ModelState.IsValid)                                  // write own IsValid method - I have a case where price is null if quantity and unit price is defined
            //    throw new Exception();                                // or rather write own validation attribute with specyfic coditions

            var expenses = _autoMapperService.MapExpenseModels(model);
            _expenseService.CreateExpenses(UserId, expenses);
        }

        [HttpGet]
        public JsonResult PrepareSingleEdit(int id)
        {
            var expense = _expenseService.GetExpense(UserId, id);
            var model = _autoMapperService.MapExpenseItem(expense);

            var view = this.RenderPartialView("_EditExpense", model);
            return Json(view, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult PrepareEdit(List<int> ids)
        {
            var expenses = _expenseService.GetExpenses(UserId, ids);
            var model = _autoMapperService.MapExpenseItems(expenses);
            
            var view = this.RenderPartialView("_EditExpenses", model);
            return Json(view, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult UpdateExpense(ExpenseItemModel model)
        {
            var expense = _autoMapperService.MapExpenseModel(model);
            _expenseService.UpdateExpenses(UserId, new List<Expense> { expense });

            var viewModel = _autoMapperService.MapExpenseItem(expense);
            return Content(JsonConvert.SerializeObject(viewModel));
        }

        [HttpPost]
        public ActionResult UpdateExpenses(List<ExpenseItemModel> items)
        {
            var expenses = _autoMapperService.MapExpenseModels(items);
            _expenseService.UpdateExpenses(UserId, expenses);

            var viewModel = _autoMapperService.MapExpenseItems(expenses);
            return Content(JsonConvert.SerializeObject(viewModel));
        }

        [HttpPost]
        public JsonResult DeleteExpenses(List<int> ids)
        {
            var userId = UserId;

            using (var transaction = new TransactionScope())
            {
                var deletedData = _expenseService.DeleteExpenses(userId, ids);
                var undoModel = _autoMapperService.MapExpenseItemsUndo(deletedData);

                var undoCommand = new DeleteExpensesUndoCommand { Expenses = undoModel };
                var key = _undoService.RegisterUndoCommand(userId, undoCommand);

                transaction.Complete();

                var view = this.RenderPartialView("~/Views/Undo/_Undo.cshtml", new UndoModel { Key = key, Description = undoCommand.Description });
                return Json(view, JsonRequestBehavior.AllowGet);
            }
        }
    }
}
