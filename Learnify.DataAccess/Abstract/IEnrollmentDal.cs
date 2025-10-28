using Learnify.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Learnify.DataAccess.Abstract
{
    public interface IEnrollmentDal : IGenericDal<Enrollment>
    {
        Task<List<Enrollment>> GetAllWithCourseAndStudentAsync();
        Task<Enrollment?> GetByIdWithCourseAndStudentAsync(int id);
        Task<Enrollment?> FirstOrDefaultAsync(Expression<Func<Enrollment, bool>> predicate);

    }

}
