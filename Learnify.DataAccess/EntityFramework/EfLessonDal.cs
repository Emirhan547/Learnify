using Learnify.DataAccess.Abstract;
using Learnify.DataAccess.Context;
using Learnify.Entity.Concrete;
using Microsoft.EntityFrameworkCore;

namespace Learnify.DataAccess.Repositories
{
    public class EfLessonDal : GenericRepository<Lesson>, ILessonDal
    {
        public EfLessonDal(LearnifyContext context) : base(context) { }

        public async Task<List<Lesson>> GetLessonsByCourseIdAsync(int courseId)
        {
            return await Query()
                .Where(l => l.CourseId == courseId)
                .OrderBy(l => l.Order)
                .ToListAsync();
        }
    }
}
