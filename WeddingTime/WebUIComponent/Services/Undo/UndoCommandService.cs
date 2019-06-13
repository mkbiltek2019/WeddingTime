using AIT.UtilitiesComponents.Strategy;
using AIT.WebUIComponent.Services.Undo.Enums;
using AIT.WebUIComponent.Services.Undo.Strategies;
using SimpleInjector;

namespace AIT.WebUIComponent.Services.Undo
{    
    public class UndoCommandService : IUndoCommandService
    {
        private Container _container;
        private IFunctionStrategyService<UndoAction, object, object> _undoStrategy;

        public UndoCommandService(Container container, IFunctionStrategyService<UndoAction, object, object> undoStrategy)
        {
            _container = container;
            _undoStrategy = undoStrategy;
            
            InitializeUndoStrategy();
        }

        private void InitializeUndoStrategy()
        {
            _undoStrategy.AddStrategy(UndoAction.PersonsUndo, _container.GetInstance<PersonsUndoStrategy>().Process)
                         .AddStrategy(UndoAction.GroupUndo, _container.GetInstance<GroupUndoStrategy>().Process)
                         .AddStrategy(UndoAction.ExpensesUndo, _container.GetInstance<ExpensesUndoStrategy>().Process)
                         .AddStrategy(UndoAction.TaskUndo, _container.GetInstance<TaskUndoStrategy>().Process);                         
        }

        public object Execute(UndoAction undoAction, object input)
        {
            return _undoStrategy.Execute(undoAction, input);
        }
    }
}