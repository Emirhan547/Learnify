using Learnify.Entity.Concrete;

namespace Learnify.DataAccess.Abstract
{
    public interface ICourseReviewDal : IGenericDal<CourseReview>
    {
        Task<List<CourseReview>> GetCourseReviewsAsync(int courseId);
    }
}
