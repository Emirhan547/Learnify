using FluentValidation;
using Learnify.Business.Abstract;
using Learnify.Business.Concrete;
using Learnify.Business.ValidationRules.AccountValidators;
using Learnify.Business.ValidationRules.CourseValidators;
using Learnify.DataAccess.Abstract;
using Learnify.DataAccess.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Learnify.Business.DependencyResolvers
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddBusinessServices(this IServiceCollection services)
        {
            // generic repo + uow
            services.AddScoped(typeof(IGenericDal<>), typeof(GenericRepository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            // ef concrete özel dal (varsa)
            services.AddScoped<ICategoryDal, EfCategoryDal>();
            services.AddScoped<ICourseDal, EfCourseDal>();
            services.AddScoped<ILessonDal, EfLessonDal>();
            services.AddScoped<IEnrollmentDal, EfEnrollmentDal>();
          

            // managers
            services.AddScoped<ICategoryService, CategoryManager>();
            services.AddScoped<ICourseService, CourseManager>();
            services.AddScoped<ILessonService, LessonManager>();
            services.AddScoped<IEnrollmentService, EnrollmentManager>();
            services.AddScoped<IInstructorService, InstructorManager>();

            services.AddScoped<IAccountService, AccountManager>();


            return services;
        }
        public static IServiceCollection AddValidationRules(this IServiceCollection services)
        {
            services.AddValidatorsFromAssemblyContaining<CreateCourseValidator>();
            return services;
        }
       
    }
}