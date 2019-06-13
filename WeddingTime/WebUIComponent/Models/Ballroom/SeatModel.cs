namespace AIT.WebUIComponent.Models.Ballroom
{
    public abstract class SeatModel
    {
        public int? PersonId { get; set; }
        public int? TakenBy { get; set; }           // value mapped from newlyweds enum
        public int TableId { get; set; }            // used in assign person to seat function (ballroom.manager)
        public bool Hidden { get; set; }
        public bool Occupied { get; set; }          // it is not possible to have person id and taken by value together, taken by means that this seat is taken by groom or bride and person can't be assigned                   
    }
}