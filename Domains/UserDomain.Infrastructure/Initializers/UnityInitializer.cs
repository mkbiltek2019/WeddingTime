using AIT.UserDomain.Infrastructure.OAuth.Wrappers;
using AIT.UserDomain.Infrastructure.Repositories;
using AIT.UserDomain.Infrastructure.Services;
using AIT.UtilitiesComponents.Services;

namespace AIT.UserDomain.Infrastructure.Initializers
{
    internal class UnityInitializer
    {
        internal static void Init()
        {
            UnityService.Get().Register<IUserRepository, UserRepository>()
                              .Register<IOAuthWrapper, OAuthWrapper>()
                              .Register<IMapperService, MapperService>();
        }
    }
}
