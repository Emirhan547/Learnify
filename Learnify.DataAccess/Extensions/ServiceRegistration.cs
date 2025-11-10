using Learnify.DataAccess.Abstract;
using Learnify.DataAccess.Context;
using Learnify.DataAccess.EntityFramework;
using Learnify.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learnify.DataAccess.Extensions
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddDataAccess(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<LearnifyContext>(options =>
                options.UseSqlServer(config.GetConnectionString("DefaultConnection")));

            // Repositories
            services.AddScoped<ICategoryDal, EfCategoryDal>();
            services.AddScoped<ICourseDal, EfCourseDal>();
            services.AddScoped<ILessonDal, EfLessonDal>();
            services.AddScoped<IEnrollmentDal, EfEnrollmentDal>();
            services.AddScoped<IMessageDal, EfMessageDal>();
            services.AddScoped<INotificationDal, EfNotificationDal>();
            services.AddScoped<ILessonProgressDal, EfLessonProgressDal>();
            services.AddScoped<ICourseReviewDal, EfCourseReviewDal>();

            // UnitOfWork
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}
