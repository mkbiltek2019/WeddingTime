using AIT.DomainUtilities;
using AIT.DomainUtilities.Repositories;
using AIT.GuestDomain.Infrastructure.DbContext;
using AIT.GuestDomain.Model.Entities;
using System.Data.Entity;
using System.Linq;

namespace AIT.GuestDomain.Infrastructure.Repositories
{
    public class GroupRepository : RepositoryBase<Group, GuestsContext>, IGroupRepository
    {
        public GroupRepository(IUnitOfWork<GuestsContext> unitOfWork)
            : base(unitOfWork)
        {
        }

        public Group LoadEagerWithPersonsAndInnerGroups(string userId, int groupId)
        {
            return GetAll().Include(x => x.Persons).Include(x => x.InnerGroups).Single(x => x.UserId == userId && x.Id == groupId);   
        }

        public Group LoadEager(string userId, int groupId)
        {
            return GetAll().Include(x => x.Persons.Select(n => n.InnerGroupMember)).Include(x => x.InnerGroups).Single(x => x.UserId == userId && x.Id == groupId);
        }

        public Group LoadEagerWithPersons(string userId, int groupId)
        {
            return GetAll().Include(x => x.Persons).Single(x => x.UserId == userId && x.Id == groupId);
        }

        public Group LoadEagerWithPersonsMembership(string userId, int groupId)
        {
            return GetAll().Include(x => x.Persons.Select(n => n.InnerGroupMember)).Single(x => x.UserId == userId && x.Id == groupId);
        }

        public Group Load(string userId, int groupId)
        {
            return GetAll().Single(n => n.Id == groupId);
        }

        public IQueryable<Group> FindAll(string userId)
        {
            return GetAll().Where(n => n.UserId == userId).OrderBy(n => n.OrderNo);
        }

        public IQueryable<Group> FindAllWithPersons(string userId)
        {
            return GetAll().Include(n => n.Persons).Where(n => n.UserId == userId).OrderBy(n => n.OrderNo);
        }

        public int? GetMaxOrderNo(string userId)
        {
            return GetAll().Where(n => n.UserId == userId).Max(n => n.OrderNo);
        }
    }
}
