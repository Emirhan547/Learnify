using Learnify.DataAccess.Abstract;
using Learnify.DataAccess.Context;
using Learnify.Entity.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learnify.DataAccess.Repositories
{
    public class EfLessonDal : GenericRepository<Lesson>, ILessonDal
    {
        public EfLessonDal(LearnifyContext context) : base(context)
        {
        }
        public async Task<List<Lesson>> GetLessonsByCourseIdAsync(int courseId)
        {
            return await _context.Lessons
                .Where(l => l.Id == courseId)
                .OrderBy(l => l.Order)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
