using AutoMapper;
using Learnify.Business.Abstract;
using Learnify.DataAccess.Abstract;
using Learnify.DataAccess.Repositories;
using Learnify.DTO.DTOs.EnrollmentDto;
using Learnify.DTO.DTOs.InstructorDto;
using Learnify.DTO.DTOs.LessonDto;
using Learnify.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learnify.Business.Concrete
{
    public class InstructorManager : IInstructorService
    {
        private readonly IInstructorDal _instructorDal;
        private readonly IMapper _mapper;

        public InstructorManager(IInstructorDal instructorDal, IMapper mapper)
        {
            _instructorDal = instructorDal;
            _mapper = mapper;
        }

        public async Task AddAsync(CreateInstructorDto dto)
        {
            var entity = _mapper.Map<Instructor>(dto);
            await _instructorDal.AddAsync(entity);
        }

        public async Task DeleteAsync(int id)
        {
            await _instructorDal.DeleteAsync(id);
        }

        public async Task<List<ResultInstructorDto>> GetAllAsync()
        {
            var values = await _instructorDal.GetAllWithIncludeAsync(x => x.InstructorID);
            return _mapper.Map<List<ResultInstructorDto>>(values);
        }

        public async Task<ResultInstructorDto> GetByIdAsync(int id)
        {
            var value = await _instructorDal.GetByIdWithIncludeAsync(id, x => x.InstructorID);
            return _mapper.Map<ResultInstructorDto>(value);
        }

        public async Task UpdateAsync(UpdateInstructorDto dto)
        {
            var entity = _mapper.Map<Instructor>(dto);
            await _instructorDal.UpdateAsync(entity);
        }
    }
}
