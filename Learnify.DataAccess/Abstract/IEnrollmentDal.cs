using Learnify.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learnify.DataAccess.Abstract
{
    public interface IEnrollmentDal:IGenericDal<Enrollment>
    {
        Task<List<Enrollment>> GetEnrollmentsByStudentIdAsync(int studentId);
        Task<int> GetEnrollmentCountByCourseIdAsync(int courseId);
    }
}
