using Learnify.DataAccess.Abstract;
using Learnify.DataAccess.Context;
using System.Threading.Tasks;

namespace Learnify.DataAccess.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly LearnifyContext _context;

        public ICategoryDal Categories { get; }
        public ICourseDal Courses { get; }
        public ILessonDal Lessons { get; }
        public IEnrollmentDal Enrollments { get; }

        public IMessageDal Messages { get; }

        public INotificationDal Notifications { get; }

        public ILessonProgressDal LessonProgresses { get; }

        public UnitOfWork(LearnifyContext context,
                          ICategoryDal categoryDal,
                          ICourseDal courseDal,
                          ILessonDal lessonDal,
                          IMessageDal messageDal,
                          IEnrollmentDal enrollmentDal,
                          INotificationDal notification,
                          ILessonProgressDal lessonProgresses)
        {
            _context = context;
            Categories = categoryDal;
            Courses = courseDal;
            Lessons = lessonDal;
            Enrollments = enrollmentDal;
            Messages = messageDal;
            Notifications = notification;
            LessonProgresses = lessonProgresses;
        }

        public async Task<int> SaveChangesAsync() => await _context.SaveChangesAsync();

        public async ValueTask DisposeAsync() => await _context.DisposeAsync();
    }
}
