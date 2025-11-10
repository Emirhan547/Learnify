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
    public class EfCourseDal : GenericRepository<Course>, ICourseDal
    {
        public EfCourseDal(LearnifyContext context) : base(context) { }

        public async Task<List<Course>> GetCoursesWithInstructorAsync()
        {
            return await Query()
                .Include(c => c.Instructor)
                .Include(c => c.Category)
                .ToListAsync();
        }

        public async Task<Course?> GetCourseDetailsAsync(int courseId)
        {
            return await Query()
                .Where(c => c.Id == courseId)
                .Include(c => c.Instructor)
                .Include(c => c.Category)
                .Include(c => c.Lessons)
                .FirstOrDefaultAsync();
        }

        public async Task<List<Course>> GetCoursesByCategoryIdAsync(int categoryId)
        {
            return await Query()
                .Where(c => c.CategoryId == categoryId)
                .Include(c => c.Instructor)
                .Include(c => c.Category)
                .ToListAsync();
        }
    }
}
