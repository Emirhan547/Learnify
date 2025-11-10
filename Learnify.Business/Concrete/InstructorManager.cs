using AutoMapper;
using Learnify.Business.Abstract;
using Learnify.Business.Utilities.Results;
using Learnify.DTO.DTOs.InstructorDto;
using Learnify.Entity.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Learnify.Business.Concrete
{
    public class InstructorManager : IInstructorService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole<int>> _roleManager;
        private readonly IMapper _mapper;

        public InstructorManager(UserManager<AppUser> userManager,
                                 RoleManager<IdentityRole<int>> roleManager,
                                 IMapper mapper)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _mapper = mapper;
        }

        public async Task<IDataResult<List<ResultInstructorDto>>> GetAllAsync()
        {
            var users = await _userManager.Users
                .Where(u => u.IsActive)
                .ToListAsync();

            var instructors = new List<AppUser>();

            foreach (var user in users)
            {
                if (await _userManager.IsInRoleAsync(user, "Instructor"))
                    instructors.Add(user);
            }

            var mapped = _mapper.Map<List<ResultInstructorDto>>(instructors);
            return new SuccessDataResult<List<ResultInstructorDto>>(mapped);
        }

        public async Task<IDataResult<ResultInstructorDto>> GetByIdAsync(int id)
        {
            var user = await _userManager.Users
                .FirstOrDefaultAsync(u => u.Id == id && u.IsActive);

            if (user == null || !await _userManager.IsInRoleAsync(user, "Instructor"))
                return new ErrorDataResult<ResultInstructorDto>("Eğitmen bulunamadı.");

            var mapped = _mapper.Map<ResultInstructorDto>(user);
            return new SuccessDataResult<ResultInstructorDto>(mapped);
        }

        public async Task<IDataResult<List<ResultInstructorDto>>> GetActiveInstructorsAsync()
        {
            var users = await _userManager.Users.Where(u => u.IsActive).ToListAsync();
            var list = new List<AppUser>();

            foreach (var user in users)
            {
                if (await _userManager.IsInRoleAsync(user, "Instructor"))
                    list.Add(user);
            }

            var mapped = _mapper.Map<List<ResultInstructorDto>>(list);
            return new SuccessDataResult<List<ResultInstructorDto>>(mapped);
        }

        public async Task<IResult> AddAsync(CreateInstructorDto dto)
        {
            var user = new AppUser
            {
                FullName = dto.FullName,
                Email = dto.Email,
                UserName = dto.Email,
                Profession = dto.Profession,
                IsActive = true
            };

            var result = await _userManager.CreateAsync(user, "Default123*");

            if (!result.Succeeded)
                return new ErrorResult("Eğitmen oluşturulamadı.");

            await _userManager.AddToRoleAsync(user, "Instructor");

            return new SuccessResult("Eğitmen başarıyla eklendi.");
        }

        public async Task<IResult> UpdateAsync(UpdateInstructorDto dto)
        {
            var user = await _userManager.FindByIdAsync(dto.Id.ToString());
            if (user == null)
                return new ErrorResult("Eğitmen bulunamadı.");

            user.FullName = dto.FullName;
            user.Email = dto.Email;
            user.UserName = dto.Email;
            user.Profession = dto.Profession;
            user.IsActive = dto.IsActive;

            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
                return new ErrorResult("Eğitmen güncellenemedi.");

            return new SuccessResult("Eğitmen güncellendi.");
        }

        public async Task<IResult> DeleteAsync(int id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
                return new ErrorResult("Eğitmen bulunamadı.");

            user.IsActive = false;
            await _userManager.UpdateAsync(user);

            return new SuccessResult("Eğitmen pasife alındı.");
        }
    }
}
