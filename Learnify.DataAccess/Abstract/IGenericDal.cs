using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Learnify.DataAccess.Abstract
{
    public interface IGenericDal<T> where T : class
    {
        Task<List<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(int id);

        // Include'lu veri çekme
        Task<List<T>> GetAllWithIncludeAsync(params Expression<Func<T, object>>[] includeProperties);
        Task<T> GetByIdWithIncludeAsync(int id, params Expression<Func<T, object>>[] includeProperties);
    }
}
