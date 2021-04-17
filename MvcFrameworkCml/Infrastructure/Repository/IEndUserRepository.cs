using System;
using System.Threading.Tasks;
using MvcFrameworkCml.DataModels;

namespace MvcFrameworkCml.Infrastructure.Repository
{
    public interface IEndUserRepository : IRepository<EndUser>
    {
        Task<bool> TryAuthenticateAsync(String emailEncrypted, String passwordEncrypted);

        Task<EndUser> GetUserWithSensitiveDataAsync(String email, Boolean mustBeActive = true);

        Task<Boolean> UpdateUserEmailAsync(Int32 id, String emailEncrypted);

        Task<Boolean> UpdateUserPasswordAsync(Int32 id, String passwordHashed);
    }
}
