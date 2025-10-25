using Learnify.DataAccess.Abstract;
using Learnify.DataAccess.Context;
using Learnify.Entity.Concrete;

namespace Learnify.DataAccess.Repositories
{
    // ✅ AppUser ile çalışıyor
    public class EfInstructorDal : GenericRepository<AppUser>, IInstructorDal
    {
        public EfInstructorDal(LearnifyContext context) : base(context)
        {
        }
    }
}