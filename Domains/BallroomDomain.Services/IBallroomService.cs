using AIT.BallroomDomain.Model.Entities;
using System.Collections.Generic;

namespace AIT.BallroomDomain.Services
{
    public interface IBallroomService
    {
        void SaveLayout(string userId, Ballroom ballroom);
        void AssignToSeats(string userId, Dictionary<int, Dictionary<int, int>> map);
        List<Seat> CollectAssignedSeats(string userId);
        Ballroom GetBallroom(string userId);
    }
}
