using System.Threading.Tasks;

namespace Core.IRepositories
{
    public interface IEditableRepository<T> : IReadOnlyRepository<T>
    {
        Task<T> AddAsync(T entity);
        Task<bool> DeleteAsync(T entity);
        Task<bool> UpdateAsync(T entity);
    }
}
