namespace SMSTelegram.Application.Models.Users;

public class UserDto
{
    public int UserId { get; set; }
    public string? UserTelegramId { get; set; }
    public string UserName { get; set; } = null!;
    public string UserPhone { get; set; } = null!;
    public string? UserInn { get; set; }
    public string? AuthCode { get; set; }
    public DateTime? AuthCodeExpiry { get; set; }
}