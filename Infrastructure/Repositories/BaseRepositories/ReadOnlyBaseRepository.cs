using System;
using System.Linq;
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

        public IQueryable<TEntity> GetAll()
        {
            return DbContext.Set<TEntity>();
        }

        public TEntity GetById(Guid id)
        {
            return DbContext.Set<TEntity>().Find(id);
        }
    }
}
