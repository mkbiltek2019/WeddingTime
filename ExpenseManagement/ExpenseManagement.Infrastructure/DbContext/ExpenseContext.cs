using AIT.DomainUtilities.Database;
using AIT.ExpenseManagement.Infrastructure.DbContext.ModelsConfig;
using AIT.ExpenseManagement.Model.DTO;
using System.Data.Entity;

namespace AIT.ExpenseManagement.Infrastructure.DbContext
{
    public class ExpenseContext : BaseContext<ExpenseContext>
    {
        DbSet<Expense> Expenses { get; set; }
        DbSet<Budget> Budget { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new ExpenseModelConfiguration())
                                       .Add(new BudgetModelConfiguration());
        }
    }
}
