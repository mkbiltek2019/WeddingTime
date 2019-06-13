using AIT.GuestDomain.Model.DTO;
using AIT.GuestDomain.Model.Entities;
using AIT.GuestDomain.Services.RenewMembership.DTO;
using AIT.WebUIComponent.Models.Guests;
using AIT.WebUIComponent.Services.Undo.DTO.Guests;
using AutoMapper;
using System.Collections.Generic;

namespace AIT.WebUIComponent.Services.AutoMapper
{
    public class GuestAutoMapperService : IGuestAutoMapperService
    {
        public Group MapGroupModel(GroupModel model)
        {
            return Mapper.Map<GroupModel, Group>(model);
        }

        public GroupModel MapGroupEntity(Group entity)
        {
            return Mapper.Map<Group, GroupModel>(entity);
        }

        public List<GroupModel> MapGroupEntities(List<Group> groupEntities)
        {
            return Mapper.Map<List<Group>, List<GroupModel>>(groupEntities);
        }

        public List<PersonModel> MapPersonEntities(List<Person> personEntities)
        {
            return Mapper.Map<List<Person>, List<PersonModel>>(personEntities);
        }

        public List<Person> MapPersonModels(List<PersonModel> items)
        {
            return Mapper.Map<List<PersonModel>, List<Person>>(items);
        }

        // undo
        public List<PersonUndo> MapPersonEntitiesUndo(List<Person> personEntities)
        {
            return Mapper.Map<List<Person>, List<PersonUndo>>(personEntities);
        }

        public List<Person> MapPersonUndoModels(List<PersonUndo> items)
        {
            return Mapper.Map<List<PersonUndo>, List<Person>>(items);
        }

        public List<RenewItem> MapDontKnowHowToNameIt(List<PersonUndo> items)
        {
            return Mapper.Map<List<PersonUndo>, List<RenewItem>>(items);
        }

        public GroupUndo MapGroupEntitiesUndo(Group groupEntity)
        {
            return Mapper.Map<Group, GroupUndo>(groupEntity);
        }

        public Group MapGroupUndoModel(GroupUndo groupUndo)
        {
            return Mapper.Map<GroupUndo, Group>(groupUndo);
        }

        // after refactoring

        public PersonModificationData MapPersonModificationModel(PersonModificationModel model)
        {
            return Mapper.Map<PersonModificationModel, PersonModificationData>(model);
        }

        public UpdatePersonOrderNo MapUpdatePersonModel(UpdatePersonOrderNoModel model)
        {
            return Mapper.Map<UpdatePersonOrderNoModel, UpdatePersonOrderNo>(model);
        }
    }
}