using AutoMapper;
using Learnify.Business.Abstract;
using Learnify.DataAccess.Abstract;
using Learnify.DTO.DTOs.CourseDto;
using Learnify.Entity.Concrete;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Learnify.Business.Concrete
{
    public class CourseManager : ICourseService
    {
        private readonly ICourseDal _courseDal;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CourseManager(ICourseDal courseDal, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _courseDal = courseDal;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<ResultCourseDto>> GetAllAsync()
        {
            var entities = await _courseDal.GetAllAsync();
            return _mapper.Map<List<ResultCourseDto>>(entities);
        }

        public async Task<ResultCourseDto?> GetByIdAsync(int id)
        {
            var entity = await _courseDal.GetByIdAsync(id);
            return entity == null ? null : _mapper.Map<ResultCourseDto>(entity);
        }

        public async Task<bool> AddAsync(CreateCourseDto dto)
        {
            var entity = _mapper.Map<Course>(dto);
            await _courseDal.AddAsync(entity);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<bool> UpdateAsync(UpdateCourseDto dto)
        {
            var existingEntity = await _courseDal.GetByIdAsync(dto.Id);
            if (existingEntity == null)
                return false;

            _mapper.Map(dto, existingEntity); // DTO’daki değerleri var olana uygula
            _courseDal.Update(existingEntity);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _courseDal.GetByIdAsync(id);
            if (entity == null)
                return false;

            _courseDal.Delete(entity);
            return await _unitOfWork.CommitAsync() > 0;
        }
    }
}
