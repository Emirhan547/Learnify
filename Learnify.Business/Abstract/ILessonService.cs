using Learnify.DTO.DTOs.LessonDto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Learnify.Business.Abstract
{
    public interface ILessonService : IGenericService<CreateLessonDto, UpdateLessonDto, ResultLessonDto>
    {
        Task<List<ResultLessonDto>> GetLessonsByCourseIdAsync(int courseId);

        // 🔹 Yeni ekliyoruz:
        Task<UpdateLessonDto?> GetForUpdateAsync(int id);
       
    }
}
