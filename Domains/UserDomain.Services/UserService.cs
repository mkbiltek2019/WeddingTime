using AIT.UserDomain.Infrastructure.Repositories;
using AIT.UserDomain.Services.Interfaces;
using System;

namespace AIT.UserDomain.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public int GetUserId(Guid userKey)
        {
            return _userRepository.GetUserId(userKey);
        }
    }
}
