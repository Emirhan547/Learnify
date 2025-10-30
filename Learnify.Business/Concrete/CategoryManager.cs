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
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CategoryManager(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task AddAsync(CreateCategoryDto dto)
        {
            var entity = _mapper.Map<Category>(dto);
            await _unitOfWork.Categories.AddAsync(entity);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var category = await _unitOfWork.Categories.GetByIdAsync(id);
            if (category == null) return;
            _unitOfWork.Categories.Delete(category);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<List<ResultCategoryDto>> GetAllAsync()
        {
            var values = await _unitOfWork.Categories.GetAllAsync();
            return _mapper.Map<List<ResultCategoryDto>>(values);
        }

        public async Task<ResultCategoryDto?> GetByIdAsync(int id)
        {
            var category = await _unitOfWork.Categories.GetByIdAsync(id);
            return _mapper.Map<ResultCategoryDto?>(category);
        }

        public async Task UpdateAsync(UpdateCategoryDto dto)
        {
            var category = await _unitOfWork.Categories.GetByIdAsync(dto.Id);
            if (category == null) return;

            _mapper.Map(dto, category);
            _unitOfWork.Categories.Update(category);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<List<ResultCategoryDto>> GetActiveCategoriesAsync()
        {
            var values = await _unitOfWork.Categories.GetActiveCategoriesAsync();
            return _mapper.Map<List<ResultCategoryDto>>(values);
        }

        public async Task<ResultCategoryDto?> GetCategoryWithCoursesAsync(int categoryId)
        {
            var category = await _unitOfWork.Categories.GetCategoryWithCoursesAsync(categoryId);
            return _mapper.Map<ResultCategoryDto?>(category);
        }

        public async Task<UpdateCategoryDto?> GetForUpdateAsync(int id)
        {
            var category = await _unitOfWork.Categories.GetByIdAsync(id);
            return _mapper.Map<UpdateCategoryDto?>(category);
        }

    }
}
