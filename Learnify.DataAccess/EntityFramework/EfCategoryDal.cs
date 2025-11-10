using Learnify.DataAccess.Abstract;
using Learnify.DataAccess.Context;
using Learnify.DataAccess.Repositories;
using Learnify.Entity.Concrete;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Learnify.DataAccess.EntityFramework
{
    public class EfCategoryDal : GenericRepository<Category>, ICategoryDal
    {
        public EfCategoryDal(LearnifyContext context) : base(context) { }

        public async Task<List<Category>> GetActiveCategoriesAsync()
        {
            return await Query()
                .Where(c => c.IsActive)
                .ToListAsync();
        }

        public async Task<Category?> GetCategoryWithCoursesAsync(int categoryId)
        {
            return await Query()
                .Where(c => c.Id == categoryId)
                .Include(c => c.Courses)
                .ThenInclude(c => c.Instructor)
                .FirstOrDefaultAsync();
        }

        public async Task<List<Category>> GetAllWithCourseCountAsync()
        {
            return await Query(true)
                .Include(c => c.Courses)
                .ToListAsync();
        }
    }
}
