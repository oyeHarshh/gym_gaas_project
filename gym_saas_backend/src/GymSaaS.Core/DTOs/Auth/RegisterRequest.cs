namespace GymSaaS.Core.DTOs.Auth;

public class RegisterRequest
{
    public string GymName { get; set; } = string.Empty;
    public string OwnerName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}
