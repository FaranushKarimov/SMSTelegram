using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace SMSTelegram.Domain.Entities;
[Table("users")]
public sealed class UserEntity
{
    [Key] 
    [JsonProperty("userId")]
    [Column("id")]
    public int UserId {  get; set; }
    [JsonProperty("userTelegramId")]
    [Column("user_telegram_id")]
    public string? UserTelegramId { get; set; }
    [JsonProperty("userName")]
    [Column("full_name")]
    public string UserName { get; set; } // Значение по умолчанию
    
    [JsonProperty("userPhone")]
    [Column("phone")]
    public string UserPhone { get; set; }
    
    [JsonProperty("userInn")]
    [Column("inn")]
    public string? UserInn { get; set; } // Значение по умолчанию


    // New properties for authentication code
    [JsonProperty("authCode")]
    [Column("auth_code")]
    public string? AuthCode { get; set; }
    
    [JsonProperty("authCodeExpiry")]
    [Column("auth_code_expiry")]
    public DateTime? AuthCodeExpiry { get; set; }
}