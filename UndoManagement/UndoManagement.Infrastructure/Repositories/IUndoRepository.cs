using AIT.DomainUtilities.Repositories;
using AIT.UndoManagement.Model.DTO;
using System;

namespace AIT.UndoManagement.Infrastructure.Repositories
{
    public interface IUndoRepository : IRepositoryBase<Undo>
    {
        Undo Find(string userId, Guid uniqueKey);
    }
}
