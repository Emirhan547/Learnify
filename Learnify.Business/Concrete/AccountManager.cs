using AutoMapper;
using Learnify.Business.Abstract;
using Learnify.DTO.DTOs.AccountDto;
using Learnify.Entity.Concrete;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Learnify.Business.Concrete
{
    public class AccountManager : IAccountService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole<int>> _roleManager;
        private readonly IMapper _mapper;

        public AccountManager(
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            RoleManager<IdentityRole<int>> roleManager,
            IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _mapper = mapper;
        }

        // 🔹 Öğrenci Kaydı
        public async Task RegisterAsync(RegisterDto dto)
        {
            var user = _mapper.Map<AppUser>(dto);
            user.EmailConfirmed = true;

            var result = await _userManager.CreateAsync(user, dto.Password);
            if (!result.Succeeded)
                throw new InvalidOperationException(string.Join(", ", result.Errors.Select(e => e.Description)));

            await EnsureRoleExistsAsync("Student");
            await _userManager.AddToRoleAsync(user, "Student");
        }

        // 🔹 Admin Panelinden Eğitmen Ekleme
        public async Task RegisterInstructorAsync(AdminAddInstructorDto dto)
        {
            var user = _mapper.Map<AppUser>(dto);
            user.EmailConfirmed = true;

            var result = await _userManager.CreateAsync(user, dto.Password);
            if (!result.Succeeded)
                throw new InvalidOperationException(string.Join(", ", result.Errors.Select(e => e.Description)));

            await EnsureRoleExistsAsync("Instructor");
            await _userManager.AddToRoleAsync(user, "Instructor");
        }

        // 🔹 Admin Oluşturma
        public async Task RegisterAdminAsync(AdminRegisterDto dto)
        {
            var user = _mapper.Map<AppUser>(dto);
            user.EmailConfirmed = true;

            var result = await _userManager.CreateAsync(user, dto.Password);
            if (!result.Succeeded)
                throw new InvalidOperationException(string.Join(", ", result.Errors.Select(e => e.Description)));

            await EnsureRoleExistsAsync("Admin");
            await _userManager.AddToRoleAsync(user, "Admin");
        }

        // 🔹 Login
        public async Task LoginAsync(LoginDto dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email)
                ?? throw new InvalidOperationException("Kullanıcı bulunamadı.");

            var result = await _signInManager.PasswordSignInAsync(user, dto.Password, dto.RememberMe, false);
            if (!result.Succeeded)
                throw new InvalidOperationException("E-posta veya şifre hatalı.");
        }

        public async Task LogoutAsync() => await _signInManager.SignOutAsync();

        // 🔹 Profil Güncelleme
        public async Task UpdateProfileAsync(ProfileDto dto)
        {
            var user = await _userManager.FindByIdAsync(dto.Id.ToString())
                ?? throw new InvalidOperationException("Kullanıcı bulunamadı.");

            _mapper.Map(dto, user);
            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
                throw new InvalidOperationException(string.Join(", ", result.Errors.Select(e => e.Description)));
        }

        // 🔹 Şifre Değiştir
        public async Task ChangePasswordAsync(ChangePasswordDto dto, int userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString())
                ?? throw new InvalidOperationException("Kullanıcı bulunamadı.");

            var result = await _userManager.ChangePasswordAsync(user, dto.CurrentPassword, dto.NewPassword);
            if (!result.Succeeded)
                throw new InvalidOperationException(string.Join(", ", result.Errors.Select(e => e.Description)));
        }

        // 🔹 Profil Bilgisi
        public async Task<ProfileDto?> GetProfileAsync(int userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            return user == null ? null : _mapper.Map<ProfileDto>(user);
        }

        public async Task<ProfileDto?> GetProfileByEmailAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            return user == null ? null : _mapper.Map<ProfileDto>(user);
        }

        // 🔹 Kullanıcının Rolleri
        public async Task<IList<string>> GetUserRolesAsync(int userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            return user == null ? new List<string>() : await _userManager.GetRolesAsync(user);
        }

        // 🔹 Rol Varlığı Kontrolü
        private async Task EnsureRoleExistsAsync(string roleName)
        {
            if (!await _roleManager.RoleExistsAsync(roleName))
                await _roleManager.CreateAsync(new IdentityRole<int>(roleName));
        }
    }
}
