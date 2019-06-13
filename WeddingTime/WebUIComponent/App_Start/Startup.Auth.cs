using AIT.UtilitiesComponents.Services;
using AIT.WebUIComponent.Models.Account;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.DataProtection;
using Microsoft.Owin.Security.Google;
using Microsoft.Owin.Security.MicrosoftAccount;
using Owin;
using System;
using System.Web.Configuration;

namespace AIT.WebUIComponent
{
    public partial class Startup
    {
        internal static IDataProtectionProvider DataProtectionProvider { get; private set; }

        public void ConfigureAuth(IAppBuilder app)
        {
            DataProtectionProvider = app.GetDataProtectionProvider();

            app.CreatePerOwinContext(() => UnityService.Get().Container().GetInstance<ApplicationUserManager>());
            app.CreatePerOwinContext(() => UnityService.Get().Container().GetInstance<ApplicationSignInManager>());

            // enable the application to use a cookie to store information for the signed in user
            // and to use a cookie to temporarily store information about a user logging in with a third party login provider
            // configure the sign in cookie
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
                Provider = new CookieAuthenticationProvider
                {
                    // enables the application to validate the security stamp when the user logs in.
                    // this is a security feature which is used when you change a password or add an external login to your account.  
                    OnValidateIdentity = SecurityStampValidator.OnValidateIdentity<ApplicationUserManager, ApplicationUser>(
                        validateInterval: TimeSpan.FromMinutes(30),
                        regenerateIdentity: (manager, user) => user.GenerateUserIdentityAsync(manager))
                }
            });
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);
            
            AddGoogleAuth(app);
            AddFacebookAuth(app);
            AddMicrosoftAuth(app);
            //AddTwitterAuth(app);          
        }

        private void AddMicrosoftAuth(IAppBuilder app)
        {
            var microsoft = new MicrosoftAccountAuthenticationOptions
            {
                Caption = "Live",
                ClientId = WebConfigurationManager.AppSettings["MicrosoftClientId"],
                ClientSecret = WebConfigurationManager.AppSettings["MicrosoftClientSecret"],
            };
            microsoft.Scope.Add("wl.basic");
            microsoft.Scope.Add("wl.emails");

            app.UseMicrosoftAccountAuthentication(microsoft);
        }

        private void AddGoogleAuth(IAppBuilder app)
        {
            app.UseGoogleAuthentication(new GoogleOAuth2AuthenticationOptions
            {
                ClientId = WebConfigurationManager.AppSettings["GoogleClientId"],
                ClientSecret = WebConfigurationManager.AppSettings["GoogleClientSecret"]
            });
        }

        private void AddFacebookAuth(IAppBuilder app)
        {
            var appId = WebConfigurationManager.AppSettings["FacebookAppId"];
            var appSecret = WebConfigurationManager.AppSettings["FacebookAppSecret"];

            app.UseFacebookAuthentication(appId, appSecret);
        }

        //private void AddTwitterAuth(IAppBuilder app)
        //{
        //    var consumerKey = WebConfigurationManager.AppSettings["TwitterConsumerKey"];
        //    var consumerSecret = WebConfigurationManager.AppSettings["TwitterConsumerSecret"];

        //    app.UseTwitterAuthentication(consumerKey, consumerSecret);
        //}
    }
}