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
        private readonly LearnifyContext _ctx;
        public EfEnrollmentDal(LearnifyContext ctx) : base(ctx) => _ctx = ctx;

        public async Task<List<Enrollment>> GetAllWithCourseAndStudentAsync()
            => await _ctx.Enrollments
                         .AsNoTracking()
                         .Include(e => e.Course)
                         .Include(e => e.Student)
                         .ToListAsync();

        public async Task<Enrollment?> GetByIdWithCourseAndStudentAsync(int id)
            => await _ctx.Enrollments
                         .AsNoTracking()
                         .Include(e => e.Course)
                         .Include(e => e.Student)
                         .FirstOrDefaultAsync(e => e.Id == id);
    }

}
