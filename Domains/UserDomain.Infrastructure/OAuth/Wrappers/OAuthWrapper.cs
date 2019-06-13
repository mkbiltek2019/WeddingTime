using DotNetOpenAuth.AspNet;
using Microsoft.Web.WebPages.OAuth;
using System.Collections.Generic;

namespace AIT.UserDomain.Infrastructure.OAuth.Wrappers
{
    public class OAuthWrapper : IOAuthWrapper
    {
        public void RequestAuthentication(string provider, string returnUrl)
        {
            OAuthWebSecurity.RequestAuthentication(provider, returnUrl);
        }

        public void CreateOrUpdateAccount(string provider, string providerUserId, string username)
        {
            OAuthWebSecurity.CreateOrUpdateAccount(provider, providerUserId, username);
        }

        public bool Login(string provider, string providerUserId, bool createPersistentCookie = false)
        {
            return OAuthWebSecurity.Login(provider, providerUserId, createPersistentCookie);
        }

        public bool HasLocalAccount(int userId)
        {
            return OAuthWebSecurity.HasLocalAccount(userId);
        }

        public bool DeleteAccount(string provider, string providerUserId)
        {
            return OAuthWebSecurity.DeleteAccount(provider, providerUserId);
        }

        public bool TryDeserializeProviderUserId(string externalLoginData, out string provider, out string providerUserId)
        {
            return OAuthWebSecurity.TryDeserializeProviderUserId(externalLoginData, out provider, out providerUserId);
        }

        public string SerializeProviderUserId(string provider, string providerUserId)
        {
            return OAuthWebSecurity.SerializeProviderUserId(provider, providerUserId);
        }

        public string GetUserName(string provider, string providerUserId)
        {
            return OAuthWebSecurity.GetUserName(provider, providerUserId);
        }

        public AuthenticationResult VerifyAuthentication(string returnUrl)
        {
            return OAuthWebSecurity.VerifyAuthentication(returnUrl);
        }

        public AuthenticationClientData GetClientData(string provider)
        {
            return OAuthWebSecurity.GetOAuthClientData(provider);
        }

        public ICollection<AuthenticationClientData> RegisteredClients()
        {
            return OAuthWebSecurity.RegisteredClientData;
        }

        public ICollection<OAuthAccount> GetAccountsFromUserName(string userName)
        {
            return OAuthWebSecurity.GetAccountsFromUserName(userName);
        }
    }
}
