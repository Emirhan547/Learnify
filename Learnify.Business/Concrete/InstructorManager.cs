using AutoMapper;
using Learnify.Business.Abstract;
using Learnify.DataAccess.Abstract;
using Learnify.DTO.DTOs.InstructorDto;
using Learnify.Entity.Concrete;
using System.Collections.Generic;
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
            // ✅ AppUser olarak map ediliyor
            var entity = _mapper.Map<AppUser>(dto);
            await _instructorDal.AddAsync(entity);
        }

        public async Task DeleteAsync(int id)
        {
            await _instructorDal.DeleteAsync(id);
        }

        public async Task<List<ResultInstructorDto>> GetAllAsync()
        {
            // ✅ Include kullanılmıyor çünkü AppUser direkt kullanılıyor
            var values = await _instructorDal.GetAllAsync();
            return _mapper.Map<List<ResultInstructorDto>>(values);
        }

        public async Task<ResultInstructorDto> GetByIdAsync(int id)
        {
            // ✅ Include kullanılmıyor
            var value = await _instructorDal.GetByIdAsync(id);
            return _mapper.Map<ResultInstructorDto>(value);
        }

        public async Task UpdateAsync(UpdateInstructorDto dto)
        {
            var entity = _mapper.Map<AppUser>(dto);
            await _instructorDal.UpdateAsync(entity);
        }
    }
}