using System;
using System.Threading.Tasks;

namespace MvcFrameworkCml.Infrastructure.Repository
{
    public interface IEndUserRepository : IRepository<EndUser>
    {
        Task<bool> TryAuthenticateAsync(String email, String password);

        Task<EndUser> GetUserWithSensitiveDataAsync(String email, Boolean mustBeActive = true);

        Task<Boolean> UpdateUserEmailAsync(Int32 id, String email);

        Task<Boolean> UpdateUserPasswordAsync(Int32 id, String password);
    }
}
