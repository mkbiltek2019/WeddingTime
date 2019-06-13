using AIT.GuestDomain.Model.DTO;
using AIT.GuestDomain.Model.Entities;
using AIT.GuestDomain.Model.ValueObjects;
using AIT.GuestDomain.Services.RenewMembership.DTO;
using AIT.WebUIComponent.Models.Guests;
using AIT.WebUIComponent.Services.Undo.DTO.Guests;
using AutoMapper;
using System;
using System.Collections.Generic;

namespace AIT.WebUIComponent.AutoMapping
{
    public class GuestMap
    {
        public static void CreateMap()
        {
            Mapper.CreateMap<GroupModel, Group>();
            Mapper.CreateMap<PersonModel, Person>()
                .ForMember(n => n.UniqueKey, o => o.ResolveUsing(n => n.Id == 0 ? Guid.NewGuid() : (Guid?)null));       // we assign new key only if new item is created, this is indicated by id which is equals 0
            Mapper.CreateMap<InnerGroupMemberModel, InnerGroupMember>();

            Mapper.CreateMap<Group, GroupModel>();
            Mapper.CreateMap<Person, PersonModel>();
            Mapper.CreateMap<InnerGroupMember, InnerGroupMemberModel>();

            //undo map
            Mapper.CreateMap<Group, GroupUndo>();
            Mapper.CreateMap<Person, PersonUndo>();
            Mapper.CreateMap<InnerGroupMember, InnerGroupMemberUndo>();
            Mapper.CreateMap<InnerGroup, InnerGroupUndo>();
            

            Mapper.CreateMap<GroupUndo, Group>();
            Mapper.CreateMap<PersonUndo, Person>();
            Mapper.CreateMap<PersonUndo, RenewItem>()
               .ForMember(n => n.InnerGroupKey, o => o.ResolveUsing(n => n.IsInnerGroupMember ? n.InnerGroupMember.InnerGroupKey : (Guid?)null));
            Mapper.CreateMap<InnerGroupMemberUndo, InnerGroupMember>();
            Mapper.CreateMap<InnerGroupUndo, InnerGroup>();

            // ?????????????????????
            // ?????????????????????
            // ?????????????????????
            // new List<int> { 0 } - only for move betwwen groups - case the same as for update  ??????????????
            Mapper.CreateMap<PersonModificationModel, PersonModificationData>()
                .ForMember(n => n.ModifiedPersonIds, o => o.MapFrom(n => n.ModifiedPersonIds.Count == 0 ? new List<int> { 0 } : n.ModifiedPersonIds)); ;

            // insert 0 into PersonIdsToUpdate for newly added elements. The same situation takes
            // place when we add new persons to the group - but in that case we add 0 to list manually
            Mapper.CreateMap<UpdatePersonOrderNoModel, UpdatePersonOrderNo>()
                .ForMember(n => n.PersonIdsToUpdate, o => o.MapFrom(n => n.PersonIdsToUpdate.Count == 0 ? new List<int> { 0 } : n.PersonIdsToUpdate));


            
        }
    }
}