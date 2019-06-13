using AIT.DomainUtilities;
using AIT.UserDomain.Infrastructure.DbContext;
using AIT.UserDomain.Infrastructure.OAuth.Wrappers;
using AIT.UserDomain.Infrastructure.Repositories;
using AIT.UserDomain.Infrastructure.Services;
using AIT.UserDomain.Model.DTO;
using AIT.UserDomain.Model.Entities;
using AIT.UserDomain.Model.Enums;
using AIT.UserDomain.Services.Interfaces;
using AIT.UserDomain.Services.OAuth;
using System;
using System.Collections.Generic;
using System.Transactions;
using System.Web;

namespace AIT.UserDomain.Services
{
    public class OAuthUserService : IOAuthUserService
    {
        private IUserRepository _userRepository;
        private IUnitOfWork<UsersContext> _unitOfWork;
        private IOAuthWrapper _oAuthWrapper;
        private IMapperService _mapperService;
        private string _currentUsername;

        public OAuthUserService(IUserRepository userRepository, IUnitOfWork<UsersContext> unitOfWork, IOAuthWrapper oAuthWrapper, IMapperService mapperService)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
            _oAuthWrapper = oAuthWrapper;
            _mapperService = mapperService;
        }

        public void RequestAuthentication(string provider, string returnUrl)
        {
            _oAuthWrapper.RequestAuthentication(provider, returnUrl);
        }

        public bool ConfirmClientRegistration(string username, string email, string provider, string providerUserId)
        {
            var user = _userRepository.GetUserByName(username);
            if (user != null)
                return false;

            user = new User { Username = username, Email = email, UserKey = Guid.NewGuid() };
            _userRepository.Insert(user);
            _unitOfWork.Save();

            _oAuthWrapper.CreateOrUpdateAccount(provider, providerUserId, username);
            return _oAuthWrapper.Login(provider, providerUserId);
        }

        public bool HasLocalAccount()
        {
            var userId = _userRepository.GetUserId(CurrentIdentityName);
            return _oAuthWrapper.HasLocalAccount(userId);
        }

        public bool DeleteAccount(string provider, string providerUserId)
        {
            var accountDeleted = false;
            var owner = _oAuthWrapper.GetUserName(provider, providerUserId);

            if (owner != CurrentIdentityName)
                return false;

            using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.Serializable }))                  //use a transaction to prevent the user from deleting their last login credential
            {
                var userId = _userRepository.GetUserId(CurrentIdentityName);
                bool hasAccount = _oAuthWrapper.HasLocalAccount(userId);

                if (hasAccount || GetUserAccounts().Count > 1)
                {
                    accountDeleted = _oAuthWrapper.DeleteAccount(provider, providerUserId);
                    scope.Complete();
                }
            }
            return accountDeleted;
        }

        public string GetUsername(string providerUserId)
        {
            return _userRepository.GetUserName(providerUserId);
        }

        public OAuthResult VerifyAuthentication(string returnUrl)
        {
            var authResult = _oAuthWrapper.VerifyAuthentication(returnUrl);
            return _mapperService.GetAuthenticationResult(authResult);
        }

        public OAuthDeserializedData TryDeserializeProviderUserId(string externalLoginData)
        {
            string provider = null;
            string providerUserId = null;
            var isDeserialized = _oAuthWrapper.TryDeserializeProviderUserId(externalLoginData, out provider, out providerUserId);

            return new OAuthDeserializedData { IsDeserialized = isDeserialized, Provider = provider, ProviderUserId = providerUserId };
        }

        public OAuthState SelectAuthenticationState(OAuthResult result)
        {
            IOAuthStateService stateService = new OAuthStateService(_oAuthWrapper);                             //not using unity because of internal type
            return stateService.SelectState(result);
        }

        public OAuthRegisterData GetClientRegisterData(OAuthResult result)
        {
            IOAuthRegisterDataService registerDataService = new OAuthRegisterDataService(_oAuthWrapper);        //not using unity because of internal type
            return registerDataService.GetClientRegisterData(result);
        }

        public OAuthClientData GetClientData(string provider)
        {
            var clientData = _oAuthWrapper.GetClientData(provider);
            return _mapperService.GetClientData(clientData);
        }

        public ICollection<OAuthClientData> GetRegisteredClients()
        {
            var clients = _oAuthWrapper.RegisteredClients();
            return _mapperService.GetClientDataCollection(clients);
        }

        public ICollection<OAuthUserAccount> GetUserAccounts()
        {
            var accounts = _oAuthWrapper.GetAccountsFromUserName(CurrentIdentityName);
            return _mapperService.GetUserAccounts(accounts);
        }

        private string CurrentIdentityName
        {
            get { return _currentUsername ?? (_currentUsername = HttpContext.Current.User.Identity.Name); }     //what if user is not logged in - controller avoids that situation? [Authorized] check if methods that uses this function are not marked as [AllowAnonymous]
        }
    }
}
