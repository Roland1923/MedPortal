using System.Threading.Tasks;
using Core.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.BaseRepositories
{
    public abstract class EditableBaseRepository<TEntity> : ReadOnlyBaseRepository<TEntity>, IEditableRepository<TEntity> where TEntity : class
    {
        protected EditableBaseRepository(DbContext dbContext) : base(dbContext)
        {
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            DbContext.Set<TEntity>().Add(entity);
            try
            {
                await DbContext.SaveChangesAsync();
            }
            catch
            {
                return null;
            }
            return entity;
        }

        public async Task<bool> DeleteAsync(TEntity entity)
        {
            DbContext.Set<TEntity>().Remove(entity);
            try
            {
                return await DbContext.SaveChangesAsync() > 0;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> UpdateAsync(TEntity entity)
        {
            DbContext.Set<TEntity>().Attach(entity);
            DbContext.Entry(entity).State = EntityState.Modified;
            try
            {
                return await DbContext.SaveChangesAsync() > 0;
            }
            catch
            {
                return false;
            }
        }
    }
}
