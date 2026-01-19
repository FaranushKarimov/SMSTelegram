using System.Net;
using SMSTelegram.Application.Abstractions;
using SMSTelegram.Application.Models.CountryCode;
using SMSTelegram.Application.Repositories;
using SMSTelegram.Application.Responses;
using SMSTelegram.Domain.Entities;

namespace SMSTelegram.Application.Services;

public class CountryCodeService(ICountryCodeRepository repository) : ICountryCodeService
{
    public async Task<ApiResponse<List<GetCountryDto>>> GetAllAsync(CancellationToken cancellationToken)
    {
        var codeCountry = await repository.GetAll(cancellationToken);

        var result = codeCountry.Select(c => new GetCountryDto
        {
            Id = c.Id,
            Name = c.Name,
            Code = c.Code,
            IsActive = c.IsActive
        }).ToList();

        return new ApiResponse<List<GetCountryDto>>(result);
    }

    public async Task<ApiResponse<GetCountryDto?>> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        var codeCountry = await repository.GetCountry(x => x.Id == id,cancellationToken);

        if (codeCountry == null)
        {
            return new ApiResponse<GetCountryDto?>(HttpStatusCode.NotFound, "Country Not Found");
        }

        var result = new GetCountryDto()
        {
            Id = codeCountry.Id,
            Name = codeCountry.Name,
            Code = codeCountry.Code,
            IsActive = codeCountry.IsActive
        };

        return new ApiResponse<GetCountryDto?>(result);
    }

    public async Task<ApiResponse<string>> AddAsync(AddCountryCodeDto dto, CancellationToken cancellationToken)
    {
        //var code = await repository.GetCountry(x => x.Code == dto.Code,cancellationToken);
        //if (code != null)
        //{
        //    return new ApiResponse<string>(HttpStatusCode.BadRequest, "Этот код страны уже есть в базе");
        //}

        var country = new Country
        {
            Code = dto.Code,
            Name = dto.Name,
            IsActive = true
        };

        var result = await repository.CreateCountry(country,cancellationToken);

        return result == 1
            ? new ApiResponse<string>(HttpStatusCode.OK, "Successfully created")
            : new ApiResponse<string>(HttpStatusCode.BadRequest, "Failed");
        ;
    }

    public async Task<ApiResponse<string>> UpdateAsync(int id, UpdateCountryCodeDto dto, CancellationToken cancellationToken)
    {
        var codeCountry = await repository.GetCountry(x => x.Id == id,cancellationToken);

        if (codeCountry == null)
        {
            return new ApiResponse<string>(HttpStatusCode.NotFound, "Country Not Found");
        }
        var existing = await repository.GetCountry(x => x.Code == dto.Code && x.Id != id, cancellationToken);
        
         if (existing != null) return new ApiResponse<string>(HttpStatusCode.BadRequest, "Этот код страны уже используется");
         
        codeCountry.Name = dto.Name;
        codeCountry.Code = dto.Code;
        codeCountry.IsActive = dto.IsActive;

        var result = await repository.UpdateCountry(codeCountry,cancellationToken);

        return result == 1
            ? new ApiResponse<string>(HttpStatusCode.OK, "Successfully updated")
            : new ApiResponse<string>(HttpStatusCode.BadRequest, "Failed");
        ;
    }

    public async Task<ApiResponse<string>> DisableAsync(int id, CancellationToken cancellationToken)
    {
        var codeCountry = await repository.GetCountry(x => x.Id == id,cancellationToken);

        if (codeCountry == null)
        {
            return new ApiResponse<string>(HttpStatusCode.NotFound, "Country Not Found");
        }

        codeCountry.IsActive = false;
        var result = await repository.UpdateCountry(codeCountry,cancellationToken);

        return result == 1
            ? new ApiResponse<string>(HttpStatusCode.OK, "Successfully disabled")
            : new ApiResponse<string>(HttpStatusCode.BadRequest, "Failed");
        ;
    }
}