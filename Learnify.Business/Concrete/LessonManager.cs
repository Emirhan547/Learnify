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

        public async Task<List<ResultLessonDto>> GetAllAsync()
        {
            var entities = await _lessonDal.GetAllAsync();
            return _mapper.Map<List<ResultLessonDto>>(entities);
        }

        public async Task<ResultLessonDto?> GetByIdAsync(int id)
        {
            var entity = await _lessonDal.GetByIdAsync(id);
            return entity == null ? null : _mapper.Map<ResultLessonDto>(entity);
        }

        public async Task AddAsync(CreateLessonDto dto)
        {
            var entity = _mapper.Map<Lesson>(dto);
            await _lessonDal.AddAsync(entity);
            await _uow.CommitAsync();
        }

        public async Task UpdateAsync(UpdateLessonDto dto)
        {
            var entity = await _lessonDal.GetByIdAsync(dto.Id);
            if (entity == null) return;

            _mapper.Map(dto, entity);
            _lessonDal.Update(entity);
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
