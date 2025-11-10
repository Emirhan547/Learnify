using Learnify.DataAccess.Abstract;
using Learnify.DataAccess.Context;
using Learnify.DataAccess.Repositories;
using Learnify.Entity.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learnify.DataAccess.EntityFramework
{
    public class EfLessonProgressDal : GenericRepository<LessonProgress>, ILessonProgressDal
    {
        public EfLessonProgressDal(LearnifyContext context) : base(context) { }

        public async Task<LessonProgress?> GetByLessonAndStudentAsync(int lessonId, int studentId)
        {
            return await Query()
                .FirstOrDefaultAsync(lp => lp.LessonId == lessonId && lp.StudentId == studentId);
        }

        public async Task<int> GetCompletedCountByCourseAndStudentAsync(int courseId, int studentId)
        {
            return await Query()
                .Where(lp => lp.CourseId == courseId && lp.StudentId == studentId && lp.IsCompleted)
                .CountAsync();
        }
    }
}
