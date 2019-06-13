using AIT.BallroomDomain.Model.Entities;
using AIT.BallroomDomain.Services.LayoutService.DTO;

namespace AIT.BallroomDomain.Services.LayoutService.Strategies
{
    internal abstract class ProcessItemStrategy<T>
    {
        protected abstract void ProcessItem(T toUpdate, T newItem);

        public void Process(UpdateContainer container) 
        {
            ProcessItem((T)container.ToUpdate, (T)container.NewItem);
        }
    }
}
