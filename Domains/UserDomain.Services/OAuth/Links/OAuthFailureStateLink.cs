using AIT.UserDomain.Model.DTO;
using AIT.UserDomain.Model.Enums;
using AIT.UtilitiesComponents.Chains;

namespace AIT.UserDomain.Services.OAuth.Links
{
    internal class OAuthFailureStateLink : ChainLink<OAuthResult, OAuthState>
    {
        public override bool CanHandle(OAuthResult specification)
        {
            return !specification.IsSuccessful;
        }

        public override OAuthState Handle(OAuthResult specification)
        {
            if (CanHandle(specification))
            {
                return OAuthState.OAuthFailed;
            }
            else
            {
                return Successor.Handle(specification);
            }
        }
    }
}
