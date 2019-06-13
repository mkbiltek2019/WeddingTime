using System.Collections.Generic;

namespace AIT.WebUIComponent.Models.Ballroom
{
    public class BallroomModel
    {        
        public int Width { get; set; }
        public int Height { get; set; }
        public bool IsExpanded { get; set; }

        public List<BallroomItemModel> BallroomItems { get; set; }

        public bool HasItems
        {
            get { return BallroomItems != null && BallroomItems.Count > 0; }
        }
    }
}