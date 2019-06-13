using AIT.DomainUtilities;
using AIT.GuestDomain.Infrastructure.DbContext;
using AIT.GuestDomain.Infrastructure.Repositories;
using AIT.GuestDomain.Model.Entities;
using System.Collections.Generic;
using System.Linq;

namespace AIT.GuestDomain.Services
{
    public class GroupService : IGroupService
    {        
        private IGroupRepository _groupRepository;
        private IUnitOfWork<GuestsContext> _unitOfWork;

        public GroupService(IGroupRepository groupRepository, IUnitOfWork<GuestsContext> unitOfWork)
        {            
            _groupRepository = groupRepository;
            _unitOfWork = unitOfWork;
        }

        public List<Group> Get(string userId)
        {
            return _groupRepository.FindAll(userId).ToList();
        }

        public Group Get(string userId, int groupId)
        {
            return _groupRepository.Load(userId, groupId);
        }

        public void Create(Group group)
        {
            _groupRepository.Insert(group);
            UpdateOrderNumber(group);

            _unitOfWork.Save();
        }

        public void Recreate(Group group)
        {
            _groupRepository.Insert(group);
            _unitOfWork.Save();
        }

        public void Delete(Group group)
        {
            _groupRepository.Delete(group);            
            _unitOfWork.Save();
        }

        public void Update(Group groupUpdate)
        {
            var group = _groupRepository.Load(groupUpdate.UserId, groupUpdate.Id);
            group.Update(groupUpdate.Name);

            _unitOfWork.Save();
        }

        // nie jest potrzebne wykonywanie kopii grupy, ponieważ tutaj mam tylko raz pobierane wszystkie dane, więc może to tak zostać
        // w przypadku gości ten proces jest bardziej skomplikowany, dlatego funkcja delete zwraca model.
        public Group GetToDelete(string userId, int groupId)
        {
            return _groupRepository.LoadEager(userId, groupId);
        }

        private void UpdateOrderNumber(Group group)
        {
            var maxOrderNo = _groupRepository.GetMaxOrderNo(group.UserId);
            group.UpdateOrderNo(maxOrderNo.HasValue ? maxOrderNo.Value + 1 : 0);
        }
    }
}
