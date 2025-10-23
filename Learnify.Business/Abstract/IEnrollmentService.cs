using Learnify.DTO.DTOs.EnrollmentDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learnify.Business.Abstract
{
    public interface IEnrollmentService
    {
        Task<List<ResultEnrollmentDto>> GetAllAsync();
        Task<ResultEnrollmentDto> GetByIdAsync(int id);
        Task AddAsync(CreateEnrollmentDto dto);
        Task UpdateAsync(UpdateEnrollmentDto dto);
        Task DeleteAsync(int id);
    }
}
