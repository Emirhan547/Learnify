using Learnify.DataAccess.Abstract;
using Learnify.DataAccess.Context;
using Learnify.DataAccess.Repositories;
using Learnify.Entity.Concrete;
using Microsoft.EntityFrameworkCore;
using System;

namespace Learnify.DataAccess.EntityFramework
{
    public class EfCourseReviewDal : GenericRepository<CourseReview>, ICourseReviewDal
    {
        public EfCourseReviewDal(LearnifyContext context) : base(context) { }

        public async Task<List<CourseReview>> GetCourseReviewsAsync(int courseId)
        {
            return await _context.CourseReviews
                .Include(x => x.Student)
                .Where(x => x.CourseId == courseId)
                .OrderByDescending(x => x.Id)
                .ToListAsync();
        }
    }
}
