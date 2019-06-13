using AIT.BallroomDomain.Services;
using AIT.GuestDomain.Model.Entities;
using AIT.GuestDomain.Services;
using AIT.GuestDomain.Services.RenewMembership.DTO;
using AIT.WebUIComponent.Services.AutoMapper;
using AIT.WebUIComponent.Services.Undo.DTO.Guests;
using System.Collections.Generic;
using System.Transactions;

namespace AIT.WebUIComponent.Services.Undo.Strategies
{
    public class PersonsUndoStrategy : GuestsUndoStrategyBase<List<PersonUndo>>
    {
        private readonly IPersonService _personService;
        private readonly IGuestAutoMapperService _autoMapperService;

        public PersonsUndoStrategy(IPersonService personService, IBallroomService ballroomService, IGuestAutoMapperService autoMapperService)
            : base(ballroomService)
        {
            _personService = personService;
            _autoMapperService = autoMapperService;
        }

        protected override object Process(List<PersonUndo> input)
        {
            int groupId;
            List<RenewItem> renewItems = _autoMapperService.MapDontKnowHowToNameIt(input);
            List<Person> entities = _autoMapperService.MapPersonUndoModels(input);
            
            using (var transaction = new TransactionScope())
            {
                groupId = RecreatePersons(entities, renewItems);        // returns groupId, it must be done first to get new ids for entities               
                RecreateSeatAssignment(input, entities);

                transaction.Complete();                
            }

            return groupId;
        }

        private int RecreatePersons(List<Person> entities, List<RenewItem> renewItems)
        {
            return _personService.Recreate(UserId, entities, renewItems);
        }
    }
}