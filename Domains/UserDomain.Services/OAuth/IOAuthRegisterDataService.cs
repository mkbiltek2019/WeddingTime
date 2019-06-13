using AIT.UserDomain.Model.DTO;

namespace AIT.UserDomain.Services.OAuth
{
    internal interface IOAuthRegisterDataService
    {
        OAuthRegisterData GetClientRegisterData(OAuthResult result);
    }
}
