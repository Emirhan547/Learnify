using Learnify.Business.Utilities.Results;
using Learnify.DTO.DTOs.CourseDto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Learnify.Business.Abstract
{
    public interface ICourseService : IGenericService<CreateCourseDto, UpdateCourseDto, ResultCourseDto>
    {
        Task<IDataResult<List<ResultCourseDto>>> GetCoursesWithInstructorAsync();
        Task<IDataResult<ResultCourseDto>> GetCourseDetailsAsync(int courseId);
        Task<IDataResult<List<ResultCourseDto>>> GetCoursesByCategoryIdAsync(int categoryId);
        Task<IDataResult<UpdateCourseDto>> GetForUpdateAsync(int id);
    }
}
