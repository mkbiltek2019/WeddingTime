using AIT.BallroomDomain.Model.Entities;
using System.Data.Entity.ModelConfiguration;

namespace AIT.BallroomDomain.Infrastructure.DbContext.EntityConfig
{
    class BallroomItemEntityConfiguration : EntityTypeConfiguration<BallroomItem>
    {
        public BallroomItemEntityConfiguration()
        {
            Map<TableRect>(m => m.Requires("ItemType").HasValue("TableRect"));      // use expression to define name (take the name from class name)
            Map<TableRound>(m => m.Requires("ItemType").HasValue("TableRound"));
            Map<PillarRect>(m => m.Requires("ItemType").HasValue("PillarRect"));
            Map<PillarRound>(m => m.Requires("ItemType").HasValue("PillarRound"));
            Map<StageRect>(m => m.Requires("ItemType").HasValue("StageRect"));
            Map<StageHalfCircle>(m => m.Requires("ItemType").HasValue("StageHalfCircle"));
        }
    }
}
