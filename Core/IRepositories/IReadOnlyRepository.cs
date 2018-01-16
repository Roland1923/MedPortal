using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.IRepositories
{
    public interface IReadOnlyRepository<T>
    {
        Task<List<T>> GetAllAsync();
        Task<PagingResult<T>> GetAllPageAsync(int skip, int take);
        Task<T> GetByIdAsync(Guid id);
    }
}
