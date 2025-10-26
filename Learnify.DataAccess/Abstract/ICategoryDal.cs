using Learnify.Entity.Concrete;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Learnify.DataAccess.Abstract
{
    public interface ICategoryDal : IGenericDal<Category>
    {
        Task<List<Category>> GetActiveCategoriesAsync();
        Task<Category?> GetCategoryWithCoursesAsync(int categoryId);
    }
}
