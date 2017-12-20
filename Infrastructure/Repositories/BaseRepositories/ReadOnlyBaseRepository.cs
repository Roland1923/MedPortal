using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.BaseRepositories
{
    public abstract class ReadOnlyBaseRepository<TEntity> : IReadOnlyRepository<TEntity> where TEntity : class
    {
        protected DbContext DbContext;

        protected ReadOnlyBaseRepository(DbContext dbContext)
        {
            DbContext = dbContext;
        }

        public async Task<List<TEntity>> GetAllAsync()
        {
            return await DbContext.Set<TEntity>().ToListAsync();
        }

        public async Task<PagingResult<TEntity>> GetAllPageAsync(int skip, int take)
        {
            var totalRecords = await DbContext.Set<TEntity>().CountAsync();
            var entity = await DbContext.Set<TEntity>()
                .Skip(skip)
                .Take(take)
                .ToListAsync();
            return new PagingResult<TEntity>(entity, totalRecords);
        }

        public async Task<TEntity> GetByIdAsync(Guid id)
        {
            return await DbContext.Set<TEntity>().FindAsync(id);
        }
    }
}
