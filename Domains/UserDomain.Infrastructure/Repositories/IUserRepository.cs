using AIT.DomainUtilities.Repositories;
using AIT.UserDomain.Model.Entities;
using System;

namespace AIT.UserDomain.Infrastructure.Repositories
{
    public interface IUserRepository : IRepositoryBase<User>
    {
        User GetUserByName(string username);
        int GetUserId(Guid userKey);
        int GetUserId(string username);
        string GetUserName(string providerId);
    }
}
