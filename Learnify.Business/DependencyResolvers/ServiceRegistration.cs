using FluentValidation;
using Learnify.Business.Abstract;
using Learnify.Business.Concrete;
using Learnify.Business.MappingProfiles;
using Learnify.Business.ValidationRules.CourseValidators;
using Learnify.DataAccess.Abstract;
using Learnify.DataAccess.EntityFramework;
using Learnify.DataAccess.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Learnify.Business.DependencyResolvers
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddBusinessLayerServices(this IServiceCollection services)
        {
            // 🧩 DataAccess: UnitOfWork + Repository Kayıtları
            services.AddScoped(typeof(IGenericDal<>), typeof(GenericRepository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<ICategoryDal, EfCategoryDal>();
            services.AddScoped<ICourseDal, EfCourseDal>();
            services.AddScoped<ILessonDal, EfLessonDal>();
            services.AddScoped<IEnrollmentDal, EfEnrollmentDal>();
            services.AddScoped<IMessageDal, EfMessageDal>();
            services.AddScoped<INotificationDal, EfNotificationDal>();
           
            services.AddScoped<INotificationDal, EfNotificationDal>();
            services.AddScoped<ILessonProgressDal, EfLessonProgressDal>();


            // 🧠 Manager (Business) Katmanı
            services.AddScoped<ICategoryService, CategoryManager>();
            services.AddScoped<ICourseService, CourseManager>();
            services.AddScoped<ILessonService, LessonManager>();
            services.AddScoped<IEnrollmentService, EnrollmentManager>();
            services.AddScoped<IInstructorService, InstructorManager>();
            services.AddScoped<IMessageService, MessageManager>();
            services.AddScoped<INotificationService, NotificationManager>();
            services.AddScoped<ILessonProgressService, LessonProgressManager>();

            // 🔁 AutoMapper Profilleri
            services.AddAutoMapper(typeof(GeneralMapping));

            // ✅ FluentValidation (tüm validator’lar otomatik bulunur)
            services.AddValidatorsFromAssemblyContaining<CreateCourseValidator>();

            return services;
        }
    }
}
