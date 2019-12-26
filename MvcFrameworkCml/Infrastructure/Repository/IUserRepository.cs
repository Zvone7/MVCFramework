using System.Threading.Tasks;

namespace MvcFrameworkCml.Infrastructure.Repository
{
    public interface IUserRepository : IDataRepository<EndUser>
    {
        Task<bool> TryAuthenticateAsync(string email, string password);

        Task<EndUser> GetAsync(string email);
    }
}
