using System.ComponentModel.DataAnnotations;

public class RegisterDto
{

    public string UserName { get; set; } = string.Empty;


    public string FullName { get; set; } = string.Empty;


    public string Email { get; set; } = string.Empty;


    public string Password { get; set; } = string.Empty;


    public string ConfirmPassword { get; set; } = string.Empty;

    public string? ProfileImage { get; set; }
}
