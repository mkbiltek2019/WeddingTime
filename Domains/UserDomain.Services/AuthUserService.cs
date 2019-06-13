using AIT.UserDomain.Infrastructure.Providers;
using AIT.UserDomain.Infrastructure.Repositories;
using AIT.UserDomain.Model.Entities;
using AIT.UserDomain.Services.Interfaces;
using System;
using System.Web;
using System.Web.Security;
using WebMatrix.WebData;

namespace AIT.UserDomain.Services
{
    public class AuthUserService : IAuthUserService
    {
        public string CreateUserAndAccount(string username, string password, string email)
        {
            return WebSecurity.UserExists(username) ? string.Empty : WebSecurity.CreateUserAndAccount(username, password, new { Email = email, UserKey = Guid.NewGuid() }, true);
        }

        public string CreateAccount(string username, string password)
        {
            return WebSecurity.CreateAccount(username, password);
        }

        public bool DeleteUserAndAccount(string username)
        {
            if (!WebSecurity.UserExists(username))
                return false;

            MembershipProvider.DeleteAccount(username);
            MembershipProvider.DeleteUser(username, true);
            return true;
        }

        public bool ChangePassword(string username, string currentPassword, string newPassword)
        {
            return WebSecurity.ChangePassword(username, currentPassword, newPassword);
        }

        public bool ConfirmAccount(string username, string confirmationToken)
        {
            return WebSecurity.ConfirmAccount(username, confirmationToken);
        }

        public bool ConfirmAccount(string confirmationToken)
        {
            return WebSecurity.ConfirmAccount(confirmationToken);
        }

        public bool SignIn(string username, string password, bool persistCookie = false)
        {
            if (WebSecurity.IsConfirmed(username))
                return WebSecurity.Login(username, password, persistCookie);

            return false;
        }

        public void SignOut()
        {
            WebSecurity.Logout();
        }

        private UserMembershipProvider MembershipProvider
        {
            get { return (UserMembershipProvider)Membership.Provider; }
        }
    }
}
