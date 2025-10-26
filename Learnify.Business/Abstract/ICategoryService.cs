using Learnify.DTO.DTOs.CategoryDto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Learnify.Business.Abstract
{
    public interface ICategoryService
    {
        Task<List<ResultCategoryDto>> GetAllAsync();
        Task<ResultCategoryDto?> GetByIdAsync(int id);
        Task<bool> AddAsync(CreateCategoryDto dto);
        Task<bool> UpdateAsync(UpdateCategoryDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
