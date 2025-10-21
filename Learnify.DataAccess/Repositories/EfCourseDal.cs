using Learnify.DataAccess.Abstract;
using Learnify.DataAccess.Context;
using Learnify.Entity.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learnify.DataAccess.Repositories
{
    public class EfCourseDal : EfGenericRepository<Course>, ICourseDal
    {
        private readonly ApplicationContext _context;
        public EfCourseDal(ApplicationContext context, DbSet<Course> set) : base(context, set)
        {
            _context = context;
        }

        public async Task<List<Course>> GetCoursesWithCategoryAsync()
        {
           return await _context.Courses
                .Include(x => x.Category)
                .ToListAsync();
        }
    }
}
