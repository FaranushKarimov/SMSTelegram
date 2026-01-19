namespace SMSTelegram.Application.Models.CountryCode;

public record AddCountryCodeDto
{
    public string Name { get; set; } = string.Empty;
    public string Code { get; set; }   
}