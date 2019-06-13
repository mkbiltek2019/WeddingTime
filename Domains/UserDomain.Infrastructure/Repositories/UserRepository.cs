using AIT.DomainUtilities;
using AIT.DomainUtilities.Repositories;
using AIT.UserDomain.Infrastructure.DbContext;
using AIT.UserDomain.Model.Entities;
using AIT.UtilitiesComponents.Extensions;
using System;
using System.Linq;

namespace AIT.UserDomain.Infrastructure.Repositories
{
    public class UserRepository : RepositoryBase<User, UsersContext>, IUserRepository
    {
        public UserRepository(IUnitOfWork<UsersContext> unitOfWork)
            : base(unitOfWork)
        {
        }

        //OK - needed for creating auth cookie
        public User GetUserByName(string username)
        {           
            return GetAll().SingleOrDefault(n => n.Username.ToLower() == username.ToLower());
        }

        //OK - think about different method. maybe they fetch al info instead only id
        public int GetUserId(Guid userKey)
        {
            return GetAll().Where(n => n.UserKey == userKey).Select(n => n.Id).Single();
        }

        public int GetUserId(string username)
        {
            return GetAll().SingleOrDefault(n => n.Username.ToLower() == username.ToLower()).IfNotNull(n => n.Id);
        }

        public string GetUserName(string providerUserId)
        {
            return GetAll().Single(x => x.OAuthMembership.ProviderUserId == providerUserId).Username;
        }
    }
}
