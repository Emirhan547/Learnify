using AutoMapper;
using Learnify.Business.Abstract;
using Learnify.DataAccess.Abstract;
using Learnify.DTO.DTOs.EnrollmentDto;
using Learnify.Entity.Concrete;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Learnify.Business.Concrete
{
    public class EnrollmentManager : IEnrollmentService
    {
        private readonly IEnrollmentDal _enrollmentDal;
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public EnrollmentManager(IEnrollmentDal enrollmentDal, IUnitOfWork uow, IMapper mapper)
        {
            _enrollmentDal = enrollmentDal;
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<List<ResultEnrollmentDto>> GetAllAsync()
        {
            // İlişkili Course ve Student verilerini de dahil et
            var entities = await _enrollmentDal.GetAllAsync(includeProperties: "Course,Student");
            return _mapper.Map<List<ResultEnrollmentDto>>(entities);
        }

        public async Task<ResultEnrollmentDto?> GetByIdAsync(int id)
        {
            var entity = await _enrollmentDal.GetByIdAsync(id);
            return entity == null ? null : _mapper.Map<ResultEnrollmentDto>(entity);
        }

        public async Task<bool> AddAsync(CreateEnrollmentDto dto)
        {
            var entity = _mapper.Map<Enrollment>(dto);
            await _enrollmentDal.AddAsync(entity);
            return await _uow.CommitAsync() > 0;
        }

        public async Task<bool> UpdateAsync(UpdateEnrollmentDto dto)
        {
            var entity = await _enrollmentDal.GetByIdAsync(dto.Id);
            if (entity == null) return false;

            _mapper.Map(dto, entity);
            _enrollmentDal.Update(entity);
            return await _uow.CommitAsync() > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _enrollmentDal.GetByIdAsync(id);
            if (entity == null) return false;

            _enrollmentDal.Delete(entity);
            return await _uow.CommitAsync() > 0;
        }
    }
}
