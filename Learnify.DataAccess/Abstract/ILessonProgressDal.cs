using Learnify.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learnify.DataAccess.Abstract
{
    public interface ILessonProgressDal : IGenericDal<LessonProgress>
    {
        Task<LessonProgress?> GetByLessonAndStudentAsync(int lessonId, int studentId);
        Task<int> GetCompletedCountByCourseAndStudentAsync(int courseId, int studentId);
    }
}
