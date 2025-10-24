using Learnify.DTO.DTOs.CourseDto;
using Learnify.DTO.DTOs.InstructorDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learnify.Business.Abstract
{
    public interface IInstructorService
    {
        Task AddAsync(CreateInstructorDto dto);
        Task UpdateAsync(UpdateInstructorDto dto);
        Task DeleteAsync(int id);
        Task<List<ResultInstructorDto>> GetAllAsync();
        Task<ResultInstructorDto> GetByIdAsync(int id);
    }
}
