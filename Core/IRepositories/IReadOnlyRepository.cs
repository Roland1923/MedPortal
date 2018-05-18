using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Core.IRepositories
{
    public interface IReadOnlyRepository<T>
    {
        Task<List<T>> GetAllAsync(string[] includes);
        Task<T> GetByIdAsync(Guid id);
        Task<PagingResult<T>> GetAllPageAsync(int skip, int take);
        Task<PagingResult<T>> GetByFilter(Expression<Func<T, bool>> predicate, int skip, int take);
    }
}
