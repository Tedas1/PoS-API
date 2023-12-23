using Microsoft.EntityFrameworkCore;
using PoS.Abstractions;
using PoS.Abstractions.Repositories;
using System.Linq.Expressions;

namespace PoS.Data.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected IApplicationDbContext _context;

        public Repository(IApplicationDbContext context) => _context = context;

        public async Task<bool> Any(Expression<Func<T, bool>> predicate) => await _context.Set<T>().AnyAsync(predicate);

        public async Task<bool> Any() => await _context.Set<T>().AnyAsync();

        public async Task<T?> Get(Expression<Func<T, bool>> predicate) => await _context.Set<T>().FirstOrDefaultAsync(predicate);

        public async Task<IEnumerable<T>> GetAll() => await _context.Set<T>().ToListAsync();

        public async Task<IEnumerable<T>> GetMany(Expression<Func<T, bool>> predicate) => await _context.Set<T>().Where(predicate).ToListAsync();

        public async Task Create(T entity) => await _context.Set<T>().AddAsync(entity);

        public async Task Create(IEnumerable<T> entities) => await _context.Set<T>().AddRangeAsync(entities);

        public void Delete(T entity) => _context.Set<T>().Remove(entity);

        public async Task Delete(Expression<Func<T, bool>> predicate)
        {
            var entities = await _context.Set<T>().Where(predicate).ToListAsync();
            _context.Set<T>().RemoveRange(entities);
        }

        public async Task DeleteAll()
        {
            var entities = await _context.Set<T>().ToListAsync();
            _context.Set<T>().RemoveRange(entities);
        }

        public async Task Save() => await _context.SaveChangesAsync();

        public void Update(T entity) => _context.Set<T>().Update(entity);

        public void Update(IEnumerable<T> entities) => _context.Set<T>().UpdateRange(entities);
    }
}
