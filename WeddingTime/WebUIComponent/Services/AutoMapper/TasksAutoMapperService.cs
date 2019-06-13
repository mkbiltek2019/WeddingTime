using AIT.TaskDomain.Model.Entities;
using AIT.WebUIComponent.Models.Tasks;
using AIT.WebUIComponent.Services.Undo.DTO.Tasks;
using AutoMapper;
using System.Collections.Generic;

namespace AIT.WebUIComponent.Services.AutoMapper
{
    public class TasksAutoMapperService : ITasksAutoMapperService
    {        
        public Task MapTaskModel(TaskModel model)
        {
            return Mapper.Map<TaskModel, Task>(model);
        }

        public List<TaskInfoModel> MapTasksEntities(List<Task> entities)
        {
            return Mapper.Map<List<Task>, List<TaskInfoModel>>(entities);
        }

        public TaskModel MapTaskEntityToTaskModel(Task entity)
        {
            return Mapper.Map<Task, TaskModel>(entity);
        }

        public TaskInfoModel MapTaskEntityToInfoModel(Task entity)
        {
            return Mapper.Map<Task, TaskInfoModel>(entity);
        }

        public TaskCard MapTaskCardModel(TaskCardModel model)
        {
            return Mapper.Map<TaskCardModel, TaskCard>(model);
        }

        public TaskCardModel MapTaskCardEntity(TaskCard entity)
        {
            return Mapper.Map<TaskCard, TaskCardModel>(entity);
        }

        public TaskUndo MapTaskEntityUndo(Task entity)
        {
            return Mapper.Map<Task, TaskUndo>(entity);
        }

        public Task MapTaskUndoModel(TaskUndo model)
        {
            return Mapper.Map<TaskUndo, Task>(model);
        }
    }
}