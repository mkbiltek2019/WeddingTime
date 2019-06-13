using AIT.DomainUtilities.Repositories;
using AIT.GuestDomain.Model.Entities;
using System.Linq;

namespace AIT.GuestDomain.Infrastructure.Repositories
{
    public interface IGroupRepository : IRepositoryBase<Group>
    {        
        Group LoadEagerWithPersonsAndInnerGroups(string userId, int groupId);
        Group LoadEager(string userId, int groupId);
        Group LoadEagerWithPersons(string userId, int groupId);
        Group LoadEagerWithPersonsMembership(string userId, int groupId);
        Group Load(string userId, int groupId);
        IQueryable<Group> FindAll(string userId);
        IQueryable<Group> FindAllWithPersons(string userId);
        int? GetMaxOrderNo(string userId);
    }
}
