using Learnify.Entity.Concrete;
using System.Linq.Expressions;

namespace Learnify.DataAccess.Abstract
{
    public interface IEnrollmentDal : IGenericDal<Enrollment>
    {
        Task<List<Enrollment>> GetAllWithCourseAndStudentAsync();
        Task<Enrollment?> GetByIdWithCourseAndStudentAsync(int id);
        Task<List<Enrollment>> GetWhere(Expression<System.Func<Enrollment, bool>> filter);
    }
}
