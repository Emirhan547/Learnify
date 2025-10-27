using Learnify.DTO.DTOs.LessonDto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Learnify.Business.Abstract
{
    public interface ILessonService
    {
        Task<List<ResultLessonDto>> GetAllAsync();
        Task<ResultLessonDto?> GetByIdAsync(int id);
        Task AddAsync(CreateLessonDto dto);
        Task UpdateAsync(UpdateLessonDto dto);
        Task DeleteAsync(int id);
    }
}
