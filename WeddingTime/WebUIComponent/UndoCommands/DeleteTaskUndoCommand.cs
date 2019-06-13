using AIT.UndoManagement.Infrastructure.Serialization;
using AIT.UndoManagement.Infrastructure.UndoInterface;
using AIT.UtilitiesComponents.Services;
using AIT.WebUIComponent.Services.Undo;
using AIT.WebUIComponent.Services.Undo.DTO.Tasks;
using AIT.WebUIComponent.Services.Undo.Enums;

namespace AIT.WebUIComponent.UndoCommands
{
    [XmlSerializationTypeKey("2BB626AF-11F5-4DF8-939E-84E2F4B4C669")]
    public class DeleteTaskUndoCommand : IUndoCommand
    {
        public TaskUndo Task { get; set; } 

        public object Execute()
        {
            var service = UnityService.Get().Container().GetInstance<IUndoCommandService>();
            return service.Execute(UndoAction.TaskUndo, Task);
        }

        public string Description
        {
            get { return string.Format("Usunięto następujące zadanie: {0}", Task.Title); }
        }
    }
}