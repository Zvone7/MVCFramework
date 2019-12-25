using System;
using System.Threading.Tasks;

namespace MvcFrameworkCml.Infrastructure
{
    public interface IUserRepository : IDataRepository<EndUser>
    {
        Task<Boolean> TryAuthenticateAsync(String email, String password);

        Task<EndUser> GetAsync(String email);
    }
}
