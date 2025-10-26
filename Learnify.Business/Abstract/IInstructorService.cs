using Learnify.DTO.DTOs.InstructorDto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Learnify.Business.Abstract
{
    public interface IInstructorService
    {
        Task<List<ResultInstructorDto>> GetAllAsync();
        Task<ResultInstructorDto?> GetByIdAsync(int id);
        Task<bool> AddAsync(CreateInstructorDto dto);
        Task<bool> UpdateAsync(UpdateInstructorDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
