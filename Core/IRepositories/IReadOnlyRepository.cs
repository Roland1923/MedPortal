using System;
using System.Linq;

namespace Core.IRepositories
{
    public interface IReadOnlyRepository<T>
    {
        IQueryable<T> GetAll();
        T GetById(Guid id);
    }
}
