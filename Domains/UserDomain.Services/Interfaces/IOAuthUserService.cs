using AIT.UserDomain.Model.DTO;
using AIT.UserDomain.Model.Enums;
using System;
using System.Collections.Generic;

namespace AIT.UserDomain.Services.Interfaces
{
    public interface IOAuthUserService
    {
        void RequestAuthentication(string provider, string returnUrl);
        bool ConfirmClientRegistration(string username, string email, string provider, string providerUserId);
        bool HasLocalAccount();
        bool DeleteAccount(string provider, string providerUserId);
        string GetUsername(string providerUserId);
        OAuthResult VerifyAuthentication(string returnUrl);
        OAuthDeserializedData TryDeserializeProviderUserId(string externalLoginData);
        OAuthState SelectAuthenticationState(OAuthResult result);
        OAuthRegisterData GetClientRegisterData(OAuthResult result);
        OAuthClientData GetClientData(string provider);        
        ICollection<OAuthClientData> GetRegisteredClients();
        ICollection<OAuthUserAccount> GetUserAccounts();
    }
}
