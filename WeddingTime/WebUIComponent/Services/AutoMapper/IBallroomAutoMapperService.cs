using AIT.BallroomDomain.Model.Entities;
using AIT.GuestDomain.Model.Entities;
using AIT.WebUIComponent.Models.Ballroom;
using System.Collections.Generic;

namespace AIT.WebUIComponent.Services.AutoMapper
{
    public interface IBallroomAutoMapperService
    {
        BallroomModel MapBallroomEntity(Ballroom entity);
        Ballroom MapBallroomModel(BallroomModel entity);
        List<BallroomGroupModel> MapBallroomGroupEntities(List<Group> entities);
        Dictionary<int, BallroomItem> MapBallroomItemModels(Dictionary<string, BallroomItemModel> items);
    }
}
