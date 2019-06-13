using AIT.UserDomain.Infrastructure.OAuth.Wrappers;
using AIT.UserDomain.Model.DTO;
using AIT.UserDomain.Services.OAuth.Strategies;
using AIT.UtilitiesComponents.Strategy;
using System;

namespace AIT.UserDomain.Services.OAuth
{
    internal class OAuthRegisterDataService : IOAuthRegisterDataService
    {
        IFunctionStrategyService<string, OAuthResult, OAuthRegisterData> _registerDataService;

        internal OAuthRegisterDataService(IOAuthWrapper oAuthWrapper)
        {
            _registerDataService = new FunctionStrategyService<string, OAuthResult, OAuthRegisterData>();

            _registerDataService.AddStrategy("google", new GoogleRegisterDataStrategy(oAuthWrapper).Execute)
                                .AddStrategy("facebook", new FacebookRegisterDataStrategy(oAuthWrapper).Execute)
                                .AddStrategy("twitter", new TwitterRegisterDataStrategy(oAuthWrapper).Execute)
                                .SetDefaultStrategy(ThrowUnknownProvider);
        }

        private OAuthRegisterData ThrowUnknownProvider(OAuthResult result)
        {
            throw new ArgumentException(string.Format("{0} is a unknown provider.", result.Provider));
        }

        public OAuthRegisterData GetClientRegisterData(OAuthResult result)
        {
            return _registerDataService.Execute(result.Provider, result);
        }
    }
}
