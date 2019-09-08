using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProfilerModels.Infrastructure
{
    public interface IDataRepository<TEntity>
    {
        IEnumerable<TEntity> GetAll();
        TEntity Get(Int32 id);
        Task Add(TEntity entity);
        Task Update(TEntity entity);
        Task Delete(TEntity entity);
    }
}
