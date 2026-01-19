using System.Linq.Expressions;
using SMSTelegram.Domain.Entities;

namespace SMSTelegram.Application.Repositories;

public interface ICountryCodeRepository
{
    Task<List<Country>> GetAll(CancellationToken cancellationToken);
    Task<Country?> GetCountry(Expression<Func<Country, bool>>? filter = null,CancellationToken cancellationToken = default);
    Task<int> CreateCountry(Country request,CancellationToken cancellationToken = default);
    Task<int> UpdateCountry(Country request,CancellationToken cancellationToken = default);
}