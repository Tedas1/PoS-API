using System.Linq.Expressions;

namespace PoS.Abstractions.Repositories
{
    public interface IRepository<T> where T : class
    {
        Task<T?> Get(Expression<Func<T, bool>> predicate);
        Task<IEnumerable<T>> GetMany(Expression<Func<T, bool>> predicate);
        Task<IEnumerable<T>> GetAll();
        Task<bool> Any();
        Task<bool> Any(Expression<Func<T, bool>> predicate);
        Task Create(T entity);
        Task Create(IEnumerable<T> entities);
        void Update(T entity);
        void Delete(T entity);
        Task Delete(Expression<Func<T, bool>> predicate);
        Task DeleteAll();
        Task Save();
    }
}
