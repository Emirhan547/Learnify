using Learnify.DTO.DTOs.EnrollmentDto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Learnify.Business.Abstract
{
    public interface IEnrollmentService
    {
        Task<List<ResultEnrollmentDto>> GetAllAsync();
        Task<ResultEnrollmentDto?> GetByIdAsync(int id);
        Task AddAsync(CreateEnrollmentDto dto);
        Task UpdateAsync(UpdateEnrollmentDto dto);
        Task DeleteAsync(int id);
    }
}
