using AutoMapper;
using Learnify.Business.Abstract;
using Learnify.DataAccess.Abstract;
using Learnify.DTO.DTOs.CategoryDto;
using Learnify.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learnify.Business.Concrete
{
    public class CategoryManager : ICategoryService
    {
        private readonly ICategoryDal _categoryDal;
        private readonly IMapper _mapper;

        public CategoryManager(ICategoryDal categoryDal, IMapper mapper)
        {
            _categoryDal = categoryDal;
            _mapper = mapper;
        }

        public async Task<List<ResultCategoryDto>> GetAllAsync()
        {
            var values = await _categoryDal.GetAllAsync();
            return _mapper.Map<List<ResultCategoryDto>>(values);
        }

        public async Task<ResultCategoryDto> GetByIdAsync(int id)
        {
            var value = await _categoryDal.GetByIdAsync(id);
            return _mapper.Map<ResultCategoryDto>(value);
        }

        public async Task AddAsync(CreateCategoryDto dto)
        {
            var entity = _mapper.Map<Category>(dto);
            await _categoryDal.AddAsync(entity);
        }

        public async Task UpdateAsync(UpdateCategoryDto dto)
        {
            var entity = _mapper.Map<Category>(dto);
            await _categoryDal.UpdateAsync(entity);
        }

        public async Task DeleteAsync(int id)
        {
            await _categoryDal.DeleteAsync(id);
        }
    }
}
