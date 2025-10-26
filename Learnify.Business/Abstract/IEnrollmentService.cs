using Learnify.DTO.DTOs.EnrollmentDto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Learnify.Business.Abstract
{
    public interface IEnrollmentService
    {
        Task<List<ResultEnrollmentDto>> GetAllAsync();
        Task<ResultEnrollmentDto?> GetByIdAsync(int id);
        Task<bool> AddAsync(CreateEnrollmentDto dto);
        Task<bool> UpdateAsync(UpdateEnrollmentDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
