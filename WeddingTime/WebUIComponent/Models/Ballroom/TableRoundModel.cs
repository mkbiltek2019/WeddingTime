using System.Collections.Generic;

namespace AIT.WebUIComponent.Models.Ballroom
{
    public class TableRoundModel : BallroomItemModel
    {
        public Dictionary<string, RoundSeatModel> Seats { get; set; }
    }
}