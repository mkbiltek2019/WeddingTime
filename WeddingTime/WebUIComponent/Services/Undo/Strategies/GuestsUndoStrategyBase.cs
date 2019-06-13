using AIT.BallroomDomain.Services;
using AIT.GuestDomain.Model.Entities;
using AIT.WebUIComponent.Services.Undo.DTO.Guests;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AIT.WebUIComponent.Services.Undo.Strategies
{
    public abstract class GuestsUndoStrategyBase<TInput> : UndoStrategyBase<TInput>
    {
        private readonly IBallroomService _ballroomService;
        private string _userId;

        protected GuestsUndoStrategyBase(IBallroomService ballroomService)
        {
            _ballroomService = ballroomService;
        }

        protected void RecreateSeatAssignment(List<PersonUndo> input, List<Person> entities)
        {
            var map = new Dictionary<int, Dictionary<int, int>>();
            var result = input.Join(entities, i => i.UniqueKey, e => e.UniqueKey, (i, e) => new { Id = e.Id, TableId = i.TableId, SeatId = i.SeatId })
                              .Where(i => i.TableId.HasValue)
                              .ToLookup(i => i.TableId.Value);

            foreach (var item in result)
                map[item.Key] = item.ToDictionary(k => k.SeatId.Value, v => v.Id);

            _ballroomService.AssignToSeats(UserId, map);
        }
        
        protected string UserId
        {
            get { return _userId ?? (_userId = HttpContext.Current.User.Identity.GetUserId()); }
        }
    }
}