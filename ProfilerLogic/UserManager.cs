using ProfilerModels;
using ProfilerModels.Infrastructure;
using System;

namespace ProfilerLogic
{
    public class UserManager
    {
        IDataRepository<User> _userRepository;
        public UserManager(IDataRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public User GetUserById(Int32 id)
        {
            var user = _userRepository.Get(id);
            return user;
        }
    }
}
