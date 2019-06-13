using AIT.BallroomDomain.Model.Entities;
using AIT.BallroomDomain.Services;
using AIT.GuestDomain.Model.DTO;
using AIT.GuestDomain.Model.Entities;
using AIT.GuestDomain.Services;
using AIT.UndoManagement.Services;
using AIT.WebUIComponent.Models.Guests;
using AIT.WebUIComponent.Models.Undo;
using AIT.WebUIComponent.Services.AutoMapper;
using AIT.WebUIComponent.Services.Undo.DTO.Guests;
using AIT.WebUIComponent.UndoCommands;
using AIT.WebUtilities.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web.Mvc;

namespace AIT.WebUIComponent.Controllers
{
    public class GuestsController : BaseController
    {
        private readonly IGroupService _groupService;
        private readonly IPersonService _personService;
        private readonly IGuestAutoMapperService _autoMapperService;
        private readonly IUndoService _undoService;
        private readonly IBallroomService _ballroomService;             // needed for undo

        public GuestsController(IGroupService groupService, 
                                IPersonService personService, 
                                IGuestAutoMapperService autoMapperService, 
                                IUndoService undoService,
                                IBallroomService ballroomService)
        {
            _groupService = groupService;
            _personService = personService;
            _autoMapperService = autoMapperService;
            _undoService = undoService;
            _ballroomService = ballroomService;
        }

        public ActionResult Index()
        {
            return View();
        }

        #region Group Related

