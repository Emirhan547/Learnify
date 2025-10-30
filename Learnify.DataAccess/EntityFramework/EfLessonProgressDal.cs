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
        private readonly LearnifyContext _context;
        public EfLessonProgressDal(LearnifyContext context) : base(context)
        {
            _context = context;
        }

        public async Task<LessonProgress?> GetByLessonAndStudentAsync(int lessonId, int studentId)
        {
            return await _context.LessonProgresses
                .FirstOrDefaultAsync(x => x.LessonId == lessonId && x.StudentId == studentId);
        }

        public async Task<int> GetCompletedCountByCourseAndStudentAsync(int courseId, int studentId)
        {
            return await _context.LessonProgresses
                .Where(x => x.CourseId == courseId && x.StudentId == studentId && x.IsCompleted)
                .CountAsync();
        }
    }
}
