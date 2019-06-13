using System;

namespace AIT.UndoManagement.Model.DTO
{
    public class Undo
    {
        public int Id { get; set; }                
        public string UserId { get; set; }
        public string SerializedData { get; set; }
        public Guid UniqueKey { get; set; }
        public Guid TypeKey { get; set; }               // serialization type key assigned to each class that implements IUndoCommand
        public DateTime CreateDate { get; set; }
    }
}
