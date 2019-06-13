using AIT.BallroomDomain.Model.Entities;
using AIT.BallroomDomain.Services.LayoutService.DTO;

namespace AIT.BallroomDomain.Services.LayoutService.Strategies
{
    internal class ProcessBallroomItemStrategy : ProcessItemStrategy<BallroomItem>
    {
        protected override void ProcessItem(BallroomItem toUpdate, BallroomItem newItem)
        {
            toUpdate.Update(newItem);
        }
    }
}
