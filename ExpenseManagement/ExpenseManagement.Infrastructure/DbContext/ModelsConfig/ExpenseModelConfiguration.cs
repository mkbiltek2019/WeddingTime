using AIT.ExpenseManagement.Model.DTO;
using System.Data.Entity.ModelConfiguration;

namespace AIT.ExpenseManagement.Infrastructure.DbContext.ModelsConfig
{
    class ExpenseModelConfiguration : EntityTypeConfiguration<Expense>
    {
        public ExpenseModelConfiguration()
        {
            ToTable("Expenses");
        }
    }
}
