using AIT.BallroomDomain.Model.Enums;

namespace AIT.BallroomDomain.Model.Entities
{
    public class Seat
    {
        public int Id { get; private set; }                             // id is kind of order number, becase on page it is not possible to sort chairs
        public int TableId { get; private set; }
        public int? PersonId { get; private set; }
        public bool Hidden { get; private set; }        
        public NewlywedsType? TakenBy { get; private set; }
        public SeatLocation? Location { get; private set; }

        public bool HasPersonAssigned
        {
            get { return PersonId.HasValue; }
        }

        public void AssignPerson(int personId)
        {
            PersonId = personId;
        }

        public void Update(Seat tableSeat)
        {
            PersonId = tableSeat.PersonId;
            Hidden = tableSeat.Hidden;
            TakenBy = tableSeat.TakenBy;
            Location = tableSeat.Location;
        }
    }
}
