using System.Collections.Generic;
using System.Threading.Tasks;

namespace Learnify.Business.Abstract
{
    public interface IGenericService<TCreateDto, TUpdateDto, TResultDto>
    {
        Task<List<TResultDto>> GetAllAsync();
        Task<TResultDto?> GetByIdAsync(int id);
        Task AddAsync(TCreateDto dto);
        Task UpdateAsync(TUpdateDto dto);
        Task DeleteAsync(int id);
    }
}
