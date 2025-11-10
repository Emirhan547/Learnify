using Learnify.Business.Utilities.Results;
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
        Task<IResult> AddOrUpdateAsync(CreateLessonProgressDto dto);
        Task<IDataResult<bool>> IsLessonCompletedAsync(int lessonId, int studentId);
        Task<IDataResult<int>> GetCompletedCountAsync(int courseId, int studentId);
    }
}
