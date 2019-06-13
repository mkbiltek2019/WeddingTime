
namespace AIT.UserDomain.Infrastructure.AutoMapping
{
    internal class AutoMapperConfig
    {
        internal static void Init()
        {
            OAuthMap.CreateMap();
        }
    }
}
