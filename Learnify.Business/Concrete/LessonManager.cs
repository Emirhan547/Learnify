using AutoMapper;
using Learnify.Business.Abstract;
using Learnify.DataAccess.Abstract;
using Learnify.DTO.DTOs.LessonDto;
using Learnify.Entity.Concrete;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Learnify.Business.Concrete
{
    public class LessonManager : ILessonService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public LessonManager(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task AddAsync(CreateLessonDto dto)
        {
            var entity = _mapper.Map<Lesson>(dto);
            await _unitOfWork.Lessons.AddAsync(entity);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var lesson = await _unitOfWork.Lessons.GetByIdAsync(id);
            if (lesson == null) return;

            _unitOfWork.Lessons.Delete(lesson);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<List<ResultLessonDto>> GetAllAsync()
        {
            var values = await _unitOfWork.Lessons.GetAllAsync();
            return _mapper.Map<List<ResultLessonDto>>(values);
        }

        public async Task<ResultLessonDto?> GetByIdAsync(int id)
        {
            var lesson = await _unitOfWork.Lessons.GetByIdAsync(id);
            return _mapper.Map<ResultLessonDto?>(lesson);
        }

        public async Task UpdateAsync(UpdateLessonDto dto)
        {
            var lesson = await _unitOfWork.Lessons.GetByIdAsync(dto.Id);
            if (lesson == null) return;

            _mapper.Map(dto, lesson);
            _unitOfWork.Lessons.Update(lesson);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<List<ResultLessonDto>> GetLessonsByCourseIdAsync(int courseId)
        {
            var values = await _unitOfWork.Lessons.GetLessonsByCourseIdAsync(courseId);
            return _mapper.Map<List<ResultLessonDto>>(values);
        }

        public async Task<UpdateLessonDto?> GetForUpdateAsync(int id)
        {
            var lesson = await _unitOfWork.Lessons.GetByIdAsync(id);
            return _mapper.Map<UpdateLessonDto?>(lesson);
        }

    }
}
