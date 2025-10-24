using Learnify.DTO.DTOs.CourseDto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Learnify.Business.Abstract
{
    public interface ICourseService
    {
        Task AddAsync(CreateCourseDto dto);
        Task UpdateAsync(UpdateCourseDto dto);
        Task DeleteAsync(int id);
        Task<List<ResultCourseDto>> GetAllAsync();
        Task<ResultCourseDto> GetByIdAsync(int id);
    }
}
