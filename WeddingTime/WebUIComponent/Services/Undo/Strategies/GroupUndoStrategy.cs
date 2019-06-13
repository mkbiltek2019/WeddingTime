using AIT.BallroomDomain.Services;
using AIT.GuestDomain.Model.Entities;
using AIT.GuestDomain.Services;
using AIT.WebUIComponent.Services.AutoMapper;
using AIT.WebUIComponent.Services.Undo.DTO.Guests;
using System.Transactions;

namespace AIT.WebUIComponent.Services.Undo.Strategies
{
    public class GroupUndoStrategy : GuestsUndoStrategyBase<GroupUndo>
    {
        private readonly IGroupService _groupService;       
        private readonly IGuestAutoMapperService _autoMapperService;

        public GroupUndoStrategy(IGroupService groupService, IBallroomService ballroomService, IGuestAutoMapperService autoMapperService)
            : base(ballroomService)
        {
            _groupService = groupService;
            _autoMapperService = autoMapperService;
        }

        protected override object Process(GroupUndo input)
        {
            Group groupEntity = _autoMapperService.MapGroupUndoModel(input);

            using (var transaction = new TransactionScope())
            {
                _groupService.Recreate(groupEntity);
                RecreateSeatAssignment(input.Persons, groupEntity.Persons);

                transaction.Complete();
            }

            return new { GroupId = groupEntity.Id, Index = input.Index };
        }
    }
}