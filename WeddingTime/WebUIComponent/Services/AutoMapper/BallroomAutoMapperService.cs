using AIT.BallroomDomain.Model.Entities;
using AIT.GuestDomain.Model.Entities;
using AIT.WebUIComponent.Models.Ballroom;
using AutoMapper;
using System.Collections.Generic;
using System;

namespace AIT.WebUIComponent.Services.AutoMapper
{
    public class BallroomAutoMapperService : IBallroomAutoMapperService
    {
        public BallroomModel MapBallroomEntity(Ballroom entity)
        {
            return Mapper.Map<Ballroom, BallroomModel>(entity);
        }

        public Ballroom MapBallroomModel(BallroomModel model)
        {
            return Mapper.Map<BallroomModel, Ballroom>(model);
        }

        public List<BallroomGroupModel> MapBallroomGroupEntities(List<Group> entities)
        {
            return Mapper.Map<List<Group>, List<BallroomGroupModel>>(entities);
        }

        public Dictionary<int, BallroomItem> MapBallroomItemModels(Dictionary<string, BallroomItemModel> items)
        {
            return Mapper.Map<Dictionary<string, BallroomItemModel>, Dictionary<int, BallroomItem>>(items);
        }
    }
}