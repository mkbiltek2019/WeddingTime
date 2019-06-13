using AIT.UserDomain.Infrastructure.OAuth.Wrappers;
using AIT.UserDomain.Model.DTO;
using AIT.UserDomain.Model.Enums;
using AIT.UserDomain.Services.OAuth.Links;
using AIT.UtilitiesComponents.Chains;

namespace AIT.UserDomain.Services.OAuth
{
    internal class OAuthStateService : ChainService<OAuthResult, OAuthState>, IOAuthStateService
    {
        private IOAuthWrapper _oAuthWrapper;

        internal OAuthStateService(IOAuthWrapper oAuthWrapper)
        {
            _oAuthWrapper = oAuthWrapper;
        }

        protected override IChainLink<OAuthResult, OAuthState> CreateChain()
        {
            var link = new OAuthFailureStateLink();
            link.AddNextLink(new OAuthLoginStateLink(_oAuthWrapper));
            link.AddNextLink(new OAuthCreateStateLink(_oAuthWrapper));
            link.AddNextLink(new OAuthConfirmStateLink());

            return link;
        }

        public OAuthState SelectState(OAuthResult result)
        {
            return Process(result);
        }
    }
}
