using Learnify.Entity.Concrete;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Learnify.DataAccess.Abstract
{
    public interface ILessonDal : IGenericDal<Lesson>
    {
        Task<List<Lesson>> GetLessonsByCourseIdAsync(int courseId);
    }
}
