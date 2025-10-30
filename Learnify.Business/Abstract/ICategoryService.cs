using Learnify.DTO.DTOs.CategoryDto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Learnify.Business.Abstract
{
    public interface ICategoryService : IGenericService<CreateCategoryDto, UpdateCategoryDto, ResultCategoryDto>
    {
        Task<List<ResultCategoryDto>> GetActiveCategoriesAsync();
        Task<ResultCategoryDto?> GetCategoryWithCoursesAsync(int categoryId);

        // 🔹 Yeni metot
        Task<UpdateCategoryDto?> GetForUpdateAsync(int id);
    }
}
