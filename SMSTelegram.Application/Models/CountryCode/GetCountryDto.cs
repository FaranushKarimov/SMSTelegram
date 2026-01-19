namespace SMSTelegram.Application.Models.CountryCode;

public record GetCountryDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Code { get; set; }   
    public bool IsActive { get; set; } = true;
}