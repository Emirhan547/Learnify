using Learnify.DTO.DTOs.CourseDto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Learnify.Business.Abstract
{
    public interface ICourseService
    {
        Task<List<ResultCourseDto>> GetAllAsync();
        Task<ResultCourseDto?> GetByIdAsync(int id);
        Task<bool> AddAsync(CreateCourseDto dto);
        Task<bool> UpdateAsync(UpdateCourseDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
