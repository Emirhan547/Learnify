// Learnify.DataAccess/Repositories/EfCourseDal.cs
using Learnify.DataAccess.Abstract;
using Learnify.DataAccess.Context;
using Learnify.Entity.Concrete;
using Microsoft.EntityFrameworkCore;

namespace Learnify.DataAccess.Repositories
{
    public class EfCourseDal : GenericRepository<Course>, ICourseDal
    {
        public EfCourseDal(LearnifyContext context) : base(context) { }

        public async Task<List<Course>> GetCoursesWithInstructorAsync()
        {
            return await _context.Courses
                .Include(c => c.Instructor)
                .Include(c => c.Category)
                .Include(c => c.Lessons) // 🔹 eklendi
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
            // 🔧 Hata düzeltildi: CategoryId üzerinden filtre
            return await _context.Courses
                .Where(c => c.CategoryId == categoryId)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
