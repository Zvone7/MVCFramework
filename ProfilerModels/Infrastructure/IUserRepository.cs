using System;

namespace ProfilerModels.Infrastructure
{
    public interface IUserRepository : IDataRepository<User>
    {
        User Authenticate(String username, String password);

        User Get(String username);

    }
}
