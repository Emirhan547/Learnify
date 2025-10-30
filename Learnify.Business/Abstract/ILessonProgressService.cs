using Learnify.DTO.DTOs.LessonProgressDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learnify.Business.Abstract
{
    public interface ILessonProgressService
    {
        Task AddOrUpdateAsync(CreateLessonProgressDto dto);
        Task<bool> IsLessonCompletedAsync(int lessonId, int studentId);
        Task<int> GetCompletedCountAsync(int courseId, int studentId);
    }
}
