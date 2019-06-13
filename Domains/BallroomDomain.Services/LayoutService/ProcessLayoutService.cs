using AIT.BallroomDomain.Infrastructure.DbContext;
using AIT.BallroomDomain.Infrastructure.Repositories;
using AIT.BallroomDomain.Model.Entities;
using AIT.BallroomDomain.Services.LayoutService.DTO;
using AIT.BallroomDomain.Services.LayoutService.Strategies;
using AIT.DomainUtilities;
using AIT.UtilitiesComponents.Extensions;
using AIT.UtilitiesComponents.Strategy;
using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace AIT.BallroomDomain.Services.LayoutService
{
    internal class ProcessLayoutService : IProcessLayoutService
    {
        private readonly Container _container;
        private readonly IBallroomRepository _ballroomRepository;
        private readonly IActionStrategyService<Type, UpdateContainer> _processItemStrategy;
        private readonly IUnitOfWork<BallroomContext> _unitOfWork;

        public ProcessLayoutService(Container container, 
            IBallroomRepository ballroomRepository, 
            IUnitOfWork<BallroomContext> unitOfWork,
            IActionStrategyService<Type, UpdateContainer> processItemStrategy)
        {
            _container = container;
            _ballroomRepository = ballroomRepository;
            _unitOfWork = unitOfWork;
            _processItemStrategy = processItemStrategy;
            
            SetupStrategy();
        }

        private void SetupStrategy()
        {
            _processItemStrategy.AddStrategy(typeof(TableRect), _container.GetInstance<ProcessTableItemStrategy>().Process);
            _processItemStrategy.AddStrategy(typeof(TableRound), _container.GetInstance<ProcessTableItemStrategy>().Process);
            _processItemStrategy.SetDefaultStrategy(_container.GetInstance<ProcessBallroomItemStrategy>().Process);
        }

        public void ProcessLayout(Ballroom ballroom, List<BallroomItem> viewItems)
        {           
            var itemsDict = viewItems.ToDictionary(x => x.Id, y => y);

            ProcessBallroomItems(ballroom, itemsDict);
            InsertNewItems(ballroom, itemsDict);                            // list of items in this step is decreased by items that were used to update existing entities 
        }

        private void ProcessBallroomItems(Ballroom ballroom, Dictionary<int, BallroomItem> viewItems)
        {
            for (var i = ballroom.BallroomItems.Count - 1; i >= 0; i--)     // since items can be removed from list - start iterating from the end of the list
            {
                var item = ballroom.BallroomItems[i];
                BallroomItem newItem;
                (viewItems.TryGetValue(item.Id, out newItem)).IfTrueOrFalse(() =>
                {
                    UpdateItem(item, newItem);
                    viewItems.Remove(newItem.Id);                           // and remove from items passed from the view
                },
                () => DeleteItem(item));
            }            
        }

        private void UpdateItem(BallroomItem toUpdate, BallroomItem newItem)
        {
            _processItemStrategy.Execute(newItem.GetType(), new UpdateContainer { ToUpdate = toUpdate, NewItem = newItem });            
        }

        private void DeleteItem(BallroomItem toDelete)
        {
            _unitOfWork.Context.Entry(toDelete).State = EntityState.Deleted;
        }

        private void InsertNewItems(Ballroom ballroom, Dictionary<int, BallroomItem> newItems)
        {
            foreach (var item in newItems.Values)
            {
                ballroom.AddBallroomItem(item);
            }
        }
    }
}
