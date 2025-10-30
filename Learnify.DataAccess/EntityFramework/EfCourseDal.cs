using Learnify.DataAccess.Abstract;
using Learnify.DataAccess.Context;
using Learnify.Entity.Concrete;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Learnify.DataAccess.Repositories
{
    public class EfCourseDal : GenericRepository<Course>, ICourseDal
    {
        private readonly LearnifyContext _context;

        public EfCourseDal(LearnifyContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Course>> GetCoursesWithInstructorAsync()
        {
            return await _context.Courses
                .Include(c => c.Instructor)
                .Include(c => c.Category)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Course?> GetCourseDetailsAsync(int courseId)
        {
            return await _context.Courses
                .Include(c => c.Instructor)
                .Include(c => c.Lessons)
                .Include(c => c.Category)
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == courseId);
        }

        public async Task<List<Course>> GetCoursesByCategoryIdAsync(int categoryId)
        {
            return await _context.Courses
                .Where(c => c.CategoryId == categoryId) // düzeltme
                .Include(c => c.Instructor)
                .Include(c => c.Category)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
