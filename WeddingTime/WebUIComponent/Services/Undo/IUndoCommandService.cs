using AIT.WebUIComponent.Services.Undo.Enums;

namespace AIT.WebUIComponent.Services.Undo
{
    public interface IUndoCommandService
    {
        object Execute(UndoAction undoAction, object input);
    }
}
