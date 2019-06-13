using AIT.BallroomDomain.Model.Entities;
using System.Collections.Generic;

namespace AIT.BallroomDomain.Services.LayoutService
{
    public interface IProcessLayoutService
    {
        void ProcessLayout(Ballroom ballroom, List<BallroomItem> viewItems);
    }
}
