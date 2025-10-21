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
    public class EfEnrollmentDal : EfGenericRepository<Enrollment>, IEnrollmentDal
    {
        public EfEnrollmentDal(LearnifyContext context, DbSet<Enrollment> set) : base(context, set)
        {
        }
    }
}
