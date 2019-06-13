using AIT.UserDomain.Model.DTO;
using DotNetOpenAuth.AspNet;
using Microsoft.Web.WebPages.OAuth;
using System.Collections.Generic;

namespace AIT.UserDomain.Infrastructure.Services
{
    public interface IMapperService
    {
        OAuthClientData GetClientData(AuthenticationClientData clientData);
        OAuthResult GetAuthenticationResult(AuthenticationResult result);
        ICollection<OAuthClientData> GetClientDataCollection(ICollection<AuthenticationClientData> clientDataCollection);
        ICollection<OAuthUserAccount> GetUserAccounts(ICollection<OAuthAccount> accounts);
    }
}
