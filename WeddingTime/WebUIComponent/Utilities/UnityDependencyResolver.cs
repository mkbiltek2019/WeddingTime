using AIT.UtilitiesComponents.Commands;
using AIT.UtilitiesComponents.Services;
using AIT.UtilitiesComponents.Strategy;
using AIT.WebUIComponent.Models.Account;
using AIT.WebUIComponent.Services.AutoMapper;
using AIT.WebUIComponent.Services.Emails;
using AIT.WebUIComponent.Services.Tasks;
using AIT.WebUIComponent.Services.Undo;
using AIT.WebUIComponent.Services.Undo.Strategies;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using SimpleInjector;
using SimpleInjector.Integration.Web;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace AIT.WebUIComponent.Utilities
{
    public class UnityDependencyResolver : IDependencyResolver, IDisposable
    {
        public UnityDependencyResolver()
        {
            UnityService.Get().Container().Options.DefaultScopedLifestyle = new WebRequestLifestyle();

            BootCommandProcessor.Run();

            UnityService.Get().Register(typeof(IFunctionStrategyService<,,>), typeof(FunctionStrategyService<,,>))
                              .Register(typeof(IActionStrategyService<,>), typeof(ActionStrategyService<,>))
                              .Register<IGuestAutoMapperService, GuestAutoMapperService>()
                              .Register<IBallroomAutoMapperService, BallroomAutoMapperService>()
                              .Register<IExpenseAutoMapperService, ExpenseAutoMapperService>()
                              .Register<IUndoCommandService, UndoCommandService>()
                              .Register<IPdfAutoMapperService, PdfAutoMapperService>()
                              .Register<ITasksAutoMapperService, TasksAutoMapperService>()
                              .Register<ExpensesUndoStrategy>()
                              .Register<GroupUndoStrategy>()
                              .Register<PersonsUndoStrategy>()
                              .Register<TaskUndoStrategy>()
                              .Register<IEmailService, EmailService>(Lifestyle.Singleton)
                              .Register<ITaskCardItemsService, TaskCardItemsService>()
                              .Register(() => HttpContext.Current.GetOwinContext().Authentication, Lifestyle.Scoped)
                              .Register<IUserStore<ApplicationUser>>(() => new UserStore<ApplicationUser>(new ApplicationDbContext()), Lifestyle.Scoped)
                              .Register<ApplicationDbContext>(Lifestyle.Scoped)
                              .Register<ApplicationSignInManager>(Lifestyle.Scoped)
                              .Register<ApplicationUserManager>(Lifestyle.Scoped);

            UnityService.Get().Container().RegisterMvcControllers(Assembly.GetExecutingAssembly());
        }

        public object GetService(Type serviceType)
        {
            try
            {
                return UnityService.Get().Container().GetInstance(serviceType);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            try
            {
                return UnityService.Get().Container().GetAllInstances(serviceType);
            }
            catch (Exception)
            {
                return new List<object>();
            }
        }

        public void Dispose()
        {
            var container = UnityService.Get().Container();
            if (container == null) return;

            container.Dispose();
        }
    }
}