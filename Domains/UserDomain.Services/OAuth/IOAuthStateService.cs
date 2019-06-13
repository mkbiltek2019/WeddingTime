using AIT.UserDomain.Model.DTO;
using AIT.UserDomain.Model.Enums;

namespace AIT.UserDomain.Services.OAuth
{
    internal interface IOAuthStateService
    {
        OAuthState SelectState(OAuthResult result);
    }
}
