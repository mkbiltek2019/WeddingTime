using AIT.DomainUtilities;
using AIT.DomainUtilities.Repositories;
using AIT.UndoManagement.Infrastructure.DbContext;
using AIT.UndoManagement.Model.DTO;
using System;
using System.Linq;

namespace AIT.UndoManagement.Infrastructure.Repositories
{
    public class UndoRepository : RepositoryBase<Undo, UndoContext>, IUndoRepository
    {
        public UndoRepository(IUnitOfWork<UndoContext> unitOfWork)
            : base(unitOfWork)
        {
        }

        public Undo Find(string userId, Guid uniqueKey)
        {
            return GetAll().Single(n => n.UserId == userId && n.UniqueKey == uniqueKey);
        }
    }
}
