using Core.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.BaseRepositories
{
    public abstract class EditableBaseRepository<TEntity> : ReadOnlyBaseRepository<TEntity>, IEditableRepository<TEntity> where TEntity : class
    {
        protected EditableBaseRepository(DbContext dbContext) : base(dbContext)
        {
        }

        public void Add(TEntity entity)
        {
            DbContext.Set<TEntity>().Add(entity);
            DbContext.SaveChanges();
        }

        public void Delete(TEntity entity)
        {
            DbContext.Set<TEntity>().Remove(entity);
            DbContext.SaveChanges();
        }

        public void Update(TEntity entity)
        {
            DbContext.Set<TEntity>().Update(entity);
            DbContext.SaveChanges();
        }
    }
}
