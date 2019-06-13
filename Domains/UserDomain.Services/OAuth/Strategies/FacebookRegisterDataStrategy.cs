using AIT.UserDomain.Infrastructure.OAuth.Wrappers;
using AIT.UserDomain.Model.DTO;

namespace AIT.UserDomain.Services.OAuth.Strategies
{
    internal class FacebookRegisterDataStrategy : OAuthRegisterDataStrategy
    {
        internal FacebookRegisterDataStrategy(IOAuthWrapper oAuthWrapper)
            : base(oAuthWrapper)
        { }

        internal override OAuthRegisterData Execute(OAuthResult result)
        {
            var loginData = GetLoginData(result);
            var username = result.ExtraData["name"];

            return new OAuthRegisterData
            {
                Username = username,
                Email = result.UserName,
                ExternalLoginData = loginData
            };
        }
    }
}
