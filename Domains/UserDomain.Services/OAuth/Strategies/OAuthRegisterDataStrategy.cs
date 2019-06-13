using AIT.UserDomain.Infrastructure.OAuth.Wrappers;
using AIT.UserDomain.Model.DTO;

namespace AIT.UserDomain.Services.OAuth.Strategies
{
    internal abstract class OAuthRegisterDataStrategy
    {
        private IOAuthWrapper _oAuthWrapper;

        internal OAuthRegisterDataStrategy(IOAuthWrapper oAuthWrapper)
        {
            _oAuthWrapper = oAuthWrapper;
        }

        internal abstract OAuthRegisterData Execute(OAuthResult result);

        protected string GetLoginData(OAuthResult result)
        {
            return _oAuthWrapper.SerializeProviderUserId(result.Provider, result.ProviderUserId);
        }
    }
}
