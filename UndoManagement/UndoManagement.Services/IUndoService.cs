using AIT.UndoManagement.Infrastructure.UndoInterface;
using System;

namespace AIT.UndoManagement.Services
{
    public interface IUndoService
    {
        void Delete(string userId, Guid uniqueKey);        
        object Undo(string userId, Guid uniqueKey);
        Guid RegisterUndoCommand(string userId, IUndoCommand undoCommand);
    }
}
