using MvcFrameworkCml.Transfer;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MvcFrameworkCml.Infrastructure.Managers
{
    public interface IManager
    {
        Task<bool> AddEntityAsync(EndUser user);
        Task<bool> DeleteEntityAsync(int id);
        Task<IEnumerable<EndUser>> GetAllEntitiesAsync();
        Task<Content<EndUser>> GetEntityAsync(int id);
    }
}