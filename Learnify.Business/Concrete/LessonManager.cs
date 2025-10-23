using AutoMapper;
using Learnify.Business.Abstract;
using Learnify.DataAccess.Abstract;
using Learnify.DTO.DTOs.LessonDto;
using Learnify.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learnify.Business.Concrete
{
    public class LessonManager : ILessonService
    {
        private readonly ILessonDal _lessonDal;
        private readonly IMapper _mapper;

        public LessonManager(ILessonDal lessonDal, IMapper mapper)
        {
            _lessonDal = lessonDal;
            _mapper = mapper;
        }

        public async Task<List<ResultLessonDto>> GetAllAsync()
        {
            var values = await _lessonDal.GetAllWithIncludeAsync(x => x.Course);
            return _mapper.Map<List<ResultLessonDto>>(values);
        }

        public async Task<ResultLessonDto> GetByIdAsync(int id)
        {
            var value = await _lessonDal.GetByIdWithIncludeAsync(id, x => x.Course);
            return _mapper.Map<ResultLessonDto>(value);
        }

        public async Task AddAsync(CreateLessonDto dto)
        {
            var entity = _mapper.Map<Lesson>(dto);
            await _lessonDal.AddAsync(entity);
        }

        public async Task UpdateAsync(UpdateLessonDto dto)
        {
            var entity = _mapper.Map<Lesson>(dto);
            await _lessonDal.UpdateAsync(entity);
        }

        public async Task DeleteAsync(int id)
        {
            await _lessonDal.DeleteAsync(id);
        }
    }
}
