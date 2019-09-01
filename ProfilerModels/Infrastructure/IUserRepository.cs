using System;

namespace ProfilerModels.Infrastructure
{
    public interface IUserRepository : IDataRepository<User>
    {
        Boolean TryAuthenticate(String email, String password);

        User Get(String email);

    }
}
