using AIT.TaskDomain.Model.Entities;
using AIT.TaskDomain.Model.Enums;
using AIT.TaskDomain.Services;
using AIT.UndoManagement.Services;
using AIT.WebUIComponent.Models.Tasks;
using AIT.WebUIComponent.Models.Undo;
using AIT.WebUIComponent.Services.AutoMapper;
using AIT.WebUIComponent.UndoCommands;
using AIT.WebUtilities.Helpers;
using System;
using System.Linq;
using System.Transactions;
using System.Web.Mvc;

namespace AIT.WebUIComponent.Controllers
{
    public class TasksController : BaseController
    {
        private readonly ITaskService _taskService;
        private readonly ITasksAutoMapperService _autoMapperService;
        private readonly IUndoService _undoService;

        public TasksController(ITaskService taskService, ITasksAutoMapperService autoMapperService, IUndoService undoService)
        {
            _taskService = taskService;
            _autoMapperService = autoMapperService;
            _undoService = undoService;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetTasks()
        {
            var tasks = _taskService.Get(UserId);
            var model = _autoMapperService.MapTasksEntities(tasks);

            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult PrepareUpdate(int taskId)                                 // only fetch cards using eager load!
        {
            var task = _taskService.GetTask(UserId, taskId);
            var model = _autoMapperService.MapTaskEntityToTaskModel(task);

            var view = this.RenderPartialView("~/Views/Tasks/_Task.cshtml", model);
            return Json(view, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult CreateTask(string taskTitle)
        {
            var task = new Task { UserId = UserId, Title = taskTitle, State = TaskState.NewTask };
            _taskService.Create(task);

            var newTask = _autoMapperService.MapTaskEntityToInfoModel(task);
            return Json(newTask, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public void UpdateTask(TaskModel model)
        {
            RemoveCardTitleFromValidation();
            
            if (!ModelState.IsValid)
                throw new Exception();

            var task = _autoMapperService.MapTaskModel(model);
            _taskService.Update(UserId, task);
        }

        [HttpPost]
        public void UpdateState(int taskId, TaskState newState)
        {
            _taskService.UpdateState(UserId, taskId, newState);
        }

        [HttpPost]
        public JsonResult Delete(int taskId)
        {
            var userId = UserId;

            using (var transaction = new TransactionScope())
            {
                var taskToDelete = _taskService.GetToDelete(userId, taskId);
                var undoModel = _autoMapperService.MapTaskEntityUndo(taskToDelete);

                _taskService.Delete(taskToDelete);

                var undoCommand = new DeleteTaskUndoCommand { Task = undoModel };
                var key = _undoService.RegisterUndoCommand(userId, undoCommand);

                transaction.Complete();

                var viewData = this.RenderPartialView("~/Views/Undo/_Undo.cshtml", new UndoModel { Key = key, Description = undoCommand.Description });
                return Json(viewData, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult UpdateCard(int taskId, TaskCardModel model)               // perform update or add
        {            
            if (!ModelState.IsValid)
                throw new Exception();

            TaskCard taskCard = _autoMapperService.MapTaskCardModel(model);
            if (model.Id.HasValue)
                _taskService.UpdateTaskCard(UserId, taskId, taskCard);
            else
                _taskService.AddTaskCard(UserId, taskId, taskCard);

            var card = _autoMapperService.MapTaskCardEntity(taskCard);            // map it again to get count values and id
            return Json(card, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetCard(int taskId, int cardId)
        {
            TaskCard card = _taskService.GetCard(UserId, taskId, cardId);
            TaskCardModel model = _autoMapperService.MapTaskCardEntity(card);

            return Json(model, JsonRequestBehavior.AllowGet);
        }

        private void RemoveCardTitleFromValidation()
        {
            // don't need title of card here, just id is needed to check if some items were removed
            foreach (var cardTitle in ModelState.Keys.Where(x => x.Contains(".Title")).ToList())
            {
                ModelState.Remove(cardTitle);
            }
        }
    }
}
