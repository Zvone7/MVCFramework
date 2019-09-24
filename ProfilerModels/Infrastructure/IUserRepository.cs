using System;

namespace ProfilerModels.Infrastructure
{
    public interface IUserRepository : IDataRepository<EndUser>
    {
        Boolean TryAuthenticate(String email, String password);

        EndUser Get(String email);

    }
}
