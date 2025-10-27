using AutoMapper;
using Learnify.Business.Abstract;
using Learnify.DataAccess.Abstract;
using Learnify.DTO.DTOs.CategoryDto;
using Learnify.Entity.Concrete;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Learnify.Business.Concrete
{
    public class CategoryManager : ICategoryService
    {
        private readonly ICategoryDal _categoryDal;
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public CategoryManager(ICategoryDal categoryDal, IUnitOfWork uow, IMapper mapper)
        {
            _categoryDal = categoryDal;
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<List<ResultCategoryDto>> GetAllAsync()
        {
            var entities = await _categoryDal.GetAllAsync();
            return _mapper.Map<List<ResultCategoryDto>>(entities);
        }

        public async Task<ResultCategoryDto?> GetByIdAsync(int id)
        {
            var entity = await _categoryDal.GetByIdAsync(id);
            return entity == null ? null : _mapper.Map<ResultCategoryDto>(entity);
        }

        public async Task AddAsync(CreateCategoryDto dto)
        {
            var entity = _mapper.Map<Category>(dto);
            await _categoryDal.AddAsync(entity);
            await _uow.CommitAsync();
        }

        public async Task UpdateAsync(UpdateCategoryDto dto)
        {
            var entity = await _categoryDal.GetByIdAsync(dto.Id);
            if (entity == null) return;

            _mapper.Map(dto, entity);
            _categoryDal.Update(entity);
            await _uow.CommitAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _categoryDal.GetByIdAsync(id);
            if (entity == null) return;

            _categoryDal.Delete(entity);
            await _uow.CommitAsync();
        }
    }
}
