using AIT.DomainUtilities;
using AIT.ExpenseManagement.Infrastructure.DbContext;
using AIT.ExpenseManagement.Infrastructure.Repositories;
using AIT.ExpenseManagement.Model.DTO;
using AIT.ExpenseManagement.Model.Enums;
using AIT.ExpenseManagement.Services.UpdateOrderNumber;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AIT.ExpenseManagement.Services
{
    public class ExpenseService : IExpenseService
    {
        private IExpenseRepository _expenseRepository;
        private IBudgetRepository _budgetRepository;
        private IUnitOfWork<ExpenseContext> _unitOfWork;

        public ExpenseService(IExpenseRepository expenseRepository, IBudgetRepository budgetRepository, IUnitOfWork<ExpenseContext> unitOfWork)
        {
            _expenseRepository = expenseRepository;
            _budgetRepository = budgetRepository;
            _unitOfWork = unitOfWork;
        }

        public void CreateBudget(Budget budget)
        {
            _budgetRepository.Insert(budget);
            _unitOfWork.Save();
        }

        public void UpdateBudget(string userId, Budget budgetUpdate)
        {
            var budget = _budgetRepository.Find(userId, budgetUpdate.Id);
            budget.Value = budgetUpdate.Value;

            _unitOfWork.Save();
        }

        public void CreateExpenses(string userId, List<Expense> expenses)
        {
            expenses.ForEach(n =>
            {
                n.UserId = userId;
                _expenseRepository.Insert(n);
            });

            UpdateOrderNo(new UpdateNewOrderNo
            {
                Expenses = expenses,
                UserId = userId,
                UpdateType = UpdateOrderNoType.Create
            });

            _unitOfWork.Save();
        }

        public void UpdateExpenses(string userId, List<Expense> expensesToUpdate)
        {
            var expenses = _expenseRepository.Find(userId, expensesToUpdate.Select(x => x.Id));

            expensesToUpdate.ForEach(n =>
            {
                var expense = expenses.Single(x => x.Id == n.Id);
                expense.Description = n.Description;
                expense.Quantity = n.Quantity;
                expense.UnitPrice = n.UnitPrice;
                expense.Price = n.Price;
            });

            _unitOfWork.Save();
        }

        public void UpdateExpensesOrderNo(UpdateOrderNoBase data)
        {
            UpdateOrderNo(data);
            _unitOfWork.Save();
        }

        public void Recreate(List<Expense> expenses)
        {
            expenses.ForEach(n => _expenseRepository.Insert(n));
            _unitOfWork.Save();
        }

        public decimal GetTotalPrice(string userId)
        {
            return _expenseRepository.GetTotalPrice(userId);
        }

        public Budget GetBudget(string userId)
        {
            return _budgetRepository.Find(userId) ?? new Budget();                  // return new object if budget has not been created yet for the user
        }

        public Expense GetExpense(string userId, int id)
        {
            return _expenseRepository.Find(userId, id);
        }

        public List<Expense> DeleteExpenses(string userId, List<int> idsToDelete)
        {
            var expenses = _expenseRepository.Find(userId, idsToDelete).ToList();

            expenses.ForEach(n => _expenseRepository.Delete(n));
            _unitOfWork.Save();

            return expenses;
        }

        public List<Expense> GetExpenses(string userId)
        {
            return _expenseRepository.Find(userId).ToList();
        }

        public List<Expense> GetExpenses(string userId, List<int> ids)
        {
            return _expenseRepository.Find(userId, ids).ToList();
        }

        private void UpdateOrderNo(UpdateOrderNoBase data)
        {
            var updateOrderNoService = new UpdateOrderNoService(_expenseRepository);
            updateOrderNoService.UpdateOrderNo(data);
        }
    }
}
