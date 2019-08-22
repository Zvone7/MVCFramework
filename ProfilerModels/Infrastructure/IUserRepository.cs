using System;

namespace ProfilerModels.Infrastructure
{
    public interface IUserRepository : IDataRepository<User>
    {
        Boolean TryAuthenticate(String username, String password);

        User Get(String username);

    }
}
