using AIT.GuestDomain.Model.Enums;
using AIT.GuestDomain.Services.UpdateOrderNo.DTO;
using AIT.GuestDomain.Services.UpdateOrderNo.Strategies;
using AIT.UtilitiesComponents.Strategy;

namespace AIT.GuestDomain.Services.UpdateOrderNo
{
    public class UpdatePersonOrderNoService : IUpdatePersonOrderNoService
    {
        IActionStrategyService<UpdateOrderNoType, UpdatePersonOrderContainer> _updateOrderNoService;

        public UpdatePersonOrderNoService()
        {
            _updateOrderNoService = new ActionStrategyService<UpdateOrderNoType, UpdatePersonOrderContainer>();

            _updateOrderNoService.AddStrategy(UpdateOrderNoType.Create, new SetOrderNoOnCreatePersonStrategy().Execute);
            _updateOrderNoService.AddStrategy(UpdateOrderNoType.SortAsLast, new SetMaxOrderNoPersonStrategy().Execute);
            _updateOrderNoService.AddStrategy(UpdateOrderNoType.Detach, new SetMaxOrderNoPersonStrategy().Execute);
            _updateOrderNoService.AddStrategy(UpdateOrderNoType.Sort, new SetOrderNoOnSortPersonStrategy().Execute);
            _updateOrderNoService.AddStrategy(UpdateOrderNoType.Join, new SetParentOrderNoPersonStrategy().Execute);
        }

        public void UpdateOrderNo(UpdateOrderNoType key, UpdatePersonOrderContainer container)
        {
            _updateOrderNoService.Execute(key, container);
        }
    }
}
