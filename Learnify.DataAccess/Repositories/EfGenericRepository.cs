
using Learnify.DataAccess.Abstract;
using Learnify.DataAccess.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Learnify.DataAccess.Repositories
{
    public class EfGenericRepository<T> : IGenericDal<T> where T : class
    {
        private readonly ApplicationContext _context;
        private readonly DbSet<T> _set;

        public EfGenericRepository(ApplicationContext context, DbSet<T> set)
        {
            _context = context;
            _set = set;
        }

        public async Task InsertAsync(T entity)
        {
            await _set.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            _set.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            _set.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            return await _set.FindAsync(id);
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _set.ToListAsync();
        }

        public async Task<List<T>> GetListByFilterAsync(Expression<Func<T, bool>> filter)
        {
            return await _set.Where(filter).ToListAsync();
        }
    }
}
