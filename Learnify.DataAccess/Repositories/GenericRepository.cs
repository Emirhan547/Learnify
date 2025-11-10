using Learnify.DataAccess.Abstract;
using Learnify.DataAccess.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Learnify.DataAccess.Repositories
{
    public class GenericRepository<T> : IGenericDal<T> where T : class
    {
        protected readonly LearnifyContext _context;
        protected readonly DbSet<T> _dbSet;

        public GenericRepository(LearnifyContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        // -------- QUERY --------
        public IQueryable<T> Query(bool tracking = false)
        {
            return tracking
                ? _dbSet.AsQueryable()
                : _dbSet.AsNoTracking().AsQueryable();
        }

        public async Task<T?> GetByIdAsync(int id, bool tracking = false)
        {
            return tracking
                ? await _dbSet.FindAsync(id)
                : await _dbSet.AsNoTracking().FirstOrDefaultAsync(x => EF.Property<int>(x, "Id") == id);
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _dbSet.AsNoTracking().ToListAsync();
        }

        public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.AsNoTracking().Where(predicate).ToListAsync();
        }

        // -------- COMMAND --------
        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public async Task AddRangeAsync(IEnumerable<T> entities)
        {
            await _dbSet.AddRangeAsync(entities);
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }

        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }

        public void DeleteRange(IEnumerable<T> entities)
        {
            _dbSet.RemoveRange(entities);
        }
    }
}
