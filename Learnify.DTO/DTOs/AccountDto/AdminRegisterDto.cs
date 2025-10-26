using System.ComponentModel.DataAnnotations;

public class AdminRegisterDto : RegisterDto
{

    public string Role { get; set; } = "Admin";

    public string? PhoneNumber { get; set; }
}
