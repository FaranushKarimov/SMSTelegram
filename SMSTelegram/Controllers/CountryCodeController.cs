using Microsoft.AspNetCore.Mvc;
using SMSTelegram.Application.Abstractions;
using SMSTelegram.Application.Models.CountryCode;
using SMSTelegram.Application.Responses;

namespace SMSTelegram.Controllers;
[ApiController]
[Route("api/[controller]")]
public class CountryCodeController(ICountryCodeService service) : ControllerBase
{
    [HttpGet]
    public async Task<ApiResponse<List<GetCountryDto>>> GetAll(CancellationToken token) =>
        await service.GetAllAsync(token);
    [HttpGet("{id}")]
    public async Task<ApiResponse<GetCountryDto>> GetById(int id,CancellationToken token) => await service.GetByIdAsync(id,token);

    [HttpPost]
    public async Task<ApiResponse<string>> Create([FromBody] AddCountryCodeDto request,CancellationToken token) =>
        await service.AddAsync(request,token);

    [HttpPut("{id}")]
    public async Task<ApiResponse<string>> Update([FromRoute] int id, [FromBody] UpdateCountryCodeDto request,CancellationToken token) =>
        await service.UpdateAsync(id, request,token);

    [HttpDelete("{id}")]
    public async Task<ApiResponse<string>> Delete([FromRoute] int id,CancellationToken token) => await service.DisableAsync(id,token); 
}