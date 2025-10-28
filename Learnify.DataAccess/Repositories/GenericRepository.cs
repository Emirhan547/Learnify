// Learnify.DataAccess/Repositories/GenericRepository.cs
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
            _dbSet = context.Set<T>();
        }

        public async Task<List<T>> GetAllAsync(
            Expression<Func<T, bool>>? filter = null,
            params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _dbSet.AsNoTracking();

            if (filter != null)
                query = query.Where(filter);

            if (includes != null)
            {
                foreach (var include in includes)
                    query = query.Include(include);
            }

            return await query.ToListAsync();
        }

        public async Task<T?> GetByIdAsync(
            int id,
            params Expression<Func<T, object>>[] includes)
        {
            // Not: Primary key adını bilmediğimiz için önce entity’i buluyoruz,
            // sonra tekrar getirip include uyguluyoruz (2 sorgu).
            var entity = await _dbSet.FindAsync(id);
            if (entity == null) return null;

            if (includes is { Length: > 0 })
            {
                IQueryable<T> query = _dbSet.AsQueryable();
                foreach (var include in includes)
                    query = query.Include(include);

                // entity aynı referanssa trackingli döner, AsNoTracking ile temizleyelim:
                return await query.AsNoTracking()
                                  .FirstOrDefaultAsync(e => e == entity);
            }

            return entity;
        }

        public async Task AddAsync(T entity) => await _dbSet.AddAsync(entity);
        public void Update(T entity) => _dbSet.Update(entity);
        public void Delete(T entity) => _dbSet.Remove(entity);
    }
}
