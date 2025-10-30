using AutoMapper;
using Learnify.Business.Abstract;
using Learnify.DTO.DTOs.InstructorDto;
using Learnify.Entity.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Learnify.Business.Concrete
{
    public class InstructorManager : IInstructorService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole<int>> _roleManager;
        private readonly IMapper _mapper;

        public InstructorManager(UserManager<AppUser> userManager, RoleManager<IdentityRole<int>> roleManager, IMapper mapper)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _mapper = mapper;
        }

        // 🔹 Tüm eğitmenleri getir
        public async Task<List<ResultInstructorDto>> GetAllAsync()
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

            return _mapper.Map<List<ResultInstructorDto>>(instructors);
        }

        // 🔹 ID’ye göre getir
        public async Task<ResultInstructorDto?> GetByIdAsync(int id)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == id && u.IsActive);
            if (user == null || !await _userManager.IsInRoleAsync(user, "Instructor"))
                return null;

            return _mapper.Map<ResultInstructorDto>(user);
        }

        // 🔹 Aktif eğitmenler
        public async Task<List<ResultInstructorDto>> GetActiveInstructorsAsync()
        {
            var users = await _userManager.Users.Where(u => u.IsActive).ToListAsync();
            var activeInstructors = new List<AppUser>();

            foreach (var user in users)
            {
                if (await _userManager.IsInRoleAsync(user, "Instructor"))
                    activeInstructors.Add(user);
            }

            return _mapper.Map<List<ResultInstructorDto>>(activeInstructors);
        }

        // ➕ Eğitmen ekle
        public async Task<IdentityResult> AddAsync(CreateInstructorDto dto)
        {
            var user = new AppUser
            {
                FullName = dto.FullName,
                Email = dto.Email,
                UserName = dto.Email,
                Profession = dto.Profession,
                IsActive = true
            };

            var result = await _userManager.CreateAsync(user, "Default123*"); // geçici şifre
            if (result.Succeeded)
                await _userManager.AddToRoleAsync(user, "Instructor");

            return result;
        }

        // ✏️ Güncelle
        public async Task<IdentityResult> UpdateAsync(UpdateInstructorDto dto)
        {
            var user = await _userManager.FindByIdAsync(dto.Id.ToString());
            if (user == null)
                return IdentityResult.Failed(new IdentityError { Description = "Kullanıcı bulunamadı." });

            user.FullName = dto.FullName;
            user.Email = dto.Email;
            user.UserName = dto.Email;
            user.Profession = dto.Profession;
            user.IsActive = dto.IsActive;

            return await _userManager.UpdateAsync(user);
        }

        // ❌ Sil
        public async Task DeleteAsync(int id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user != null)
            {
                user.IsActive = false;
                await _userManager.UpdateAsync(user);
            }
        }
    }
}
