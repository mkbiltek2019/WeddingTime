using AIT.UserDomain.Infrastructure.OAuth.Clients;
using Microsoft.Web.WebPages.OAuth;

namespace AIT.UserDomain.Infrastructure.OAuth
{
    internal class OAuthConfig
    {
        internal static void RegisterClients()
        {
            //OAuthWebSecurity.RegisterMicrosoftClient(
            //    clientId: "",
            //    clientSecret: "");

            //OAuthWebSecurity.RegisterTwitterClient(
            //    consumerKey: "",
            //    consumerSecret: "");

            OAuthWebSecurity.RegisterFacebookClient(appId: "1",
                                                    appSecret: "2",
                                                    displayName: "Facebook",
                                                    extraData: null);

            OAuthWebSecurity.RegisterClient(client: new GoogleClient(),
                                            displayName: "Google",
                                            extraData: null);
        }
    }
}
