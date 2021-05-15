using System;
using System.Threading.Tasks;
using Cml.DataModels;
using Cml.Transfer;

namespace Cml.Infrastructure.Managers
{
    public interface IEndUserManager : IManager<EndUser>
    {
        Task<Content<EndUser>> AuthenticateUserAsync(
            String email,
            String password);

        Task<Content<EndUser>> GetEntityAsync(
          String email,
          Boolean isHashed = false,
          Boolean requestOnlyActiveUsers = true);

        Task<Content<Boolean>> UpdateUserEmailAsync(
            Int32 id,
            String email);

        Task<Content<Boolean>> UpdateUserPasswordAsync(
            Int32 id,
            String email);
    }
}