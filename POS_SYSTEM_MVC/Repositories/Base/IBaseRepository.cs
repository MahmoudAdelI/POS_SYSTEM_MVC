using System.Linq.Expressions;

namespace POS_SYSTEM_MVC.Repositories.Base
{
    public interface IBaseRepository<T> where T : class
    {
        Task AddAsync(T entity);
        Task<T?> GetByIdAsync(int id);
        Task<T?> GetAsync(Expression<Func<T, bool>> expression);
        int Count();
        int Count(Expression<Func<T, bool>> expression);
        void Update(T entity);
        void Delete(T entity);
        void Delete(Expression<Func<T, bool>> expression);
    }
}
