using Learnify.DataAccess.Abstract;
using Learnify.DataAccess.Context;
using Learnify.Entity.Concrete;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Learnify.DataAccess.Repositories
{
    public class EfEnrollmentDal : GenericRepository<Enrollment>, IEnrollmentDal
    {
        private readonly LearnifyContext _context;

        public EfEnrollmentDal(LearnifyContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Enrollment>> GetAllWithCourseAndStudentAsync()
        {
            return await _context.Enrollments
                .Include(e => e.Course)
                    .ThenInclude(c => c.Instructor)
                .Include(e => e.Student)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Enrollment?> GetByIdWithCourseAndStudentAsync(int id)
        {
            return await _context.Enrollments
                .Include(e => e.Course)
                    .ThenInclude(c => c.Instructor)
                .Include(e => e.Student)
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<List<Enrollment>> GetWhere(Expression<Func<Enrollment, bool>> filter)
        {
            return await _context.Enrollments.Where(filter).ToListAsync();
        }

    }
}
