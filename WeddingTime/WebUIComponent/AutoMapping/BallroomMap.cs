using AIT.BallroomDomain.Model.Entities;
using AIT.BallroomDomain.Model.Enums;
using AIT.GuestDomain.Model.Entities;
using AIT.WebUIComponent.Models.Ballroom;
using AutoMapper;
using System.Collections.Generic;
using System.Linq;

namespace AIT.WebUIComponent.AutoMapping
{
    public class BallroomMap
    {
        public static void CreateMap()
        {
            Mapper.CreateMap<Ballroom, BallroomModel>();

            Mapper.CreateMap<BallroomItem, BallroomItemModel>()
                .Include<TableRect, TableRectModel>()
                .Include<TableRound, TableRoundModel>()
                .Include<PillarRect, PillarRectModel>()
                .Include<PillarRound, PillarRoundModel>()
                .Include<StageRect, StageRectModel>()
                .Include<StageHalfCircle, StageHalfCircleModel>();

            Mapper.CreateMap<TableRect, TableRectModel>()
                .ForMember(n => n.ItemType, o => o.UseValue(ItemType.TableRect))
                .ForMember(n => n.Seats, o => o.ResolveUsing(x => x.Seats.ToDictionary(k => k.Id, v => v)));

            Mapper.CreateMap<TableRound, TableRoundModel>()
                .ForMember(n => n.ItemType, o => o.UseValue(ItemType.TableRound))
                .ForMember(n => n.Seats, o => o.ResolveUsing(x => x.Seats.ToDictionary(k => k.Id, v => v)));

            Mapper.CreateMap<PillarRect, PillarRectModel>()
                .ForMember(n => n.ItemType, o => o.UseValue(ItemType.PillarRect));

            Mapper.CreateMap<PillarRound, PillarRoundModel>()
                .ForMember(n => n.ItemType, o => o.UseValue(ItemType.PillarRound));
            
            Mapper.CreateMap<StageRect, StageRectModel>()
                .ForMember(n => n.ItemType, o => o.UseValue(ItemType.StageRect));

            Mapper.CreateMap<StageHalfCircle, StageHalfCircleModel>()
                .ForMember(n => n.ItemType, o => o.UseValue(ItemType.StageHalfCircle));

            Mapper.CreateMap<Seat, RectSeatModel>()
                .ForMember(n => n.TakenBy, o => o.ResolveUsing(x => x.TakenBy))
                .ForMember(n => n.Occupied, o => o.ResolveUsing(x => x.PersonId.HasValue || x.TakenBy.HasValue))
                .ForMember(n => n.Location, o => o.ResolveUsing(x => x.Location));
            Mapper.CreateMap<Seat, RoundSeatModel>()
                .ForMember(n => n.TakenBy, o => o.ResolveUsing(x => x.TakenBy))
                .ForMember(n => n.Occupied, o => o.ResolveUsing(x => x.PersonId.HasValue || x.TakenBy.HasValue));

            Mapper.CreateMap<Group, BallroomGroupModel>()
                .ForMember(n => n.Persons, o => o.ResolveUsing(x => x.Persons.ToDictionary(k => k.Id, v => v)));
            Mapper.CreateMap<Person, PersonModel>();

            // --------------------------------------------------------------------------------------

            Mapper.CreateMap<BallroomModel, Ballroom>();

            Mapper.CreateMap<BallroomItemModel, BallroomItem>()
               .Include<TableRectModel, TableRect>()
               .Include<TableRoundModel,TableRound>()
               .Include<PillarRectModel, PillarRect>()
               .Include<PillarRoundModel, PillarRound>()
               .Include<StageRectModel, StageRect>()
               .Include<StageHalfCircleModel, StageHalfCircle>();

            Mapper.CreateMap<StageRectModel, StageRect>();
            Mapper.CreateMap<StageHalfCircleModel, StageHalfCircle>();
            Mapper.CreateMap<PillarRectModel, PillarRect>();
            Mapper.CreateMap<PillarRoundModel, PillarRound>();
            
            Mapper.CreateMap<TableRectModel, TableRect>();
            Mapper.CreateMap<KeyValuePair<string, RectSeatModel>, Seat>()
                .ConstructUsing(x => Mapper.Map<RectSeatModel, Seat>(x.Value))
                .ForMember(n => n.Id, o => o.ResolveUsing(x => x.Key));
            Mapper.CreateMap<RectSeatModel, Seat>()
                .ForMember(n => n.TakenBy, o => o.ResolveUsing(x => x.TakenBy))
                .ForMember(n => n.Location, o => o.ResolveUsing(x => x.Location));

            Mapper.CreateMap<TableRoundModel, TableRound>();
            Mapper.CreateMap<KeyValuePair<string, RoundSeatModel>, Seat>()
                .ConstructUsing(x => Mapper.Map<RoundSeatModel, Seat>(x.Value))
                .ForMember(n => n.Id, o => o.ResolveUsing(x => x.Key));
            Mapper.CreateMap<RoundSeatModel, Seat>()
               .ForMember(n => n.TakenBy, o => o.ResolveUsing(x => x.TakenBy));
        }
    }
}