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

        public InstructorManager(
            UserManager<AppUser> userManager,
            RoleManager<IdentityRole<int>> roleManager,
            IMapper mapper)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _mapper = mapper;
        }

        // 🔹 Tümü async, rol & aktif filtreli
        public async Task<List<ResultInstructorDto>> GetAllAsync()
        {
            // Kullanıcıları gerçekten veritabanından çek (ToListAsync!)
            var users = await _userManager.Users
                                          .Where(u => u.IsActive) // sadece aktifler
                                          .ToListAsync();

            var instructors = new List<AppUser>();
            foreach (var user in users)
            {
                if (await _userManager.IsInRoleAsync(user, "Instructor"))
                    instructors.Add(user);
            }

            return _mapper.Map<List<ResultInstructorDto>>(instructors);
        }

        public async Task<ResultInstructorDto?> GetByIdAsync(int id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null || !user.IsActive) return null;

            var isInstructor = await _userManager.IsInRoleAsync(user, "Instructor");
            if (!isInstructor) return null;

            return _mapper.Map<ResultInstructorDto>(user);
        }

        // 🔹 IdentityResult döndür: UI hatayı gösterebilsin
        public async Task AddAsync(CreateInstructorDto dto)
        {
            var user = _mapper.Map<AppUser>(dto);
            user.EmailConfirmed = true;
            user.IsActive = true;

            // Rol yoksa oluştur
            if (!await _roleManager.RoleExistsAsync("Instructor"))
                await _roleManager.CreateAsync(new IdentityRole<int>("Instructor"));

            var result = await _userManager.CreateAsync(user, dto.Password);
            if (!result.Succeeded)
                throw new System.Exception(string.Join(" | ", result.Errors.Select(e => e.Description)));

            var roleResult = await _userManager.AddToRoleAsync(user, "Instructor");
            if (!roleResult.Succeeded)
                throw new System.Exception(string.Join(" | ", roleResult.Errors.Select(e => e.Description)));
        }

        public async Task UpdateAsync(UpdateInstructorDto dto)
        {
            var user = await _userManager.FindByIdAsync(dto.Id.ToString());
            if (user == null) return;

            // Şifre UpdateInstructorDto’dan kaldırıldığı için sadece temel alanlar
            user.UserName = dto.UserName;
            user.FullName = dto.FullName;
            user.Email = dto.Email;
            user.Profession = dto.Profession;
            user.IsActive = dto.IsActive; // UI’da checkbox kullanabilirsin

            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
                throw new System.Exception(string.Join(" | ", result.Errors.Select(e => e.Description)));
        }

        // 🔹 Soft delete (AppUser için)
        public async Task DeleteAsync(int id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null) return;

            user.IsActive = false;
            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
                throw new System.Exception(string.Join(" | ", result.Errors.Select(e => e.Description)));
        }
    }
}
