using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AIT.WebUIComponent.Models.Account
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

        public bool AutoLoginEnabled { get; set; }
        public AccountState AccountState { get; set; }
        public DateTime? CreationDate { get; set; }
        public DateTime? LastLoginDate { get; set; }
    }

    public enum AccountState
    {
        Confirmed = 0,
        RequiresConfirmation = 1,
        EmailChanged = 2
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("WeddingTimeConnection", throwIfV1Schema: false)
        {
        }        
    }
}