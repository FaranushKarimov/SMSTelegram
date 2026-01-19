using SMSTelegram.Application.Models.CountryCode;
using SMSTelegram.Application.Responses;
using SMSTelegram.Domain.Entities;

namespace SMSTelegram.Application.Abstractions;

public interface ICountryCodeService
{
    Task<ApiResponse<List<GetCountryDto>>> GetAllAsync(CancellationToken cancellationToken);
    Task<ApiResponse<GetCountryDto?>> GetByIdAsync(int id, CancellationToken cancellationToken);
    Task<ApiResponse<string>>AddAsync(AddCountryCodeDto dto, CancellationToken cancellationToken);
    Task<ApiResponse<string>>UpdateAsync(int id,UpdateCountryCodeDto dto, CancellationToken cancellationToken);
    Task<ApiResponse<string>>DisableAsync(int id, CancellationToken cancellationToken);
}