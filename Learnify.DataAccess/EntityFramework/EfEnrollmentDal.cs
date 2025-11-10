using Learnify.DataAccess.Abstract;
using Learnify.DataAccess.Context;
using Learnify.DataAccess.Repositories;
using Learnify.Entity.Concrete;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

public class EfEnrollmentDal : GenericRepository<Enrollment>, IEnrollmentDal
{
    public EfEnrollmentDal(LearnifyContext context) : base(context) { }

    public async Task<List<Enrollment>> GetAllWithCourseAndStudentAsync()
    {
        return await Query()
            .Include(e => e.Course).ThenInclude(c => c.Lessons)
            .Include(e => e.Student)
            .Include(e => e.LessonProgresses)
            .ToListAsync();
    }

    public async Task<Enrollment?> GetByIdWithCourseAndStudentAsync(int id)
    {
        return await Query()
            .Include(e => e.Course).ThenInclude(c => c.Instructor)
            .Include(e => e.Student)
            .FirstOrDefaultAsync(e => e.Id == id);
    }

    public async Task<List<Enrollment>> GetWhere(Expression<System.Func<Enrollment, bool>> filter)
    {
        return await Query().Where(filter).ToListAsync();
    }
}
