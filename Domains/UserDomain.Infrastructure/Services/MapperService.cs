using AIT.UserDomain.Model.DTO;
using AutoMapper;
using DotNetOpenAuth.AspNet;
using Microsoft.Web.WebPages.OAuth;
using System.Collections.Generic;

namespace AIT.UserDomain.Infrastructure.Services
{
    public class MapperService : IMapperService
    {
        public OAuthClientData GetClientData(AuthenticationClientData clientData)
        {
            return Mapper.Map<AuthenticationClientData, OAuthClientData>(clientData);
        }

        public OAuthResult GetAuthenticationResult(AuthenticationResult result)
        {
            return Mapper.Map<AuthenticationResult, OAuthResult>(result);
        }

        public ICollection<OAuthClientData> GetClientDataCollection(ICollection<AuthenticationClientData> clientDataCollection)
        {
            return Mapper.Map<ICollection<AuthenticationClientData>, ICollection<OAuthClientData>>(clientDataCollection);
        }

        public ICollection<OAuthUserAccount> GetUserAccounts(ICollection<OAuthAccount> accounts)
        {
            return Mapper.Map<ICollection<OAuthAccount>, ICollection<OAuthUserAccount>>(accounts);
        }
    }
}
