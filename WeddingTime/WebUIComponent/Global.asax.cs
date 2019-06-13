using AIT.UndoManagement.Infrastructure.Initializers;
using AIT.WebUIComponent.CustomModelBinders;
using AIT.WebUIComponent.Models.Ballroom;
using AIT.WebUIComponent.Utilities;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace AIT.WebUIComponent
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            DependencyResolver.SetResolver(new UnityDependencyResolver());

            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AutoMapperConfig.RegisterMapping();            

            ModelBinders.Binders.Add(typeof(BallroomItemModel), new BallroomItemModelBinder());
            JsonConvert.DefaultSettings = () => new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() };

            XmlSerializationInitializer.Init();
        }        
    }
}