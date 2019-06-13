using AIT.TaskDomain.Model.Entities;
using AIT.TaskDomain.Services;
using AIT.WebUIComponent.Services.AutoMapper;
using AIT.WebUIComponent.Services.Undo.DTO.Tasks;

namespace AIT.WebUIComponent.Services.Undo.Strategies
{
    public class TaskUndoStrategy : UndoStrategyBase<TaskUndo>
    {
        private readonly ITaskService _taskService;
        private readonly ITasksAutoMapperService _autoMapperService;

        public TaskUndoStrategy(ITaskService taskService, ITasksAutoMapperService autoMapperService)
        {
            _taskService = taskService;
            _autoMapperService = autoMapperService;
        }

        protected override object Process(TaskUndo input)
        {
            Task entity = _autoMapperService.MapTaskUndoModel(input);
            _taskService.Create(entity);

            return _autoMapperService.MapTaskEntityToInfoModel(entity);
        }
    }
}