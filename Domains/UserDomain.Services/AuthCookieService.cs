using AIT.UserDomain.Infrastructure.Repositories;
using AIT.UserDomain.Model.DTO;
using AIT.UserDomain.Services.Interfaces;
using AIT.UtilitiesComponents.Services;
using System;
using System.Security.Principal;
using System.Web;
using System.Web.Security;

namespace AIT.UserDomain.Services
{
    public class AuthCookieService : IAuthCookieService
    {
        public void CreateCookie(string userName)
        {
            var userRepository = UnityService.Get().Container().GetInstance<IUserRepository>();     // because IAuthCookieService is singleton
            var user = userRepository.GetUserByName(userName);

            var userData = user.UserKey.ToString();
            var authTicket = new FormsAuthenticationTicket(1, user.Username, DateTime.Now, DateTime.Now.AddMinutes(FormsAuthentication.Timeout.TotalMinutes), false, userData);
            var encryptedTicket = FormsAuthentication.Encrypt(authTicket);
            var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);

            HttpContext.Current.Response.Cookies.Add(cookie);
        }

        public void SetCookieSecurityInformation()
        {
            var authCookie = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];

            if (authCookie == null)
                return;

            var authTicket = FormsAuthentication.Decrypt(authCookie.Value);
            var userKey = Guid.Parse(authTicket.UserData);

            var userIdentity = new GenericIdentity(authTicket.Name);
            var userPrincipal = new CustomPrincipal(userIdentity, userKey);

            HttpContext.Current.User = userPrincipal;
        }
    }
}
