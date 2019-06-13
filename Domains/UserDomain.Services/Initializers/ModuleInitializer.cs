using AIT.UserDomain.Services.Interfaces;
using AIT.UtilitiesComponents.Commands;
using AIT.UtilitiesComponents.Services;
using SimpleInjector;

namespace AIT.UserDomain.Services.Initializers
{
    public class ModuleInitializer : IBootCommand
    {
        public void Execute()
        {
            UnityService.Get().Register<IAuthUserService, AuthUserService>()
                              .Register<IOAuthUserService, OAuthUserService>()
                              .Register<IAuthCookieService, AuthCookieService>(Lifestyle.Singleton)
                              .Register<IUserService, UserService>();
        }
    }
}
