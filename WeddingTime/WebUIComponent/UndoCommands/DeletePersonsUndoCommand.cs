using AIT.UndoManagement.Infrastructure.Serialization;
using AIT.UndoManagement.Infrastructure.UndoInterface;
using System.Collections.Generic;
using AIT.WebUIComponent.Services.Undo.DTO.Guests;
using AIT.UtilitiesComponents.Services;
using AIT.WebUIComponent.Services.Undo;
using AIT.WebUIComponent.Services.Undo.Enums;

namespace AIT.WebUIComponent.UndoCommands
{
    [XmlSerializationTypeKey("7F2044BA-C357-41B6-944E-83EFFF986B63")]
    public class DeletePersonsUndoCommand : IUndoCommand
    {
        public List<PersonUndo> Persons { get; set; }

        public object Execute()
        {
            var service = UnityService.Get().Container().GetInstance<IUndoCommandService>();
            return service.Execute(UndoAction.PersonsUndo, Persons);
        }

        public string Description
        {
            get { return string.Format("Liczba usuniętych gości: {0}", Persons.Count); }
        }
    }
}