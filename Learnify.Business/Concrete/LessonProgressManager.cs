using AutoMapper;
using Learnify.Business.Abstract;
using Learnify.DataAccess.Abstract;
using Learnify.DTO.DTOs.LessonProgressDto;
using Learnify.Entity.Concrete;
using System.Threading.Tasks;

namespace Learnify.Business.Concrete
{
    public class LessonProgressManager : ILessonProgressService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public LessonProgressManager(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task AddOrUpdateAsync(CreateLessonProgressDto dto)
        {
            var existing = await _unitOfWork.LessonProgresses
                .GetByLessonAndStudentAsync(dto.LessonId, dto.StudentId);

            if (existing != null)
            {
                existing.IsCompleted = dto.IsCompleted;
                await _unitOfWork.SaveChangesAsync();
                return;
            }

            var entity = _mapper.Map<LessonProgress>(dto);
            await _unitOfWork.LessonProgresses.AddAsync(entity);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<bool> IsLessonCompletedAsync(int lessonId, int studentId)
        {
            var data = await _unitOfWork.LessonProgresses
                .GetByLessonAndStudentAsync(lessonId, studentId);

            return data != null && data.IsCompleted;
        }

        public async Task<int> GetCompletedCountAsync(int courseId, int studentId)
        {
            return await _unitOfWork.LessonProgresses
                .GetCompletedCountByCourseAndStudentAsync(courseId, studentId);
        }
    }
}
