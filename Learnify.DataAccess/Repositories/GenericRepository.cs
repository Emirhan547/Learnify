using Learnify.DataAccess.Abstract;
using Learnify.DataAccess.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
            _dbSet = _context.Set<T>();
        }

        public async Task<List<T>> GetAllAsync() =>
            await _dbSet.AsNoTracking().ToListAsync();

        public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>> predicate) =>
            await _dbSet.AsNoTracking().Where(predicate).ToListAsync();

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
