using Learnify.DataAccess.Abstract;
using Learnify.DataAccess.Context;
using Learnify.Entity.Concrete;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Learnify.DataAccess.Repositories
{
    public class EfLessonDal : GenericRepository<Lesson>, ILessonDal
    {
        private readonly LearnifyContext _context;

        public EfLessonDal(LearnifyContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Lesson>> GetLessonsByCourseIdAsync(int courseId)
        {
            return await _context.Lessons
                .Where(l => l.CourseId == courseId) // düzeltme
                .OrderBy(l => l.Order)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
