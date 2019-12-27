using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MvcFrameworkCml.Infrastructure.Repository
{
    public interface IDataRepository<TEntity>
    {
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<TEntity> GetAsync(Int32 id, Boolean mustBeActive = true);
        Task AddAsync(TEntity entity);
        Task UpdateAsync(TEntity entity);
        Task<Boolean> DeleteAsync(int id);
    }
}
