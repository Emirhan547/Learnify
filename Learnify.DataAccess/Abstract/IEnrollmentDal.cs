using Learnify.Entity.Concrete;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Learnify.DataAccess.Abstract
{
    public interface IEnrollmentDal : IGenericDal<Enrollment>
    {
        Task<List<Enrollment>> GetAllWithCourseAndStudentAsync();
        Task<Enrollment?> GetByIdWithCourseAndStudentAsync(int id);
        Task<List<Enrollment>> GetWhere(Expression<Func<Enrollment, bool>> filter);

    }
}
