using AIT.BallroomDomain.Model.Enums;

namespace AIT.BallroomDomain.Model.Entities
{
    public abstract class BallroomItem
    {
        public int Id { get; set; }
        public int BallroomId { get; set; }
        public int PositionX { get; set; }
        public int PositionY { get; set; }
        public RotationType Rotation { get; set; }

        public void Update(BallroomItem item)
        {
            PositionX = item.PositionX;
            PositionY = item.PositionY;
            Rotation = item.Rotation;
        }
    }
}
