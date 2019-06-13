using DotNetOpenAuth.AspNet.Clients;
using DotNetOpenAuth.OpenId.Extensions.AttributeExchange;
using DotNetOpenAuth.OpenId.RelyingParty;
using System.Collections.Generic;

namespace AIT.UserDomain.Infrastructure.OAuth.Clients
{
    internal class GoogleClient : OpenIdClient
    {
        internal GoogleClient()
            : base("google", WellKnownProviders.Google) { }

        protected override Dictionary<string, string> GetExtraData(IAuthenticationResponse response)
        {
            var fetchResponse = response.GetExtension<FetchResponse>();

            if (fetchResponse == null)
                return null;

            var extraData = new Dictionary<string, string>();
            extraData.Add("email", fetchResponse.GetAttributeValue(WellKnownAttributes.Contact.Email));
            extraData.Add("firstName", fetchResponse.GetAttributeValue(WellKnownAttributes.Name.First));
            extraData.Add("lastName", fetchResponse.GetAttributeValue(WellKnownAttributes.Name.Last));

            return extraData;
        }

        protected override void OnBeforeSendingAuthenticationRequest(IAuthenticationRequest request)
        {
            var fetchRequest = new FetchRequest();
            fetchRequest.Attributes.AddRequired(WellKnownAttributes.Contact.Email);
            fetchRequest.Attributes.AddRequired(WellKnownAttributes.Name.First);
            fetchRequest.Attributes.AddRequired(WellKnownAttributes.Name.Last);

            request.AddExtension(fetchRequest);
        }
    }
}
