using AutoMapper;
using Learnify.Business.Abstract;
using Learnify.DataAccess.Abstract;
using Learnify.DTO.DTOs.EnrollmentDto;
using Learnify.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learnify.Business.Concrete
{
    public class EnrollmentManager:IEnrollmentService
    {
        private readonly IEnrollmentDal _enrollmentDal;
        private readonly IMapper _mapper;

        public EnrollmentManager(IEnrollmentDal enrollmentDal, IMapper mapper)
        {
            _enrollmentDal = enrollmentDal;
            _mapper = mapper;
        }

        public async Task<List<ResultEnrollmentDto>> GetAllAsync()
        {
            var values = await _enrollmentDal.GetAllWithIncludeAsync(x => x.Student, x => x.Course);
            return _mapper.Map<List<ResultEnrollmentDto>>(values);
        }

        public async Task<ResultEnrollmentDto> GetByIdAsync(int id)
        {
            var value = await _enrollmentDal.GetByIdWithIncludeAsync(id, x => x.Student, x => x.Course);
            return _mapper.Map<ResultEnrollmentDto>(value);
        }

        public async Task AddAsync(CreateEnrollmentDto dto)
        {
            var entity = _mapper.Map<Enrollment>(dto);
            await _enrollmentDal.AddAsync(entity);
        }

        public async Task UpdateAsync(UpdateEnrollmentDto dto)
        {
            var entity = _mapper.Map<Enrollment>(dto);
            await _enrollmentDal.UpdateAsync(entity);
        }

        public async Task DeleteAsync(int id)
        {
            await _enrollmentDal.DeleteAsync(id);
        }
    }
}
