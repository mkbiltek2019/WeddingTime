using AIT.ExpenseManagement.Model.DTO;
using System.Data.Entity.ModelConfiguration;

namespace AIT.ExpenseManagement.Infrastructure.DbContext.ModelsConfig
{
    class BudgetModelConfiguration : EntityTypeConfiguration<Budget>
    {
        public BudgetModelConfiguration()
        {
            ToTable("Budget");
        }
    }
}
