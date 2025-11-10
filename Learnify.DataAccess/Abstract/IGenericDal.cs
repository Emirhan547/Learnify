using System.Linq.Expressions;

namespace Learnify.DataAccess.Abstract
{
    public interface IGenericDal<T> where T : class
    {
        IQueryable<T> Query(bool tracking = false);

        Task<T?> GetByIdAsync(int id, bool tracking = false);
        Task<List<T>> GetAllAsync();
        Task<List<T>> GetAllAsync(Expression<Func<T, bool>> predicate);

        Task AddAsync(T entity);
        Task AddRangeAsync(IEnumerable<T> entities);

        void Update(T entity);
        void Delete(T entity);
        void DeleteRange(IEnumerable<T> entities);
    }
}
