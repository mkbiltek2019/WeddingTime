using AIT.BallroomDomain.Infrastructure.DbContext;
using AIT.BallroomDomain.Model.Entities;
using AIT.DomainUtilities;
using AIT.DomainUtilities.Repositories;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace AIT.BallroomDomain.Infrastructure.Repositories
{
    public class BallroomRepository : RepositoryBase<Ballroom, BallroomContext>, IBallroomRepository
    {
        public BallroomRepository(IUnitOfWork<BallroomContext> unitOfWork)
            : base(unitOfWork)
        {
        }

        public Ballroom Find(string userId)
        {
            return GetAll().SingleOrDefault(x => x.UserId == userId);
        }

        public IQueryable<TableBase> FindTablesWithAssignedSeats(string userId)
        {            
            return GetAll().Where(x => x.UserId == userId).SelectMany(x => x.BallroomItems).OfType<TableBase>().Include(x => x.Seats).Where(x => x.Seats.Any(n => n.PersonId.HasValue));
        }

        public IQueryable<TableBase> FindTablesById(string userId, List<int> tableIds)
        {
            return GetAll().Where(x => x.UserId == userId).SelectMany(x => x.BallroomItems).OfType<TableBase>().Include(x => x.Seats).Where(x => tableIds.Contains(x.Id));
        }       
    }
}
