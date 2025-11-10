using Learnify.Business.Utilities.Results;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Learnify.Business.Abstract
{
    public interface IGenericService<TCreateDto, TUpdateDto, TResultDto>
    {
        Task<IDataResult<List<TResultDto>>> GetAllAsync();
        Task<IDataResult<TResultDto>> GetByIdAsync(int id);
        Task<IResult> AddAsync(TCreateDto dto);
        Task<IResult> UpdateAsync(TUpdateDto dto);
        Task<IResult> DeleteAsync(int id);
    }
}
