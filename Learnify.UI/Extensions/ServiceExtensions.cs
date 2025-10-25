using Learnify.Business.Abstract;
using Learnify.Business.Concrete;
using Learnify.DataAccess.Abstract;

using Learnify.DataAccess.Repositories;

namespace Learnify.UI.Extensions
{
    public static class ServiceExtensions
    {
        public static void AddServiceExtensions(this IServiceCollection services)
        {
            // ✅ Category
            services.AddScoped<ICategoryDal, EfCategoryDal>();
            services.AddScoped<ICategoryService, CategoryManager>();

            // ✅ Course
            services.AddScoped<ICourseDal, EfCourseDal>();
            services.AddScoped<ICourseService, CourseManager>();

            // ✅ Enrollment
            services.AddScoped<IEnrollmentDal, EfEnrollmentDal>();
            services.AddScoped<IEnrollmentService, EnrollmentManager>();

            // ✅ Lesson
            services.AddScoped<ILessonDal, EfLessonDal>();
            services.AddScoped<ILessonService, LessonManager>();

            // ✅ Instructor (EKLENDİ)
            services.AddScoped<IInstructorDal, EfInstructorDal>();
            services.AddScoped<IInstructorService, InstructorManager>();
        }
    }
}