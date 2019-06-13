using AIT.UserDomain.Model.DTO;
using AIT.UserDomain.Model.Enums;
using AIT.UtilitiesComponents.Chains;

namespace AIT.UserDomain.Services.OAuth.Links
{
    internal class OAuthConfirmStateLink : ChainLink<OAuthResult, OAuthState>
    {
        public override bool CanHandle(OAuthResult specification)
        {
            return true;
        }

        public override OAuthState Handle(OAuthResult specification)
        {
            return OAuthState.OAuthConfirm;
        }
    }
}
