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
        private readonly ILessonDal _lessonDal;
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public LessonManager(ILessonDal lessonDal, IUnitOfWork uow, IMapper mapper)
        {
            _lessonDal = lessonDal;
            _uow = uow;
            _mapper = mapper;
        }

        // 🔹 Artık kurs bilgisiyle birlikte dersleri getiriyor
        public async Task<List<ResultLessonDto>> GetAllAsync()
        {
            var lessons = await _lessonDal.GetLessonsWithCourseAsync();
            return _mapper.Map<List<ResultLessonDto>>(lessons);
        }

        public async Task<ResultLessonDto?> GetByIdAsync(int id)
        {
            var lesson = await _lessonDal.GetByIdAsync(id);
            return lesson == null ? null : _mapper.Map<ResultLessonDto>(lesson);
        }

        public async Task AddAsync(CreateLessonDto dto)
        {
            var entity = _mapper.Map<Lesson>(dto);
            await _lessonDal.AddAsync(entity);
            await _uow.CommitAsync();
        }

        public async Task UpdateAsync(UpdateLessonDto dto)
        {
            var existing = await _lessonDal.GetByIdAsync(dto.Id);
            if (existing == null) return;

            _mapper.Map(dto, existing);
            _lessonDal.Update(existing);
            await _uow.CommitAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _lessonDal.GetByIdAsync(id);
            if (entity == null) return;

            _lessonDal.Delete(entity);
            await _uow.CommitAsync();
        }
    }
}
