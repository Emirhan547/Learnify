using Learnify.DataAccess.Abstract;
using Learnify.DataAccess.Context;
using Microsoft.Extensions.DependencyInjection;

namespace Learnify.DataAccess.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly LearnifyContext _context;
        private readonly IServiceProvider _serviceProvider;

        public UnitOfWork(LearnifyContext context, IServiceProvider serviceProvider)
        {
            _context = context;
            _serviceProvider = serviceProvider;
        }

        private T GetRepository<T>() where T : notnull =>
            _serviceProvider.GetRequiredService<T>();

        public ICategoryDal Categories => GetRepository<ICategoryDal>();
        public ICourseDal Courses => GetRepository<ICourseDal>();
        public ILessonDal Lessons => GetRepository<ILessonDal>();
        public IEnrollmentDal Enrollments => GetRepository<IEnrollmentDal>();
        public IMessageDal Messages => GetRepository<IMessageDal>();
        public INotificationDal Notifications => GetRepository<INotificationDal>();
        public ILessonProgressDal LessonProgresses => GetRepository<ILessonProgressDal>();
        public ICourseReviewDal CourseReviews => GetRepository<ICourseReviewDal>();

        public async Task<int> SaveChangesAsync() => await _context.SaveChangesAsync();

        public async ValueTask DisposeAsync() => await _context.DisposeAsync();
    }
}
