using AIT.BallroomDomain.Services;
using AIT.GuestDomain.Services;
using AIT.UndoManagement.Services;
using AIT.WebUIComponent.Models.Ballroom;
using AIT.WebUIComponent.Services.AutoMapper;
using System;
using System.Web.Mvc;

namespace AIT.WebUIComponent.Controllers
{
    public class BallroomController : BaseController
    {
        private readonly IBallroomService _ballroomService;
        private readonly IBallroomAutoMapperService _autoMapperService;
        private readonly IUndoService _undoService;
        private readonly IGroupService _groupService;
        private readonly IPersonService _personService;

        public BallroomController(IBallroomService ballroomService,
                                  IBallroomAutoMapperService autoMapperService,
                                  IUndoService undoService,
                                  IGroupService guestService,
                                  IPersonService personService)
        {
            _ballroomService = ballroomService;
            _autoMapperService = autoMapperService;
            _undoService = undoService;
            _groupService = guestService;
            _personService = personService;
        }

        public ActionResult Index()
        {
            return View();       
        }

        [HttpGet]
        public JsonResult GetLayout()
        {           
            var ballroom = _ballroomService.GetBallroom(UserId);
            var model = ballroom != null ? _autoMapperService.MapBallroomEntity(ballroom) : new BallroomModel();

            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public void SaveLayout(BallroomModel ballroom)
        {
            var entity = _autoMapperService.MapBallroomModel(ballroom);
            _ballroomService.SaveLayout(UserId, entity);
        }

        [HttpGet]
        public JsonResult GetPersons()
        {
            var entities = _groupService.Get(UserId);
            var groups = _autoMapperService.MapBallroomGroupEntities(entities);

            return Json(groups, JsonRequestBehavior.AllowGet);
        }
    }
}
