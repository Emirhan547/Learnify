using Learnify.Entity.Concrete;

namespace Learnify.DataAccess.Abstract
{
    // ✅ AppUser kullanıyoruz artık
    public interface IInstructorDal : IGenericDal<AppUser>
    {
    }
}