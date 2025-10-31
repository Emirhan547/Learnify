
namespace Learnify.DataAccess.Abstract
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        ICategoryDal Categories { get; }
        ICourseDal Courses { get; }
        ILessonDal Lessons { get; }
        IEnrollmentDal Enrollments { get; }
        IMessageDal Messages { get; }
        INotificationDal Notifications { get; }
        ILessonProgressDal LessonProgresses { get; }
        ICourseReviewDal CourseReviews { get; }
        Task<int> SaveChangesAsync();
    }
}
