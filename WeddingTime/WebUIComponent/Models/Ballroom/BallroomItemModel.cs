namespace AIT.WebUIComponent.Models.Ballroom
{
    public abstract class BallroomItemModel
    {
        public int Id { get; set; }
        public int PositionX { get; set; }
        public int PositionY { get; set; }
        public int Rotation { get; set; }
        public int ItemType { get; set; }        // used in js code, assigned by auto mapper, int value taken from enum
    }
}