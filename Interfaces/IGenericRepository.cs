using skinet.Entities;

namespace skinet.Interfaces
{
    public interface IGenericRepository<T>where T:BaseEntity
    {
        Task<T?>GetByIdAsync(int id);
        Task<IReadOnlyList<T>> GetAllAsync();
        Task<T?>GetEntityWithSpec(ISpecification<T> spec);
        Task<IReadOnlyList<T>> GetAllWithSpec(ISpecification<T> spec);
        Task<TResult?> GetEntityWithSpec<TResult>(ISpecification<T,TResult> spec);
        Task<IReadOnlyList<TResult>> GetAllWithSpec<TResult>(ISpecification<T, TResult> spec);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        Task<bool> SaveChangesAsync();
        bool Existed(int id);
        Task<int>CountAsync(ISpecification<T>spec);
    }
}
