using AIT.GuestDomain.Model.DTO;
using AIT.GuestDomain.Model.Entities;
using AIT.GuestDomain.Services.RenewMembership.DTO;
using AIT.WebUIComponent.Models.Guests;
using AIT.WebUIComponent.Services.Undo.DTO.Guests;
using System.Collections.Generic;

namespace AIT.WebUIComponent.Services.AutoMapper
{
    public interface IGuestAutoMapperService
    {
        Group MapGroupModel(GroupModel model);
        GroupModel MapGroupEntity(Group entity);

        List<GroupModel> MapGroupEntities(List<Group> groupEntities);
        List<PersonModel> MapPersonEntities(List<Person> personEntities);
        List<Person> MapPersonModels(List<PersonModel> items);

        // undo mapping
        List<PersonUndo> MapPersonEntitiesUndo(List<Person> personEntities);
        List<Person> MapPersonUndoModels(List<PersonUndo> items);
        List<RenewItem> MapDontKnowHowToNameIt(List<PersonUndo> items);

        GroupUndo MapGroupEntitiesUndo(Group groupEntity);
        Group MapGroupUndoModel(GroupUndo groupUndo);

        // after refactoring
        PersonModificationData MapPersonModificationModel(PersonModificationModel model);
        UpdatePersonOrderNo MapUpdatePersonModel(UpdatePersonOrderNoModel model);       
    }
}
