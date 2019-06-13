using AIT.BallroomDomain.Model.Entities;
using AIT.BallroomDomain.Services.LayoutService.Services;
using System.Collections.Generic;

namespace AIT.BallroomDomain.Services.LayoutService.Strategies
{
    internal class ProcessTableItemStrategy : ProcessItemStrategy<TableBase>
    {
        private readonly IProcessTableSeatsService _seatsItemService;

        public ProcessTableItemStrategy(IProcessTableSeatsService seatsItemService)
        {
            _seatsItemService = seatsItemService;
        }

        protected override void ProcessItem(TableBase toUpdate, TableBase newItem)
        {
            toUpdate.Update(newItem);
            ProcessTableSeats(toUpdate, newItem.Seats);
        }

        private void ProcessTableSeats(TableBase toUpdate, List<Seat> viewSeats)
        {
            _seatsItemService.Process(toUpdate, viewSeats);
        }
    }
}
