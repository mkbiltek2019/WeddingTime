using System.Collections.Generic;

namespace AIT.BallroomDomain.Model.Entities
{
    public abstract class TableBase : BallroomItem
    {
        public TableBase()
        {
            Seats = new List<Seat>();
        }

        public virtual List<Seat> Seats { get; private set; }

        public void AddSeats(List<Seat> seats)
        {
            Seats.AddRange(seats);
        }
    }
}
