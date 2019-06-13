using AIT.BallroomDomain.Infrastructure.DbContext;
using AIT.BallroomDomain.Model.Entities;
using AIT.DomainUtilities;
using AIT.UtilitiesComponents.Extensions;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;

namespace AIT.BallroomDomain.Services.LayoutService.Services
{
    internal class ProcessTableSeatsService : IProcessTableSeatsService
    {
        private readonly IUnitOfWork<BallroomContext> _unitOfWork;

        public ProcessTableSeatsService(IUnitOfWork<BallroomContext> unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void Process(TableBase toUpdate, List<Seat> viewSeats)
        {
            ProcessSeats(toUpdate.Seats, viewSeats);
            InsertNewItems(toUpdate, viewSeats);                                // list of items in this step is decreased by items that were used to update existing seats 
        }

        private void ProcessSeats(List<Seat> dbSeats, List<Seat> viewSeats)
        {
            Dictionary<int, Seat> viewSeatsDict = viewSeats.ToDictionary(seat => seat.Id, seat => seat);

            for (var i = dbSeats.Count - 1; i >= 0; i--)                        // since items can be removed from list - start iterating from the end of the list
            {
                Seat dbSeat = dbSeats[i];
                Seat viewSeat;
                (viewSeatsDict.TryGetValue(dbSeat.Id, out viewSeat)).IfTrueOrFalse(() =>
                {
                    UpdateSeat(dbSeat, viewSeat);
                    viewSeats.Remove(viewSeat);
                },
                () => DeleteSeat(dbSeat));
            }
        }

        private void UpdateSeat(Seat toUpdate, Seat newSeat)
        {
            toUpdate.Update(newSeat);
        }

        private void DeleteSeat(Seat dbSeat)
        {
            _unitOfWork.Context.Entry(dbSeat).State = EntityState.Deleted;      // another way would be to create seat repository and call delete method, this seems to be simpler        
        }

        private void InsertNewItems(TableBase toUpdate, List<Seat> viewSeats)
        {
            toUpdate.AddSeats(viewSeats);            
        }
    }
}
