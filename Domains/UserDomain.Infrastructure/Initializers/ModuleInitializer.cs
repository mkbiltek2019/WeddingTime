using AIT.UserDomain.Infrastructure.AutoMapping;
using AIT.UserDomain.Infrastructure.OAuth;
using AIT.UtilitiesComponents.Commands;

namespace AIT.UserDomain.Infrastructure.Initializers
{
    public class ModuleInitializer : IBootCommand
    {
        public void Execute()
        {
            UnityInitializer.Init();
            AutoMapperConfig.Init();
            OAuthConfig.RegisterClients();
        }
    }
}
