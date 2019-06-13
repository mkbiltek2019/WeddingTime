using AIT.UndoManagement.Infrastructure.Serialization;
using AIT.UndoManagement.Infrastructure.UndoInterface;
using AIT.UtilitiesComponents.Services;
using AIT.WebUIComponent.Services.Undo;
using AIT.WebUIComponent.Services.Undo.DTO.Guests;
using AIT.WebUIComponent.Services.Undo.Enums;

namespace AIT.WebUIComponent.UndoCommands
{
    [XmlSerializationTypeKey("2F10D838-4F68-4532-B899-6EBE661555C0")]
    public class DeleteGroupUndoCommand : IUndoCommand
    {
        public GroupUndo Group { get; set; }

        public object Execute()
        {
            var service = UnityService.Get().Container().GetInstance<IUndoCommandService>();
            return service.Execute(UndoAction.GroupUndo, Group);
        }

        public string Description
        {
            get { return string.Format("Usunięto następującą grupę: {0}", Group.Name); }
        }
    }
}