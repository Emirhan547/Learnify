using Learnify.DataAccess.Abstract;
using Learnify.DataAccess.Context;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Learnify.DataAccess.Repositories
{
    public class GenericRepository<T> : IGenericDal<T> where T : class
    {
        protected readonly LearnifyContext _context;
        protected readonly DbSet<T> _dbSet;

        public GenericRepository(LearnifyContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public async Task<List<T>> GetAllAsync() =>
            await _dbSet.AsNoTracking().ToListAsync();

        public async Task<T?> GetByIdAsync(int id) =>
            await _dbSet.FindAsync(id);

        public async Task AddAsync(T entity) =>
            await _dbSet.AddAsync(entity);

        public void Update(T entity) =>
            _dbSet.Update(entity);

        public void Delete(T entity) =>
            _dbSet.Remove(entity);
    }
}
