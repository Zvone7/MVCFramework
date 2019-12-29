using MvcFrameworkCml.Transfer;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MvcFrameworkCml.Infrastructure.Managers
{
    public interface IManager<T>
    {
        Task<Content<Boolean>> AddEntityAsync(T entity);
        Task<Content<Boolean>> UpdateEntityAsync(T entity);
        Task<Content<Boolean>> DeleteEntityAsync(int id);
        Task<Content<IEnumerable<EndUser>>> GetAllEntitiesAsync();
        Task<Content<EndUser>> GetEntityAsync(int id);
    }
}