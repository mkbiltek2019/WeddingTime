using AIT.BallroomDomain.Model.Entities;
using AIT.DomainUtilities.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace AIT.BallroomDomain.Infrastructure.Repositories
{
    public interface IBallroomRepository : IRepositoryBase<Ballroom>
    {        
        Ballroom Find(string userId);                    
        IQueryable<TableBase> FindTablesWithAssignedSeats(string userId);              // method used when deleting persons or group
        IQueryable<TableBase> FindTablesById(string userId, List<int> tableIds);       // method used when undoing persons or group deletion
    }
}
