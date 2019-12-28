using System.Threading.Tasks;
using MvcFrameworkCml;
using MvcFrameworkCml.Transfer;

namespace MvcFrameworkCml.Infrastructure.Managers
{
    public interface IEndUserManager : IManager
    {
        Task<Content<EndUser>> AuthenticateAsync(string email, string password);
        Task<Content<EndUser>> GetUserAsync(
          string email,
          bool isHashed = false,
          bool requestOnlyActiveUsers = true);
    }
}