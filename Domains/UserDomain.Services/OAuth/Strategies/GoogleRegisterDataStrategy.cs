using AIT.UserDomain.Infrastructure.OAuth.Wrappers;
using AIT.UserDomain.Model.DTO;

namespace AIT.UserDomain.Services.OAuth.Strategies
{
    internal class GoogleRegisterDataStrategy : OAuthRegisterDataStrategy
    {
        internal GoogleRegisterDataStrategy(IOAuthWrapper oAuthWrapper)
            : base(oAuthWrapper)
        { }

        internal override OAuthRegisterData Execute(OAuthResult result)
        {
            var loginData = GetLoginData(result);
            var username = string.Format("{0} {1}", result.ExtraData["firstName"], result.ExtraData["lastName"]);

            return new OAuthRegisterData
            {
                Username = username,
                Email = result.UserName,
                ExternalLoginData = loginData
            };
        }
    }
}
