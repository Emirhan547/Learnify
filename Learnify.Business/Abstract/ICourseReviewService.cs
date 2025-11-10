using Learnify.Business.Utilities.Results;
using Learnify.DTO.DTOs.CourseReviewDto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Learnify.Business.Abstract
{
    public interface ICourseReviewService : IGenericService<CreateCourseReviewDto, UpdateCourseReviewDto, ResultCourseReviewDto>
    {
        Task<IDataResult<List<ResultCourseReviewDto>>> GetByCourseIdAsync(int courseId);
        Task<IDataResult<UpdateCourseReviewDto>> GetForUpdateAsync(int id);
    }
}
