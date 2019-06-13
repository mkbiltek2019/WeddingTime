using AIT.UserDomain.Infrastructure.OAuth.Wrappers;
using AIT.UserDomain.Model.DTO;
using AIT.UserDomain.Model.Enums;
using AIT.UtilitiesComponents.Chains;

namespace AIT.UserDomain.Services.OAuth.Links
{
    internal class OAuthLoginStateLink : ChainLink<OAuthResult, OAuthState>
    {
        private IOAuthWrapper _oAuthWrapper;

        public OAuthLoginStateLink(IOAuthWrapper oAuthWrapper)
        {
            _oAuthWrapper = oAuthWrapper;
        }

        public override bool CanHandle(OAuthResult specification)
        {
            return _oAuthWrapper.Login(specification.Provider, specification.ProviderUserId);
        }

        public override OAuthState Handle(OAuthResult specification)
        {
            if (CanHandle(specification))
            {
                return OAuthState.OAuthLogin;
            }
            else
            {
                return Successor.Handle(specification);
            }
        }
    }
}
