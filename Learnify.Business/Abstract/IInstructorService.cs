using Learnify.DTO.DTOs.InstructorDto;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Learnify.Business.Abstract
{
    public interface IInstructorService
    {
        Task<List<ResultInstructorDto>> GetAllAsync();
        Task<ResultInstructorDto?> GetByIdAsync(int id);
        Task<List<ResultInstructorDto>> GetActiveInstructorsAsync();

        // Yeni eklenen CRUD metotları:
        Task<IdentityResult> AddAsync(CreateInstructorDto dto);
        Task<IdentityResult> UpdateAsync(UpdateInstructorDto dto);
        Task DeleteAsync(int id);
    }
}
