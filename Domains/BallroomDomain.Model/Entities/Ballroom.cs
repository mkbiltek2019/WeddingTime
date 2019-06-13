using System;
using System.Collections.Generic;

namespace AIT.BallroomDomain.Model.Entities
{
    public class Ballroom
    {
        public string UserId { get; set; }
        public int Id { get; set; }        
        public int Width { get; set; }
        public int Height { get; set; }
        public bool IsExpanded { get; set; }

        public virtual List<BallroomItem> BallroomItems { get; set; }
        
        public void AddBallroomItem(BallroomItem item)
        {
            BallroomItems.Add(item);
        }

        public void Update(Ballroom item)
        {
            Width = item.Width;
            Height = item.Height;
            IsExpanded = item.IsExpanded;
        }
    }
}
