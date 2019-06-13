using AIT.BallroomDomain.Infrastructure.DbContext;
using AIT.BallroomDomain.Infrastructure.Repositories;
using AIT.BallroomDomain.Model.Entities;
using AIT.BallroomDomain.Services.LayoutService;
using AIT.DomainUtilities;
using AIT.UtilitiesComponents.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AIT.BallroomDomain.Services
{
    public class BallroomService : IBallroomService
    {
        private IBallroomRepository _ballroomRepository;
        private IUnitOfWork<BallroomContext> _unitOfWork;
        private IProcessLayoutService _layoutService;

        public BallroomService(IBallroomRepository ballroomRepository, IUnitOfWork<BallroomContext> unitOfWork, IProcessLayoutService layoutService)
        {
            _ballroomRepository = ballroomRepository;
            _unitOfWork = unitOfWork;
            _layoutService = layoutService;
        }

        public Ballroom GetBallroom(string userId)
        {
            return _ballroomRepository.Find(userId);
        }

        public List<Seat> CollectAssignedSeats(string userId)
        {
            IEnumerable<TableBase> tables = _ballroomRepository.FindTablesWithAssignedSeats(userId).AsEnumerable();
            return tables.SelectMany(x => x.Seats.Where(y => y.HasPersonAssigned)).ToList();
        }

        public void SaveLayout(string userId, Ballroom fromView)
        {
            var entity = _ballroomRepository.Find(userId).IfNull(()=> CreateBallroom(userId));
            entity.Update(fromView);

            _layoutService.ProcessLayout(entity, fromView.BallroomItems);
            _unitOfWork.Save();
        }

        public void AssignToSeats(string userId, Dictionary<int, Dictionary<int, int>> map)
        {
            var tableIds = new List<int>(map.Keys);
            List<TableBase> tables = _ballroomRepository.FindTablesById(userId, tableIds).ToList();

            tables.ForEach(table => {
                var assignmentMap = map[table.Id];
                table.Seats.ForEach(seat =>
                {
                    int personIdToAssign;
                    if (assignmentMap.TryGetValue(seat.Id, out personIdToAssign))
                        seat.AssignPerson(personIdToAssign);
                });
            });

            _unitOfWork.Save();
        }

        private Ballroom CreateBallroom(string userId)
        {
            var ballroom = new Ballroom
            {
                UserId = userId,                            // user id must be added in this step
                BallroomItems = new List<BallroomItem>()    // list has to be created to add new items
            };          
            _ballroomRepository.Insert(ballroom);

            return ballroom;
        }
    }
}
