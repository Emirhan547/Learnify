using Learnify.Business.Utilities.Results;
using Learnify.DTO.DTOs.InstructorDto;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Learnify.Business.Abstract
{
    public interface IInstructorService
    {
        Task<IDataResult<List<ResultInstructorDto>>> GetAllAsync();
        Task<IDataResult<ResultInstructorDto>> GetByIdAsync(int id);
        Task<IDataResult<List<ResultInstructorDto>>> GetActiveInstructorsAsync();

        Task<IResult> AddAsync(CreateInstructorDto dto);
        Task<IResult> UpdateAsync(UpdateInstructorDto dto);
        Task<IResult> DeleteAsync(int id);
    }
}
