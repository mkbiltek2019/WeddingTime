using AIT.BallroomDomain.Model.Entities;
using System.Collections.Generic;

namespace AIT.BallroomDomain.Services.LayoutService.Services
{
    internal interface IProcessTableSeatsService
    {
        void Process(TableBase toUpdate, List<Seat> viewSeats);
    }
}
