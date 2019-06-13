using AIT.UndoManagement.Infrastructure.Serialization;
using AIT.UndoManagement.Infrastructure.UndoInterface;
using AIT.UtilitiesComponents.Services;
using AIT.WebUIComponent.Services.Undo;
using AIT.WebUIComponent.Services.Undo.DTO.Expenses;
using AIT.WebUIComponent.Services.Undo.Enums;
using System.Collections.Generic;

namespace AIT.WebUIComponent.UndoCommands
{
    [XmlSerializationTypeKey("89636D74-0902-11E3-AA75-14F66188709B")]
    public class DeleteExpensesUndoCommand : IUndoCommand
    {
        public List<ExpenseUndo> Expenses { get; set; } 

        public object Execute()
        {
            var service = UnityService.Get().Container().GetInstance<IUndoCommandService>();
            return service.Execute(UndoAction.ExpensesUndo, Expenses);
        }

        public string Description
        {
            get { return string.Format("Liczba usuniętych wydatków: {0}", Expenses.Count); }
        }
    }
}