using AutoMapper;
using Learnify.Business.Abstract;
using Learnify.Business.Utilities.Results;
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

        public async Task<IResult> AddAsync(CreateCategoryDto dto)
        {
            var entity = _mapper.Map<Category>(dto);
            await _unitOfWork.Categories.AddAsync(entity);
            await _unitOfWork.SaveChangesAsync();
            return new SuccessResult("Kategori başarıyla eklendi.");
        }

        public async Task<IResult> DeleteAsync(int id)
        {
            var category = await _unitOfWork.Categories.GetByIdAsync(id);
            if (category == null)
                return new ErrorResult("Kategori bulunamadı.");

            _unitOfWork.Categories.Delete(category);
            await _unitOfWork.SaveChangesAsync();
            return new SuccessResult("Kategori silindi.");
        }

        public async Task<IDataResult<List<ResultCategoryDto>>> GetAllAsync()
        {
            var values = await _unitOfWork.Categories.GetAllAsync();
            var mapped = _mapper.Map<List<ResultCategoryDto>>(values);
            return new SuccessDataResult<List<ResultCategoryDto>>(mapped);
        }

        public async Task<IDataResult<ResultCategoryDto>> GetByIdAsync(int id)
        {
            var category = await _unitOfWork.Categories.GetByIdAsync(id);
            if (category == null)
                return new ErrorDataResult<ResultCategoryDto>("Kategori bulunamadı.");

            var mapped = _mapper.Map<ResultCategoryDto>(category);
            return new SuccessDataResult<ResultCategoryDto>(mapped);
        }

        public async Task<IResult> UpdateAsync(UpdateCategoryDto dto)
        {
            var category = await _unitOfWork.Categories.GetByIdAsync(dto.Id);
            if (category == null)
                return new ErrorResult("Kategori bulunamadı.");

            _mapper.Map(dto, category);
            _unitOfWork.Categories.Update(category);
            await _unitOfWork.SaveChangesAsync();

            return new SuccessResult("Kategori güncellendi.");
        }

        public async Task<IDataResult<List<ResultCategoryDto>>> GetActiveCategoriesAsync()
        {
            var values = await _unitOfWork.Categories.GetActiveCategoriesAsync();
            return new SuccessDataResult<List<ResultCategoryDto>>(
                _mapper.Map<List<ResultCategoryDto>>(values)
            );
        }

        public async Task<IDataResult<ResultCategoryDto>> GetCategoryWithCoursesAsync(int categoryId)
        {
            var category = await _unitOfWork.Categories.GetCategoryWithCoursesAsync(categoryId);
            if (category == null)
                return new ErrorDataResult<ResultCategoryDto>("Kategori bulunamadı.");

            return new SuccessDataResult<ResultCategoryDto>(_mapper.Map<ResultCategoryDto>(category));
        }

        public async Task<IDataResult<UpdateCategoryDto>> GetForUpdateAsync(int id)
        {
            var category = await _unitOfWork.Categories.GetByIdAsync(id);
            if (category == null)
                return new ErrorDataResult<UpdateCategoryDto>("Kategori bulunamadı.");

            return new SuccessDataResult<UpdateCategoryDto>(_mapper.Map<UpdateCategoryDto>(category));
        }

    }
}
