using AIT.TaskDomain.Model.Entities;
using AIT.WebUIComponent.Models.Tasks;
using AIT.WebUIComponent.Services.Undo.DTO.Tasks;
using System.Collections.Generic;

namespace AIT.WebUIComponent.Services.AutoMapper
{
    public interface ITasksAutoMapperService
    {
        Task MapTaskModel(TaskModel model);
        List<TaskInfoModel> MapTasksEntities(List<Task> entities);
        TaskModel MapTaskEntityToTaskModel(Task entity);
        TaskInfoModel MapTaskEntityToInfoModel(Task entity);
        TaskCard MapTaskCardModel(TaskCardModel model);
        TaskCardModel MapTaskCardEntity(TaskCard entity);

        TaskUndo MapTaskEntityUndo(Task entity);
        Task MapTaskUndoModel(TaskUndo model);
    }
}