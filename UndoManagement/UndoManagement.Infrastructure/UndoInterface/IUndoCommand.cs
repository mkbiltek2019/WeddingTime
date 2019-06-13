namespace AIT.UndoManagement.Infrastructure.UndoInterface
{
    public interface IUndoCommand
    {
        object Execute();
        string Description { get; }
    }
}
