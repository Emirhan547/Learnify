using Learnify.DTO.DTOs.CategoryDto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Learnify.Business.Abstract
{
    public interface ICategoryService
    {
        Task<List<ResultCategoryDto>> GetAllAsync();
        Task<ResultCategoryDto?> GetByIdAsync(int id);
        Task AddAsync(CreateCategoryDto dto);
        Task UpdateAsync(UpdateCategoryDto dto);
        Task DeleteAsync(int id);
    }
}
