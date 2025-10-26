using Learnify.DTO.DTOs.AccountDto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Learnify.Business.Abstract
{
    public interface IAccountService
    {
        // Kullanıcı işlemleri
        Task RegisterAsync(RegisterDto dto);
        Task LoginAsync(LoginDto dto);
        Task LogoutAsync();

        // Admin işlemleri
        Task RegisterInstructorAsync(AdminAddInstructorDto dto);
        Task RegisterAdminAsync(AdminRegisterDto dto);

        // Profil & Şifre
        Task UpdateProfileAsync(ProfileDto dto);
        Task ChangePasswordAsync(ChangePasswordDto dto, int userId);

        // Görüntüleme
        Task<ProfileDto?> GetProfileAsync(int userId);
        Task<ProfileDto?> GetProfileByEmailAsync(string email);
        Task<IList<string>> GetUserRolesAsync(int userId);
    }
}
