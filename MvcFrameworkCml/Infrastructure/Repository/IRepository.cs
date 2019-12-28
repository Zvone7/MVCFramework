using MvcFrameworkCml.Transfer;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MvcFrameworkCml.Infrastructure.Repository
{
    public interface IRepository<TEntity>
    {
        Task<IEnumerable<TEntity>> GetAllEntitiesAsync();
        Task<TEntity> GetEntityAsync(Int32 id, Boolean mustBeActive = true);
        Task AddEntityAsync(TEntity entity);
        Task UpdateEntityAsync(TEntity entity);
        Task<Boolean> DeleteEntityAsync(int id);
    }
}
