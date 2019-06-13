using AIT.UndoManagement.Services;
using System;
using System.Web.Mvc;

namespace AIT.WebUIComponent.Controllers
{
    public class UndoController : BaseController
    {
        private readonly IUndoService _undoService;

        public UndoController(IUndoService undoService)
        {
            _undoService = undoService;
        }

        [HttpPost]
        public JsonResult Undo(Guid key)
        {
            string userId = UserId;

            object data = _undoService.Undo(userId, key);
            _undoService.Delete(userId, key);

            return Json(data ?? new object(), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public void Delete(Guid key)
        {
            _undoService.Delete(UserId, key);
        }
    }
}
