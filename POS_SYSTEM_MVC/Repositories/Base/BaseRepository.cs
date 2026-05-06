using Microsoft.EntityFrameworkCore;
using POS_SYSTEM_MVC.Data;
using System.Linq.Expressions;

namespace POS_SYSTEM_MVC.Repositories.Base
{
    public class BaseRepository<T>(POSContext context) : IBaseRepository<T> where T : class
    {
        protected readonly POSContext _context = context;
        protected readonly DbSet<T> _dbset = context.Set<T>();

        public async Task AddAsync(T entity) => await _dbset.AddAsync(entity);

        public async Task<T?> GetByIdAsync(int id) => await _dbset.FindAsync(id);

        public async Task<IReadOnlyList<T>> GetAllAsync() => await _dbset.ToListAsync();

        public async Task<IReadOnlyList<T>> GetAllAsync(params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _dbset;
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
            return await query.ToListAsync();
        }

        public Task<T?> GetAsync(Expression<Func<T, bool>> expression) => _dbset.FirstOrDefaultAsync(expression);

        public int Count() => _dbset.Count();

        public int Count(Expression<Func<T, bool>> expression) => _dbset.Count(expression);

        public void Update(T entity) => _dbset.Update(entity);

        public void Delete(T entity) => _dbset.Remove(entity);

        public void Delete(Expression<Func<T, bool>> expression)
        {
            var entity = _dbset.FirstOrDefault(expression);
            if (entity != null)
            {
                _dbset.Remove(entity);
            }
        }




    }
}
