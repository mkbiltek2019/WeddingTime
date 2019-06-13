using DotNetOpenAuth.AspNet;
using Microsoft.Web.WebPages.OAuth;
using System.Collections.Generic;

namespace AIT.UserDomain.Infrastructure.OAuth.Wrappers
{
    public interface IOAuthWrapper
    {
        void RequestAuthentication(string provider, string returnUrl);
        void CreateOrUpdateAccount(string provider, string providerUserId, string username);
        bool Login(string provider, string providerUserId, bool createPersistentCookie = false);
        bool HasLocalAccount(int userId);
        bool DeleteAccount(string provider, string providerUserId);
        bool TryDeserializeProviderUserId(string externalLoginData, out string provider, out string providerUserId);
        string SerializeProviderUserId(string provider, string providerUserId);
        string GetUserName(string provider, string providerUserId);
        AuthenticationResult VerifyAuthentication(string returnUrl);
        AuthenticationClientData GetClientData(string provider);
        ICollection<AuthenticationClientData> RegisteredClients();
        ICollection<OAuthAccount> GetAccountsFromUserName(string userName);
    }
}
