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
    public class EfEnrollmentDal : GenericRepository<Enrollment>, IEnrollmentDal
    {
        public EfEnrollmentDal(LearnifyContext context) : base(context)
        {
        }
        public async Task<List<Enrollment>> GetEnrollmentsByStudentIdAsync(int studentId)
        {
            return await _context.Enrollments
                .Include(e => e.Course)
                .Where(e => e.Id == studentId)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<int> GetEnrollmentCountByCourseIdAsync(int courseId)
        {
            return await _context.Enrollments
                .CountAsync(e => e.Id == courseId);
        }
    }
}
