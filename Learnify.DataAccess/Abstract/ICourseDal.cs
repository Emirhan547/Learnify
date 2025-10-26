using Learnify.Entity.Concrete;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Learnify.DataAccess.Abstract
{
    public interface ICourseDal : IGenericDal<Course>
    {
        Task<List<Course>> GetCoursesWithInstructorAsync();
        Task<Course?> GetCourseDetailsAsync(int courseId);
        Task<List<Course>> GetCoursesByCategoryIdAsync(int categoryId);
    }
}
