using Learnify.DTO.DTOs.CourseDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learnify.Business.Abstract
{
    public interface ICourseService
    {
        Task<List<ResultCourseDto>> GetAllAsync();
        Task<ResultCourseDto> GetByIdAsync(int id);
        Task AddAsync(CreateCourseDto dto);
        Task UpdateAsync(UpdateCourseDto dto);
        Task DeleteAsync(int id);
    }
}
