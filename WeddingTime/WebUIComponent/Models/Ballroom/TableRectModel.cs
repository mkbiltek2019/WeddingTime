using AIT.BallroomDomain.Model.Enums;
using System.Collections.Generic;
using System.Linq;

namespace AIT.WebUIComponent.Models.Ballroom
{
    public class TableRectModel : BallroomItemModel
    {
        public Dictionary<string, RectSeatModel> Seats { get; set; }

        public KeyValuePair<string, RectSeatModel> TopSeat                                  // top and bottom seats are automatically created when new rect table is created
        {   
            get { return Seats.Single(x => x.Value.Location == (int)SeatLocation.Top); }
        }

        public KeyValuePair<string, RectSeatModel> BottomSeat
        {
            get { return Seats.Single(x => x.Value.Location == (int)SeatLocation.Bottom); }
        }
    }
}