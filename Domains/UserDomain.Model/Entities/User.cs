using System;

namespace AIT.UserDomain.Model.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public Guid UserKey { get; set; }

        public virtual OAuthMembership OAuthMembership { get; set; }
    }
}
