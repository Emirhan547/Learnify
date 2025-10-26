using AutoMapper;
using Learnify.Business.Abstract;
using Learnify.DTO.DTOs.InstructorDto;
using Learnify.Entity.Concrete;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Learnify.Business.Concrete
{
    public class InstructorManager : IInstructorService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole<int>> _roleManager;
        private readonly IMapper _mapper;

        public InstructorManager(
            UserManager<AppUser> userManager,
            RoleManager<IdentityRole<int>> roleManager,
            IMapper mapper)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _mapper = mapper;
        }

        public async Task<List<ResultInstructorDto>> GetAllAsync()
        {
            var users = await Task.FromResult(_userManager.Users.ToList());
            var instructors = users.Where(u => _userManager.IsInRoleAsync(u, "Instructor").Result).ToList();

            return _mapper.Map<List<ResultInstructorDto>>(instructors);
        }

        public async Task<ResultInstructorDto?> GetByIdAsync(int id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null) return null;

            var isInstructor = await _userManager.IsInRoleAsync(user, "Instructor");
            if (!isInstructor) return null;

            return _mapper.Map<ResultInstructorDto>(user);
        }

        public async Task<bool> AddAsync(CreateInstructorDto dto)
        {
            var user = _mapper.Map<AppUser>(dto);
            user.EmailConfirmed = true; // varsayılan olarak onaylı kabul edilebilir

            var result = await _userManager.CreateAsync(user, dto.Password);
            if (!result.Succeeded)
                return false;

            // "Instructor" rolü yoksa oluştur
            if (!await _roleManager.RoleExistsAsync("Instructor"))
                await _roleManager.CreateAsync(new IdentityRole<int>("Instructor"));

            await _userManager.AddToRoleAsync(user, "Instructor");
            return true;
        }

        public async Task<bool> UpdateAsync(UpdateInstructorDto dto)
        {
            var user = await _userManager.FindByIdAsync(dto.Id.ToString());
            if (user == null) return false;

            _mapper.Map(dto, user);
            var result = await _userManager.UpdateAsync(user);

            return result.Succeeded;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null) return false;

            var result = await _userManager.DeleteAsync(user);
            return result.Succeeded;
        }
    }
}
