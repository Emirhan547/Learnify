using Learnify.DTO.DTOs.CourseReviewDto;

namespace Learnify.Business.Abstract
{
    public interface ICourseReviewService
    {
        Task AddReviewAsync(int studentId, CreateCourseReviewDto dto);
        Task<List<ResultCourseReviewDto>> GetCourseReviewsAsync(int courseId);
    }
}
