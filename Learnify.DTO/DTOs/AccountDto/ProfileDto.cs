using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learnify.DTO.DTOs.AccountDto
{
    public class ProfileDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Kullanıcı adı zorunludur")]
        public string UserName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Ad Soyad zorunludur")]
        public string FullName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email adresi zorunludur")]
        [EmailAddress(ErrorMessage = "Geçerli bir email adresi giriniz")]
        public string Email { get; set; } = string.Empty;

        public string? PhoneNumber { get; set; }
        public string? Profession { get; set; }
        public string? ProfileImage { get; set; }
    }
}
