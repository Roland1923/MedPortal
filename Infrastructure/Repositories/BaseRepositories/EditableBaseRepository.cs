using System;
using System.Threading;
using System.Threading.Tasks;
using Core.IRepositories;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.BaseRepositories
{
    public abstract class EditableBaseRepository<TEntity> : ReadOnlyBaseRepository<TEntity>, IEditableRepository<TEntity> where TEntity : class
    {
        protected EditableBaseRepository(IDatabaseService databaseService) : base(databaseService)
        {
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            DatabaseService.Set<TEntity>().Add(entity);
            try
            {
                await DatabaseService.SaveChangesAsync(new CancellationToken());
            }
            catch
            {
                return null;
            }
            return entity;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var itemToRemove = DatabaseService.Set<TEntity>().Find(id);
            DatabaseService.Set<TEntity>().Remove(itemToRemove);
            try
            {
                return await DatabaseService.SaveChangesAsync(new CancellationToken()) > 0;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> UpdateAsync(TEntity entity)
        {
            DatabaseService.Set<TEntity>().Attach(entity);
            DatabaseService.Entry(entity).State = EntityState.Modified;
            try
            {
                return await DatabaseService.SaveChangesAsync(new CancellationToken()) > 0;
            }
            catch
            {
                return false;
            }
        }
    }
}
