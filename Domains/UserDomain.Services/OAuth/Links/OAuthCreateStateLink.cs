using AIT.UserDomain.Infrastructure.OAuth.Wrappers;
using AIT.UserDomain.Model.DTO;
using AIT.UserDomain.Model.Enums;
using AIT.UtilitiesComponents.Chains;
using System.Web;

namespace AIT.UserDomain.Services.OAuth.Links
{
    internal class OAuthCreateStateLink : ChainLink<OAuthResult, OAuthState>
    {
        private IOAuthWrapper _oAuthWrapper;

        public OAuthCreateStateLink(IOAuthWrapper oAuthWrapper)
        {
            _oAuthWrapper = oAuthWrapper;
        }

        public override bool CanHandle(OAuthResult specification)
        {
            return HttpContext.Current.User.Identity.IsAuthenticated;
        }

        public override OAuthState Handle(OAuthResult specification)
        {
            if (CanHandle(specification))
            {
                _oAuthWrapper.CreateOrUpdateAccount(specification.Provider, specification.ProviderUserId, HttpContext.Current.User.Identity.Name);
                return OAuthState.OAuthCreated;
            }
            else
            {
                return Successor.Handle(specification);
            }
        }
    }
}
