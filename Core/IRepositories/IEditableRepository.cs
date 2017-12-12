namespace Core.IRepositories
{
    public interface IEditableRepository<T> : IReadOnlyRepository<T>
    {
        void Add(T entity);
        void Delete(T entity);
        void Update(T entity);
    }
}
