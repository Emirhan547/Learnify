using Learnify.DTO.DTOs.CourseDto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Learnify.Business.Abstract
{
    public interface ICourseService : IGenericService<CreateCourseDto, UpdateCourseDto, ResultCourseDto>
    {
        Task<List<ResultCourseDto>> GetCoursesWithInstructorAsync();
        Task<ResultCourseDto?> GetCourseDetailsAsync(int courseId);
        Task<List<ResultCourseDto>> GetCoursesByCategoryIdAsync(int categoryId);
        Task<UpdateCourseDto?> GetForUpdateAsync(int id);
    }
}
