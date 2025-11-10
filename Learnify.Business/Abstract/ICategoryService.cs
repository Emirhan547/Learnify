using Learnify.Business.Utilities.Results;
using Learnify.DTO.DTOs.CategoryDto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Learnify.Business.Abstract
{
    public interface ICategoryService : IGenericService<CreateCategoryDto, UpdateCategoryDto, ResultCategoryDto>
    {
        Task<IDataResult<List<ResultCategoryDto>>> GetActiveCategoriesAsync();
        Task<IDataResult<ResultCategoryDto>> GetCategoryWithCoursesAsync(int categoryId);
        Task<IDataResult<UpdateCategoryDto>> GetForUpdateAsync(int id);
    }
}
