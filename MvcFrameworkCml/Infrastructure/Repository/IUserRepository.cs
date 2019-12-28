using System;
using System.Threading.Tasks;

namespace MvcFrameworkCml.Infrastructure.Repository
{
    public interface IUserRepository : IDataRepository<EndUser>
    {
        Task<bool> TryAuthenticateAsync(String email, String password);

        Task<EndUser> GetUserWithSensitiveDataAsync(String email, Boolean requestOnlyActiveUsers = true);
    }
}
