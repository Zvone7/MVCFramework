﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MvcFrameworkCml.Infrastructure
{
    public interface IDataRepository<TEntity>
    {
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<TEntity> GetAsync(Int32 id);
        Task AddAsync(TEntity entity);
        Task UpdateAsync(TEntity entity);
        Task DeleteAsync(Int32 id);
    }
}
