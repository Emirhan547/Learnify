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
            services.AddScoped<ICategoryDal, EfCategoryDal>();
            services.AddScoped<ICategoryService, CategoryManager>();

            services.AddScoped<ICourseDal, EfCourseDal>();
            services.AddScoped<ICourseService, CourseManager>();

            services.AddScoped<IEnrollmentDal, EfEnrollmentDal>();
            services.AddScoped<IEnrollmentService, EnrollmentManager>();

            services.AddScoped<ILessonDal, EfLessonDal>();
            services.AddScoped<ILessonService, LessonManager>();
        }
        }
}
