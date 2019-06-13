using AIT.UndoManagement.Model.DTO;
using System.Data.Entity.ModelConfiguration;

namespace AIT.UndoManagement.Infrastructure.DbContext.ModelsConfig
{
    public class UndoModelConfiguration : EntityTypeConfiguration<Undo>
    {
        public UndoModelConfiguration()
        {
            ToTable("Undo");
        }
    }
}
