using AIT.UserDomain.Infrastructure.OAuth.Wrappers;
using AIT.UserDomain.Model.DTO;
using System;

namespace AIT.UserDomain.Services.OAuth.Strategies
{
    internal class TwitterRegisterDataStrategy : OAuthRegisterDataStrategy
    {
        internal TwitterRegisterDataStrategy(IOAuthWrapper oAuthWrapper)
            : base(oAuthWrapper)
        { }

        internal override OAuthRegisterData Execute(OAuthResult result)
        {
            throw new NotImplementedException();
        }
    }
}
