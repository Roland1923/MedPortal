using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Core.IRepositories;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.BaseRepositories
{
    public abstract class ReadOnlyBaseRepository<TEntity> : IReadOnlyRepository<TEntity> where TEntity : class
    {
        protected IDatabaseService DatabaseService;

        protected ReadOnlyBaseRepository(IDatabaseService dbContext)
        {
            DatabaseService = dbContext;
        }

        public async Task<List<TEntity>> GetAllAsync(string[] includes)
        {
            /*var query = DatabaseService.Set<TEntity>().AsQueryable();
            if (includes != null)
            {
                foreach (string include in includes)
                {
                    query = query.AsNoTracking().Include(include);

                }
            }

            return await query.ToListAsync();*/
            return await DatabaseService.Set<TEntity>().ToListAsync();
        }

        public async Task<PagingResult<TEntity>> GetAllPageAsync(int skip, int take)
        {
            var totalRecords = await DatabaseService.Set<TEntity>().CountAsync();
            var entity = await DatabaseService.Set<TEntity>()
                .Skip(skip)
                .Take(take)
                .ToListAsync();
            return new PagingResult<TEntity>(entity, totalRecords);
        }

        public async Task<PagingResult<TEntity>> GetByFilter(Expression<Func<TEntity, bool>> predicate, int skip, int take)
        {
            var totalRecords = await DatabaseService.Set<TEntity>().CountAsync();
            var records = await DatabaseService.Set<TEntity>().Where(predicate)
                .Skip(skip)
                .Take(take)
                .ToListAsync();
            return new PagingResult<TEntity>(records, totalRecords);
        }

        public async Task<TEntity> GetByIdAsync(Guid id)
        {
            return await DatabaseService.Set<TEntity>().FindAsync(id);
        }
        
    }
}
