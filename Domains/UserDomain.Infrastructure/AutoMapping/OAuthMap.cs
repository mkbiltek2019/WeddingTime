using AIT.UserDomain.Model.DTO;
using AutoMapper;
using DotNetOpenAuth.AspNet;
using Microsoft.Web.WebPages.OAuth;

namespace AIT.UserDomain.Infrastructure.AutoMapping
{
    internal class OAuthMap
    {
        internal static void CreateMap()
        {
            Mapper.CreateMap<AuthenticationClientData, OAuthClientData>()
                .ForMember(n => n.ProviderName, expression => expression.ResolveUsing(o => o.AuthenticationClient.ProviderName));

            Mapper.CreateMap<AuthenticationResult, OAuthResult>();
            Mapper.CreateMap<OAuthAccount, OAuthUserAccount>();
        }
    }
}
