using Learnify.DTO.DTOs.LessonDto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Learnify.Business.Abstract
{
    public interface ILessonService
    {
        Task<List<ResultLessonDto>> GetAllAsync();
        Task<ResultLessonDto?> GetByIdAsync(int id);
        Task<bool> AddAsync(CreateLessonDto dto);
        Task<bool> UpdateAsync(UpdateLessonDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
