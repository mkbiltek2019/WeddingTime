using AIT.BallroomDomain.Model.Enums;
using AIT.UtilitiesComponents.Strategy;
using AIT.WebUIComponent.Models.Ballroom;
using System;
using System.Web.Mvc;

namespace AIT.WebUIComponent.CustomModelBinders
{
    public class BallroomItemModelBinder : DefaultModelBinder
    {
        private IFunctionStrategyService<int, Type> itemModelTypeStrategy;

        public BallroomItemModelBinder()
        {
            InitializeItemModelTypeStrategy();
        }

        protected override object CreateModel(ControllerContext controllerContext, ModelBindingContext bindingContext, Type modelType)
        {
            var itemTypeValue = bindingContext.ValueProvider.GetValue(bindingContext.ModelName + ".ItemType");
            var itemType = (int)itemTypeValue.ConvertTo(typeof(int));

            Type ballroomItemModelType = itemModelTypeStrategy.Execute(itemType);

            var model = Activator.CreateInstance(ballroomItemModelType);
            bindingContext.ModelMetadata = ModelMetadataProviders.Current.GetMetadataForType(() => model, ballroomItemModelType);
            // bindingContext.ModelMetadata.Model = model;
            return model;
        }

        private void InitializeItemModelTypeStrategy()
        {
            itemModelTypeStrategy = new FunctionStrategyService<int, Type>();
            itemModelTypeStrategy.AddStrategy((int)ItemType.TableRect, () => { return typeof(TableRectModel); });
            itemModelTypeStrategy.AddStrategy((int)ItemType.TableRound, () => { return typeof(TableRoundModel); });
            itemModelTypeStrategy.AddStrategy((int)ItemType.PillarRect, () => { return typeof(PillarRectModel); });
            itemModelTypeStrategy.AddStrategy((int)ItemType.PillarRound, () => { return typeof(PillarRoundModel); });
            itemModelTypeStrategy.AddStrategy((int)ItemType.StageRect, () => { return typeof(StageRectModel); });
            itemModelTypeStrategy.AddStrategy((int)ItemType.StageHalfCircle, () => { return typeof(StageHalfCircleModel); });
        }
    }
}