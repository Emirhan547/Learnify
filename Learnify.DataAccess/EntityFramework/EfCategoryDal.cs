using Learnify.DataAccess.Abstract;
using Learnify.DataAccess.Context;
using Learnify.Entity.Concrete;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Learnify.DataAccess.Repositories
{
    public class EfCategoryDal : GenericRepository<Category>, ICategoryDal
    {
        private readonly LearnifyContext _context;

        public EfCategoryDal(LearnifyContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Category>> GetActiveCategoriesAsync()
        {
            return await _context.Categories
                .Where(c => c.IsActive)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Category?> GetCategoryWithCoursesAsync(int categoryId)
        {
            return await _context.Categories
                .Include(c => c.Courses)
                    .ThenInclude(crs => crs.Instructor)
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == categoryId);
        }
    }
}
