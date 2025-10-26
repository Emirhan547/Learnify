using System.Collections.Generic;
using System.Threading.Tasks;

namespace Learnify.DataAccess.Abstract
{
    public interface IGenericDal<T> where T : class
    {
        Task<List<T>> GetAllAsync(string? includeProperties = null);
        Task<T?> GetByIdAsync(int id);
        Task AddAsync(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