        [HttpPost]
        public JsonResult AddGroup(GroupModel model)
        {
            if (!ModelState.IsValid)
                throw new Exception();                                              // TODO

            Group group = _autoMapperService.MapGroupModel(model);
            group.SetUserId(UserId);

            _groupService.Create(group);

            GroupModel viewData = _autoMapperService.MapGroupEntity(group);
            string view = this.RenderPartialView("~/Views/Guests/_Group.cshtml", viewData);

            return Json(new { Id = viewData.Id, Html = view }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetGroups()
        {
            List<Group> groups = _groupService.Get(UserId);                                   // here is order by orderno
            List<GroupModel> model = _autoMapperService.MapGroupEntities(groups);

            var groupIds = model.Select(n => n.Id);                                             // needed to get persons list for concrete group
            string view = this.RenderPartialView("~/Views/Guests/_Groups.cshtml", model);

            return Json(new { Html = view, GroupIds = groupIds }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetGroup(int groupId)
        {
            Group group = _groupService.Get(UserId, groupId);
            GroupModel model = _autoMapperService.MapGroupEntity(group);

            string view = this.RenderPartialView("~/Views/Guests/_Group.cshtml", model);

            return Json(view, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult DeleteGroup(int groupId, int index)                                   // index is group position on the page
        {
            var userId = UserId;

            using (var transaction = new TransactionScope())
            {
                List<Seat> assignedSeats = _ballroomService.CollectAssignedSeats(userId);     // has to be fetched before deleting group because inside seats table there is person id reference and delete rule is set to set null

                Group groupToDelete = _groupService.GetToDelete(userId, groupId);
                GroupUndo undoModel = CreateUnodModel(groupToDelete, assignedSeats, index);

                _groupService.Delete(groupToDelete);

                var undoCommand = new DeleteGroupUndoCommand { Group = undoModel };
                Guid key = _undoService.RegisterUndoCommand(userId, undoCommand);

                transaction.Complete();

                string viewData = this.RenderPartialView("~/Views/Undo/_Undo.cshtml", new UndoModel { Key = key, Description = undoCommand.Description });
                return Json(viewData, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public void UpdateGroup(GroupModel model)
        {
            if (!ModelState.IsValid)
                throw new Exception();                                              // TODO

            Group group = _autoMapperService.MapGroupModel(model);
            group.SetUserId(UserId);

            _groupService.Update(group);
        }

        private GroupUndo CreateUnodModel(Group deletedData, List<Seat> assignedSeats, int index)
        {
            GroupUndo undoModel = _autoMapperService.MapGroupEntitiesUndo(deletedData);
            undoModel.Index = index;

            UpdateUndoModelWithAssignedSeat(undoModel.Persons, assignedSeats);
            return undoModel;
        }

        #endregion

        #region Guest Related

        [HttpGet]
        public JsonResult GetPersons(int groupId)
        {
            List<Person> persons = _personService.Get(UserId, groupId);                     // here is order by order no
            List<PersonModel> members = _autoMapperService.MapPersonEntities(persons);

            var model = new MembersModel { UniqueId = groupId, Members = members };
            string view = this.RenderPartialView("_PersonsList", model);

            return Json(new { View = view, Members = members }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public void CreatePersons(List<PersonModel> model)
        {
            if (!ModelState.IsValid)
                throw new Exception();                                              // TODO

            List<Person> persons = _autoMapperService.MapPersonModels(model);
            _personService.Create(UserId, persons);
        }

        [HttpGet]
        public JsonResult CreateNewPerson(int groupId, int nextIndex)
        {
            var model = new List<PersonModel> { new PersonModel { GroupId = groupId, Id = nextIndex } };        // since new item has no id, pass next index value as id
            string data = this.RenderPartialView("_CreatePerson", model).Replace("[0]", string.Format("[{0}]", nextIndex));                                                                     

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult DeletePersons(PersonModificationModel model)
        {
            string userId = UserId;
            PersonModificationData data = _autoMapperService.MapPersonModificationModel(model);

            using (var transaction = new TransactionScope())
            {
                List<Seat> assignedSeats = _ballroomService.CollectAssignedSeats(userId);        // has to be fetched before deleting persons because inside seats table there is person id reference and delete rule is set to set null

                PersonsDeletedData deletedData = _personService.DeleteAndModify(userId, data);
                List<PersonUndo> undoModel = CreateUnodModel(deletedData, assignedSeats);

                var undoCommand = new DeletePersonsUndoCommand { Persons = undoModel };
                Guid key = _undoService.RegisterUndoCommand(userId, undoCommand);

                transaction.Complete();

                string viewData = this.RenderPartialView("~/Views/Undo/_Undo.cshtml", new UndoModel { Key = key, Description = undoCommand.Description });
                return Json(viewData, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public void CreateInnerGroup(PersonModificationModel personModel, UpdatePersonOrderNoModel updateModel)           //tutaj musi wpasc model dla update z wypelnionym elementem baseItemId
        {            
            PersonModificationData personData = _autoMapperService.MapPersonModificationModel(personModel);
            personData.InnerGroupKey = Guid.NewGuid();

            UpdatePersonOrderNo updateData = _autoMapperService.MapUpdatePersonModel(updateModel);

            _personService.CreateInnerGroupAndMembership(UserId, personData, updateData);
        }

        [HttpPost]
        public void AddInnerGroupMemeber(PersonModificationModel personModel, UpdatePersonOrderNoModel updateModel)
        {
            PersonModificationData personData = _autoMapperService.MapPersonModificationModel(personModel);
            UpdatePersonOrderNo updateData = _autoMapperService.MapUpdatePersonModel(updateModel);

            _personService.AddInnerGroupMember(UserId, personData, updateData);
        }

        [HttpPost]
        public void DetachInnerGroupMemeber(PersonModificationModel personModel, UpdatePersonOrderNoModel updateModel)
        {
            PersonModificationData personData = _autoMapperService.MapPersonModificationModel(personModel);
            UpdatePersonOrderNo updateData = _autoMapperService.MapUpdatePersonModel(updateModel);

            _personService.DetachMember(UserId, personData, updateData);
        }

        [HttpGet]
        public JsonResult PrepareEdit(int groupId, List<int> ids)
        {
            List<Person> persons = _personService.Get(UserId, groupId, ids);

            List<PersonModel> model = _autoMapperService.MapPersonEntities(persons);
            string data = this.RenderPartialView("_EditPersons", model);

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public void UpdatePersons(List<PersonModel> items)
        {
            List<Person> persons = _autoMapperService.MapPersonModels(items);
            _personService.Update(UserId, persons);
        }

        [HttpPost]
        public void UpdatePersonOrderNo(int groupId, UpdatePersonOrderNoModel model)
        {
            UpdatePersonOrderNo data = _autoMapperService.MapUpdatePersonModel(model);
            _personService.UpdateOrderNo(UserId, groupId, data);
        }

        [HttpPost]
        public void MoveBetweenGroups(int groupIdTo, PersonModificationModel modelFrom, UpdatePersonOrderNoModel updateModel)
        {
            PersonModificationData dataFrom = _autoMapperService.MapPersonModificationModel(modelFrom);
            UpdatePersonOrderNo updateData = _autoMapperService.MapUpdatePersonModel(updateModel);

            _personService.MoveBetweenGroups(UserId, groupIdTo, dataFrom, updateData);
        }

        private List<PersonUndo> CreateUnodModel(PersonsDeletedData deletedData, List<Seat> assignedSeats)
        {
            List<PersonUndo> undoModel = _autoMapperService.MapPersonEntitiesUndo(deletedData.Deleted);
            UpdateUndoModelWithRelatedPerson(undoModel, deletedData.Related.PersonsToModify);
            UpdateUndoModelWithAssignedSeat(undoModel, assignedSeats);

            return undoModel;
        }

        //update each personUndo item with information which person shoul be updated if undo action will be performed
        //this is case where we have 2 persosn in group and one of them will be deleted - we delete group memebership for the second one
        //if undo will be performed we must reasign the inner group key to appropriate person id.
        private void UpdateUndoModelWithRelatedPerson(IEnumerable<PersonUndo> undoModel, IEnumerable<PersonItem> personsToModify)
        {
            foreach (var personUndo in undoModel.Where(personUndo => personUndo.IsInnerGroupMember && personsToModify.Select(x => x.InnerGroupKey).Contains(personUndo.InnerGroupMember.InnerGroupKey)))
            {
                personUndo.RelatedPersonId = personsToModify.Single(n => n.InnerGroupKey == personUndo.InnerGroupMember.InnerGroupKey).Id;
            }            
        }

        private void UpdateUndoModelWithAssignedSeat(List<PersonUndo> undoModel, List<Seat> assignedSeats)
        {
            foreach (var personUndo in undoModel.Where(personUndo => assignedSeats.Select(s => s.PersonId).Contains(personUndo.Id)))
            {
                Seat seat = assignedSeats.Single(s => s.PersonId == personUndo.Id);
                personUndo.TableId = seat.TableId;
                personUndo.SeatId = seat.Id;
            }
        }

        #endregion
    }
}
